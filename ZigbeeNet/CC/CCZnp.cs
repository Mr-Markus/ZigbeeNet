//using System;
//using System.Collections.Concurrent;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Threading;
//using System.Threading.Tasks;
//using ZigbeeNet.CC.Handler;
//using ZigbeeNet.CC.Packet;
//using ZigbeeNet.CC.Packet.AF;
//using ZigbeeNet.CC.Packet.SimpleAPI;
//using ZigbeeNet.CC.Packet.SYS;
//using ZigbeeNet.CC.Packet.ZDO;
//using ZigbeeNet.Logging;
//using ZigbeeNet.ZCL;

//namespace ZigbeeNet.CC
//{
//    public class CCZnp : IHardwareChannel
//    {
//        private readonly ILog _logger = LogProvider.For<CCZnp>();
//        private readonly SemaphoreSlim semaphore;

//        private UnifiedNetworkProcessorInterface unpi { get; set; }
//        private CancellationTokenSource _tokenSource;

//        private List<IPacketHandler> _handlers;

//        private Task _readTask;
//        private Task _transmitTask;
//        private Task _areqTask;

//        private TimeSpan ResponseTimeout { get; } = TimeSpan.FromSeconds(5);

//        private BlockingCollection<AsynchronousRequest> _areqQueue;
//        private BlockingCollection<SerialPacket> _transmitQueue;
//        private BlockingCollection<SynchronousResponse> _responseQueue;

//        public ZigBeeNetwork Network { get; set; }
//        public ZigBeeNode Coordinator { get; private set; }

//        public int MaxRetryCount => 3;
        
//        public event EventHandler Started;
//        public event EventHandler<ZigBeeNode> NewDevice;
//        public event EventHandler<ZigBeeEndpoint> NewEndpoint;
//        public event EventHandler<ZigBeeNode> DeviceInfoChanged;

//        public CCZnp(Options options)
//        {
//            semaphore = new SemaphoreSlim(1, 1);

//            _handlers = new List<IPacketHandler>();

//            unpi = new UnifiedNetworkProcessorInterface(options.Port, options.Baudrate, 1);
//        }

//        public void Start()
//        {
//            _tokenSource = new CancellationTokenSource();

//            _logger.Debug("Opening interface...");
//            // Port must be open!!
//            unpi.Port.Open();

//            _handlers.Clear();
//            _handlers.Add(new PacketHandler(this));
//            _handlers.Add(new DeviceHandler(this));

//            // create or reset queues
//            _transmitQueue = new BlockingCollection<SerialPacket>();
//            _areqQueue = new BlockingCollection<AsynchronousRequest>();
//            _responseQueue = new BlockingCollection<SynchronousResponse>();

//            // Create tasks
//            _readTask = ReadSerialPortAsync(unpi);
//            _transmitTask = ProcessQueueAsync(_transmitQueue, OnSend);
//            _areqTask = ProcessQueueAsync(_areqQueue, OnReceive);

//            _logger.Debug("Interface opened...");

//            SysReset();
//        }

//        public void Stop()
//        {
//            _logger.Debug("Closing interface...");

//            _transmitQueue.CompleteAdding();
//            _areqQueue.CompleteAdding();
//            _responseQueue.CompleteAdding();

//            _tokenSource.Cancel();

//            _logger.Debug("Closed interface...");
//        }

//        private async Task SysReset()
//        {
//            ZB_SYSTEM_RESET reset = new ZB_SYSTEM_RESET();
//            await SendAsync(reset).ConfigureAwait(false);
//        }

//        public async Task PermitJoinAsync(int time)
//        {
//            ZDO_MGMT_PERMIT_JOIN_REQ join = new ZDO_MGMT_PERMIT_JOIN_REQ(0x02, new ZigbeeAddress16(0, 0), (byte)time, false);
//            await SendAsync<ZDO_MGMT_PERMIT_JOIN_REQ_SRSP>(join).ConfigureAwait(false);

//            Network.PermitJoining = time > 0 ? true : false;
//            _logger.Info("PermitJoining enabled: {Time} seconds", time);

//            if (time > 0 && time < 255)
//            {
//                Task joinCountdown = new Task(() =>
//                    {
//                        Timer t = new Timer((network) =>
//                        {
//                            lock (Network)
//                            {
//                                Network.PermitJoining = false;
//                                _logger.Info("PermitJoining disabled");
//                            }
//                        }, Network, time * 1000, Timeout.Infinite);
//                    });
//                joinCountdown.Start();
//            }
//        }

//        private async Task ReadSerialPortAsync(UnifiedNetworkProcessorInterface port)
//        {
//            if (port == null) throw new ArgumentNullException(nameof(port));

//            while (true)
//            {
//                try
//                {
//                    if (_tokenSource.IsCancellationRequested)
//                    {
//                        break;
//                    }

//                    var serialPacket = await PacketStream.ReadAsync(port.InputStream).ConfigureAwait(false);

//                    _logger.Debug("Paket read: SubSystem: {subSystem}, Type: {type}, Length: {length}, Cmd: {cmd}", serialPacket.SubSystem, serialPacket.Type, serialPacket.Length, serialPacket.Cmd);

