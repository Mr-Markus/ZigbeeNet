using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.Hardware.CC.Packet.SimpleAPI
{
    public class ZB_WRITE_CONFIGURATION_RSP : ZToolPacket
    {
        public PacketStatus Status { get; private set; }

        public ZB_WRITE_CONFIGURATION_RSP(byte[] framedata)
        {
            Status = (PacketStatus)framedata[0];

            BuildPacket(new DoubleByte(ZToolCMD.ZB_WRITE_CONFIGURATION_RSP), framedata);
        }
    }
}
