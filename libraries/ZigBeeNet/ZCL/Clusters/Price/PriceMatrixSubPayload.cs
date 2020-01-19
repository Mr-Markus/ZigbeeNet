using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.Serialization;
using ZigBeeNet.ZCL.Clusters.Price;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.ZCL.Clusters.Price
{
    /// <summary>
    /// Price Matrix Sub Payload structure implementation.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class PriceMatrixSubPayload : IZigBeeSerializable
    {
        /// <summary>
        /// Tier Block ID structure field.
        /// </summary>
        public byte TierBlockId { get; set; }

        /// <summary>
        /// Price structure field.
        /// </summary>
        public uint Price { get; set; }



        public void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(TierBlockId, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            serializer.Serialize(Price, ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER));
        }

        public void Deserialize(ZclFieldDeserializer deserializer)
        {
            TierBlockId = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            Price = deserializer.Deserialize<uint>(ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("PriceMatrixSubPayload [");
            builder.Append(base.ToString());
            builder.Append(", TierBlockId=");
            builder.Append(TierBlockId);
            builder.Append(", Price=");
            builder.Append(Price);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