//                    // SerialPacket.ReadAsync() return the correct package class, so 
//                    // we can start processing them into the correct queue here
//                    if (serialPacket is SynchronousResponse srsp)
//                    {
//                        _responseQueue.Add(srsp);
//                        continue;
//                    }

//                    if (serialPacket is AsynchronousRequest areq)
//                    {
//                        _areqQueue.Add(areq);
//                    }
//                }
//                catch (Exception e)
//                {
//                    // TODO improve this by adding a good error handling
//                    OnError(e);
//                }
//            }
//        }

//        public async Task SendAsync(byte[] payload)
//        {
//            if (unpi.Port.IsOpen == false)
//                throw new Exception("Port is not opened");

//            using (MemoryStream stream = new MemoryStream(payload))
//            {
//                var transmitPacket = await PacketStream.ReadAsync(stream).ConfigureAwait(false);

//                if (transmitPacket is SynchronousRequest sreq)
//                {
//                    var responsePacket = await SendAsync<SynchronousResponse>(transmitPacket).ConfigureAwait(false);

//                    await responsePacket.ToFrame();
//                }
//                else if (transmitPacket is AsynchronousRequest areq)
//                {
//                    await SendAsync(areq);
//                }
//            }
//        }

//        protected virtual void OnError(Exception e)
//        {
//            _logger.ErrorException("Exception: {e}", e);
//        }

//        internal async void OnStarted()
//        {
//            Network = new ZigBeeNetwork()
//            {
//                IeeeAddress = await GetIeeeAddress(),
//                PanId = await GetCurrentPanId(),
//                Channel = await GetCurrentChannel(),
//                NetworkAddress = await GetShortAddress()
//            };

//            _logger.Info("Network started: {@Network}", Network);

//            Coordinator = new ZigBeeNode()
//            {
//                IeeeAddress = Network.IeeeAddress,
//                NwkAdress = Network.NetworkAddress,
//                Status = ZigBeeNodeStatus.Online,
//                DeviceEnabled = ZigbeeNodeState.Enabled,
//                JoinTime = DateTime.Now,
//                PowerSource = PowerSource.DCSource
//            };

//            byte newEpId = 0;

//            //await CreateEndpoint(Coordinator, newEpId, ZclProfile.ZIGBEE_HOME_AUTOMATION);

//            OnNewDevice(Coordinator);

//            Started?.Invoke(this, EventArgs.Empty);
//        }

//        internal void OnNewDevice(ZigBeeNode device)
//        {
//            _logger.Info("New Device! NwkAddr: {NwkAddr}, IeeeAddr: {ieeeAddr}", device.NwkAdress, device.IeeeAddress);

//            NewDevice?.Invoke(this, device);
//        }

//        internal void OnDeviceInfoChanged(ZDO_NODE_DESC_RSP nodeDesc)
//        {
//            //TODO: What to do here???
//            DeviceInfoChanged?.Invoke(nodeDesc.NwkAddr, null);
//        }

//        internal void OnNewEndpoint(ZigbeeAddress16 nwkAddress, ZigBeeEndpoint endpoint)
//        {
//            NewEndpoint?.Invoke(nwkAddress, endpoint);
//        }

//        private void OnSend(SerialPacket serialPacket)
//        {
//            if (serialPacket == null)
//                throw new ArgumentNullException(nameof(serialPacket));

//            serialPacket.WriteAsync(unpi.OutputStream).ConfigureAwait(false);
//            _logger.Debug("Transmitted: SubSystem: {subSystem}, Type: {type}, Length: {length}, Cmd: {cmd}", serialPacket.SubSystem, serialPacket.Type, serialPacket.Length, serialPacket.Cmd);
//        }

//        private async void OnReceive(AsynchronousRequest asynchronousRequest)
//        {
//            foreach (var handler in _handlers)
//            {
//                await handler.Handle(asynchronousRequest);
//            }
//        }

//        private Task ProcessQueueAsync<T>(BlockingCollection<T> queue, Action<T> action) where T : SerialPacket
//        {
//            if (queue == null)
//                throw new ArgumentNullException(nameof(queue));
//            if (action == null)
//                throw new ArgumentNullException(nameof(action));

//            return Task.Run(() =>
//            {
//                while (!queue.IsCompleted)
//                {
//                    var packet = default(SerialPacket);
//                    try
//                    {
//                        packet = queue.Take();
//                    }
//                    catch (InvalidOperationException ie)
//                    {
//                        OnError(ie);
//                    }

//                    if (packet != null)
//                        action((T)packet);
//                }
//            });
//        }

//        private async Task<TResponse> RetryWithResponseAsync<TResponse>(Func<Task<TResponse>> func, string message) where TResponse : SynchronousResponse
//        {
//            if (func == null)
//                throw new ArgumentNullException(nameof(func));

//            await semaphore.WaitAsync(_tokenSource.Token).ConfigureAwait(false);
//            try
//            {
//                var attempt = 0;
//                while (!_tokenSource.Token.IsCancellationRequested)
//                {
//                    try
//                    {
//                        return await func().ConfigureAwait(false);
//                    }
//                    // TODO
//                    // Catch more specific exceptions
//                    catch (Exception)
//                    {
//                        if (attempt++ >= MaxRetryCount)
//                            throw;

