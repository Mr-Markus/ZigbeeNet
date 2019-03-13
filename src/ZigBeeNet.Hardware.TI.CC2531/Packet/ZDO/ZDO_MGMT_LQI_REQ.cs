using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.Hardware.TI.CC2531.Util;

namespace ZigBeeNet.Hardware.TI.CC2531.Packet.ZDO
{
    /// <summary>
    /// This command is generated to request the destination device to return its neighbor table.
    /// </summary>
    public class ZDO_MGMT_LQI_REQ : ZToolPacket
    {
        /// <name>TI.ZPI1.ZDO_MGMT_LQI_REQ.DstAddr</name>
        /// <summary>Destination network address.</summary>
        public ZToolAddress16 DstAddr { get; private set; }
        /// <name>TI.ZPI1.ZDO_MGMT_LQI_REQ.StartIndex</name>
        /// <summary>Where to start. The result can be more networks than can be reported, so this field allows a user to
        /// ask for more.</summary>
        public byte StartIndex { get; private set; }

        /// <name>TI.ZPI1.ZDO_MGMT_LQI_REQ</name>
        /// <summary>Constructor</summary>
        public ZDO_MGMT_LQI_REQ()
        {
        }

        /// <name>TI.ZPI1.ZDO_MGMT_LQI_REQ</name>
        /// <summary>Constructor</summary>
        public ZDO_MGMT_LQI_REQ(ZToolAddress16 num1, byte num2)
        {
            this.DstAddr = num1;
            this.StartIndex = num2;

            byte[] framedata = new byte[3];
            framedata[0] = this.DstAddr.Lsb;
            framedata[1] = this.DstAddr.Msb;
            framedata[2] = this.StartIndex;
            BuildPacket(new DoubleByte((ushort)ZToolCMD.ZDO_MGMT_LQI_REQ), framedata);
        }
    }
}
