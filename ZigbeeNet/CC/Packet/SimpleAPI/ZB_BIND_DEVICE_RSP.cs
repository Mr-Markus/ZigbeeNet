using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.CC.Packet.SimpleAPI
{
    public class ZB_BIND_DEVICE_RSP : SynchronousResponse
    {
        public ZB_BIND_DEVICE_RSP()
        {
            BuildPacket(CommandType.ZB_BIND_DEVICE_RSP, new byte[0]);
        }

        public ZB_BIND_DEVICE_RSP(byte[] framedata)
        {
            BuildPacket(CommandType.ZB_BIND_DEVICE_RSP, framedata);
        }
    }
}
