using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using BinarySerialization;
using ZigbeeNet.CC.SYS;
using ZigbeeNet.Logging;

namespace ZigbeeNet.CC
{
    public class CCZnp : IHardwareChannel
    {
        private readonly ILog _logger = LogProvider.For<CCZnp>();

        private ConcurrentQueue<ZpiObject> _requestQueue;

        private bool _reset;
        private Timer _resetTimeout;
        private ZpiSREQ _sreqRunning;
        private Timer _timeout;

        public CCZnp()
        {
            _requestQueue = new ConcurrentQueue<ZpiObject>();
            semaphore = new SemaphoreSlim(1, 1);
        }

        public bool Enabled { get; set; }
        private UnifiedNetworkProcessorInterface unpi { get; set; }

        private bool _resetting
        {
            get => _reset;
            set
            {
                var dueTime = Timeout.Infinite;
                var period = Timeout.Infinite;

                if (value)
                {
                    dueTime = 30000;
                    period = 30000;
                }

                _resetTimeout = new Timer(state =>
                {
                    if (_resetting)
                    {
                        // if AREQ:SYS:RESET does not return in 30 sec
                        // release the lock to avoid the requests from enqueuing
                        _sreqRunning = null;
                        var ignore = new ZpiObject();
                        _requestQueue.TryDequeue(out ignore);
                    }
                }, null, dueTime, period);


                _reset = value;
            }
        }

        public event EventHandler Ready;
        public event EventHandler Closed;
        public event EventHandler ResetDone;

        public event EventHandler<ZpiObject> AsyncResponse;
        
        public void Init(string port, int baudrate = 115200)
        {
            ZpiMeta.Init();
            ZdoMeta.Init();

            unpi = new UnifiedNetworkProcessorInterface(port, baudrate, 1);
            //unpi.DataReceived += Unpi_DataReceived;
            //unpi.Opened += Unpi_Opened;
            //unpi.Closed += Unpi_Closed;

            unpi.Open();
        }

        private void Unpi_Closed(object sender, EventArgs e)
        {
            Stop();
        }

        private void Unpi_Opened(object sender, EventArgs e)
        {
            Ready?.Invoke(this, EventArgs.Empty);
        }

        private void Unpi_DataReceived(object sender, SerialPacket e)
        {
            //Log.Information("{@SerialPacket}", e);
            if (_sreqRunning != null && _sreqRunning.IndObject != null
                                     && (byte) _sreqRunning.IndObject.SubSystem == (byte) e.SubSystem
                                     && _sreqRunning.IndObject.CommandId == e.Cmd1)
            {
                _timeout.Change(Timeout.Infinite, Timeout.Infinite);

                _resetting = false;

                _sreqRunning.IndObject.OnParsed += (s, result) =>
                {
                    _logger.Info("{Type} - {SubSystem} - {Name}", e.Type, result.SubSystem, result.Name);

                    ZpiObject current = _sreqRunning;

                    // schedule next transmission if something in txQueue
                    ScheduleNextSend();

                    current.Response(result);
                };
                _sreqRunning.IndObject.Parse(e.Type, e.Length, e.Payload);

                return;
            }

            if ((byte) e.Type == (byte) MessageType.AREQ)
            {
                if (e.SubSystem == SubSystem.SYS && e.Cmd1 == 0
                    || e.SubSystem == SubSystem.SAPI && e.Cmd1 == 9)
                {
                    //Reset done
                    _resetting = false;

                    ResetDone?.Invoke(this, EventArgs.Empty);

                    return;
                }

                var zpiObject = new ZpiObject(e.SubSystem, e.Type, e.Cmd1);
                zpiObject.OnParsed += (s, result) =>
                {
                    _logger.Info("{Type} - {SubSystem} - {Name}", e.Type, result.SubSystem, result.Name);

                    AsyncResponse?.Invoke(this, result);
                };
                zpiObject.Parse(e.Type, e.Length, e.Payload);
            }
            else if ((byte) e.Type == (byte) MessageType.SRSP)
            {
                if (_sreqRunning != null)
                    if ((byte) _sreqRunning.SubSystem == (byte) e.SubSystem && _sreqRunning.CommandId == e.Cmd1)
                    {
                        // Status Response
                        _timeout.Change(Timeout.Infinite, Timeout.Infinite);

                        _resetting = false;

                        _sreqRunning.OnParsed += (s, result) =>
                        {
                            var sREQ = (ZpiSREQ) result;

                            _logger.Info("{Type} - {SubSystem} - {Name} - {Status}", e.Type, sREQ.SubSystem, sREQ.Name,
                                sREQ.Status);

                            if (_sreqRunning.IndObject == null)
                            {
                                ZpiObject current = _sreqRunning;

                                // schedule next transmission if something in txQueue
                                ScheduleNextSend();

                                current.Response(result);
                            }
                        };
                        _sreqRunning.Parse(e.Type, e.Length, e.Payload);
                    }
            }
        }

        public void Start()
        {
            Enabled = true;
            unpi.Open();
        }

