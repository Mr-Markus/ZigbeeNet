using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.CC.Packet.SimpleAPI
{
    public class ZB_FIND_DEVICE_REQUEST_RSP : SynchronousResponse
    {
        public ZB_FIND_DEVICE_REQUEST_RSP()
        {
            BuildPacket(CommandType.ZB_FIND_DEVICE_REQUEST_RSP, new byte[0]);
        }

        public ZB_FIND_DEVICE_REQUEST_RSP(byte[] framedata)
        {
            BuildPacket(CommandType.ZB_FIND_DEVICE_REQUEST_RSP, framedata);
        }
    }
}
