using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.CC.Packet.ZDO
{
    public class ZDO_SIMPLE_DESC_REQ : ZToolPacket
    {
        public ZDO_SIMPLE_DESC_REQ(ZigBeeAddress16 nwkAddr, byte endPoint)
        {
            byte[] framedata = new byte[5];

            framedata[0] = nwkAddr.DoubleByte.Lsb;
            framedata[1] = nwkAddr.DoubleByte.Msb;
            framedata[2] = nwkAddr.DoubleByte.Lsb;
            framedata[3] = nwkAddr.DoubleByte.Msb;
            framedata[4] = endPoint;

            BuildPacket(new DoubleByte(ZToolCMD.ZDO_SIMPLE_DESC_REQ), framedata);
        }
    }
}
