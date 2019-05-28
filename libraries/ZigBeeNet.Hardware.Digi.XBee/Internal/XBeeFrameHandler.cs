using Serilog;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ZigBeeNet.Hardware.Digi.XBee.Enums;
using ZigBeeNet.Hardware.Digi.XBee.Internal.Protocol;
using ZigBeeNet.Transport;

namespace ZigBeeNet.Hardware.Digi.XBee.Internal
{
    public class XBeeFrameHandler : IXBeeFrameHandler
    {
        #region fields

        private readonly ConcurrentQueue<IXBeeCommand> _sendQueue = new ConcurrentQueue<IXBeeCommand>();
        private static CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
        private static readonly CancellationToken _cancellationToken = _cancellationTokenSource.Token;

        private readonly TaskFactory _taskFactory = new TaskFactory(_cancellationToken);

        private readonly IList<IXBeeListener> _transactionListeners = new List<IXBeeListener>();
        private readonly IList<IXBeeEventListener> _eventListeners = new List<IXBeeEventListener>();
        private IZigBeePort _serialPort;
        private Task _parserThread = null;
        private readonly object _commandLock = new object();
        private readonly object _frameIdLock = new object();
        private IXBeeCommand _sentCommand = null;
        private bool _closeHandler = false;
        private Timer _timeoutTimer;
        private static int _frameId = 0;

        private const int DEFAULT_TRANSACTION_TIMEOUT = 500;
        private const int DEFAULT_COMMAND_TIMEOUT = 10000;
        private int _transactionTimeout = DEFAULT_TRANSACTION_TIMEOUT;
        private int _commandTimeout = DEFAULT_COMMAND_TIMEOUT;

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

        #endregion fields

        #region methods

