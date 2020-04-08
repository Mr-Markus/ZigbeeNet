using Serilog;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using ZigBeeNet.Hardware.Ember.Ezsp;
using ZigBeeNet.Hardware.Ember.Ezsp.Command;
using ZigBeeNet.Hardware.Ember.Transaction;
using ZigBeeNet.Transport;

namespace ZigBeeNet.Hardware.Ember.Internal.Ash
{
    /// <summary>
    /// Frame parser for the Silicon Labs Asynchronous Serial Host (ASH) protocol.
    /// 
    /// This class handles all the ASH protocol including retries, and provides
    /// services to higher layers to allow sending of EZSP frames and the correlation
    /// of their responds.Higher layers can simply send EZSP messages synchronously
    /// and the handler will return with the completed result.
    /// 
    /// UG101: UART GATEWAY PROTOCOL REFERENCE: FOR THE EMBER® EZSP NETWORK CO-PROCESSOR
    /// 
    /// Any errors in this class will be reported to the next layer by setting the handler OFFLINE(reported through the
    /// {@link EzspFrameHandler#handleLinkStateChange(boolean)} method). The application is then responsible to reconfigure
    /// and restart the connection.
    /// </summary>
    public class AshFrameHandler : IEzspProtocolHandler 
    {
        /**
         * The receive timeout settings - min/initial/max - defined in milliseconds
         */
        private const int T_RX_ACK_MIN = 400;
        private const int T_RX_ACK_INIT = 1600;
        private const int T_RX_ACK_MAX = 3200;
        private int receiveTimeout = T_RX_ACK_INIT;

        private const int T_CON_HOLDOFF = 1250;
        private int connectTimeout = T_CON_HOLDOFF;

        /**
         * Maximum number of consecutive timeouts allowed while waiting to receive an ACK
         */
        private const int ACK_TIMEOUTS = 4;
        private int _retries = 0;

        /**
         * Maximum number of DATA frames we can transmit without an ACK
         */
        private const int TX_WINDOW = 1;

        private DateTime? _sentTime = null;

        private const int ASH_CANCEL_BYTE = 0x1A;
        private const int ASH_FLAG_BYTE = 0x7E;
        private const int ASH_SUBSTITUTE_BYTE = 0x18;
        private const int ASH_XON_BYTE = 0x11;
        private const int ASH_OFF_BYTE = 0x13;
        private const int ASH_TIMEOUT = -1;

        private const int ASH_MAX_LENGTH = 220;

        private int _ackNum = 0;
        private int _frmNum = 0;

        private long _statsTxAcks = 0;
        private long _statsTxNaks = 0;
        private long _statsTxData = 0;
        private long _statsRxAcks = 0;
        private long _statsRxNaks = 0;
        private long _statsRxData = 0;
        private long _statsRxErrs = 0;

        /**
         * The queue of {@link EzspFrameRequest} frames waiting to be sent
         */
        private ConcurrentQueue<EzspFrameRequest> _sendQueue = new ConcurrentQueue<EzspFrameRequest>();

        /**
         * The queue of {@link AshFrameData} frames that we have sent. These are kept in case a resend is required.
         */
        private ConcurrentQueue<AshFrameData> _sentQueue = new ConcurrentQueue<AshFrameData>();

        private System.Timers.Timer _timer;
        private object _timerLock = new object();

        private bool _stateConnected = false;

        private List<AshListener> _transactionListeners = new List<AshListener>();

        /**
         * The packet handler.
         */
        private IEzspFrameHandler _frameHandler;

        /**
         * The port.
         */
        private IZigBeePort _port;

        /**
         * The parser parserThread.
         */
        private Task _parserTask;

        /**
         * Flag reflecting that parser has been closed and parser parserThread
         * should exit.
         */
        private CancellationTokenSource _parserCancellationToken;

        /**
         * Construct the handler and provide the {@link EzspFrameHandler}
         *
         * @param frameHandler the {@link EzspFrameHandler} packet handler
         */
        public AshFrameHandler(IEzspFrameHandler frameHandler) 
        {
            _frameHandler = frameHandler;
        }

