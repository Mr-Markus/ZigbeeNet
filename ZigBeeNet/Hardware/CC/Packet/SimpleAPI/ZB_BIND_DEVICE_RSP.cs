using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.Hardware.CC.Packet.SimpleAPI
{
    public class ZB_BIND_DEVICE_RSP : ZToolPacket
    {
        public ZB_BIND_DEVICE_RSP()
        {
            BuildPacket(new DoubleByte(ZToolCMD.ZB_BIND_DEVICE_RSP), new byte[0]);
        }

        public ZB_BIND_DEVICE_RSP(byte[] framedata)
        {
            BuildPacket(new DoubleByte(ZToolCMD.ZB_BIND_DEVICE_RSP), framedata);
        }
    }
}
