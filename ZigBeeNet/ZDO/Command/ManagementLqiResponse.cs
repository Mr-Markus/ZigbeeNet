using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.ZCL;
using ZigBeeNet.ZCL.Protocol;
using ZigBeeNet.ZDO.Field;

namespace ZigBeeNet.ZDO.Command
{
    /**
     * Management LQI Response value object class.
     * 
     * The Mgmt_Lqi_rsp is generated in response to an Mgmt_Lqi_req. If this
     * management command is not supported, a status of NOT_SUPPORTED shall be
     * returned and all parameter fields after the Status field shall be omitted. Otherwise,
     * the Remote Device shall implement the following processing.
     */
    public class ManagementLqiResponse : ZdoResponse
    {
        /**
         * NeighborTableEntries command message field.
         */
        public byte NeighborTableEntries { get; set; }

        /**
         * StartIndex command message field.
         */
        public byte StartIndex { get; set; }

        /**
         * NeighborTableList command message field.
         */
        public List<NeighborTable> NeighborTableList { get; set; }

        /**
         * Default constructor.
         */
        public ManagementLqiResponse()
        {
            ClusterId = 0x8031;
        }

        public override void Serialize(ZclFieldSerializer serializer)
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

        public override void Deserialize(ZclFieldDeserializer deserializer)
        {
            base.Deserialize(deserializer);

            // Create lists
            NeighborTableList = new List<NeighborTable>();

            Status = (ZdoStatus)deserializer.Deserialize(ZclDataType.Get(DataType.ZDO_STATUS));
            if (Status != ZdoStatus.SUCCESS)
            {
                // Don't read the full response if we have an error
                return;
            }
            NeighborTableEntries = (byte)deserializer.Deserialize(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            StartIndex = (byte)deserializer.Deserialize(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            byte? neighborTableListCount = (byte)deserializer.Deserialize(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));

            if (neighborTableListCount != null)
            {
                for (int cnt = 0; cnt < neighborTableListCount; cnt++)
                {
                    NeighborTableList.Add((NeighborTable)deserializer.Deserialize(ZclDataType.Get(DataType.NEIGHBOR_TABLE)));
                }
            }
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append("ManagementLqiResponse [")
                   .Append(base.ToString())
                   .Append(", status=")
                   .Append(Status)
                   .Append(", neighborTableEntries=")
                   .Append(NeighborTableEntries)
                   .Append(", startIndex=")
                   .Append(StartIndex)
                   .Append(", neighborTableList=")
                   .Append(NeighborTableList)
                   .Append(']');

            return builder.ToString();
        }

    }
}
