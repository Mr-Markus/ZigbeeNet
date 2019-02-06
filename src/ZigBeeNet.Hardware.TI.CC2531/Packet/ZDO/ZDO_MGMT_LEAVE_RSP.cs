using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.Hardware.TI.CC2531.Util;

namespace ZigBeeNet.Hardware.TI.CC2531.Packet.ZDO
{
    public class ZDO_MGMT_LEAVE_RSP : ZToolPacket
    {
        /// <name>TI.ZPI1.ZDO_MGMT_LEAVE_RSP.SrcAddress</name>
        /// <summary>Source address of the message</summary>
        public ZToolAddress16 SrcAddress { get; private set; }
        /// <name>TI.ZPI1.ZDO_MGMT_LEAVE_RSP.Status</name>
        /// <summary>this field indicates either SUCCESS (0) or FAILURE (1).</summary>
        public int Status { get; private set; }

        /// <name>TI.ZPI1.ZDO_MGMT_LEAVE_RSP</name>
        /// <summary>Constructor</summary>
        public ZDO_MGMT_LEAVE_RSP()
        {
        }

        public ZDO_MGMT_LEAVE_RSP(byte[] framedata)
        {
            this.SrcAddress = new ZToolAddress16(framedata[1], framedata[0]);
            this.Status = framedata[2];
            BuildPacket(new DoubleByte((ushort)ZToolCMD.ZDO_MGMT_LEAVE_RSP), framedata);
        }

        public override string ToString()
        {
            return "ZDO_MGMT_LEAVE_RSP{" + "SrcAddress=" + SrcAddress + ", Status=" + Status
                    + '}';
        }
    }
}
