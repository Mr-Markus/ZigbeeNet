using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.Hardware.TI.CC2531.Packet.SimpleAPI
{
    public class ZB_ALLOW_BIND_RSP : ZToolPacket
    {
        public ZB_ALLOW_BIND_RSP()
        {
            BuildPacket(new DoubleByte((ushort)ZToolCMD.ZB_ALLOW_BIND_RSP), new byte[0]);
        }

        public ZB_ALLOW_BIND_RSP(byte[] framedata)
        {
            BuildPacket(new DoubleByte((ushort)ZToolCMD.ZB_ALLOW_BIND_RSP), framedata);
        }
    }
}
