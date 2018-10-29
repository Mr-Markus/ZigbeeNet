using System;
using System.Collections.Concurrent;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ZigbeeNet.CC.Packet;
using ZigbeeNet.CC.Packet.SimpleAPI;
using ZigbeeNet.CC.Packet.SYS;
using ZigbeeNet.CC.Packet.ZDO;
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
        private BlockingCollection<SynchronousResponse> _responseQueue;

        private ConcurrentBag<Device> _devices;
        private ConcurrentQueue<ZDO_END_DEVICE_ANNCE_IND> _joinQueue;
        private bool _spinLock;

        public int MaxRetryCount => 3;

        public event EventHandler Started;
        public event EventHandler<Device> NewDevice;

        public CCZnp()
        {
            semaphore = new SemaphoreSlim(1, 1);
            _joinQueue = new ConcurrentQueue<ZDO_END_DEVICE_ANNCE_IND>();
            _devices = new ConcurrentBag<Device>();

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
            _responseQueue = new BlockingCollection<SynchronousResponse>();

            // TODO
            // Create tasks
            _readTask = ReadSerialPortAsync(unpi);
            _transmitTask = ProcessQueueAsync(_transmitQueue, OnSend);
            _areqTask = ProcessQueueAsync(_areqQueue, OnReceive);

            _logger.Debug("Interface opened...");

            ZB_SYSTEM_RESET reset = new ZB_SYSTEM_RESET();
            SendAsync(reset).ConfigureAwait(false);
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
            ZDO_MGMT_PERMIT_JOIN_REQ join = new ZDO_MGMT_PERMIT_JOIN_REQ(0x02, new ZAddress16(0,0), 255, false);
            var responsePacket = await SendAsync<ZDO_MGMT_PERMIT_JOIN_REQ_SRSP>(join, msg => { return msg is SynchronousResponse && msg.SubSystem == join.SubSystem; }).ConfigureAwait(false);

            return await responsePacket.ToFrame();
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

                    var serialPacket = await PacketStream.ReadAsync(port.InputStream).ConfigureAwait(false);

                    _logger.Debug("Paket read: SubSystem: {subSystem}, Type: {type}, Length: {length}, Cmd: {cmd}", serialPacket.SubSystem, serialPacket.Type, serialPacket.Length, serialPacket.Cmd);

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

        public async Task SendAsync(byte[] payload)
        {
            if (unpi.Port.IsOpen == false)
                throw new Exception("Port is not opened");

            using (MemoryStream stream = new MemoryStream(payload))
            {
                var transmitPacket = await PacketStream.ReadAsync(stream).ConfigureAwait(false);

                if (transmitPacket is SynchronousRequest sreq)
                {
                    var responsePacket = await SendAsync<SynchronousResponse>(transmitPacket,
                        msg => { return msg is SynchronousResponse && msg.SubSystem == transmitPacket.SubSystem; }).ConfigureAwait(false);

                    await responsePacket.ToFrame();
                } else if(transmitPacket is AsynchronousRequest areq)
                {
                    await SendAsync(areq);
                }
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
            _logger.Debug("Transmitted: SubSystem: {subSystem}, Type: {type}, Length: {length}, Cmd: {cmd}", serialPacket.SubSystem, serialPacket.Type, serialPacket.Length, serialPacket.Cmd);
        }

        private async void OnReceive(AsynchronousRequest asynchronousRequest)
        {
            if(asynchronousRequest is SYS_RESET_RESPONSE res)
            {
                if (res.Reason == SYS_RESET_RESPONSE.ResetType.PowerUp)
                {
                    //TODO: Maybe it is not correct at this point
                    //==> Starts the hardware CC2531
                    ZB_START_REQUEST start = new ZB_START_REQUEST();
                    await SendAsync<ZB_START_REQUEST_RSP>(start, msg => { return msg is SynchronousResponse && msg.SubSystem == start.SubSystem; }).ContinueWith((t) =>
                    {
                        Started?.Invoke(this, EventArgs.Empty);
                    });
                }
            }
            //TODO: HandleIncommingMessage
            if (asynchronousRequest is ZDO_STATE_CHANGE_IND stateInd)
            {
                _logger.Info("State changed: {state}", stateInd.Status);

                if(stateInd.Status == DeviceState.Started_as_ZigBee_Coordinator)
                {
                    ZAddress64 ieeeAddr = await GetIeeeAddress();
                    //ZAddress16 panId = await GetCurrentPanId();

                    await PermitJoinAsync(255);
                }
            }
            if (asynchronousRequest is ZDO_END_DEVICE_ANNCE_IND endDevInd)
            {
                endDeviceAnnceHdlr(endDevInd);
                _logger.Info("New Device! NwkAddr: {NwkAddr}, IeeeAddr: {ieeeAddr}", endDevInd.NwkAddr, endDevInd.IEEEAddr);
            }
            if(asynchronousRequest is ZDO_NODE_DESC_RSP nodeDesc)
            {
                Device device = _devices.SingleOrDefault(d => d.NwkAdress.Value == nodeDesc.NwkAddr.Value);

                device.ManufacturerId = nodeDesc.ManufacturerCode;
            }
            if(asynchronousRequest is ZDO_ACTIVE_EP_RSP spRsp)
            {
                foreach (var ep in spRsp.ActiveEpList)
                {

                }
            }
            //TODO: EventHandler or Bridge class should handle special requests
        }

        private Task ProcessQueueAsync<T>(BlockingCollection<T> queue, Action<T> action) where T : SerialPacket
        {
            if (queue == null)
                throw new ArgumentNullException(nameof(queue));
            if (action == null)
                throw new ArgumentNullException(nameof(action));

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

        private async Task<TResponse> RetryWithResponseAsync<TResponse>(Func<Task<TResponse>> func, string message) where TResponse : SynchronousResponse
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

        private async Task<TResponse> WaitForResponseAsync<TResponse>(SynchronousRequest request, Func<TResponse, bool> predicate) where TResponse : SynchronousResponse
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            while (!_tokenSource.Token.IsCancellationRequested)
            {
                var result = await Task<TResponse>.Run(() =>
                {
                    var msg = default(SynchronousResponse);
                    _responseQueue.TryTake(out msg, (int) ResponseTimeout.TotalMilliseconds, _tokenSource.Token);

                    //TODO: What to do if msg is null because no response came in?

                    if (msg.SubSystem == request.SubSystem && msg.Cmd1 == request.Cmd1)
                        return msg;
                    else
                        throw new Exception("Response does not belong to SREQ");
                }).ConfigureAwait(false);

                // TODO Sanity checks

                if (predicate(result as TResponse))
                    return result as TResponse;
            }

            throw new TaskCanceledException();
        }
        
        private async Task<TResponse> SendAsync<TResponse>(SerialPacket packet, Func<TResponse, bool> predicate) where TResponse : SynchronousResponse
        {
            return await RetryWithResponseAsync(async () =>
            {
                var request = new SynchronousRequest(packet.Cmd, packet.Payload);
                _transmitQueue.Add(request);

                var response = await WaitForResponseAsync<TResponse>(request, msg => predicate((TResponse) msg))
                    .ConfigureAwait(false);

                return ((TResponse) response);
            }, $"{packet.SubSystem} {(packet.Payload != null ? BitConverter.ToString(packet.Payload) : string.Empty)}");
        }

        private async Task SendAsync(AsynchronousRequest packet)
        {
            await Task.Run(() =>
            {
                _transmitQueue.Add(packet);
            });
        }

        private async void endDeviceAnnceHdlr(ZDO_END_DEVICE_ANNCE_IND deviceInd)
        {
            //TODO: Try to get device from device db and check status if it is online. If true continue with next ind
            Device device = _devices.SingleOrDefault(d=> d.IeeeAddress.Value == deviceInd.IEEEAddr.Value);
            if (device != null && device.Status == DeviceStatus.Online)
            {
                Console.WriteLine("Device already in Network");

                ZDO_END_DEVICE_ANNCE_IND removed = null;
                if (_joinQueue.TryDequeue(out removed))
                {
                    ZDO_END_DEVICE_ANNCE_IND next = null;

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

            device = new Device();
            device.NwkAdress = deviceInd.NwkAddr;
            device.IeeeAddress = deviceInd.IEEEAddr;

            _devices.Add(device);

            NewDevice?.Invoke(this, device);

            ZDO_NODE_DESC_REQ nodeReq = new ZDO_NODE_DESC_REQ(deviceInd.NwkAddr, deviceInd.NwkAddr);
            var nodeRsp = await SendAsync<ZDO_NODE_DESC_REQ_SRSP>(nodeReq, msg => msg.SubSystem == nodeReq.SubSystem && msg.Cmd1 == nodeReq.Cmd1).ConfigureAwait(false);

            ZDO_ACTIVE_EP_REQ epReq = new ZDO_ACTIVE_EP_REQ(deviceInd.NwkAddr, deviceInd.NwkAddr);
            var epRsp = await SendAsync<ZDO_ACTIVE_EP_REQ_SRSP>(epReq, msg => msg.SubSystem == epReq.SubSystem && msg.Cmd1 == epReq.Cmd1).ConfigureAwait(false);
        }

        private async Task<byte[]> GetDeviceInfo(DEV_INFO_TYPE info)
        {
            ZB_GET_DEVICE_INFO infoReq = new ZB_GET_DEVICE_INFO(info);
            ZB_GET_DEVICE_INFO_RSP infoRsp = await SendAsync<ZB_GET_DEVICE_INFO_RSP>(infoReq, msg => msg.SubSystem == infoReq.SubSystem && msg.Cmd1 == infoReq.Cmd1).ConfigureAwait(false);

            return infoRsp.Value;
        }

        private async Task<ZAddress64> GetIeeeAddress()
        {
            byte[] result = await GetDeviceInfo(DEV_INFO_TYPE.IEEE_ADDR);

            ZAddress64 ieeeAddr = new ZAddress64(result);

            return ieeeAddr;
        }

        private async Task<ZAddress16> GetCurrentPanId()
        {
            byte[] result = await GetDeviceInfo(DEV_INFO_TYPE.PAN_ID);

            ZAddress16 panId = new ZAddress16(result);

            return panId;
        }
    }
}