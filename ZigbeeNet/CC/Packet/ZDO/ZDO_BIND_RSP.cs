using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet.CC.Packet.ZDO
{
    public class ZDO_BIND_RSP : AsynchronousRequest
    {
        public ZAddress16 srcAddr { get; private  set; }

        public PacketStatus Status { get; private set; }


        public ZDO_BIND_RSP(byte[] data)
        {
            srcAddr = new ZAddress16(data[1], data[0]);
            Status = (PacketStatus)data[2];

            BuildPacket(CommandType.ZDO_BIND_RSP, data);
        }
    }
}
