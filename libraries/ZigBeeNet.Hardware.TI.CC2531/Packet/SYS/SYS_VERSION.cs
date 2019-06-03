using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.Hardware.TI.CC2531.Packet.SYS
{
    public class SYS_VERSION : ZToolPacket
    {

        public SYS_VERSION()
        {
            BuildPacket(new DoubleByte((ushort)ZToolCMD.SYS_VERSION), new byte[0]);
        }
    }
}
