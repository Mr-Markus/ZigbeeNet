using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet.CC.Packet.ZDO
{
    public class ZDO_SIMPLE_DESC_REQ : SynchronousRequest
    {
        public ZDO_SIMPLE_DESC_REQ(ZigbeeAddress16 nwkAddr, byte endPoint)
        {
            byte[] framedata = new byte[5];

            framedata[0] = nwkAddr.DoubleByte.Lsb;
            framedata[1] = nwkAddr.DoubleByte.Msb;
            framedata[2] = nwkAddr.DoubleByte.Lsb;
            framedata[3] = nwkAddr.DoubleByte.Msb;
            framedata[4] = endPoint;

            BuildPacket(CommandType.ZDO_SIMPLE_DESC_REQ, framedata);
        }
    }
}
