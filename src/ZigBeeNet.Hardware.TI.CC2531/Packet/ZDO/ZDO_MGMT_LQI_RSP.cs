using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.Hardware.TI.CC2531.Util;

namespace ZigBeeNet.Hardware.TI.CC2531.Packet.ZDO
{
    /// <summary>
    /// Processes the LQI Response.
    /// 
    /// The MGMT_Lqi_RSP is generated in response to an MGMT_LQI_REQ. If this
    /// management command is not supported, a status of NOT_SUPPORTED will be
    /// returned and all parameter fields after the Status field will be omitted.
    /// </summary>
    public class ZDO_MGMT_LQI_RSP : ZToolPacket
    {
        /// <name>TI.ZPI1.ZDO_MGMT_LQI_RSP.NeighborLQICount</name>
        /// <summary>Number of entries in this response.</summary>
        public int NeighborLQICount { get; private set; }
        /// <name>TI.ZPI1.ZDO_MGMT_LQI_RSP.NeighborLQIEntries</name>
        /// <summary>Total number of entries available in the device.</summary>
        public int NeighborLQIEntries { get; private set; }
        /// <name>TI.ZPI1.ZDO_MGMT_LQI_RSP.NeighborLqiList</name>
        /// <summary>Dynamic array, Number of entries in this response.</summary>
        public NeighborLqiListItemClass[] NeighborLqiList { get; private set; }
        // private NeighborLqiListItemClass NeighborLqiListItemClassDummyObj;
        /// <name>TI.ZPI1.ZDO_MGMT_LQI_RSP.SrcAddress</name>
        /// <summary>Source address of the message</summary>
        public ZToolAddress16 SrcAddress { get; private set; }
        /// <name>TI.ZPI1.ZDO_MGMT_LQI_RSP.StartIndex</name>
        /// <summary>Where in the total number of entries this response starts.</summary>
        public int StartIndex { get; private set; }
        /// <name>TI.ZPI1.ZDO_MGMT_LQI_RSP.Status</name>
        /// <summary>this field indicates either SUCCESS or FAILURE.</summary>
        public int Status { get; private set; }

        /// <name>TI.ZPI1.ZDO_MGMT_LQI_RSP</name>
        /// <summary>Constructor</summary>
        public ZDO_MGMT_LQI_RSP()
        {
            NeighborLqiList = new NeighborLqiListItemClass[] { };
        }

        public ZDO_MGMT_LQI_RSP(byte[] framedata)
        {
            SrcAddress = new ZToolAddress16(framedata[1], framedata[0]);
            Status = framedata[2];
            if (framedata.Length > 3)
            {
                NeighborLQIEntries = framedata[3];
                StartIndex = framedata[4];
                NeighborLQICount = framedata[5];
                NeighborLqiList = new NeighborLqiListItemClass[framedata[5]];

                int NOpt1;
                int NOpt2;

                int k = 0;
                byte[] bytes = new byte[8];
                for (int z = 0; z < this.NeighborLqiList.Length; z++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        bytes[7 - j] = (byte)framedata[6 + k + j];/// MSB><LSB?
                    }
                    long panId = ByteUtils.ConvertMultiByteToLong(bytes);
                    for (int j = 0; j < 8; j++)
                    {
                        bytes[7 - j] = (byte)framedata[14 + k + j];/// MSB><LSB?
                    }
                    ZToolAddress64 ieeeAddr = new ZToolAddress64(bytes);
                    ZToolAddress16 nwkAddr = new ZToolAddress16(framedata[23 + k], framedata[22 + k]);/// MSB><LSB?
                    NOpt1 = framedata[24 + k];
                    NOpt2 = framedata[25 + k];
                    byte lqi = framedata[26 + k];
                    byte depth = framedata[27 + k];
                    NeighborLqiList[z] = new NeighborLqiListItemClass(panId, ieeeAddr, nwkAddr, NOpt1, NOpt2, lqi, depth);
                    k += 22;
                }
            }
            BuildPacket(new DoubleByte((ushort)ZToolCMD.ZDO_MGMT_LQI_RSP), framedata);
        }

        /// <name>TI.ZPI1.ZDO_MGMT_LQI_RSP.NeighborLqiListItemClass</name>
        /// <summary>Contains information in a single item of a network list</summary>
        public class NeighborLqiListItemClass
        {

            public int depth;
            public ZToolAddress64 extendedAddress;
            public long extendedPanID;
            public ZToolAddress16 networkAddress;
            public int Reserved_Relationship_RxOnWhenIdle_DeviceType;
            public int Reserved_PermitJoining;
            public int rxLQI;

            public NeighborLqiListItemClass()
            {
            }

            public NeighborLqiListItemClass(long extendedPanID, ZToolAddress64 extendedAddress,
                    ZToolAddress16 networkAddress, int flags, int joining, int depth, int rxLqi)
            {
                this.extendedPanID = extendedPanID;
                this.extendedAddress = extendedAddress;
                this.networkAddress = networkAddress;
                this.Reserved_Relationship_RxOnWhenIdle_DeviceType = flags;
                this.Reserved_PermitJoining = joining;
                this.depth = depth;
                this.rxLQI = rxLqi;
            }

            public override string ToString()
            {
                return "NeighborLqi [depth=" + depth + ", networkAddress=" + networkAddress + ", joining="
                        + Reserved_PermitJoining + ", LQI=" + rxLQI + "]";
            }

        }

        public override string ToString()
        {
            return "ZDO_MGMT_LQI_RSP{" + "NeighborLQICount=" + NeighborLQICount + ", NeighborLQIEntries="
                    + NeighborLQIEntries + ", NeighborLqiList=" + NeighborLqiList + ", SrcAddress="
                    + SrcAddress + ", StartIndex=" + StartIndex + ", Status=" + Status + '}';
        }
    }
}
