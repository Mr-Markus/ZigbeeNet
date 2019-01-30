using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.Hardware.CC.Packet.SYS
{
    public class SYS_PING : ZToolPacket
    {
        public SYS_PING()
        {
            BuildPacket(new DoubleByte(ZToolCMD.SYS_PING), new byte[0]);
        }
    }
}
