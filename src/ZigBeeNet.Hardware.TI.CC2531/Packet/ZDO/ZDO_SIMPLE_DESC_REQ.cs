using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.Hardware.TI.CC2531.Util;

namespace ZigBeeNet.Hardware.TI.CC2531.Packet.ZDO
{
    public class ZDO_SIMPLE_DESC_REQ : ZToolPacket
    {
        public ZDO_SIMPLE_DESC_REQ(ZToolAddress16 nwkAddr, byte endPoint)
        {
            byte[] framedata = new byte[5];

            framedata[0] = nwkAddr.Lsb;
            framedata[1] = nwkAddr.Msb;
            framedata[2] = nwkAddr.Lsb;
            framedata[3] = nwkAddr.Msb;
            framedata[4] = endPoint;

            BuildPacket(new DoubleByte((ushort)ZToolCMD.ZDO_SIMPLE_DESC_REQ), framedata);
        }
    }
}
