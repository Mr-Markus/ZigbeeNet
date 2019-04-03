using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ZigBeeNet.Hardware.TI.CC2531.Packet;
using Serilog;
using ZigBeeNet.Transport;

namespace ZigBeeNet.Hardware.TI.CC2531.Packet
{
    public class ZToolPacketParser
    {
        /// <summary>
        /// The packet handler.
        /// </summary>
        private IZToolPacketHandler _packetHandler;

        /// <summary>
        /// The input port.
        /// </summary>
        private IZigBeePort _port;

        /// <summary>
        /// The parser parserThread.
        /// </summary>
        private Task _parserTask = null;

        private CancellationTokenSource _cancellationToken;

        /// <summary>
        /// Construct which sets input stream where the packet is read from the and handler
        /// which further processes the received packet.
        ///
        /// <param name="port">the <see cref="ZigBeePort"></param>
        /// <param name="packetHandler">the packet handler</param>
        /// </summary>
        public ZToolPacketParser(IZigBeePort port, IZToolPacketHandler packetHandler)
        {
            Log.Verbose("Creating ZToolPacketParser");

            _port = port;
            _cancellationToken = new CancellationTokenSource();
            _packetHandler = packetHandler;

            _parserTask = new Task(Run);
            _parserTask.Start();
        }

        /// <summary>
        /// Run method executed by the parser thread.
        /// </summary>
        public void Run()
        {
            Log.Verbose("ZToolPacketParser parserThread started");
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

                        Log.Verbose("Response is {Type} -> {Response}", response.GetType().Name, response);
                        if (response.Error)
                        {
                            Log.Debug("Received a BAD PACKET {Response}", response.ToString());
                            // inputStream.reset();
                            continue;
                        }

                        _packetHandler.HandlePacket(response);
                    }
                    else if (val != null)
                    {
                        // Log if not end of stream.
                        Log.Debug("Discarded stream: expected start byte but received {Value}", val);
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
            Log.Debug("ZToolPacketParser parserThread exited.");
        }

        /// <summary>
        /// Requests parser thread to shutdown.
        /// </summary>
        public void Close()
        {
            _cancellationToken.Cancel();
        }

        /// <summary>
        /// Checks if parser thread is alive.
        ///
        /// <returns>true if parser thread is alive.</returns>
        /// </summary>
        public bool IsAlive()
        {
            return _parserTask != null && _parserTask.Status == TaskStatus.Running;
        }

    }
}
