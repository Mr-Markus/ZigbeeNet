using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.Hardware.TI.CC2531.Util;

namespace ZigBeeNet.Hardware.TI.CC2531.Packet.ZDO
{
    /// <summary>
    /// This command is generated to inquire about the Node Descriptor information of the destination device
    /// </summary>
    public class ZDO_NODE_DESC_REQ : ZToolPacket
    {
        /// <summary>
        /// Specifies NWK address of the device generating the inquiry
        /// </summary>
        public ZToolAddress16 DstAddr { get; private set; }

        /// <summary>
        /// Specifies NWK address of the destination device being queried
        /// </summary>
        public ZToolAddress16 NwkAddrOfInterest { get; private set; }

        public ZDO_NODE_DESC_REQ(ZToolAddress16 dstAddr, ZToolAddress16 nwkAddrOfinterest)
        {
            DstAddr = dstAddr;
            NwkAddrOfInterest = nwkAddrOfinterest;

            byte[] framedata = new byte[4];
            framedata[0] = DstAddr.Lsb;
            framedata[1] = DstAddr.Msb;
            framedata[2] = NwkAddrOfInterest.Lsb;
            framedata[3] = NwkAddrOfInterest.Msb;

            BuildPacket(new DoubleByte((ushort)ZToolCMD.ZDO_NODE_DESC_REQ), framedata);
        }
    }
}
