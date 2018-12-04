using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet.CC.Packet.ZDO
{
    public class ZDO_MSG_CB_REGISTER : SynchronousRequest
    {
        public ZDO_MSG_CB_REGISTER(DoubleByte cluster)
        {
            byte[] framedata = new byte[2];
            framedata[0] = cluster.Lsb;
            framedata[1] = cluster.Msb;

            BuildPacket(CommandType.ZDO_MSG_CB_REGISTER, framedata);
        }
}
}