        public void Start(IZigBeePort port)
        {
            _port = port;
            _parserCancellationToken = new CancellationTokenSource();
            _parserTask = Task.Factory.StartNew(ParserTaskLoop, _parserCancellationToken.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
        }

        private void ParserTaskLoop()
        {
            Log.Debug("AshFrameHandler parser task started");

            int exceptionCnt = 0;

            while (!_parserCancellationToken.IsCancellationRequested) 
            {
                try 
                {
                    int[] packetData = GetPacket();
                    if (packetData == null)
                        continue;

                    AshFrame packet = AshFrame.CreateFromInput(packetData);
                    AshFrame responseFrame = null;
                    if (packet == null) 
                    {
                        Log.Debug("<-- RX ASH error: BAD PACKET {Frame}", FrameToString(packetData));

                        // Send a NAK
                        responseFrame = new AshFrameNak(_ackNum);
                    } 
                    else 
                    {
                        Log.Debug("<-- RX ASH frame: {Frame}", packet.ToString());

                        // Reset the exception counter
                        exceptionCnt = 0;

                        // Extract the flags for DATA/ACK/NAK frames
                        switch (packet.GetFrameType()) 
                        {
                            case AshFrame.FrameType.DATA:
                                _statsRxData++;

                                // Always use the ackNum - even if this frame is discarded
                                AckSentQueue(packet.GetAckNum());

                                AshFrameData dataPacket = (AshFrameData) packet;

                                // Check for out of sequence frame number
                                if (packet.GetFrmNum() == _ackNum) 
                                {
                                    // Frame was in sequence - prepare the response
                                    _ackNum = (_ackNum + 1) & 0x07;
                                    responseFrame = new AshFrameAck(_ackNum);

                                    // Get the EZSP frame
                                    EzspFrameResponse response = EzspFrame.CreateHandler(dataPacket.GetDataBuffer());
                                    Log.Verbose("ASH RX EZSP: {Response}", response);
                                    if (response == null) 
                                    {
                                        Log.Debug("ASH: No frame handler created for {Packet}", packet);
                                    } 
                                    else 
                                    {
                                        NotifyTransactionComplete(response);
                                        HandleIncomingFrame(response);
                                    }
                                } 
                                else if (!dataPacket.GetReTx()) 
                                {
                                    // Send a NAK - this is out of sequence and not a retransmission
                                    Log.Debug("ASH: Frame out of sequence - expected {Expected}, received {Received}", _ackNum, packet.GetFrmNum());
                                    responseFrame = new AshFrameNak(_ackNum);
                                } 
                                else 
                                {
                                    // Send an ACK - this was out of sequence but was a retransmission
                                    responseFrame = new AshFrameAck(_ackNum);
                                }
                                break;
                            case AshFrame.FrameType.ACK:
                                _statsRxAcks++;
                                AckSentQueue(packet.GetAckNum());
                                break;
                            case AshFrame.FrameType.NAK:
                                _statsRxNaks++;
                                SendRetry();
                                break;
                            case AshFrame.FrameType.RSTACK:
                                // Stack has been reset!
                                HandleReset((AshFrameRstAck) packet);
                                break;
                            case AshFrame.FrameType.ERROR:
                                // Stack has entered FAILED state
                                HandleError((AshFrameError) packet);
                                break;
                            default:
                                break;
                        }
                    }

                    // Due to possible I/O buffering, it is important to note that the Host could receive several
                    // valid or invalid frames after triggering a reset of the NCP. The Host must discard all frames
                    // and errors until a valid RSTACK frame is received.
                    if (!_stateConnected)
                        continue;

                    // Send the next frame
                    // Note that ASH protocol requires the host always sends an ack.
                    // Piggybacking on data packets is not allowed
                    if (responseFrame != null)
                        SendFrame(responseFrame);

                    SendNextFrame();
                } 
                catch (Exception e) 
                {
                    Log.Error(e, "AshFrameHandler Exception: ", e);

                    if (exceptionCnt++ > 10) 
                    {
                        Log.Error("AshFrameHandler exception count exceeded: {Exception}");
                        _parserCancellationToken.Cancel();
                    }
                }
            }
            Log.Debug("AshFrameHandler exited.");
        }

        private int[] GetPacket()
        {
            int[] inputBuffer = new int[ASH_MAX_LENGTH];
            int inputCount = 0;
            bool inputError = false;

            while (!_parserCancellationToken.IsCancellationRequested) 
            {
                int? val = _port.Read();
                if (val == null)
                    continue;
                
                Log.Verbose("ASH RX: {Byte}", val.Value.ToString("X2"));
                switch (val.Value) 
                {
                    case ASH_CANCEL_BYTE:
                        // Cancel Byte: Terminates a frame in progress. A Cancel Byte causes all data received since the
                        // previous Flag Byte to be ignored. Note that as a special case, RST and RSTACK frames are preceded
                        // by Cancel Bytes to ignore any link startup noise.
                        inputCount = 0;
                        inputError = false;
                        break;
                    case ASH_FLAG_BYTE:
                        // Flag Byte: Marks the end of a frame.When a Flag Byte is received, the data received since the
                        // last Flag Byte or Cancel Byte is tested to see whether it is a valid frame.
                        if (!inputError && inputCount != 0) 
                        {
                            int[] resultPacket = new int[inputCount];
                            Array.Copy(inputBuffer, 0, resultPacket, 0, inputCount);
                            return resultPacket;
                        }
                        inputCount = 0;
                        inputError = false;
                        break;
                    case ASH_SUBSTITUTE_BYTE:
                        // Substitute Byte: Replaces a byte received with a low-level communication error (e.g., framing
                        // error) from the UART.When a Substitute Byte is processed, the data between the previous and the
                        // next Flag Bytes is ignored.
                        inputError = true;
                        break;
                    case ASH_XON_BYTE:
                        // XON: Resume transmissionUsed in XON/XOFF flow control. Always ignored if received by the NCP.
                        break;
                    case ASH_OFF_BYTE:
                        // XOFF: Stop transmissionUsed in XON/XOFF flow control. Always ignored if received by the NCP.
                        break;
                    case ASH_TIMEOUT:
                        break;
                    default:
                        if (inputCount >= ASH_MAX_LENGTH) 
                        {
                            inputCount = 0;
                            inputError = true;
                        }
                        inputBuffer[inputCount++] = val.Value;
                        break;
                }
            }
            return null;
        }

        private void HandleIncomingFrame(EzspFrame ezspFrame) 
        {
            if (_stateConnected && ezspFrame != null) 
            {
                try 
                {
                    _frameHandler.HandlePacket(ezspFrame);
                } 
                catch (Exception e) 
                {
                    Log.Error(e, "AshFrameHandler Exception processing EZSP frame: {Exception}");
                }
            }
        }

        private void HandleReset(AshFrameRstAck rstAck) 
        {
            // If we are already connected, we need to reconnect
            if (_stateConnected) 
            {
                Log.Debug("ASH: RESET received while connected. Disconnecting.");
                Disconnect();
                return;
            }

            // Make sure this is a software reset.
            // This avoids us reacting to a HW reset before our SW ack
            if (rstAck.GetResetType().GetErrorCode() != AshErrorCode.RESET_SOFTWARE)
                return;

            // Check the version
            if (rstAck.GetVersion() == 2)
                StartConnectTimer();
            else
                Log.Debug("ASH: Invalid version");
        }

        private void HandleError(AshFrameError packet) 
        {
            Log.Debug("ASH: ERROR received (code {Code}).", packet.GetErrorCode());
            _statsRxErrs++;
            if (_stateConnected) 
            {
                Log.Warning("ASH: ERROR received (code {Code}). Disconnecting.", packet.GetErrorCode());
                Disconnect();
            }
        }

        public void SetClosing() 
        {
            _parserCancellationToken.Cancel();
        }

        public void Close() 
        {
            Log.Debug("AshFrameHandler close.");
            SetClosing();
            StopTimer();
            _stateConnected = false;

            ClearTransactionQueue();

            _sentQueue = new ConcurrentQueue<AshFrameData>();
            _sendQueue = new ConcurrentQueue<EzspFrameRequest>();

            _frameHandler.HandleLinkStateChange(false);

            _parserTask.Wait(500);
            Log.Debug("AshFrameHandler close complete.");
        }

        public bool IsAlive() 
        {
            return _parserTask != null && _parserTask.Status == TaskStatus.Running;
        }

        // Synchronize this method so we can do the window check without interruption.
        // Otherwise this method could be called twice from different threads that could end up with
        // more than the TX_WINDOW number of frames sent.
        [MethodImpl(MethodImplOptions.Synchronized)]
        private bool SendNextFrame() 
        {
            // We're not allowed to send if we're not connected
            if (!_stateConnected)
                return false;

            // Check how many frames are outstanding
            if (_sentQueue.Count >= TX_WINDOW) {
                // check timer task
                if (_timer == null) 
                {
                    StartRetryTimer();
                }
                return false;
            }

            EzspFrameRequest nextFrame = null;
            if (!_sendQueue.TryDequeue(out nextFrame) || nextFrame == null) {
                // Nothing to send
                return false;
            }

            // Encapsulate the EZSP frame into the ASH packet
            Log.Verbose("TX ASH EZSP: {Frame}", nextFrame);
            AshFrameData ashFrame = new AshFrameData(nextFrame);

            _retries = 0;
            SendFrame(ashFrame);
            return true;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        private void SendFrame(AshFrame ashFrame) 
        {
            switch (ashFrame.GetFrameType()) 
            {
                case AshFrame.FrameType.ACK:
                    _statsTxAcks++;
                    break;
                case AshFrame.FrameType.DATA:
                    _statsTxData++;
                    // Set the frame number
                    ((AshFrameData) ashFrame).SetFrmNum(_frmNum);
                    _frmNum = (_frmNum + 1) & 0x07;

                    // DATA frames need to go into a sent queue so we can retry if needed
                    _sentQueue.Enqueue((AshFrameData) ashFrame);
                    break;
                case AshFrame.FrameType.NAK:
                    _statsTxNaks++;
                    break;
                default:
                    break;
            }

            OutputFrame(ashFrame);
        }

        private void SendRetry() 
        {
            Log.Debug("ASH: Retry Sent Queue Length {Count}", _sentQueue.Count);
            AshFrameData ashFrame = null;
            if (!_sentQueue.TryPeek(out ashFrame) || ashFrame == null) 
            {
                Log.Debug("ASH: Retry nothing to resend!");
                return;
            }

            ashFrame.SetReTx();
            OutputFrame(ashFrame);
        }

        // Synchronize this method to ensure a packet gets sent as a block
        [MethodImpl(MethodImplOptions.Synchronized)]
        private void OutputFrame(AshFrame ashFrame) 
        {
            ashFrame.SetAckNum(_ackNum);
            Log.Debug("--> TX ASH frame: {Frame}", ashFrame);

            // Send the data
            int[] data = ashFrame.GetOutputBuffer();
            byte[] bytes = new byte[data.Length];
            for(int i = 0; i < data.Length; i++) 
            {
                bytes[i] = (byte)data[i];
            }
            _port.Write(bytes);

            // Only start the timer for data and reset frames
            if (ashFrame is AshFrameData || ashFrame is AshFrameRst) 
            {
                _sentTime = DateTime.Now;
                StartRetryTimer();
            }
        }

        public void QueueFrame(EzspFrameRequest request) 
        {
            if (_parserCancellationToken.IsCancellationRequested) 
            {
                Log.Debug("ASH: Handler is closed");
                return;
            }
            _sendQueue.Enqueue(request);

            Log.Debug("ASH: TX EZSP queue size: {Count}", _sendQueue.Count);

            SendNextFrame();
        }

        /**
         * Connect to the ASH/EZSP NCP
         */
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Connect() 
        {
            Log.Debug("ASH: Connect");
            _stateConnected = false;

            _sentQueue = new ConcurrentQueue<AshFrameData>();
            _sendQueue = new ConcurrentQueue<EzspFrameRequest>(); ;

            Reconnect();
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Reconnect() 
        {
            Log.Debug("ASH: Reconnect");
            _ackNum = 0;
            _frmNum = 0;
            receiveTimeout = T_RX_ACK_INIT;

            SendFrame(new AshFrameRst());
        }

        private void Disconnect() 
        {
            Log.Debug("ASH: Disconnected!");

            Close();
        }

        /**
         * Acknowledge frames we've sent and removes the from the sent queue.
         * This method is called for each DATA or ACK frame where we have the 'ack' property.
         *
         * @param ackNum the last ack from the NCP
         */
        private void AckSentQueue(int ackNum) 
        {
            // Handle the timer if it's running
            if (_sentTime.HasValue)
            {
                StopTimer();
                receiveTimeout = (int) ((receiveTimeout * 7 / 8) + ((DateTime.Now - _sentTime.Value).TotalMilliseconds / 2));
                
                if (receiveTimeout < T_RX_ACK_MIN) 
                    receiveTimeout = T_RX_ACK_MIN;
                else if (receiveTimeout > T_RX_ACK_MAX)
                    receiveTimeout = T_RX_ACK_MAX;
                
                Log.Verbose("ASH: RX Timer took {TimeSpent}ms, timer now {ReceiveTimeout}ms", (int)(DateTime.Now - _sentTime.Value).TotalMilliseconds, receiveTimeout);
                _sentTime = null;
            }

            AshFrameData ackedFrame = null;
            while (_sentQueue.TryPeek(out ackedFrame) && ackedFrame.GetFrmNum() != ackNum) 
            {
                _sentQueue.TryDequeue(out ackedFrame);
                Log.Debug("ASH: Frame acked and removed {Frame}", ackedFrame);
            }
        }

        private void StartRetryTimer()
        {
            lock (_timerLock)
            {
                StopTimer();

                _timer = new System.Timers.Timer();
                _timer.AutoReset = false;
                _timer.Interval = receiveTimeout;
                _timer.Elapsed += new ElapsedEventHandler(OnRetryTimerElapsedEvent);
                _timer.Start();
                Log.Verbose("ASH: Started retry timer");
            }
        }

        private void StopTimer()
        {
            lock (_timerLock)
            {
                // Stop any existing timer
                if (_timer != null)
                {
                    _timer.Stop();
                    _timer = null;
                }
            }
        }

        private void OnRetryTimerElapsedEvent(object sender, ElapsedEventArgs e)
        {
            try
            {
                // Resend the first message in the sentQueue
                if (_stateConnected && _sentQueue.IsEmpty)
                    return;

                if (++_retries > ACK_TIMEOUTS) 
                {
                    Log.Debug("ASH: Error number of retries exceeded [{Retries}].", _retries);

                    // Too many retries.
                    // We should alert the upper layer so they can reset the link?
                    Disconnect();
                    return;
                }

                // If we're not connected, then try the reset again
                if (!_stateConnected) 
                {
                    StopTimer();
                    Reconnect();
                    return;
                }

                // If a DATA frame acknowledgement is not received within the current timeout value,
                // then t_rx_ack is doubled.
                receiveTimeout *= 2;
                if (receiveTimeout > T_RX_ACK_MAX) {
                    receiveTimeout = T_RX_ACK_MAX;
                }

                SendRetry();

            }
            catch (Exception ex)
            {
                Log.Debug(ex, "Caught exception while attempting to retry message in OnRetryTimerElapsedEvent");
            }
        }

        private void StartConnectTimer()
        {
            lock (_timerLock)
            {
                StopTimer();

                _timer = new System.Timers.Timer();
                _timer.AutoReset = false;
                _timer.Interval = connectTimeout;
                _timer.Elapsed += new ElapsedEventHandler(OnConnectTimerElapsedEvent);
                _timer.Start();
                Log.Verbose("ASH: Started connect timer");
            }
        }

        private void OnConnectTimerElapsedEvent(object sender, ElapsedEventArgs e)
        {
            try
            {
                Log.Debug("ASH: Connected");
                StopTimer();
                _stateConnected = true;
                _ackNum = 0;
                _frmNum = 0;
                _sentQueue = new ConcurrentQueue<AshFrameData>();
                _frameHandler.HandleLinkStateChange(true);
                SendNextFrame();
            }
            catch (Exception ex)
            {
                Log.Debug(ex, "Caught exception in OnConnectTimerElapsedEvent");
            }
        }

        interface AshListener
        {
            bool TransactionEvent(EzspFrameResponse ezspResponse);

            void TransactionComplete();
        }

        /**
         * Aborts all waiting transactions
         */
        private void ClearTransactionQueue() 
        {
            lock(_transactionListeners) 
            {
                foreach (AshListener listener in _transactionListeners) 
                {
                    listener.TransactionComplete();
                }
            }
        }

        /**
         * Notify any transaction listeners when we receive a response.
         *
         * @param response the response data received
         * @return true if the response was processed
         */
        private bool NotifyTransactionComplete(EzspFrameResponse response) 
        {
            bool processed = false;

            lock (_transactionListeners) 
            {
                foreach (AshListener listener in _transactionListeners) 
                {
                    if (listener.TransactionEvent(response))
                        processed = true;
                }
            }

            return processed;
        }

        private void AddTransactionListener(AshListener listener) 
        {
            lock (_transactionListeners) 
            {
                if (_transactionListeners.Contains(listener))
                    return;

                _transactionListeners.Add(listener);
            }
        }

        private void RemoveTransactionListener(AshListener listener) 
        {
            lock (_transactionListeners) 
            {
                _transactionListeners.Remove(listener);
            }
        }

        private class TransactionWaiter : AshListener 
        {
            private bool _complete = false;
            private IEzspTransaction _ezspTransaction;
            private AshFrameHandler _frameHandler;
            private TaskCompletionSource<bool> _tcs;

            public TransactionWaiter(IEzspTransaction ezspTransaction, AshFrameHandler frameHandler)
            {
                _ezspTransaction = ezspTransaction;
                _frameHandler = frameHandler;
                //Without RunContinuationsAsynchronously, calling SetResult can block the calling thread, because the continuation is run synchronously
                //see https://stackoverflow.com/a/37492081
                _tcs = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
            }

            public async Task<EzspFrame> Wait()
            {
                // Register a listener
                _frameHandler.AddTransactionListener(this);

                // Send the transaction
                _frameHandler.QueueFrame(_ezspTransaction.GetRequest());

                _complete = await _tcs.Task;

                // Remove the listener
                _frameHandler.RemoveTransactionListener(this);

                return _ezspTransaction.GetResponse();
            }

            public bool TransactionEvent(EzspFrameResponse ezspResponse)
            {
                if (ezspResponse.GetSequenceNumber() == _ezspTransaction.GetRequest().GetSequenceNumber()
                        && ezspResponse is EzspInvalidCommandResponse) 
                {
                    // NCP doesn't support this command!
                    TransactionComplete();
                    return true;
                }

                // Check if this response completes our transaction
                if (!_ezspTransaction.IsMatch(ezspResponse))
                {
                    return false;
                }

                TransactionComplete();

                return true;
            }

            public void TransactionComplete()
            {
                _tcs.SetResult(true);
            }
        }

        public Task<EzspFrame> SendEzspRequestAsync(IEzspTransaction ezspTransaction) 
        {
            if (_parserCancellationToken.IsCancellationRequested) 
            {
                Log.Debug("ASH: Handler is closed");
                return null;
            }

            return new TransactionWaiter(ezspTransaction, this).Wait();
        }

        public IEzspTransaction SendEzspTransaction(IEzspTransaction ezspTransaction) 
        {
            try
            {
                Log.Debug("TX EZSP: {Request}", ezspTransaction.GetRequest());

                Task transactionTask = SendEzspRequestAsync(ezspTransaction);
                if (transactionTask == null) 
                {
                    Log.Debug("ASH: Error sending EZSP transaction task is null");
                    return null;
                }
                transactionTask.Wait();
            }
            catch(Exception ex)
            {
                Log.Debug(ex, "ASH: Error while sending EZSP transaction {Request}", ezspTransaction.GetRequest());
            }


            return ezspTransaction;
        }

        private class EventWaiter :  AshListener 
        {
            private bool _complete = false;
            private EzspFrameResponse _receivedEvent = null;
            private Type _eventClass;
            private AshFrameHandler _frameHandler;
            private TaskCompletionSource<bool> _tcs;

            public EventWaiter(Type eventClass, AshFrameHandler frameHandler)
            {
                _eventClass = eventClass;
                _frameHandler = frameHandler;
                //Without RunContinuationsAsynchronously, calling SetResult can block the calling thread, because the continuation is run synchronously
                //see https://stackoverflow.com/a/37492081
                _tcs = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
            }

            public async Task<EzspFrameResponse> Wait()
            {
                // Register a listener
                _frameHandler.AddTransactionListener(this);

                // Wait for the event
                _complete = await _tcs.Task;

                // Remove the listener
                _frameHandler.RemoveTransactionListener(this);

                return _receivedEvent;
            }

            public bool TransactionEvent(EzspFrameResponse ezspResponse)
            {
                // Check if this response completes our transaction
                if (ezspResponse.GetType() != _eventClass)
                {
                    return false;
                }

                _receivedEvent = ezspResponse;
                TransactionComplete();

                return true;
            }

            public void TransactionComplete()
            {
                _tcs.SetResult(true);
            }
        }

        /**
         * Wait for the requested {@link EzspFrameResponse} to be received
         *
         * @param eventClass Request {@link EzspFrameResponse} to wait for
         * @return response {@link Future} {@link EzspFrameResponse}
         */
        public Task<EzspFrameResponse> EventWaitAsync(Type eventClass) 
        {
            return new EventWaiter(eventClass, this).Wait();
        }

        /**
         * Wait for the requested {@link EzspFrameResponse} to be received
         *
         * @param eventClass Request {@link EzspFrameResponse} to wait for
         * @param timeout the time in milliseconds to wait for the response
         * @return the {@link EzspFrameResponse} once received, or null on exception
         */
        public EzspFrameResponse EventWait(Type eventClass, int timeout) 
        {
            try
            {
                Task<EzspFrameResponse> eventWaitTask = EventWaitAsync(eventClass);

                bool taskResult = eventWaitTask.Wait(timeout);

                if (taskResult)
                {
                    return eventWaitTask.Result;
                }
                else
                {
                    Log.Debug($"ASH timed out in EventWait {eventClass}");
                    return null;
                }
            } 
            catch (Exception ex) 
            {
                Log.Debug(ex, $"ASH interrupted in EventWait {eventClass}");
                return null;
            }
        }

        public Dictionary<string, long> GetCounters() 
        {
            Dictionary<string, long> counters = new Dictionary<string, long>();

            counters.Add("ASH_TX_DAT", _statsTxData);
            counters.Add("ASH_TX_NAK", _statsTxNaks);
            counters.Add("ASH_TX_ACK", _statsTxAcks);
            counters.Add("ASH_RX_DAT", _statsRxData);
            counters.Add("ASH_RX_NAK", _statsRxNaks);
            counters.Add("ASH_RX_ACK", _statsRxAcks);
            counters.Add("ASH_RX_ERR", _statsRxErrs);

            return counters;
        }

        private string FrameToString(int[] inputBuffer) 
        {
            if (inputBuffer == null || inputBuffer.Length == 4) 
            {
                return "";
            }
            StringBuilder result = new StringBuilder();
            for (int i = 1; i < inputBuffer.Length - 2; i++) 
            {
                result.Append(inputBuffer[i].ToString("X2"));
            }
            return result.ToString();
        }
    }
}