        /// <summary>
        /// Construct which sets input stream where the packet is read from the and
        /// handler which further processes the received packet.
        /// </summary>
        /// <param name="serialPort">The serial port.</param>
        public void Start(IZigBeePort serialPort)
        {
            Interlocked.Exchange(ref _frameId, 1);

            _serialPort = serialPort;
            _timeoutTimer = new Timer(new TimerCallback(StartTimer));
            // TODO af: find the equivalent in C# --> maybe ThreadPool-Class is the right one
            //timeoutScheduler = Executors.newSingleThreadScheduledExecutor();

            // Clear anything in the receive buffer before we start
            EmptyRxBuffer();

            // TODO af: find a more elegant way to solve this --> maybe async/await
            // This might be resolved with a TaskCompletionSource
            // See the refactored while loop in ZigBeeTransaction.cs line 84
            _parserThread = _taskFactory.StartNew(() =>
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

                        // Get a packet from the serial port
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
                        foreach (int value in responseData)
                        {
                            builder.Append(string.Format(" 0x{0:X2}", value.ToString()));
                        }
                        Log.Debug($"RX XBEE Data: {builder}");

                        // Use the Event Factory to get an event
                        IXBeeEvent xBeeEvent = XBeeEventFactory.GetXBeeFrame(responseData);
                        if (xBeeEvent != null)
                        {
                            NotifyEventReceived(xBeeEvent);
                        }

                        // Use the Response Factory to get a response
                        IXBeeResponse response = XBeeResponseFactory.GetXBeeFrame(responseData);
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
                        Log.Error(ex, "XBeeFrameHandler exception");
                    }
                }
                Log.Debug("XBeeFrameHandler thread exited.");
            }, _cancellationToken);
        }

        /// <summary>
        /// Reads all input from the receive queue until a timeout occurs.
        /// This is used on startup to clear any data from the XBee internal 
        /// buffers before we start sending packets ourselves.
        /// </summary>
        private void EmptyRxBuffer()
        {
            Log.Debug("XBeeFrameHandler clearing receive buffer.");
            while (true)
            {
                byte? val = _serialPort.Read(100);
                if (val == null)
                {
                    // Timeout
                    break;
                }
            }
            Log.Debug("XBeeFrameHandler cleared receive buffer.");
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

        /// <summary>
        /// Set the close flag to true.
        /// </summary>
        public void SetClosing()
        {
            _closeHandler = true;
        }

        /// <summary>
        /// Requests parser thread to shutdown.
        /// </summary>
        public void Close()
        {
            SetClosing();
            StopTimer();
            _cancellationTokenSource.Cancel();
            Log.Debug("XBeeFrameHandler closed.");
        }

        /// <summary>
        /// Checks if parser thread is alive.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if parser thread is alive; otherwise, <c>false</c>.
        /// </returns>
        public bool IsAlive()
        {
            return _parserThread != null && !_parserThread.IsCanceled;
        }

        private void SendNextFrame()
        {
            // Are we already processing a command?
            if (_sentCommand != null)
            {
                Log.Information($"TX XBEE Frame outstanding: {_sentCommand}");
                return;
            }

            bool isFrameDequeuedSuccsess = _sendQueue.TryDequeue(out IXBeeCommand nextFrame);
            if (isFrameDequeuedSuccsess == false)
            {
                Log.Information("XBEE TX: Nothing to send");
                // Nothing to send
                StopTimer();
                return;
            }

            Log.Debug($"TX XBEE: {nextFrame}");

            // Remember the command we're processing
            _sentCommand = nextFrame;

            _serialPort.Write(new byte[] { XBEE_FLAG });

            // Send the data
            StringBuilder builder = new StringBuilder();
            int[] frames = nextFrame.Serialize();
            foreach (int sendByte in frames)
            {
                builder.Append(string.Format(" 0x{0:X2}", sendByte));
                if (_escapeCodes.Contains((byte)sendByte))
                {
                    _serialPort.Write(new byte[] { XBEE_ESCAPE });
                    _serialPort.Write(new byte[] { (byte)(sendByte ^ XBEE_XOR) });
                }
                else
                {
                    _serialPort.Write(new byte[] { (byte)sendByte });
                }
            }
            Log.Debug($"TX XBEE Data:{builder.ToString()}");

            // Start the timeout
            Log.Information("XBEE Timer: Start");
            _timeoutTimer.Change(_commandTimeout, _commandTimeout);
        }

        /// <summary>
        /// Add a XBee command frame to the send queue.The sendQueue is a FIFO queue.
        /// This method queues a <see cref="IXBeeCommand"/>
        /// frame without waiting for a response.
        /// </summary>
        private void QueueFrame(IXBeeCommand request)
        {
            _sendQueue.Enqueue(request);

            Log.Debug($"TX XBEE queue: {_sendQueue.Count}: {request}");

            SendNextFrame();
        }

        /// <summary>
        /// Notify any transaction listeners when we receive a response.
        /// </summary>
        /// <param name="response">The <see cref="IXBeeEvent"/> data received</param>
        /// <returns>true if the response was processed</returns>
        private bool NotifyResponseReceived(IXBeeResponse response)
        {
            bool processed = false;

            Log.Debug($"RX XBEE: {response.ToString()}");
            lock (_transactionListeners)
            {
                foreach (IXBeeListener listener in _transactionListeners)
                {
                    try
                    {
                        if (listener.TransactionEvent(response))
                        {
                            processed = true;
                        }
                    }
                    catch (Exception e)
                    {
                        Log.Debug("Exception processing XBee frame: {}: ", response, e);
                    }
                }
            }

            return processed;
        }

        /// <summary>
        /// Sets the command timeout.This is the number of milliseconds to wait for a response from the stick once the
        /// command has been sent.
        /// </summary>
        /// <param name="commandTimeOut">The number of milliseconds to wait for a response from the stick once the
        /// command has been sent.</param>
        public void SetCommandTimeout(int commandTimeOut)
        {
            _commandTimeout = commandTimeOut;
        }

        /// <summary>
        /// Sets the transaction timeout. This is the number of milliseconds to wait for a response from the stick once the
        /// command has been initially queued.
        /// </summary>
        /// <param name="transactionTimeOut">The number of milliseconds to wait for a response from the stick once the
        /// command has been initially queued.</param>
        public void SetTransactionTimeout(int transactionTimeOut)
        {
            _transactionTimeout = transactionTimeOut;
        }

        private void AddTransactionListener(IXBeeListener listener)
        {
            lock (_transactionListeners)
            {
                if (_transactionListeners.Contains(listener))
                {
                    return;
                }

                _transactionListeners.Add(listener);
            }
        }

        private void RemoveTransactionListener(IXBeeListener listener)
        {
            lock (_transactionListeners)
            {
                _transactionListeners.Remove(listener);
            }
        }

        /// <summary>
        /// Notify any event listeners when we receive an event.
        /// </summary>
        /// <param name="xBeeEvent">the <see cref="IXBeeEvent"/> received.</param>
        private void NotifyEventReceived(IXBeeEvent xBeeEvent)
        {
            Log.Debug($"RX XBEE: {xBeeEvent.ToString()}");
            lock (_eventListeners)
            {
                foreach (IXBeeEventListener listener in _eventListeners)
                {
                    try
                    {
                        listener.XbeeEventReceived(xBeeEvent);
                    }
                    catch (Exception e)
                    {
                        Log.Debug($"Exception processing XBee frame: {xBeeEvent}: ", e);
                    }
                }
            }
        }

        public void AddEventListener(IXBeeEventListener listener)
        {
            lock (_eventListeners)
            {
                if (_eventListeners.Contains(listener))
                {
                    return;
                }

                _eventListeners.Add(listener);
            }
        }

        public void RemoveEventListener(IXBeeEventListener listener)
        {
            lock (_eventListeners)
            {
                _eventListeners.Remove(listener);
            }
        }

        /// <summary>
        /// Sends a XBee request to the NCP without waiting for the response.
        /// </summary>
        /// <param name="command">Request <see cref="IXBeeCommand"/> to send.</param>
        /// <returns></returns>
        public async Task<IXBeeResponse> SendRequestAsync(IXBeeCommand command)
        {
            IXBeeListener xbeeListener = new XBeeListener();

            await _taskFactory.StartNew(() =>
            {
                AddTransactionListener(xbeeListener);
                lock (_frameIdLock)
                {
                    xbeeListener.OurFrameId = Interlocked.Increment(ref _frameId);
                    command.SetFrameId(xbeeListener.OurFrameId);
                    Interlocked.CompareExchange(ref _frameId, 1, 256);
                }

                // Send the transaction
                QueueFrame(command);

                lock (xbeeListener)
                {
                    while (!xbeeListener.Complete)
                    {
                        // wait until transaction is complete
                    }
                }
                RemoveTransactionListener(xbeeListener);
            });
            return xbeeListener.CompletionResponse;
        }

        /// <summary>
        /// Sends a XBee request to the dongle and waits for the response.The response is correlated with the request
        /// and the response data is returned as a <see cref="IXBeeEvent"/>.
        /// </summary>
        /// <param name="command">Request <see cref="IXBeeCommand"/> to send</param>
        /// <returns>Response <see cref="IXBeeResponse"/> the response, or null if there was a timeout</returns>
        public IXBeeResponse SendRequest(IXBeeCommand command)
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            CancellationToken cancellationToken = cancellationTokenSource.Token;

            Task<IXBeeResponse> sendRequestTask = SendRequestAsync(command);
            bool taskResult = sendRequestTask.Wait(_transactionTimeout, cancellationToken);
            if (taskResult)
            {
                return sendRequestTask.Result;
            }
            else
            {
                Log.Debug($"XBee interrupted in sendRequest{command}");
                lock (_commandLock)
                {
                    _sentCommand = null;
                }
                cancellationTokenSource.Cancel();
                return null;
            }
        }

        private async Task<IXBeeEvent> WaitEventAsync(Type eventClass)
        {
            IXBeeEventListenerProperties xbeeEventListener = new XBeeEventListener(eventClass);

            await _taskFactory.StartNew(() =>
            {
                // Register a listener
                AddEventListener(xbeeEventListener);

                // Wait for the event
                lock (xbeeEventListener)
                {
                    while (!xbeeEventListener.Complete)
                    {
                        // wait until transaction is complete
                    }
                }

                // Remove the listener
                RemoveEventListener(xbeeEventListener);
            });
            return xbeeEventListener.ReceivedEvent;
        }

        public IXBeeEvent EventWait(Type eventClass)
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            CancellationToken cancellationToken = cancellationTokenSource.Token;

            Task<IXBeeEvent> eventWaitTask = WaitEventAsync(eventClass);
            bool taskResult = eventWaitTask.Wait(_commandTimeout, cancellationToken);
            if (taskResult)
            {
                return eventWaitTask.Result;
            }
            else
            {
                Log.Debug($"XBee interrupted in sendRequest{eventClass}");
                cancellationTokenSource.Cancel();
                return null;
            }
        }

        /// <summary>
        /// Starts the transaction timeout. This will simply cancel the transaction and send the next frame from the queue if
        /// the timer times out. We don't try and retry as this might cause other unwanted issues.
        /// </summary>
        private void StartTimer(object state)
        {
            StopTimer();
            Log.Debug("XBEE Timer: Timeout");
            lock (_commandLock)
            {
                if (_sentCommand != null)
                {
                    _sentCommand = null;
                    SendNextFrame();
                }
            }
        }

        private void StopTimer()
        {
            _timeoutTimer.Change(Timeout.Infinite, Timeout.Infinite);
            Log.Information("XBEE Timer: Stop");
        }

        #endregion methods
    }
}
