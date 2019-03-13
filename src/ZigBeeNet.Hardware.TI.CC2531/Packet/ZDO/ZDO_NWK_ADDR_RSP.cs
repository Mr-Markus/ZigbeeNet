using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.Hardware.TI.CC2531.Util;

namespace ZigBeeNet.Hardware.TI.CC2531.Packet.ZDO
{
    public class ZDO_NWK_ADDR_RSP : ZToolPacket //// implements IRESPONSE_CALLBACK,IZDO /// </summary>
    {
        /// <name>TI.ZPI1.ZDO_NWK_ADDR_RSP.AssocDevList</name>
        /// <summary>Dynamic array, array of 16 bit short addresses - list of network address for associated devices. This
        /// list can be a partial list if the entire list doesn't fit into a packet. If it is a partial list, the starting
        /// index is StartIndex.</summary>
        public ZToolAddress16[] AssocDevList { get; private set; }
        /// <name>TI.ZPI1.ZDO_NWK_ADDR_RSP.IEEEAddr</name>
        /// <summary>64 bit IEEE address of source device</summary>
        public ZToolAddress64 IEEEAddr { get; private set; }
        /// <name>TI.ZPI1.ZDO_NWK_ADDR_RSP.NumAssocDev</name>
        /// <summary>number of associated devices</summary>
        public int NumAssocDev { get; private set; }
        /// <name>TI.ZPI1.ZDO_NWK_ADDR_RSP.nwkAddr</name>
        /// <summary>short network address of responding device</summary>
        public ZToolAddress16 NwkAddr { get; private set; }
        /// <name>TI.ZPI1.ZDO_NWK_ADDR_RSP.SrcAddress</name>
        /// <summary>Source address, size is dependent on SrcAddrMode</summary>
        public ZToolAddress64 SrcAddress { get; private set; }
        /// <name>TI.ZPI1.ZDO_NWK_ADDR_RSP.StartIndex</name>
        /// <summary>Starting index into the list of associated devices for this report.</summary>
        public int StartIndex { get; private set; }
        /// <name>TI.ZPI1.ZDO_NWK_ADDR_RSP.Status</name>
        /// <summary>this field indicates either SUCCESS or FAILURE</summary>
        public int Status { get; private set; }

        /// <name>TI.ZPI1.ZDO_NWK_ADDR_RSP</name>
        /// <summary>Constructor</summary>
        public ZDO_NWK_ADDR_RSP()
        {
            this.AssocDevList = new ZToolAddress16[0xff];
        }

        public ZDO_NWK_ADDR_RSP(byte[] framedata)
        {
            /// WARNING: variable length.
            /// resulting SrcAddress is 8 bytes but serialized is either 2 or 8 depending on Mode
            this.Status = framedata[0];
            byte[] bytes = new byte[8];
            for (int i = 0; i < 8; i++)
            {
                bytes[i] = framedata[8 - i];
            }
            this.IEEEAddr = new ZToolAddress64(bytes);
            this.NwkAddr = new ZToolAddress16(framedata[10], framedata[9]);
            this.StartIndex = framedata[11];
            this.NumAssocDev = framedata[12];
            this.AssocDevList = new ZToolAddress16[this.NumAssocDev];
            for (int i = 0; i < this.AssocDevList.Length; i++)
            {
                this.AssocDevList[i] = new ZToolAddress16(framedata[14 + (i * 2)], framedata[13 + (i * 2)]);
            }
            BuildPacket(new DoubleByte((ushort)ZToolCMD.ZDO_NWK_ADDR_RSP), framedata);
        }

        public override string ToString()
        {
            return "ZDO_NWK_ADDR_RSP{" + "AssocDevList=" + AssocDevList.ToString() + ", IEEEAddr=" + IEEEAddr
                    + ", NumAssocDev=" + NumAssocDev + ", nwkAddr=" + NwkAddr + ", SrcAddress=" + SrcAddress
                    + ", StartIndex=" + StartIndex + ", Status=" + Status + '}';
        }
    }
}
