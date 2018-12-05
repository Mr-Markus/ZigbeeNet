using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.CC.Packet.ZDO
{
    public class ZDO_BIND_REQ_SRSP : SynchronousResponse
    {
        public PacketStatus Status { get; private set; }

        public ZDO_BIND_REQ_SRSP(byte[] data)
        {
            Status = (PacketStatus)data[0];

            BuildPacket(CommandType.ZDO_BIND_REQ_SRSP, data);
        }
    }
}
