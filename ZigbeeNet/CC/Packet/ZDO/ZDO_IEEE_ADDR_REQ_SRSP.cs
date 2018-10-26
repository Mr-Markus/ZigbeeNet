using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet.CC.Packet.ZDO
{
    public class ZDO_IEEE_ADDR_REQ_SRSP : SynchronousResponse
    {
        public PacketStatus Status { get; set; }

        public ZDO_IEEE_ADDR_REQ_SRSP()
        {

        }

        public ZDO_IEEE_ADDR_REQ_SRSP(byte[] data)
        {
            Status = (PacketStatus)data[0];

            BuildPacket(CommandType.ZDO_IEEE_ADDR_REQ_SRSP, data);
        }
    }
}
