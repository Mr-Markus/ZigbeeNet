using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.Hardware.TI.CC2531.Packet.UTIL
{
    public class UTIL_LED_CONTROL : ZToolPacket
    {
        public UTIL_LED_CONTROL(byte led, bool mode)
        {
            byte[] framedata = new byte[2];
            framedata[0] = led;
            framedata[1] = mode ? (byte)1 : (byte)0;

            base.BuildPacket(new DoubleByte((ushort)ZToolCMD.UTIL_LED_CONTROL), framedata);
        }
    }
}
