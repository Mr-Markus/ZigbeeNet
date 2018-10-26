using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet.CC.Packet.ZDO
{
    /// <summary>
    /// This command is generated to inquire about the Node Descriptor information of the destination device
    /// </summary>
    public class ZDO_NODE_DESC_REQ : SynchronousRequest
    {
        /// <summary>
        /// Specifies NWK address of the device generating the inquiry
        /// </summary>
        public ZAddress16 DstAddr { get; private set; }

        /// <summary>
        /// Specifies NWK address of the destination device being queried
        /// </summary>
        public ZAddress16 NwkAddrOfInterest { get; private set; }

        public ZDO_NODE_DESC_REQ(ZAddress16 dstAddr, ZAddress16 nwkAddrOfinterest)
        {
            DstAddr = dstAddr;
            NwkAddrOfInterest = nwkAddrOfinterest;

            byte[] framedata = new byte[4];
            framedata[0] = DstAddr.DoubleByte.Low;
            framedata[1] = DstAddr.DoubleByte.High;
            framedata[2] = NwkAddrOfInterest.DoubleByte.Low;
            framedata[3] = NwkAddrOfInterest.DoubleByte.High;

            BuildPacket(CommandType.ZDO_NODE_DESC_REQ, framedata);
        }
    }
}
