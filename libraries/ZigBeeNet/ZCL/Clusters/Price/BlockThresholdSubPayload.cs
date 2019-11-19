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
    /// Block Threshold Sub Payload structure implementation.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class BlockThresholdSubPayload : IZigBeeSerializable
    {
        /// <summary>
        /// Tier Number Of Block Thresholds structure field.
        /// </summary>
        public byte TierNumberOfBlockThresholds { get; set; }

        /// <summary>
        /// Block Threshold structure field.
        /// </summary>
        public ulong BlockThreshold { get; set; }



        public void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(TierNumberOfBlockThresholds, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            serializer.Serialize(BlockThreshold, ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER));
        }

        public void Deserialize(ZclFieldDeserializer deserializer)
        {
            TierNumberOfBlockThresholds = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            BlockThreshold = deserializer.Deserialize<ulong>(ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("BlockThresholdSubPayload [");
            builder.Append(base.ToString());
            builder.Append(", TierNumberOfBlockThresholds=");
            builder.Append(TierNumberOfBlockThresholds);
            builder.Append(", BlockThreshold=");
            builder.Append(BlockThreshold);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
