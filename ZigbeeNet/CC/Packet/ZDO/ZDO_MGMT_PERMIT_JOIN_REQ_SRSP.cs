using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet.CC.Packet.ZDO
{
    public class ZDO_MGMT_PERMIT_JOIN_REQ_SRSP : SynchronousResponse
    {
        public ZpiStatus Status { get; private set; }

        public ZDO_MGMT_PERMIT_JOIN_REQ_SRSP(byte[] data)
        {
            Status = (ZpiStatus)data[0];
            BuildPacket(CommandType.ZDO_MGMT_PERMIT_JOIN_REQ_SRSP, data);
        }
    }
}
