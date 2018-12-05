using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.CC.Packet.ZDO
{
    public class ZDO_MGMT_PERMIT_JOIN_RSP : AsynchronousRequest
    {
        /// <summary>
        /// Source address of the message
        /// </summary>
        public ZigbeeAddress16 SrcAddr { get; private set; }

        public PacketStatus Status { get; private set; }


        public ZDO_MGMT_PERMIT_JOIN_RSP(byte[] data)
        {
            SrcAddr = new ZigbeeAddress16(data[1], data[0]);
            Status = (PacketStatus)data[2];

            BuildPacket(CommandType.ZDO_MGMT_PERMIT_JOIN_RSP, data);
        }
    }
}
