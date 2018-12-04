using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet.CC.Packet.SYS
{
    public class SYS_VERSION : SynchronousRequest
    {

        public SYS_VERSION()
        {
            BuildPacket(CommandType.SYS_VERSION, new byte[0]);
        }
    }
}
