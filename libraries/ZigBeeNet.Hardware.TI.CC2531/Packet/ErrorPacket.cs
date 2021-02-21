using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.Hardware.TI.CC2531.Packet
{
    public class ErrorPacket : ZToolPacket
    {
        public ErrorPacket()
            :base(0, new byte[0])
        {

        }
    }
}
