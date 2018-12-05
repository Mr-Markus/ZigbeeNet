using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.CC.Packet.ZDO
{
    public class ZDO_ACTIVE_EP_REQ : SynchronousRequest
    {
        public ZDO_ACTIVE_EP_REQ(ZigbeeAddress16 dstAddr, ZigbeeAddress16 nwkAddrOfInterest)
        {
            byte[] framedata = new byte[4];
            framedata[0] = dstAddr.DoubleByte.Lsb;
            framedata[1] = dstAddr.DoubleByte.Msb;
            framedata[2] = nwkAddrOfInterest.DoubleByte.Lsb;
            framedata[3] = nwkAddrOfInterest.DoubleByte.Msb;

            BuildPacket(CommandType.ZDO_ACTIVE_EP_REQ, framedata);
        }
    }
}
