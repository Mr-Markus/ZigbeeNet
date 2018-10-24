using System;
using System.Collections.Concurrent;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using ZigbeeNet.CC.ZDO;
using ZigbeeNet.Logging;

namespace ZigbeeNet.CC
{
    public class CCZnp : IHardwareChannel
    {
        private readonly ILog _logger = LogProvider.For<CCZnp>();
        private readonly SemaphoreSlim semaphore;

        private UnifiedNetworkProcessorInterface unpi { get; set; }
        private CancellationTokenSource _tokenSource;
        
        private Task _readTask;
        private Task _transmitTask;
        private Task _areqTask;

        private TimeSpan ResponseTimeout { get; } = TimeSpan.FromSeconds(5);

        private BlockingCollection<AsynchronousRequest> _areqQueue;
        private BlockingCollection<SerialPacket> _transmitQueue;
        private BlockingCollection<SerialPacket> _responseQueue;

        private ConcurrentQueue<EndDeviceAnnouncedInd> _joinQueue;
        private bool _spinLock;

        public int MaxRetryCount => 3;

        public event EventHandler Started;
        public event EventHandler<Device> NewDevice;

        public CCZnp()
        {
            semaphore = new SemaphoreSlim(1, 1);
            _joinQueue = new ConcurrentQueue<EndDeviceAnnouncedInd>();

            ZpiMeta.Init();
            ZdoMeta.Init();

            unpi = new UnifiedNetworkProcessorInterface("COM3", 115200, 1); //TODO: Get by config
        }

        public void Open()
        {
            _tokenSource = new CancellationTokenSource();

            _logger.Debug("Opening interface...");
            // Port must be open!!
            unpi.Port.Open();

            // create or reset queues
            _transmitQueue = new BlockingCollection<SerialPacket>();
            _areqQueue = new BlockingCollection<AsynchronousRequest>();
            _responseQueue = new BlockingCollection<SerialPacket>();

            // TODO
            // Create tasks
            _readTask = ReadSerialPortAsync(unpi);
            _transmitTask = ProcessQueueAsync(_transmitQueue, OnSend);
            _areqTask = ProcessQueueAsync(_areqQueue, OnReceive);

            _logger.Debug("Interface opened...");

            //TODO: Maybe it is not correct at this point
            //==> Starts the hardware CC2531
            SAPI.StartRequest startRequest = new SAPI.StartRequest();
            startRequest.OnResponse += (_, e) => Started?.Invoke(this, EventArgs.Empty);
            startRequest.RequestAsync(this);
        }

        public void Close()
        {
            _logger.Debug("Closing interface...");

            _transmitQueue.CompleteAdding();
            _areqQueue.CompleteAdding();
            _responseQueue.CompleteAdding();

            _tokenSource.Cancel();

            _logger.Debug("Closed interface...");
        }

        public async Task<byte[]> PermitJoinAsync(int time)
        {
            PermitJoinRequest permitJoinRequest = new PermitJoinRequest(Convert.ToByte(time));

            byte[] data = await permitJoinRequest.ToSerialPacket().ToFrame().ConfigureAwait(false);

            return await SendAsync(data);
        }

        private async Task ReadSerialPortAsync(UnifiedNetworkProcessorInterface port)
        {
            if (port == null) throw new ArgumentNullException(nameof(port));

            while (true)
            {
                try
                {
                    if(_tokenSource.IsCancellationRequested)
                    {
                        break;
                    }

                    var serialPacket = await SerialPacket.ReadAsync(port.InputStream).ConfigureAwait(false);

                    _logger.Debug("Paket read: SubSystem: {subSystem}, Type: {type}, Length: {length}, Cmd1: {cmdId}", serialPacket.SubSystem, serialPacket.Type, serialPacket.Length, serialPacket.Cmd1);

                    // SerialPacket.ReadAsync() return the correct package class, so 
                    // we can start processing them into the correct queue here
                    if (serialPacket is SynchronousResponse srsp)
                    {
                        _responseQueue.Add(srsp);
                        continue;
                    }

                    if (serialPacket is AsynchronousRequest areq)
                    {
                        _areqQueue.Add(areq);
                    }
                }
                catch (Exception e)
                {
                    // TODO improve this by adding a good error handling
                    OnError(e);
                }
            }
        }

        public async Task<byte[]> SendAsync(byte[] payload)
        {
            if (unpi.Port.IsOpen == false)
                throw new Exception("Port is not opened");

            using (MemoryStream stream = new MemoryStream(payload))
            {
                var packet = await SerialPacket.ReadAsync(stream).ConfigureAwait(false);

                return await SendAsync(packet.SubSystem, packet.Cmd1, packet.Payload,
                    msg => { return msg is SynchronousResponse && msg.SubSystem == packet.SubSystem; }).ConfigureAwait(false);
            }
        }

        protected virtual void OnError(Exception e)
        {
            _logger.ErrorException("Exception: {e}", e);
        }

        private void OnSend(SerialPacket serialPacket)
        {
            if (serialPacket == null)
                throw new ArgumentNullException(nameof(serialPacket));

            serialPacket.WriteAsync(unpi.OutputStream).ConfigureAwait(false);
            _logger.Debug("Transmitted: SubSystem: {subSystem}, Type: {type}, Length: {length}, Cmd1: {cmdId}", serialPacket.SubSystem, serialPacket.Type, serialPacket.Length, serialPacket.Cmd1);
        }

