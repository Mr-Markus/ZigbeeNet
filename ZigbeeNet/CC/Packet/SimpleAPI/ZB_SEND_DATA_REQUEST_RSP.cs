using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet.CC.Packet.SimpleAPI
{
    public class ZB_SEND_DATA_REQUEST_RSP : SynchronousResponse
    {
        public ZB_SEND_DATA_REQUEST_RSP()
        {
            BuildPacket(CommandType.ZB_SEND_DATA_REQUEST_RSP, new byte[0]);
        }

        public ZB_SEND_DATA_REQUEST_RSP(byte[] framedata)
        {
            BuildPacket(CommandType.ZB_SEND_DATA_REQUEST_RSP, framedata);
        }
    }
}
