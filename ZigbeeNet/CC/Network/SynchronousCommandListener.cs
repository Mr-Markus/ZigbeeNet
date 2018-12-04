using System;
using System.Collections.Generic;
using System.Text;
using ZigbeeNet.CC.Packet;
using ZigbeeNet.Logging;

namespace ZigbeeNet.CC.Network
{
    public class SynchronousCommandListener : ISynchronousCommandListener
    {
        private readonly ILog _logger = LogProvider.For<SynchronousCommandListener>();

        public event EventHandler<SerialPacket> OnResponseReceived;

        public void ReceivedCommandResponse(SerialPacket packet)
        {
            _logger.Trace(" {} received as synchronous command.", packet.GetType().Name);
            lock(packet) {
                OnResponseReceived?.Invoke(this, packet);

                // Do not set response[0] again.
                //response[0] = packet;
                //response.notify();
            }
        }
    }
}
