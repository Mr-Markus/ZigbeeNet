using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet.CC.Packet.ZDO
{
    public class ZDO_BIND_RSP : AsynchronousRequest
    {
        public ZAddress16 srcAddr { get; set; }

        public PacketStatus Status { get; set; }

        public ZDO_BIND_RSP()
        {

        }

        public ZDO_BIND_RSP(byte[] data)
        {
            srcAddr = new ZAddress16(data[0], data[1]);
            Status = (PacketStatus)data[2];

            BuildPacket(CommandType.ZDO_BIND_RSP, data);
        }
    }
}
