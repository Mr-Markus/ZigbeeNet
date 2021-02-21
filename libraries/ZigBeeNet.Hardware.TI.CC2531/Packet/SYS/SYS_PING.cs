using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.Hardware.TI.CC2531.Packet.SYS
{
    public class SYS_PING : ZToolPacket
    {
        public SYS_PING()
        {
            BuildPacket((ushort)ZToolCMD.SYS_PING, new byte[0]);
        }
    }
}
