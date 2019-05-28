using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.Serialization;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.ZCL.Field
{
    public class ExtensionFieldSet : IZclListItemField
    {
        /// <summary>
         /// The cluster id.
         /// </summary>
        public ushort ClusterId { get; private set; }

        /// <summary>
         /// The data length.
         /// </summary>
        // private int length;

        /// <summary>
         /// The extension data.
         /// </summary>
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
