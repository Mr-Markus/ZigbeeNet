using System;
using System.Collections.Generic;
using System.Text;
using ZigbeeNet.Serialization;
using ZigbeeNet.ZCL.Protocol;

namespace ZigbeeNet.ZCL.Fileld
{
    public class ExtensionFieldSet : IZclListItemField
    {
        /**
         * The cluster id.
         */
        public int ClusterId { get; private set; }

        /**
         * The data length.
         */
        // private int length;

        /**
         * The extension data.
         */
        public int[] Data { get; private set; }


        public void Serialize(IZigBeeSerializer serializer)
        {
            serializer.AppendZigBeeType(ClusterId, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            serializer.AppendZigBeeType(Data, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER_ARRAY);
        }

        public void Deserialize(IZigBeeDeserializer deserializer)
        {
            ClusterId = (int)deserializer.ReadZigBeeType(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER);
            Data = (int[])deserializer.ReadZigBeeType(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER_ARRAY);
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("ExtensionFieldSet [clusterId=")
                   .Append(ClusterId)
                   .Append(", data=");

            foreach (int value in Data)
            {
                builder.Append(string.Format("{0}2X ", value));
            }

            builder.Append(']');

            return builder.ToString();
        }
    }
}
