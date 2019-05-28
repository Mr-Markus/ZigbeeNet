using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.Hardware.TI.CC2531.Packet
{
    public class ErrorPacket : ZToolPacket
    {
        public ErrorPacket()
            :base(new DoubleByte(), new byte[0])
        {

        }
    }
}
