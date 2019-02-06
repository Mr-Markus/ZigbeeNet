using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ZigBeeNet.Hardware.TI.CC2531.Packet;
using ZigBeeNet.Logging;
using ZigBeeNet.Transport;

namespace ZigBeeNet.Hardware.TI.CC2531.Packet
{
    public class ZToolPacketParser
    {
        /**
         * The logger.
         */
        private readonly ILog _logger = LogProvider.For<ZToolPacketParser>();

        /**
         * The packet handler.
         */
        private IZToolPacketHandler _packetHandler;
        /**
         * The input port.
         */
        private IZigBeePort _port;
        /**
         * The parser parserThread.
         */
        private Task parserTask = null;

        private CancellationTokenSource _cancellationToken;
        
        /**
         * Construct which sets input stream where the packet is read from the and handler
         * which further processes the received packet.
         *
         * @param port the {@link ZigBeePort}
         * @param packetHandler the packet handler
         */
        public ZToolPacketParser(IZigBeePort port, IZToolPacketHandler packetHandler)
        {
            _logger.Trace("Creating ZToolPacketParser");

            _port = port;
            _cancellationToken = new CancellationTokenSource();
            _packetHandler = packetHandler;

            parserTask = new Task(Run);
            parserTask.Start();
        }

        /**
         * Run method executed by the parser thread.
         */
        public void Run()
        {
            _logger.Trace("ZToolPacketParser parserThread started");
            while (!_cancellationToken.IsCancellationRequested)
            {
                try
                {
                    byte? val = _port.Read();
                    if (val == ZToolPacket.START_BYTE)
                    {
                        // inputStream.mark(256);
                        ZToolPacketStream packetStream = new ZToolPacketStream(_port);
                        ZToolPacket response = packetStream.ParsePacket();

                        _logger.Trace("Response is {Type} -> {Response}", response.GetType().Name, response);
                        if (response.Error)
                        {
                            _logger.Debug("Received a BAD PACKET {Response}", response.ToString());
                            // inputStream.reset();
                            continue;
                        }

                        _packetHandler.HandlePacket(response);
                    }
                    else if (val != null)
                    {
                        // Log if not end of stream.
                        _logger.Debug("Discarded stream: expected start byte but received {Value}", val);
                    }
                }
                catch (IOException e)
                {
                    if (!_cancellationToken.IsCancellationRequested)
                    {
                        _packetHandler.Error(e);
                        _cancellationToken.Cancel();
                    }
                }
            }
            _logger.Debug("ZToolPacketParser parserThread exited.");
        }

        /**
         * Requests parser thread to shutdown.
         */
        public void Close()
        {
            _cancellationToken.Cancel();
        }

        /**
         * Checks if parser thread is alive.
         *
         * @return true if parser thread is alive.
         */
        public bool IsAlive()
        {
            return parserTask != null && parserTask.Status == TaskStatus.Running;
        }

    }
}
