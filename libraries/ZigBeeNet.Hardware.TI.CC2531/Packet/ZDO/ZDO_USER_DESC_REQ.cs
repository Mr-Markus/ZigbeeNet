using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.Hardware.TI.CC2531.Util;

namespace ZigBeeNet.Hardware.TI.CC2531.Packet.ZDO
{
    public class ZDO_USER_DESC_REQ : ZToolPacket
    {
        /// <name>TI.ZPI1.ZDO_USER_DESC_REQ.DstAddr</name>
        /// <summary>destination address</summary>
        public ZToolAddress16 DstAddr { get; set; }
        /// <name>TI.ZPI1.ZDO_USER_DESC_REQ.NWKAddrOfInterest</name>
        /// <summary>NWK address for the request</summary>
        public ZToolAddress16 NwkAddr { get; set; }

        public ZDO_USER_DESC_REQ(ZToolAddress16 num1, ZToolAddress16 num2)
        {
            this.DstAddr = num1;
            this.NwkAddr = num2;

            byte[] framedata = new byte[4];
            framedata[0] = this.DstAddr.Lsb;
            framedata[1] = this.DstAddr.Msb;
            framedata[2] = this.NwkAddr.Lsb;
            framedata[3] = this.NwkAddr.Msb;

            BuildPacket(new DoubleByte((ushort)ZToolCMD.ZDO_USER_DESC_REQ), framedata);
        }
    }
}