        public void Stop()
        {
            Enabled = false;
            _requestQueue = new ConcurrentQueue<ZpiObject>();
            unpi = null;

            Closed?.Invoke(this, EventArgs.Empty);
        }

        public byte[] SendCommand(MessageType type, SubSystem subSystem, byte commandId, byte[] payload)
        {
            return unpi.Send((int) type, (int) subSystem, commandId, payload);
        }

        public void Request(SubSystem subSystem, byte cmdId, ArgumentCollection reqestArgs)
        {
            if (unpi == null) throw new NullReferenceException("CCZnp has not been initialized yet");

            var zpiObject = new ZpiObject(subSystem, cmdId);
            zpiObject.RequestArguments = reqestArgs;

            Request(zpiObject);
        }

        public void Request(ZpiObject zpiObject)
        {
            _logger.Info("{Type} - {SubSystem} - {Name}", zpiObject.Type, zpiObject.SubSystem, zpiObject.Name);

            if (_sreqRunning != null)
            {
                _requestQueue.Enqueue(zpiObject);

                return;
            }

            //prepare for transmission

            if (zpiObject.Type == MessageType.SREQ)
            {
                var zpiSREQ = new ZpiSREQ(zpiObject);
                SendSREQ(zpiSREQ, true);
            }
            else if (zpiObject.Type == MessageType.AREQ)
            {
                SendAREQ(zpiObject);
            }
        }

        public void SendSREQ(ZpiSREQ zpiObject, bool queueDone = true)
        {
            //prepare for transmission
            if (queueDone == false)
            {
                _logger.Info("{Type} - {SubSystem} - {Name}", zpiObject.Type, zpiObject.SubSystem, zpiObject.Name);

                if (_sreqRunning != null)
                {
                    _requestQueue.Enqueue(zpiObject);

                    return;
                }
            }

            _sreqRunning = zpiObject;

            _timeout = new Timer(state =>
            {
                if (_sreqRunning != null)
                {
                    _sreqRunning = null;
                    _timeout.Change(Timeout.Infinite, Timeout.Infinite);

                    if (state is ZpiSREQ)
                    {
                        var zpi = state as ZpiSREQ;
                        throw new TimeoutException(
                            $"Request timeout: {zpi.Type.ToString()}:{zpi.SubSystem.ToString()}:{zpi.Name}");
                    }

                    throw new TimeoutException("Request timeout");
                }
            }, zpiObject, 300000, 300000); //TODO: Get timeout by config            

            unpi.Send((int) MessageType.SREQ, (int) zpiObject.SubSystem, zpiObject.CommandId, zpiObject.Frame);
        }

        public void SendAREQ(ZpiObject zpiObject)
        {
            if (_sreqRunning != null)
            {
                _requestQueue.Enqueue(zpiObject);

                return;
            }

            if (zpiObject.SubSystem == SubSystem.SYS && zpiObject.CommandId == 0
                || zpiObject.SubSystem == SubSystem.SAPI && zpiObject.CommandId == 9) //resetReq or systemReset
            {
                _resetting = true;

                // clear all pending requests, since the system is reset
                _requestQueue = new ConcurrentQueue<ZpiObject>();

                AsyncResponse += (sender, e) =>
                {
                    if (e.Type == MessageType.AREQ && e.SubSystem == SubSystem.SYS &&
                        e.CommandId == (byte) SysCommand.resetInd) _resetting = false;
                };
            }

            unpi.Send((int) MessageType.AREQ, (int) zpiObject.SubSystem, zpiObject.CommandId, zpiObject.Frame);
        }

        private void ScheduleNextSend()
        {
            _sreqRunning = null;

            var next = new ZpiObject();
            if (_requestQueue.TryDequeue(out next)) Request(next);
        }

        private void ParseIncomingData(ZpiObject request, byte[] buffer)
        {
            if (buffer == null || buffer.Length == 0) throw new NullReferenceException("Buffer is empty");

            if (buffer[0] != 0xfe) //Fix SOF
                throw new FormatException("Buffer is not a vailid frame");

            var serializer = new BinarySerializer();

            var packets = new List<SerialPacket>();

            using (var stream = new MemoryStream(buffer))
            {
                packets.AddRange(serializer.Deserialize<List<SerialPacket>>(stream));
            }

            foreach (var packet in packets)
            {
                if (packet.FrameCheckSequence.Equals(packet.Checksum) == false)
                    throw new Exception("Received FCS is not equal with new packet");

                var result = new ZpiObject(packet.SubSystem, packet.Type, packet.Cmd1);
                result.Parse(packet.Type, packet.Length, packet.Payload);

                request.Response(result);
            }
        }

        #region New Approach

        private readonly SemaphoreSlim semaphore;
        private Task readTask;
        private Task transmitTask;
        public int MaxRetryCount => 3;
        private TimeSpan ResponseTimeout { get; } = TimeSpan.FromSeconds(5);

        private BlockingCollection<SerialPacket> eventQueue;
        private BlockingCollection<SerialPacket> transmitQueue;
        private BlockingCollection<SerialPacket> responseQueue;

