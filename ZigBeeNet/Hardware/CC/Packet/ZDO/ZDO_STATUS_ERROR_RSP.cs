using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.Hardware.CC.Util;

namespace ZigBeeNet.Hardware.CC.Packet.ZDO
{
    public class ZDO_STATUS_ERROR_RSP : ZToolPacket
    {
        /// <summary>
        /// Source address of the message
        /// </summary>
        public ZToolAddress16 SrcAddr { get; private set; }

        /// <summary>
        /// This field indicates either SUCCESS (0) or FAILURE (1).
        /// </summary>
        public PacketStatus Status { get; private set; }

        public ZDO_STATUS_ERROR_RSP(byte[] data)
        {
            SrcAddr = new ZToolAddress16(data[1], data[0]);
            Status = (PacketStatus)data[2];

            BuildPacket(new DoubleByte(ZToolCMD.ZDO_STATUS_ERROR_RSP), data);
        }
    }
}
