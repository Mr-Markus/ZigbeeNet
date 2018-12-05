using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.CC.Packet.SimpleAPI
{
    public class ZB_WRITE_CONFIGURATION_RSP : SynchronousResponse
    {
        public PacketStatus Status { get; private set; }

        public ZB_WRITE_CONFIGURATION_RSP(byte[] framedata)
        {
            Status = (PacketStatus)framedata[0];

            BuildPacket(CommandType.ZB_WRITE_CONFIGURATION_RSP, framedata);
        }
    }
}
