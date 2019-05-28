using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.Hardware.TI.CC2531.Util;

namespace ZigBeeNet.Hardware.TI.CC2531.Packet.ZDO
{
    public class ZDO_USER_DESC_RSP : ZToolPacket
    {

        /// <name>TI.ZPI1.ZDO_USER_DESC_RSP.DescLen</name>
        /// <summary>Length, in bytes, of the user descriptor</summary>
        public int DescLen { get; private set; }
        /// <name>TI.ZPI1.ZDO_USER_DESC_RSP.Descriptor</name>
        /// <summary>Dynamic array, User descriptor array (can be up to 15 bytes).</summary>
        public byte[] Descriptor { get; private set; }
        /// <name>TI.ZPI1.ZDO_USER_DESC_RSP.NWKAddrOfInterest</name>
        /// <summary>Device's short address that this response describes.</summary>
        public ZToolAddress16 nwkAddr { get; private set; }
        /// <name>TI.ZPI1.ZDO_USER_DESC_RSP.SrcAddress</name>
        /// <summary>the message's source network address.</summary>
        public ZToolAddress16 SrcAddress { get; private set; }
        /// <name>TI.ZPI1.ZDO_USER_DESC_RSP.Status</name>
        /// <summary>this field indicates either SUCCESS or FAILURE.</summary>
        public int Status { get; private set; }

        /// <name>TI.ZPI1.ZDO_USER_DESC_RSP</name>
        /// <summary>Constructor</summary>
        public ZDO_USER_DESC_RSP()
        {
            this.Descriptor = new byte[0xff];
        }

        public ZDO_USER_DESC_RSP(byte[] framedata)
        {
            this.SrcAddress = new ZToolAddress16(framedata[1], framedata[0]);
            this.Status = framedata[2];
            if (framedata.Length > 3)
            {
                this.nwkAddr = new ZToolAddress16(framedata[4], framedata[3]);
                this.DescLen = framedata[5];
                this.Descriptor = new byte[this.DescLen];
                for (int i = 0; i < this.Descriptor.Length; i++)
                {
                    this.Descriptor[i] = framedata[i + 6];
                }
            }
            else
            {
                this.nwkAddr = new ZToolAddress16();
                this.DescLen = 0;
                this.Descriptor = new byte[0];
            }
            BuildPacket(new DoubleByte((ushort)ZToolCMD.ZDO_USER_DESC_RSP), framedata);
        }

        public override string ToString()
        {
            return "ZDO_USER_DESC_RSP{" + "DescLen=" + DescLen + ", Descriptor=" + Descriptor
                    + ", nwkAddr=" + nwkAddr + ", SrcAddress=" + SrcAddress + ", Status=" + Status
                    + '}';
        }
    }
}
