using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.ZCL;
using ZigBeeNet.ZCL.Protocol;
using ZigBeeNet.ZDO.Field;


namespace ZigBeeNet.ZDO.Command
{
    /// <summary>
    /// Management LQI Response value object class.
    ///
    ///
    /// The Mgmt_Lqi_rsp is generated in response to an Mgmt_Lqi_req. If this management
    /// command is not supported, a status of NOT_SUPPORTED shall be returned and all parameter
    /// fields after the Status field shall be omitted. Otherwise, the Remote Device shall
    /// implement the following processing.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class ManagementLqiResponse : ZdoResponse
    {
        /// <summary>
        /// The ZDO cluster ID.
        /// </summary>
        public const ushort CLUSTER_ID = 0x8031;

        /// <summary>
        /// Neighbor Table Entries command message field.
        /// </summary>
        public byte NeighborTableEntries { get; set; }

        /// <summary>
        /// Start Index command message field.
        /// </summary>
        public byte StartIndex { get; set; }

        /// <summary>
        /// Neighbor Table List command message field.
        /// </summary>
        public List<NeighborTable> NeighborTableList { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ManagementLqiResponse()
        {
            ClusterId = CLUSTER_ID;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            base.Serialize(serializer);

            serializer.Serialize(Status, ZclDataType.Get(DataType.ZDO_STATUS));
            serializer.Serialize(NeighborTableEntries, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            serializer.Serialize(StartIndex, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            serializer.Serialize(NeighborTableList.Count, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            for (int cnt = 0; cnt < NeighborTableList.Count; cnt++)
            {
                serializer.Serialize(NeighborTableList[cnt], ZclDataType.Get(DataType.NEIGHBOR_TABLE));
            }
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            base.Deserialize(deserializer);

            // Create lists
            NeighborTableList = new List<NeighborTable>();

            Status = deserializer.Deserialize<ZdoStatus>(ZclDataType.Get(DataType.ZDO_STATUS));
            if (Status != ZdoStatus.SUCCESS)
            {
                // Don't read the full response if we have an error
                return;
            }
            NeighborTableEntries = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            StartIndex = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            byte? neighborTableListCount = (byte?) deserializer.Deserialize(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            if (neighborTableListCount != null)
            {
                for (int cnt = 0; cnt < neighborTableListCount; cnt++)
                {
                    NeighborTableList.Add((NeighborTable) deserializer.Deserialize(ZclDataType.Get(DataType.NEIGHBOR_TABLE)));
                }
            }
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("ManagementLqiResponse [");
            builder.Append(base.ToString());
            builder.Append(", Status=");
            builder.Append(Status);
            builder.Append(", NeighborTableEntries=");
            builder.Append(NeighborTableEntries);
            builder.Append(", StartIndex=");
            builder.Append(StartIndex);
            builder.Append(", NeighborTableList=");
            builder.Append(string.Join(", ", NeighborTableList));
            builder.Append(']');

            return builder.ToString();
        }
    }
}
