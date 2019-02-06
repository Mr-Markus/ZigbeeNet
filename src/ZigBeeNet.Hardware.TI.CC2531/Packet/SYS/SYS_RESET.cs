using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.Hardware.TI.CC2531.Packet.SYS
{
    public class SYS_RESET : ZToolPacket
    {
        public SYS_RESET(byte resetType)
        {
            byte[] framedata = new byte[1];
            framedata[0] = resetType;

            BuildPacket(new DoubleByte((ushort)ZToolCMD.SYS_RESET), framedata);
        }

        /// <name>TI.ZPI1.SYS_RESET.RESET_TYPE</name>
        /// <summary>Reset type</summary>
        public class RESET_TYPE
        {
            /// <name>TI.ZPI1.SYS_RESET.RESET_TYPE.SERIAL_BOOTLOADER</name>
            /// <summary>Reset type</summary>
            public const byte SERIAL_BOOTLOADER = 1;
            /// <name>TI.ZPI1.SYS_RESET.RESET_TYPE.TARGET_DEVICE</name>
            /// <summary>Reset type</summary>
            public const byte TARGET_DEVICE = 0;
        }
    }
}
