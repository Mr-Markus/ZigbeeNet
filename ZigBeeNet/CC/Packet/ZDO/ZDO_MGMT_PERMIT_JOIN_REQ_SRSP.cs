using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.CC.Packet.ZDO
{
    public class ZDO_MGMT_PERMIT_JOIN_REQ_SRSP : ZToolPacket
    {
        public PacketStatus Status { get; private set; }

        public ZDO_MGMT_PERMIT_JOIN_REQ_SRSP(byte[] data)
        {
            Status = (PacketStatus)data[0];
            BuildPacket(new DoubleByte(ZToolCMD.ZDO_MGMT_PERMIT_JOIN_REQ_SRSP), data);
        }
    }
}
