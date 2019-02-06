using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.Hardware.TI.CC2531.Util;

namespace ZigBeeNet.Hardware.TI.CC2531.Packet.ZDO
{
    public class ZDO_ACTIVE_EP_REQ : ZToolPacket
    {
        public ZDO_ACTIVE_EP_REQ(ZToolAddress16 dstAddr, ZToolAddress16 nwkAddrOfInterest)
        {
            byte[] framedata = new byte[4];
            framedata[0] = dstAddr.Lsb;
            framedata[1] = dstAddr.Msb;
            framedata[2] = nwkAddrOfInterest.Lsb;
            framedata[3] = nwkAddrOfInterest.Msb;

            BuildPacket(new DoubleByte((ushort)ZToolCMD.ZDO_ACTIVE_EP_REQ), framedata);
        }
    }
}
