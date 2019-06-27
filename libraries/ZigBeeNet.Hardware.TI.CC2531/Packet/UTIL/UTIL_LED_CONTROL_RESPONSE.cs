using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.Hardware.TI.CC2531.Packet.UTIL
{
    public class UTIL_LED_CONTROL_RESPONSE : ZToolPacket
    {
        public PacketStatus Status;

        public UTIL_LED_CONTROL_RESPONSE(byte[] framedata)
        {
            Status = (PacketStatus)framedata[0];

            base.BuildPacket(new DoubleByte((ushort)ZToolCMD.UTIL_LED_CONTROL_RESPONSE), framedata);
        }
    }
}
