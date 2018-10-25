using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet.CC.Packet.ZDO
{
    public class ZDO_MGMT_PERMIT_JOIN_RSP : AsynchronousRequest
    {
        public ZAddress16 SrcAddr { get; set; }

        public ZpiStatus Status { get; set; }

        public ZDO_MGMT_PERMIT_JOIN_RSP()
        {

        }

        public ZDO_MGMT_PERMIT_JOIN_RSP(byte[] data)
        {
            SrcAddr = new ZAddress16(data[0], data[1]);
            Status = (ZpiStatus)data[2];

            BuildPacket(CommandType.ZDO_MGMT_PERMIT_JOIN_RSP, data);
        }
    }
}