//                        _logger.Error("Some error occured on: {message}. Retrying {attempt} of {MaxRetryCount}.", message, attempt, MaxRetryCount);
//                        await Task.Delay(TimeSpan.FromMilliseconds(500), _tokenSource.Token).ConfigureAwait(false);
//                    }
//                }
//            }
//            finally
//            {
//                semaphore.Release();
//            }

//            throw new TaskCanceledException();
//        }

//        private async Task<TResponse> WaitForResponseAsync<TResponse>(SynchronousRequest request) where TResponse : SynchronousResponse
//        {
//            while (!_tokenSource.Token.IsCancellationRequested)
//            {
//                var result = await Task<TResponse>.Run(() =>
//                {
//                    var msg = default(SynchronousResponse);
//                    _responseQueue.TryTake(out msg, (int)ResponseTimeout.TotalMilliseconds, _tokenSource.Token);

//                    //TODO: What to do if msg is null because no response came in?

//                    if (msg.SubSystem == request.SubSystem && msg.Cmd1 == request.Cmd1)
//                        return msg;
//                    else
//                        throw new Exception("Response does not belong to SREQ");
//                }).ConfigureAwait(false);

//                return result as TResponse;
//            }

//            throw new TaskCanceledException();
//        }

//        internal async Task<TResponse> SendAsync<TResponse>(SerialPacket packet) where TResponse : SynchronousResponse
//        {
//            return await RetryWithResponseAsync(async () =>
//            {
//                var request = new SynchronousRequest(packet.Cmd, packet.Payload);
//                _transmitQueue.Add(request);

//                var response = await WaitForResponseAsync<TResponse>(request)
//                    .ConfigureAwait(false);

//                return ((TResponse)response);
//            }, $"{packet.SubSystem} {(packet.Payload != null ? BitConverter.ToString(packet.Payload) : string.Empty)}");
//        }

//        private async Task SendAsync(AsynchronousRequest packet)
//        {
//            await Task.Run(() =>
//            {
//                _transmitQueue.Add(packet);
//            });
//        }    

//        public async Task<ZigBeeEndpoint> CreateEndpoint(ZigBeeNode node, byte endpointId, ZigBeeProfileType profileId)
//        {
//            AF_REGISTER register = new AF_REGISTER(endpointId, new DoubleByte((ushort)profileId.Key), new DoubleByte(0), 0, new DoubleByte[0], new DoubleByte[0]);
//            AF_REGISTER_SRSP result = await SendAsync<AF_REGISTER_SRSP>(register);

//            if(result.Status != PacketStatus.SUCESS)
//            {
//                throw new Exception($"Unable create a new Endpoint. AF_REGISTER command failed with {result.Status}");
//            }

//            ZigBeeEndpoint endpoint = new ZigBeeEndpoint(node);
//            endpoint.Id = endpointId;
//            endpoint.ProfileId = profileId;

//            node.Endpoints.Add(endpoint);

//            _logger.Info("Endpoint {Endpoint} created for node {Node}", endpoint.Id, node.NwkAdress);

//            return endpoint;
//        }

//        private async Task<byte[]> GetDeviceInfo(ZB_GET_DEVICE_INFO.DEV_INFO_TYPE info)
//        {
//            ZB_GET_DEVICE_INFO infoReq = new ZB_GET_DEVICE_INFO(info);
//            ZB_GET_DEVICE_INFO_RSP infoRsp = await SendAsync<ZB_GET_DEVICE_INFO_RSP>(infoReq).ConfigureAwait(false);

//            return infoRsp.Value;
//        }

//        internal async Task<ZigBeeAddress64> GetIeeeAddress()
//        {
//            byte[] result = await GetDeviceInfo(ZB_GET_DEVICE_INFO.DEV_INFO_TYPE.IEEE_ADDR);

//            ZigBeeAddress64 ieeeAddr = new ZigBeeAddress64(result);

//            return ieeeAddr;
//        }

//        internal async Task<ZigbeeAddress16> GetShortAddress()
//        {
//            byte[] result = await GetDeviceInfo(ZB_GET_DEVICE_INFO.DEV_INFO_TYPE.SHORT_ADDR);

//            ZigbeeAddress16 addr = new ZigbeeAddress16(ByteHelper.ShortFromBytes(result, 1, 0));

//            return addr;
//        }

//        internal async Task<ZigbeeAddress16> GetCurrentPanId()
//        {
//            byte[] result = await GetDeviceInfo(ZB_GET_DEVICE_INFO.DEV_INFO_TYPE.PAN_ID);

//            ushort relevantValue = ByteHelper.ShortFromBytes(result, 1, 0);

//            ZigbeeAddress16 panId = new ZigbeeAddress16(relevantValue);

//            return panId;
//        }

//        internal async Task<byte> GetCurrentChannel()
//        {
//            byte[] result = await GetDeviceInfo(ZB_GET_DEVICE_INFO.DEV_INFO_TYPE.CHANNEL);

//            return result[0];
//        }
//    }
//}