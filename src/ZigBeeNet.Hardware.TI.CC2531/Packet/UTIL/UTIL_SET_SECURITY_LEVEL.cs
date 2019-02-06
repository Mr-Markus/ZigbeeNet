using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.Hardware.TI.CC2531.Packet.UTIL
{
    public class UTIL_SET_SECURITY_LEVEL : ZToolPacket
    {
        /// <name>TI.ZPI1.SYS_SET_SECURITY_LEVEL.SecurityLevel</name>
        /// <summary>Security Level.</summary>
        public byte SecurityLevel;

        /// <name>TI.ZPI1.SYS_SET_SECURITY_LEVEL</name>
        /// <summary>Constructor</summary>
        public UTIL_SET_SECURITY_LEVEL()
        {
        }

        /// <name>TI.ZPI1.SYS_SET_SECURITY_LEVEL</name>
        /// <summary>Constructor</summary>
        public UTIL_SET_SECURITY_LEVEL(byte num1)
        {
            this.SecurityLevel = num1;

            byte[] framedata = new byte[1];
            framedata[0] = this.SecurityLevel;

            BuildPacket(new DoubleByte((ushort)ZToolCMD.UTIL_SET_SECURITY_LEVEL), framedata);
        }

        public UTIL_SET_SECURITY_LEVEL(byte[] framedata)
        {
            this.SecurityLevel = framedata[0];

            BuildPacket(new DoubleByte((ushort)ZToolCMD.UTIL_SET_SECURITY_LEVEL), framedata);
        }
    }
}
