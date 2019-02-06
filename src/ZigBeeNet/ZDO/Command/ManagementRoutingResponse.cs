using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.ZCL;
using ZigBeeNet.ZCL.Protocol;
using ZigBeeNet.ZDO.Field;

namespace ZigBeeNet.ZDO.Command
{
    /**
     * Management Routing Response value object class.
     * 
     * The Mgmt_Rtg_rsp is generated in response to an Mgmt_Rtg_req. If this
     * management command is not supported, a status of NOT_SUPPORTED shall be
     * returned and all parameter fields after the Status field shall be omitted. Otherwise,
     * the Remote Device shall implement the following processing.
     */
    public class ManagementRoutingResponse : ZdoResponse
    {
        /**
         * RoutingTableEntries command message field.
         */
        public byte RoutingTableEntries { get; set; }

        /**
         * StartIndex command message field.
         */
        public byte StartIndex { get; set; }

        /**
         * RoutingTableList command message field.
         */
        public List<RoutingTable> RoutingTableList { get; set; }

        /**
         * Default constructor.
         */
        public ManagementRoutingResponse()
        {
            ClusterId = 0x8032;
        }

        public override void Serialize(ZclFieldSerializer serializer)
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

        public override void Deserialize(ZclFieldDeserializer deserializer)
        {
            base.Deserialize(deserializer);

            // Create lists
            RoutingTableList = new List<RoutingTable>();

            Status = (ZdoStatus)deserializer.Deserialize(ZclDataType.Get(DataType.ZDO_STATUS));

            if (Status != ZdoStatus.SUCCESS)
            {
                // Don't read the full response if we have an error
                return;
            }

            RoutingTableEntries = (byte)deserializer.Deserialize(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            StartIndex = (byte)deserializer.Deserialize(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            byte? routingTableListCount = (byte?)deserializer.Deserialize(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));

            if (routingTableListCount != null)
            {
                for (int cnt = 0; cnt < routingTableListCount; cnt++)
                {
                    RoutingTableList.Add((RoutingTable)deserializer.Deserialize(ZclDataType.Get(DataType.ROUTING_TABLE)));
                }
            }
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append("ManagementRoutingResponse [")
                   .Append(base.ToString())
                   .Append(", status=")
                   .Append(Status)
                   .Append(", routingTableEntries=")
                   .Append(RoutingTableEntries)
                   .Append(", startIndex=")
                   .Append(StartIndex)
                   .Append(", routingTableList=")
                   .Append(RoutingTableList)
                   .Append(']');

            return builder.ToString();
        }

    }
}