        private void OnReceive(AsynchronousRequest asynchronousRequest)
        {
            //TODO: HandleIncommingMessage
            if (asynchronousRequest.SubSystem == SubSystem.ZDO && asynchronousRequest.Cmd1 == (byte)ZdoCommand.stateChangeInd)
            {
                ZpiObject zpiObject = new ZpiObject(asynchronousRequest.SubSystem, asynchronousRequest.Cmd1);
                zpiObject.Parse(asynchronousRequest.Type, asynchronousRequest.Length, asynchronousRequest.Payload);

                DeviceState state = (DeviceState)(byte)zpiObject.RequestArguments["state"];

                _logger.Info("State changed: {state}", state);

                if(state == DeviceState.Started_as_ZigBee_Coordinator)
                {
                    PermitJoinAsync(255);
                }
            }
            if (asynchronousRequest.SubSystem == SubSystem.ZDO && asynchronousRequest.Cmd1 == (byte)ZdoCommand.endDeviceAnnceInd)
            {
                ZpiObject zpiObject = new ZpiObject(asynchronousRequest.SubSystem, asynchronousRequest.Cmd1);
                zpiObject.Parse(asynchronousRequest.Type, asynchronousRequest.Length, asynchronousRequest.Payload);

                EndDeviceAnnouncedInd ind = zpiObject.ToSpecificObject<EndDeviceAnnouncedInd>();

                endDeviceAnnceHdlr(ind);
                _logger.Info("New Device! NwkAddr: {NwkAddr}, IeeeAddr: {ieeeAddr}", ind.NetworkAddress, ind.IeeeAddress);
            }
            //TODO: EventHandler or Bridge class should handle special requests
        }

        private Task ProcessQueueAsync<T>(BlockingCollection<T> queue, Action<T> action) where T : SerialPacket
        {
            if (queue == null) throw new ArgumentNullException(nameof(queue));
            if (action == null) throw new ArgumentNullException(nameof(action));

            return Task.Run(() =>
            {
                while (!queue.IsCompleted)
                {
                    var packet = default(SerialPacket);
                    try
                    {
                        packet = queue.Take();
                    }
                    catch (InvalidOperationException ie)
                    {
                        OnError(ie);
                    }

                    if (packet != null)
                        action((T)packet);
                }
            });
        }

        private async Task<byte[]> RetryAsync(Func<Task<byte[]>> func, string message)
        {
            if (func == null)
                throw new ArgumentNullException(nameof(func));

            await semaphore.WaitAsync(_tokenSource.Token).ConfigureAwait(false);
            try
            {
                var attempt = 0;
                while (!_tokenSource.Token.IsCancellationRequested)
                {
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

                        _logger.Error("Some error occured on: {message}. Retrying {attempt} of {MaxRetryCount}.", message, attempt, MaxRetryCount);
                        await Task.Delay(TimeSpan.FromMilliseconds(500), _tokenSource.Token).ConfigureAwait(false);
                    }
                }
            }
            finally
            {
                semaphore.Release();
            }

            throw new TaskCanceledException();
        }

        private async Task<SerialPacket> WaitForResponseAsync(SynchronousRequest request, Func<SerialPacket, bool> predicate)
        {
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));

            while (!_tokenSource.Token.IsCancellationRequested)
            {
                var result = await Task<SerialPacket>.Run(() =>
                {
                    var msg = default(SerialPacket);
                    _responseQueue.TryTake(out msg, (int) ResponseTimeout.TotalMilliseconds, _tokenSource.Token);

                    if (msg.SubSystem == request.SubSystem && msg.Cmd1 == request.Cmd1)
                        return msg;
                    else
                        throw new Exception("Response does not belong to SREQ");
                }).ConfigureAwait(false);

                // TODO Sanity checks

                if (predicate(result))
                    return result;
            }

            throw new TaskCanceledException();
        }
        
        private async Task<byte[]> SendAsync(SubSystem subSystem, byte cmdId, byte[] payload, Func<SynchronousResponse, bool> predicate)
        {
            return await RetryAsync(async () =>
            {
                var request = new SynchronousRequest(subSystem, cmdId, payload);
                _transmitQueue.Add(request);

                var response = await WaitForResponseAsync(request, msg => predicate((SynchronousResponse) msg))
                    .ConfigureAwait(false);

                var zpiObject = new ZpiObject(response.SubSystem, response.Type, response.Cmd1);
                zpiObject.OnParsed += (s, result) =>
                {
                    _logger.Info("Parsed: {Type} - {SubSystem} - {Name}", response.Type, result.SubSystem, result.Name);
                };
                zpiObject.Parse(response.Type, response.Length, response.Payload);

                return ((SynchronousResponse) response).Payload;
            }, $"{subSystem} {(payload != null ? BitConverter.ToString(payload) : string.Empty)}");
        }

        private void endDeviceAnnceHdlr(EndDeviceAnnouncedInd deviceInd)
        {
            //TODO: Try to get device from device db and check status if it is online. If true continue with next ind
            Device device = null;
            if (device != null && device.Status == DeviceStatus.Online)
            {
                Console.WriteLine("Device already in Network");

                EndDeviceAnnouncedInd removed = null;
                if (_joinQueue.TryDequeue(out removed))
                {
                    EndDeviceAnnouncedInd next = null;

                    if (_joinQueue.TryDequeue(out next))
                    {
                        endDeviceAnnceHdlr(next);
                    }
                    else
                    {
                        _spinLock = false;
                    }
                }

                return;
            }

            //TODO: Timeout

            Query query = new Query(this);

            query.GetDevice(deviceInd.NetworkAddress, deviceInd.IeeeAddress, (dev) =>
            {
                NewDevice?.Invoke(this, dev);
            });

            NewDevice?.Invoke(this, device);
        }
    }
}