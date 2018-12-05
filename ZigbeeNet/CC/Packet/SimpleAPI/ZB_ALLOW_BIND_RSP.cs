using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.CC.Packet.SimpleAPI
{
    public class ZB_ALLOW_BIND_RSP : SynchronousResponse
    {
        public ZB_ALLOW_BIND_RSP()
        {
            BuildPacket(CommandType.ZB_ALLOW_BIND_RSP, new byte[0]);
        }

        public ZB_ALLOW_BIND_RSP(byte[] framedata)
        {
            BuildPacket(CommandType.ZB_ALLOW_BIND_RSP, framedata);
        }
    }
}
