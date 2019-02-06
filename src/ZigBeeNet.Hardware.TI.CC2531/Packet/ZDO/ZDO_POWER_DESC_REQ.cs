using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.Extensions;
using ZigBeeNet.Hardware.TI.CC2531.Util;

namespace ZigBeeNet.Hardware.TI.CC2531.Packet.ZDO
{
    public class ZDO_POWER_DESC_REQ : ZToolPacket
    {
        /// <name>TI.ZPI1.ZDO_POWER_DESC_REQ.DstAddr</name>
        /// <summary>destination address</summary>
        public ZToolAddress16 DstAddr { get; set; }
        /// <name>TI.ZPI1.ZDO_POWER_DESC_REQ.NWKAddrOfInterest</name>
        /// <summary>NWK address for the request</summary>
        public ZToolAddress16 NWKAddrOfInterest { get; set; }

        /// <name>TI.ZPI1.ZDO_POWER_DESC_REQ</name>
        /// <summary>Constructor</summary>
        public ZDO_POWER_DESC_REQ()
        {
        }

        public ZDO_POWER_DESC_REQ(ushort destination)
        {
            // TODO Check compatibility with other Constructor
            byte[] framedata = new byte[4];
            framedata[0] = destination.GetByte(0);
            framedata[1] = destination.GetByte(1);
            framedata[2] = framedata[0];
            framedata[3] = framedata[1];
            BuildPacket(new DoubleByte((ushort)ZToolCMD.ZDO_POWER_DESC_REQ), framedata);
        }
    }
}
