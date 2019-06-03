using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.Hardware.TI.CC2531.Util;

namespace ZigBeeNet.Hardware.TI.CC2531.Packet.ZDO
{
    public class ZDO_MATCH_DESC_RSP : ZToolPacket
    {
        /// <name>TI.ZPI1.ZDO_MATCH_DESC_RSP.MatchCount</name>
        /// <summary>Number of active endpoints in the list</summary>
        public int MatchCount { get; private set; }
        /// <name>TI.ZPI1.ZDO_MATCH_DESC_RSP.MatchEndpointList</name>
        /// <summary>Array of active endpoints on this device</summary>
        public int[] MatchEndpointList { get; private set; }
        /// <name>TI.ZPI1.ZDO_MATCH_DESC_RSP.NWKAddrOfInterest</name>
        /// <summary>Device's short address that this response describes</summary>
        public ZToolAddress16 NWKAddrOfInterest { get; private set; }
        /// <name>TI.ZPI1.ZDO_MATCH_DESC_RSP.SrcAddress</name>
        /// <summary>the message's source network address</summary>
        public ZToolAddress16 SrcAddress { get; private set; }
        /// <name>TI.ZPI1.ZDO_MATCH_DESC_RSP.Status</name>
        /// <summary>this field indicates either SUCCESS or FAILURE</summary>
        public int Status { get; private set; }

        /// <name>TI.ZPI1.ZDO_MATCH_DESC_RSP</name>
        /// <summary>Constructor</summary>
        public ZDO_MATCH_DESC_RSP()
        {
            this.MatchEndpointList = new int[0xff];
        }

        public ZDO_MATCH_DESC_RSP(byte[] framedata)
        {
            this.SrcAddress = new ZToolAddress16(framedata[1], framedata[0]);
            this.Status = framedata[2];
            this.NWKAddrOfInterest = new ZToolAddress16(framedata[4], framedata[3]);
            this.MatchCount = framedata[5];
            this.MatchEndpointList = new int[this.MatchCount];
            for (int i = 0; i < this.MatchEndpointList.Length; i++)
            {
                this.MatchEndpointList[i] = framedata[i + 6];
            }
            BuildPacket(new DoubleByte((ushort)ZToolCMD.ZDO_MATCH_DESC_RSP), framedata);
        }

        public override string ToString()
        {
            return "ZDO_MATCH_DESC_RSP{" + "MatchCount=" + MatchCount + ", MatchEndpointList="
                    + MatchEndpointList.ToString() + ", NWKAddrOfInterest=" + NWKAddrOfInterest + ", SrcAddress="
                    + SrcAddress + ", Status=" + Status + '}';
        }
    }
}
