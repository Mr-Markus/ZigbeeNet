using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.CC.Packet.SimpleAPI
{
    public class ZB_FIND_DEVICE_REQUEST_RSP : ZToolPacket
    {
        public ZB_FIND_DEVICE_REQUEST_RSP()
        {
            BuildPacket(new DoubleByte(ZToolCMD.ZB_FIND_DEVICE_REQUEST_RSP), new byte[0]);
        }

        public ZB_FIND_DEVICE_REQUEST_RSP(byte[] framedata)
        {
            BuildPacket(new DoubleByte(ZToolCMD.ZB_FIND_DEVICE_REQUEST_RSP), framedata);
        }
    }
}
