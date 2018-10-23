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

        public bool Enabled { get; set; }
        private UnifiedNetworkProcessorInterface unpi { get; set; }

        public CCZnp()
        {
            semaphore = new SemaphoreSlim(1, 1);

            Init("COM3", 115200);
        }
 
        public void Init(string port, int baudrate = 115200)
        {
            ZpiMeta.Init();
            ZdoMeta.Init();

            unpi = new UnifiedNetworkProcessorInterface(port, baudrate, 1);
        }

        #region New Approach

        private readonly SemaphoreSlim semaphore;
        private Task readTask;
        private Task transmitTask;
        public int MaxRetryCount => 3;
        private TimeSpan ResponseTimeout { get; } = TimeSpan.FromSeconds(5);

        private BlockingCollection<SerialPacket> areqQueue;
        private BlockingCollection<SerialPacket> transmitQueue;
        private BlockingCollection<SerialPacket> responseQueue;

        public void Open()
        {
            _logger.Debug("Opening interface...");
            // TODO: port must be open!!
            unpi.Port.Open();

            Enabled = true;

            // create or reset queues
            transmitQueue = new BlockingCollection<SerialPacket>();
            areqQueue = new BlockingCollection<SerialPacket>();
            responseQueue = new BlockingCollection<SerialPacket>();

            // TODO
            // Create tasks
            readTask = new Task(() => ReadSerialPortAsync(unpi));
            transmitTask = new Task(() => ProcessQueue(transmitQueue, OnSend));

            // TODO
            // Start tasks
            readTask.Start();
            transmitTask.Start();

            _logger.Debug("Interface opened...");
        }

        private void OnSend(SerialPacket serialPacket)
        {
            if (serialPacket == null)
                throw new ArgumentNullException(nameof(serialPacket));

            serialPacket.WriteAsync(unpi.OutputStream).ConfigureAwait(false);
            _logger.Debug($"Transmitted: {serialPacket}");
        }

        public void Close()
        {
            _logger.Debug("Closing interface...");

            Enabled = false;

            transmitQueue.CompleteAdding();
            areqQueue.CompleteAdding();
            responseQueue.CompleteAdding();

            readTask.Wait();
            transmitTask.Wait();

            _logger.Debug("Closed interface...");
        }

        public async Task<byte[]> PermitJoin(int time, CancellationToken token)
        {
            PermitJoinRequest permitJoinRequest = new PermitJoinRequest(Convert.ToByte(time));

            return await Send(token, await permitJoinRequest.ToSerialPacket().ToFrame());
        }

        private async void ReadSerialPortAsync(UnifiedNetworkProcessorInterface port)
        {
            if (port == null) throw new ArgumentNullException(nameof(port));

            while (Enabled)
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

                    if (serialPacket is AsynchronousRequest)
                    {
                        //TODO: Check if it is a callback for an SREQ

                        areqQueue.Add(serialPacket);
                    }
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

        private async Task<SerialPacket> WaitForResponseAsync(Func<SerialPacket, bool> predicate,
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

                var response = await WaitForResponseAsync(msg => predicate((SynchronousResponse) msg), cancellationToken)
                    .ConfigureAwait(false);

                var zpiObject = new ZpiObject(response.SubSystem, response.Type, response.Cmd1);
                zpiObject.OnParsed += (s, result) =>
                {
                    _logger.Info("Parsed: {Type} - {SubSystem} - {Name}", response.Type, result.SubSystem, result.Name);
                };
                zpiObject.Parse(response.Type, response.Length, response.Payload);

                return ((SynchronousResponse) response).Payload;
            }, $"{subSystem} {(payload != null ? BitConverter.ToString(payload) : string.Empty)}", cancellationToken);
        }

        public async Task<byte[]> Send(CancellationToken cancellationToken, params byte[] payload)
        {
            using(MemoryStream stream = new MemoryStream(payload))
            {
                var packet = await SerialPacket.ReadAsync(stream);

                return await Send(packet.SubSystem, packet.Payload,
                    msg => { return msg is SynchronousResponse && msg.SubSystem == packet.SubSystem; },
                    cancellationToken);
            }
        }

        #endregion
    }
}