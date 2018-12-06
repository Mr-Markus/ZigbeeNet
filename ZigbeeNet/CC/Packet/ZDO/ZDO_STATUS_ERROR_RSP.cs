using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.CC.Packet.ZDO
{
    public class ZDO_STATUS_ERROR_RSP : AsynchronousRequest
    {
        /// <summary>
        /// Source address of the message
        /// </summary>
        public ZigBeeAddress16 SrcAddr { get; private set; }

        /// <summary>
        /// This field indicates either SUCCESS (0) or FAILURE (1).
        /// </summary>
        public PacketStatus Status { get; private set; }

        public ZDO_STATUS_ERROR_RSP(byte[] data)
        {
            SrcAddr = new ZigBeeAddress16(data[1], data[0]);
            Status = (PacketStatus)data[2];

            BuildPacket(CommandType.ZDO_STATUS_ERROR_RSP, data);
        }
    }
}
