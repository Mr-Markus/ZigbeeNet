using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.Extensions;

namespace ZigBeeNet.Hardware.TI.CC2531.Packet.ZDO
{
    public class ZDO_MSG_CB_REGISTER : ZToolPacket
    {
        public ZDO_MSG_CB_REGISTER(ushort cluster)
        {
            byte[] framedata = new byte[2];
            framedata[0] = cluster.GetLSB();
            framedata[1] = cluster.GetMSB();

            BuildPacket((ushort)ZToolCMD.ZDO_MSG_CB_REGISTER, framedata);
        }
}
}
