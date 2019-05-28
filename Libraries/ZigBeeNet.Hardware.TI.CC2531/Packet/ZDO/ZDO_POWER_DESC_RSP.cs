using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.Hardware.TI.CC2531.Util;
using ZigBeeNet.ZDO;

namespace ZigBeeNet.Hardware.TI.CC2531.Packet.ZDO
{
    /// <summary>
    /// Processes the response of the Power Descriptor. Only a single Power Descriptor
    /// is available per node.
    /// 
    /// The node power descriptor gives a dynamic indication of the power status of
    /// the node and is mandatory for each node. There shall be only one node
    /// power descriptor in a node.
    /// </summary>
    public class ZDO_POWER_DESC_RSP : ZToolPacket
    {
        /// <name>TI.ZPI1.ZDO_NODE_DESC_RSP.NWKAddrOfInterest</name>
        /// <summary>Device's short address of this Node descriptor</summary>
        public ZToolAddress16 NwkAddr { get; private set; }
        /// <name>TI.ZPI1.ZDO_NODE_DESC_RSP.SrcAddress</name>
        /// <summary>the message's source network address.</summary>
        public ZToolAddress16 SrcAddress { get; private set; }
        /// <name>TI.ZPI1.ZDO_NODE_DESC_RSP.Status</name>
        /// <summary>this field indicates either SUCCESS or FAILURE.</summary>
        public ZdoStatus Status { get; private set; }

        // TODO: Add doc!
        private int CurrentMode;
        private int AvailableSources;
        private int CurrentSource;
        private int CurrentLevel;

        /// <name>TI.ZPI1.ZDO_NODE_DESC_RSP</name>
        /// <summary>Constructor</summary>
        public ZDO_POWER_DESC_RSP()
        {
        }

        public ZDO_POWER_DESC_RSP(byte[] framedata)
        {
            this.SrcAddress = new ZToolAddress16(framedata[1], framedata[0]);
            this.Status = (ZdoStatus)framedata[2];

            if (Status == ZdoStatus.SUCCESS)
            {
                this.NwkAddr = new ZToolAddress16(framedata[4], framedata[3]);
                this.CurrentMode = (framedata[5] & (0x0F));
                this.AvailableSources = (framedata[5] & (0xF0)) >> 4;
                this.CurrentSource = (framedata[6] & (0x0F));
                this.CurrentLevel = (framedata[6] & (0xF0)) >> 4;
            }
            BuildPacket(new DoubleByte((ushort)ZToolCMD.ZDO_POWER_DESC_RSP), framedata);
        }

        public override string ToString()
        {
            return "ZDO_NODE_DESC_RSP{" + "nwkAddr=" + NwkAddr + ", SrcAddress=" + SrcAddress + ", Status="
                    + Status + ", CurrentMode=" + CurrentMode + ", AvailableSources="
                    + AvailableSources + ", CurrentSource=" + CurrentSource + ", CurrentLevel=" + CurrentLevel + "}";
        }
    }
}
