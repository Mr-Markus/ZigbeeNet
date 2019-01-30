using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.Hardware.CC.Packet.SYS
{
    public class SYS_VERSION : ZToolPacket
    {

        public SYS_VERSION()
        {
            BuildPacket(new DoubleByte(ZToolCMD.SYS_VERSION), new byte[0]);
        }
    }
}
