using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.Hardware.TI.CC2531.Util;

namespace ZigBeeNet.Hardware.TI.CC2531.Packet.ZDO
{
    public class ZDO_END_DEVICE_ANNCE : ZToolPacket //// implements IREQUEST,IZDO /// </summary>
    {
        /// <name>TI.ZPI1.ZDO_END_DEVICE_ANNCE.Capabilities</name>
        /// <summary>MAC capabilities</summary>
        public int Capabilities { get; private set; }
        /// <name>TI.ZPI1.ZDO_END_DEVICE_ANNCE.DevAddr</name>
        /// <summary>Device network address.</summary>
        public ZToolAddress16 nwkAddr { get; private set; }
        /// <name>TI.ZPI1.ZDO_END_DEVICE_ANNCE.DeviceAddress</name>
        /// <summary>The 64 bit IEEE Address of the device you want to announce</summary>
        public ZToolAddress64 IEEEAddress { get; private set; }

        /// <name>TI.ZPI1.ZDO_END_DEVICE_ANNCE</name>
        /// <summary>Constructor</summary>
        public ZDO_END_DEVICE_ANNCE()
        {
        }

        public ZDO_END_DEVICE_ANNCE(ZToolAddress16 num1, ZToolAddress64 num2, int capability_info1)
        {
            this.nwkAddr = num1;
            this.IEEEAddress = num2;
            this.Capabilities = capability_info1;

            byte[] framedata = new byte[11];
            framedata[0] = this.nwkAddr.Lsb;
            framedata[1] = this.nwkAddr.Msb;
            byte[] bytes = this.IEEEAddress.Address;
            for (int i = 0; i < 8; i++)
            {
                framedata[i + 2] = bytes[7 - i];
            }
            BuildPacket(new DoubleByte((ushort)ZToolCMD.ZDO_END_DEVICE_ANNCE), framedata);
        }

        /// <name>TI.ZPI1.ZDO_END_DEVICE_ANNCE.CAPABILITY_INFO</name>
        /// <summary>Capability Information bitfield</summary>
        public class CAPABILITY_INFO
        {
            /// <name>TI.ZPI1.ZDO_END_DEVICE_ANNCE.CAPABILITY_INFO.ALTER_PAN_COORD</name>
            /// <summary>Capability Information bitfield</summary>
            public readonly int ALTER_PAN_COORD = 1;
            /// <name>TI.ZPI1.ZDO_END_DEVICE_ANNCE.CAPABILITY_INFO.DEVICE_TYPE</name>
            /// <summary>Capability Information bitfield</summary>
            public readonly int DEVICE_TYPE = 2;
            /// <name>TI.ZPI1.ZDO_END_DEVICE_ANNCE.CAPABILITY_INFO.NONE</name>
            /// <summary>Capability Information bitfield</summary>
            public readonly int NONE = 0;
            /// <name>TI.ZPI1.ZDO_END_DEVICE_ANNCE.CAPABILITY_INFO.POWER_SOURCE</name>
            /// <summary>Capability Information bitfield</summary>
            public readonly int POWER_SOURCE = 4;
            /// <name>TI.ZPI1.ZDO_END_DEVICE_ANNCE.CAPABILITY_INFO.RECEIVER_ON_WHEN_IDLE</name>
            /// <summary>Capability Information bitfield</summary>
            public readonly int RECEIVER_ON_WHEN_IDLE = 8;
            /// <name>TI.ZPI1.ZDO_END_DEVICE_ANNCE.CAPABILITY_INFO.SECURITY_CAPABILITY</name>
            /// <summary>Capability Information bitfield</summary>
            public readonly int SECURITY_CAPABILITY = 0x40;
        }

    }
}
