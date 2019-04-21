using Serilog;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ZigBeeNet.Hardware.Digi.XBee.Enums;
using ZigBeeNet.Hardware.Digi.XBee.Internal.Protocol;
using ZigBeeNet.Transport;

namespace ZigBeeNet.Hardware.Digi.XBee.Internal
{
    public class XBeeFrameHandler
    {
        private readonly ConcurrentQueue<IXBeeCommand> _sendQueue = new ConcurrentQueue<IXBeeCommand>();

        // TODO af: find the equivalent in C# --> maybe ThreadPool-Class is the right one
        //private readonly ExecutorService _executor = Executors.NewCachedThreadPool();

        private readonly IList<IXBeeListener> _transactionListeners = new List<IXBeeListener>();
        private readonly IList<IXBeeEventListener> _eventListeners = new List<IXBeeEventListener>();
        private IZigBeePort _serialPort;
        private Task _parserThread = null;
        private readonly object _commandLock = new object();
        private readonly IXBeeCommand _sentCommand = null;
        private readonly bool _closeHandler = false;

        // TODO af: find the equivalent in C#
        //private readonly ScheduledExecutorService _timeoutScheduler;
        //private readonly ScheduledFuture<?> _timeoutTimer = null;

        // TODO af: find the equivalent in C# --> maybe Interlocked is the right fit here.
        // https://docs.microsoft.com/en-us/dotnet/api/system.threading.interlocked.increment?view=netframework-4.7.2
        private static int _frameId = 0;

        private const int DEFAULT_TRANSACTION_TIMEOUT = 500;
        private const int DEFAULT_COMMAND_TIMEOUT = 10000;
        private readonly int _transactionTimeout = DEFAULT_TRANSACTION_TIMEOUT;
        private readonly int _commandTimeout = DEFAULT_COMMAND_TIMEOUT;

        private const byte XBEE_FLAG = 0x7E;
        private const byte XBEE_ESCAPE = 0x7D;
        private const byte XBEE_XOR = 0x20;
        private const byte XBEE_XON = 0x11;
        private const byte XBEE_XOFF = 0x13;

        private readonly List<byte> _escapeCodes = new List<byte>()
        {
            XBEE_FLAG,
            XBEE_ESCAPE,
            XBEE_XOR,
            XBEE_XON,
            XBEE_XOFF
        };

        public void Start(IZigBeePort serialPort)
        {
            Interlocked.Exchange(ref _frameId, 1);

            _serialPort = serialPort;

            // TODO af: find the equivalent in C# --> maybe ThreadPool-Class is the right one
            //timeoutScheduler = Executors.newSingleThreadScheduledExecutor();

            EmptyRxBuffer();

            // TODO af: find a more elegant way to solve this --> maybe async/await
            _parserThread = new Task(() =>
            {
                Log.Debug("XBeeFrameHandler task started.");
                while (!_closeHandler)
                {
                    try
                    {
                        lock (_commandLock)
                        {
                            if (_sentCommand == null)
                            {
                                SendNextFrame();
                            }
                        }

                        //int[] responseData = GetPacket();
                        //if (responseData == null)
                        //{
                        //    lock (_commandLock)
                        //    {
                        //        _sentCommand = null;
                        //    }
                        //    continue;
                        //}

                        //StringBuilder builder = new StringBuilder();
                        //foreach (var value in responseData)
                        //{
                        //    builder.Append(value.ToString("X"));
                        //}
                        //Log.Debug($"RX XBEE Data: {builder}");

                        //XBeeEvent xBeeEvent = XBeeEventFactory.GetXBeeFrame(responseData);
                        //if (xBeeEvent != null)
                        //{
                        //    NotifyEventReceived(xBeeEvent);
                        //}

                        //XBeeResponse response = XBeeResponseFactory.GetXBeeFrame(responseData);
                        //if (response != null && NotifyResponseReceived(response))
                        //{
                        //    lock (_commandLock)
                        //    {
                        //        _sentCommand = null;
                        //    }
                        //}
                    }
                    catch (Exception ex)
                    {
                        Log.Error(ex, "XBeeFrameHandler exception");
                    }
                }
                Log.Debug("XBeeFrameHandler thread exited.");
            });
            _parserThread.Start();
        }

        private int[] GetPacket()
        {
            int?[] inputBuffer = new int?[180];
            int inputBufferLength = 0;
            RxStateMachine rxState = RxStateMachine.WAITING;
            int? length = 0;
            int cnt = 0;
            int? checksum = 0;
            bool escaped = false;

            Log.Information("XBEE: Get Packet");

            while (!_closeHandler)
            {
                int? val = _serialPort.Read();
                if (val == null)
                {
                    // Timeout
                    continue;
                }

                if (inputBufferLength >= inputBuffer.Length)
                {
                    // If we overrun the buffer, reset and go to WAITING mode
                    inputBufferLength = 0;
                    rxState = RxStateMachine.WAITING;
                    Log.Debug("XBEE RX buffer overrun - resetting!");
                }

                Log.Information($"RX XBEE: {{{string.Format("0x{0:X2} {1:C}", val, val)}}}");

                if (escaped)
                {
                    escaped = false;
                    val = val ^ XBEE_XOR;
                }
                else if (val == XBEE_ESCAPE)
                {
                    escaped = true;
                    continue;
                }

                switch (rxState)
                {
                    case RxStateMachine.WAITING:
                        {
                            if (val == XBEE_FLAG)
                            {
                                rxState = RxStateMachine.RECEIVE_LEN1;
                            }
                        }
                        continue;
                    case RxStateMachine.RECEIVE_LEN1:
                        {
                            inputBuffer[cnt++] = val;
                            rxState = RxStateMachine.RECEIVE_LEN2;
                            length += val << 8;
                        }
                        break;
                    case RxStateMachine.RECEIVE_LEN2:
                        {
                            inputBuffer[cnt++] = val;
                            rxState = RxStateMachine.RECEIVE_DATA;
                            length += val + 3;
                            if (length > inputBuffer.Length)
                            {
                                // Return null and let the system resync by searching for the next FLAG
                                Log.Debug($"XBEE RX length too long ({length}) - ignoring!");
                                return null;
                            }
                        }
                        break;
                    case RxStateMachine.RECEIVE_DATA:
                        {
                            checksum += val;
                            inputBuffer[cnt++] = val;
                        }
                        break;
                    default:
                        break;
                }

                if (cnt == length)
                {
                    if ((checksum & 0xff) == 255)
                    {
                        int?[] tempArray = new int?[180];
                        Array.Copy(inputBuffer, tempArray, length != null ? length.Value : 0);
                        return tempArray.Where(value => value != null).Select(value => value.Value).ToArray();
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            return null;
        }

        private void SendNextFrame()
        {
            throw new NotImplementedException();
        }

        private void EmptyRxBuffer()
        {
            throw new NotImplementedException();
        }
    }
}
