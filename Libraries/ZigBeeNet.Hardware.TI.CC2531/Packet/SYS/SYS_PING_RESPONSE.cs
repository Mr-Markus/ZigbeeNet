using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.Hardware.TI.CC2531.Packet.SYS
{
    public class SYS_PING_RESPONSE : ZToolPacket
    {
        /// <name>TI.ZPI1.SYS_PING_RESPONSE.Capabilities</name>
        /// <summary>This field represents the interfaces that this device can handle (compiled into the device).</summary>
        public int Capabilities;

        /// <name>TI.ZPI1.SYS_PING_RESPONSE</name>
        /// <summary>Constructor</summary>
        public SYS_PING_RESPONSE()
        {
        }

        public SYS_PING_RESPONSE(DoubleByte capabilities1)
        {
            this.Capabilities = capabilities1.Value;

            byte[] framedata = new byte[2];
            framedata[0] = capabilities1.Lsb;
            framedata[1] = capabilities1.Msb;

            BuildPacket(new DoubleByte((ushort)ZToolCMD.SYS_PING_RESPONSE), framedata);
        }

        public SYS_PING_RESPONSE(byte[] framedata)
        {
            this.Capabilities = new DoubleByte(framedata[1], framedata[0]).Value;

            BuildPacket(new DoubleByte((ushort)ZToolCMD.SYS_PING_RESPONSE), framedata);
        }

        /// <name>TI.ZPI1.SYS_PING_RESPONSE.CAPABILITIES</name>
        /// <summary>Capabilities bitfield</summary>
        public class CAPABILITIES
        {
            /// <name>TI.ZPI2.SYS_PING_RESPONSE.CAPABILITIES.MT_CAP_AF</name>
            /// <summary>Capabilities bitfield</summary>
            public const byte MT_CAP_AF = 8;
            /// <name>TI.ZPI2.SYS_PING_RESPONSE.CAPABILITIES.MT_CAP_APP</name>
            /// <summary>Capabilities bitfield</summary>
            //public const byte MT_CAP_APP = 0x100;
            /// <name>TI.ZPI2.SYS_PING_RESPONSE.CAPABILITIES.MT_CAP_DEBUG</name>
            /// <summary>Capabilities bitfield</summary>
            public const byte MT_CAP_DEBUG = 0x80;
            /// <name>TI.ZPI2.SYS_PING_RESPONSE.CAPABILITIES.MT_CAP_MAC</name>
            /// <summary>Capabilities bitfield</summary>
            public const byte MT_CAP_MAC = 2;
            /// <name>TI.ZPI2.SYS_PING_RESPONSE.CAPABILITIES.MT_CAP_NWK</name>
            /// <summary>Capabilities bitfield</summary>
            public const byte MT_CAP_NWK = 4;
            /// <name>TI.ZPI2.SYS_PING_RESPONSE.CAPABILITIES.MT_CAP_SAPI</name>
            /// <summary>Capabilities bitfield</summary>
            public const byte MT_CAP_SAPI = 0x20;
            /// <name>TI.ZPI2.SYS_PING_RESPONSE.CAPABILITIES.MT_CAP_SYS</name>
            /// <summary>Capabilities bitfield</summary>
            public const byte MT_CAP_SYS = 1;
            /// <name>TI.ZPI2.SYS_PING_RESPONSE.CAPABILITIES.MT_CAP_UTIL</name>
            /// <summary>Capabilities bitfield</summary>
            public const byte MT_CAP_UTIL = 0x40;
            /// <name>TI.ZPI2.SYS_PING_RESPONSE.CAPABILITIES.MT_CAP_ZDO</name>
            /// <summary>Capabilities bitfield</summary>
            public const byte MT_CAP_ZDO = 0x10;
            /// <name>TI.ZPI2.SYS_PING_RESPONSE.CAPABILITIES.NONE</name>
            /// <summary>Capabilities bitfield</summary>
            public const byte NONE = 0;
        }
    }
}
