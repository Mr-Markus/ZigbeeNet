using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.Serialization;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.ZCL.Field
{
    public class ExtensionFieldSet : IZclListItemField
    {
        /**
         * The cluster id.
         */
        public ushort ClusterId { get; private set; }

        /**
         * The data length.
         */
        // private int length;

        /**
         * The extension data.
         */
        public byte[] Data { get; private set; }


        public void Serialize(IZigBeeSerializer serializer)
        {
            serializer.AppendZigBeeType(ClusterId, DataType.UNSIGNED_16_BIT_INTEGER);
            serializer.AppendZigBeeType(Data, DataType.UNSIGNED_8_BIT_INTEGER_ARRAY);
        }

        public void Deserialize(IZigBeeDeserializer deserializer)
        {
            ClusterId = deserializer.ReadZigBeeType<ushort>(DataType.UNSIGNED_16_BIT_INTEGER);
            Data = deserializer.ReadZigBeeType<byte[]>(DataType.UNSIGNED_8_BIT_INTEGER_ARRAY);
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
