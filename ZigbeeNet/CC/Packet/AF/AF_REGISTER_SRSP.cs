using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.CC.Packet.AF
{
    public class AF_REGISTER_SRSP : SynchronousResponse
    {
        public PacketStatus Status { get; private set; }

        public AF_REGISTER_SRSP(byte[] framedata)
        {
            Status = (PacketStatus)framedata[0];

            BuildPacket(CommandType.AF_REGISTER_SRSP, framedata);
        }
    }
}