        public void Open()
        {
            _logger.Debug("Opening interface...");
            // TODO: port must be open!!

            // create or reset queues
            transmitQueue = new BlockingCollection<SerialPacket>();
            eventQueue = new BlockingCollection<SerialPacket>();
            responseQueue = new BlockingCollection<SerialPacket>();

            // TODO
            // Create tasks
            readTask = new Task(() => ReadSerialPort(unpi));
            transmitTask = new Task(() => ProcessQueue(transmitQueue, OnSend));

            // TODO
            // Start tasks
            readTask.Start();
            transmitTask.Start();

            _logger.Debug("Interface opened...");
        }

        private void OnSend(SerialPacket serialPacket)
        {
            if (serialPacket == null) throw new ArgumentNullException(nameof(serialPacket));

            serialPacket.WriteAsync(unpi.OutputStream).ConfigureAwait(false);
            _logger.Debug($"Transmitted: {serialPacket}");
        }

        public void Close()
        {
            _logger.Debug("Closing interface...");

            transmitQueue.CompleteAdding();
            eventQueue.CompleteAdding();
            responseQueue.CompleteAdding();

            readTask.Wait();
            transmitTask.Wait();

            _logger.Debug("Closed interface...");
        }

        private async void ReadSerialPort(UnifiedNetworkProcessorInterface port)
        {
            if (port == null) throw new ArgumentNullException(nameof(port));

            while (true)
                try
                {
                    var serialPacket = await SerialPacket.ReadAsync(port.InputStream).ConfigureAwait(false);

                    // SerialPacket.ReadAsync() return the correct package class, so 
                    // we can start processing them into the correct queue here
                    if (serialPacket is SynchronousResponse)
                    {
                        responseQueue.Add(serialPacket);
                        continue;
                    }

                    if (serialPacket is AsynchronousRequest) eventQueue.Add(serialPacket);

                    // Ok, not sure but if we reach here something is afoot?
                }
                catch (Exception e)
                {
                    // TODO improve this by adding a good error handling
                    OnError(e);
                }
        }

        protected virtual void OnError(Exception e)
        {
            _logger.Error($"Exception: {e}");
        }

        private void ProcessQueue<T>(BlockingCollection<T> queue, Action<T> action) where T : SerialPacket
        {
            if (queue == null) throw new ArgumentNullException(nameof(queue));
            if (action == null) throw new ArgumentNullException(nameof(action));

            while (!queue.IsCompleted)
            {
                var packet = default(SerialPacket);
                try
                {
                    packet = queue.Take();
                }
                catch (InvalidOperationException)
                {
                }

                if (packet != null) action((T) packet);
            }
        }

        private async Task<byte[]> RetryAsync(Func<Task<byte[]>> func, string message,
            CancellationToken cancellationToken)
        {
            if (func == null) throw new ArgumentNullException(nameof(func));

            await semaphore.WaitAsync(cancellationToken).ConfigureAwait(false);
            try
            {
                var attempt = 0;
                while (!cancellationToken.IsCancellationRequested)
                    try
                    {
                        return await func().ConfigureAwait(false);
                    }
                    // TODO
                    // Catch more specific exceptions
                    catch (Exception)
                    {
                        if (attempt++ >= MaxRetryCount)
                            throw;

                        _logger.Error($"Some error occured on: {message}. Retrying {attempt} of {MaxRetryCount}.");
                        await Task.Delay(TimeSpan.FromMilliseconds(500), cancellationToken).ConfigureAwait(false);
                    }
            }
            finally
            {
                semaphore.Release();
            }

            throw new TaskCanceledException();
        }

        private async Task<SerialPacket> WaitForResponse(Func<SerialPacket, bool> predicate,
            CancellationToken cancellationToken)
        {
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));

            while (!cancellationToken.IsCancellationRequested)
            {
                var result = await Task.Run(() =>
                {
                    var msg = default(SerialPacket);
                    responseQueue.TryTake(out msg, (int) ResponseTimeout.TotalMilliseconds, cancellationToken);
                    return msg;
                }).ConfigureAwait(false);

                // TODO Sanity checks

                if (predicate(result)) return result;
            }

            throw new TaskCanceledException();
        }
        
        private Task<byte[]> Send(SubSystem subSystem, byte[] payload, Func<SynchronousResponse, bool> predicate,
            CancellationToken cancellationToken)
        {
            return RetryAsync(async () =>
            {
                var request = new SynchronousRequest(subSystem, 0, payload);
                transmitQueue.Add(request);

                var response = await WaitForResponse(msg => predicate((SynchronousResponse) msg), cancellationToken)
                    .ConfigureAwait(false);

                return ((SynchronousResponse) response).Payload;
            }, $"{subSystem} {(payload != null ? BitConverter.ToString(payload) : string.Empty)}", cancellationToken);
        }

        public Task<byte[]> Send(SubSystem subSystem, CancellationToken cancellationToken, params byte[] payload)
        {
            return Send(subSystem, payload,
                msg => { return msg is SynchronousResponse && msg.SubSystem == subSystem; },
                cancellationToken);
        }

        #endregion
    }
}