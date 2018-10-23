using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using BinarySerialization;
using ZigbeeNet.CC.SYS;
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

        public int MaxRetryCount => 3;
        public bool Enabled { get; set; }

        public CCZnp()
        {
            semaphore = new SemaphoreSlim(1, 1);

            ZpiMeta.Init();
            ZdoMeta.Init();

            unpi = new UnifiedNetworkProcessorInterface("COM3", 115200, 1); //TODO: Get by config
        }

        public void Open()
        {
            _tokenSource = new CancellationTokenSource();

            _logger.Debug("Opening interface...");
            // TODO: port must be open!!
            unpi.Port.Open();

            Enabled = true;

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
        }

        private void OnSend(SerialPacket serialPacket)
        {
            if (serialPacket == null)
                throw new ArgumentNullException(nameof(serialPacket));

            serialPacket.WriteAsync(unpi.OutputStream).ConfigureAwait(false);
            _logger.Debug($"Transmitted: {serialPacket}");
        }

        private void OnReceive(AsynchronousRequest asynchronousRequest)
        {
            //TODO: HandleIncommingMessage
        }

        public void Close()
        {
            _logger.Debug("Closing interface...");

            Enabled = false;

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

            while (Enabled)
            {
                try
                {
                    var serialPacket = await SerialPacket.ReadAsync(port.InputStream).ConfigureAwait(false);

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

        protected virtual void OnError(Exception e)
        {
            _logger.Error($"Exception: {e}");
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

                        _logger.Error($"Some error occured on: {message}. Retrying {attempt} of {MaxRetryCount}.");
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
        
        private async Task<byte[]> SendAsync(SubSystem subSystem, byte[] payload, Func<SynchronousResponse, bool> predicate)
        {
            return await RetryAsync(async () =>
            {
                var request = new SynchronousRequest(subSystem, 0, payload);
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

        public async Task<byte[]> SendAsync(byte[] payload)
        {
            if (unpi.Port.IsOpen == false)
                throw new Exception("Port is not opened");

            using(MemoryStream stream = new MemoryStream(payload))
            {
                var packet = await SerialPacket.ReadAsync(stream).ConfigureAwait(false);

                return await SendAsync(packet.SubSystem, packet.Payload,
                    msg => { return msg is SynchronousResponse && msg.SubSystem == packet.SubSystem; }).ConfigureAwait(false);
            }
        }
    }
}