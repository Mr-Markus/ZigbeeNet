using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.Hardware.TI.CC2531.Util;

namespace ZigBeeNet.Hardware.TI.CC2531.Packet.ZDO
{
    public class ZDO_USER_DESC_SET : ZToolPacket
    {
        /// <name>TI.ZPI1.ZDO_USER_DESC_SET.DescLen</name>
        /// <summary>Length, in bytes, of the user descriptor.</summary>
        public byte DescLen { get; set; }
        /// <name>TI.ZPI1.ZDO_USER_DESC_SET.Descriptor</name>
        /// <summary>User descriptor array (can be up to 15 bytes).</summary>
        public byte[] Descriptor { get; set; }
        /// <name>TI.ZPI1.ZDO_USER_DESC_SET.DstAddr</name>
        /// <summary>Destination network address.</summary>
        public ZToolAddress16 DstAddr { get; set; }
        /// <name>TI.ZPI1.ZDO_USER_DESC_SET.NWKAddrOfInterest</name>
        /// <summary>NWK address for the request.</summary>
        public ZToolAddress16 NwkAddr { get; set; }

        /// <name>TI.ZPI1.ZDO_USER_DESC_SET</name>
        /// <summary>Constructor</summary>
        public ZDO_USER_DESC_SET()
        {
            this.Descriptor = new byte[15];
        }

        public ZDO_USER_DESC_SET(ZToolAddress16 num1, ZToolAddress16 num2, byte num3, byte[] buffer1)
        {
            this.DstAddr = num1;
            this.NwkAddr = num2;
            this.DescLen = num3;
            this.Descriptor = new byte[buffer1.Length];
            this.Descriptor = buffer1;
            ////
            /// if (buffer1.Length > 15)
            /// {
            /// throw new Exception("Error creating object.");
            /// }
            /// this.Descriptor = new byte[15];
            /// Array.Copy(buffer1, this.Descriptor, buffer1.Length);
            /// </summary>

            byte[] framedata = new byte[5 + this.Descriptor.Length];
            framedata[0] = this.DstAddr.Lsb;
            framedata[1] = this.DstAddr.Msb;
            framedata[2] = this.NwkAddr.Lsb;
            framedata[3] = this.NwkAddr.Msb;
            framedata[4] = this.DescLen;
            for (int i = 0; i < this.Descriptor.Length; i++)
            {
                framedata[i + 5] = this.Descriptor[i];
            }
            BuildPacket(new DoubleByte((ushort)ZToolCMD.ZDO_USER_DESC_SET), framedata);
        }
    }
}
