using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.Serialization;
using ZigBeeNet.ZCL.Clusters.Prepayment;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.ZCL.Clusters.Prepayment
{
    /// <summary>
    /// Top Up Payload structure implementation.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class TopUpPayload : IZigBeeSerializable
    {
        /// <summary>
        /// Top Up Code structure field.
        /// </summary>
        public ByteArray TopUpCode { get; set; }

        /// <summary>
        /// Top Up Amount structure field.
        /// </summary>
        public int TopUpAmount { get; set; }

        /// <summary>
        /// Top Up Time structure field.
        /// </summary>
        public DateTime TopUpTime { get; set; }



        public void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(TopUpCode, ZclDataType.Get(DataType.OCTET_STRING));
            serializer.Serialize(TopUpAmount, ZclDataType.Get(DataType.SIGNED_32_BIT_INTEGER));
            serializer.Serialize(TopUpTime, ZclDataType.Get(DataType.UTCTIME));
        }

        public void Deserialize(ZclFieldDeserializer deserializer)
        {
            TopUpCode = deserializer.Deserialize<ByteArray>(ZclDataType.Get(DataType.OCTET_STRING));
            TopUpAmount = deserializer.Deserialize<int>(ZclDataType.Get(DataType.SIGNED_32_BIT_INTEGER));
            TopUpTime = deserializer.Deserialize<DateTime>(ZclDataType.Get(DataType.UTCTIME));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("TopUpPayload [");
            builder.Append(base.ToString());
            builder.Append(", TopUpCode=");
            builder.Append(TopUpCode);
            builder.Append(", TopUpAmount=");
            builder.Append(TopUpAmount);
            builder.Append(", TopUpTime=");
            builder.Append(TopUpTime);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
