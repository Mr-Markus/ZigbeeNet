using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ZigBeeNet.Hardware.Digi.XBee.Internal.Protocol;
using ZigBeeNet.Logging;
using ZigBeeNet.Transport;

namespace ZigBeeNet.Hardware.Digi.XBee.Internal
{
    public class XBeeFrameHandler
    {
        private readonly ILog _logger = LogProvider.For<XBeeFrameHandler>();
        private readonly ConcurrentQueue<IXBeeCommand> _sendQueue = new ConcurrentQueue<IXBeeCommand>();

        // TODO af: find the equivalent in C# --> maybe ThreadPool-Class is the right one
        private readonly ExecutorService _executor = Executors.NewCachedThreadPool();

        private readonly IList<XBeeListener> _transactionListeners = new List<XBeeListener>();
        private readonly IList<XBeeEventListener> _eventListeners = new List<XBeeEventListener>();
        private IZigBeePort _serialPort;
        private Task _parserThread = null;
        private readonly object _commandLock = new object();
        private readonly IXBeeCommand _sentCommand = null;
        private readonly bool _closeHandler = false;

        // TODO af: find the equivalent in C#
        private readonly ScheduledExecutorService _timeoutScheduler;
        private readonly ScheduledFuture<?> _timeoutTimer = null;

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
            timeoutScheduler = Executors.newSingleThreadScheduledExecutor();

            EmptyRxBuffer();

            // TODO af: find a more elegant way to solve this --> maybe async/await
            _parserThread = new Task(() =>
            {
                _logger.Debug("XBeeFrameHandler task started.");
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

                        int[] responseData = GetPacket();
                        if (responseData == null)
                        {
                            lock (_commandLock)
                            {
                                _sentCommand = null;
                            }
                            continue;
                        }

                        StringBuilder builder = new StringBuilder();
                        foreach (var value in responseData)
                        {
                            builder.Append(value.ToString("X"));
                        }
                        _logger.Debug($"RX XBEE Data: {builder}");

                        XBeeEvent xBeeEvent = XBeeEventFactory.GetXBeeFrame(responseData);
                        if (xBeeEvent != null)
                        {
                            NotifyEventReceived(xBeeEvent);
                        }

                        XBeeResponse response = XBeeResponseFactory.GetXBeeFrame(responseData);
                        if (response != null && NotifyResponseReceived(response))
                        {
                            lock (_commandLock)
                            {
                                _sentCommand = null;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.Error(ex, "XBeeFrameHandler exception");
                    }
                }
                _logger.Debug("XBeeFrameHandler thread exited.");
            });
            _parserThread.Start();
        }

        private void EmptyRxBuffer()
        {
            throw new NotImplementedException();
        }

        public XBeeFrameHandler()
        {
            throw new NotImplementedException();
        }
    }
}
