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
    /// Management Routing Response value object class.
    ///
    ///
    /// The Mgmt_Rtg_rsp is generated in response to an Mgmt_Rtg_req. If this management
    /// command is not supported, a status of NOT_SUPPORTED shall be returned and all parameter
    /// fields after the Status field shall be omitted. Otherwise, the Remote Device shall
    /// implement the following processing.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class ManagementRoutingResponse : ZdoResponse
    {
        /// <summary>
        /// The ZDO cluster ID.
        /// </summary>
        public const ushort CLUSTER_ID = 0x8032;

        /// <summary>
        /// Routing Table Entries command message field.
        /// </summary>
        public byte RoutingTableEntries { get; set; }

        /// <summary>
        /// Start Index command message field.
        /// </summary>
        public byte StartIndex { get; set; }

        /// <summary>
        /// Routing Table List command message field.
        /// </summary>
        public List<RoutingTable> RoutingTableList { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ManagementRoutingResponse()
        {
            ClusterId = CLUSTER_ID;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            base.Serialize(serializer);

            serializer.Serialize(Status, ZclDataType.Get(DataType.ZDO_STATUS));
            serializer.Serialize(RoutingTableEntries, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            serializer.Serialize(StartIndex, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            serializer.Serialize(RoutingTableList.Count, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            for (int cnt = 0; cnt < RoutingTableList.Count; cnt++)
            {
                serializer.Serialize(RoutingTableList[cnt], ZclDataType.Get(DataType.ROUTING_TABLE));
            }
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            base.Deserialize(deserializer);

            // Create lists
            RoutingTableList = new List<RoutingTable>();

            Status = deserializer.Deserialize<ZdoStatus>(ZclDataType.Get(DataType.ZDO_STATUS));
            if (Status != ZdoStatus.SUCCESS)
            {
                // Don't read the full response if we have an error
                return;
            }
            RoutingTableEntries = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            StartIndex = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            byte? routingTableListCount = (byte?) deserializer.Deserialize(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            if (routingTableListCount != null)
            {
                for (int cnt = 0; cnt < routingTableListCount; cnt++)
                {
                    RoutingTableList.Add((RoutingTable) deserializer.Deserialize(ZclDataType.Get(DataType.ROUTING_TABLE)));
                }
            }
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("ManagementRoutingResponse [");
            builder.Append(base.ToString());
            builder.Append(", Status=");
            builder.Append(Status);
            builder.Append(", RoutingTableEntries=");
            builder.Append(RoutingTableEntries);
            builder.Append(", StartIndex=");
            builder.Append(StartIndex);
            builder.Append(", RoutingTableList=");
            builder.Append(string.Join(", ", RoutingTableList));
            builder.Append(']');

            return builder.ToString();
        }
    }
}
