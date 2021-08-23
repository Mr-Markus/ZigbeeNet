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
    /// Debt Payload structure implementation.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class DebtPayload : IZigBeeSerializable
    {
        /// <summary>
        /// Collection Time structure field.
        /// </summary>
        public DateTime CollectionTime { get; set; }

        /// <summary>
        /// Amount Collected structure field.
        /// </summary>
        public uint AmountCollected { get; set; }

        /// <summary>
        /// Debt Type structure field.
        /// </summary>
        public byte DebtType { get; set; }

        /// <summary>
        /// Outstanding Debt structure field.
        /// </summary>
        public uint OutstandingDebt { get; set; }



        public void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(CollectionTime, DataType.UTCTIME);
            serializer.Serialize(AmountCollected, DataType.UNSIGNED_32_BIT_INTEGER);
            serializer.Serialize(DebtType, DataType.ENUMERATION_8_BIT);
            serializer.Serialize(OutstandingDebt, DataType.UNSIGNED_32_BIT_INTEGER);
        }

        public void Deserialize(ZclFieldDeserializer deserializer)
        {
            CollectionTime = deserializer.Deserialize<DateTime>(DataType.UTCTIME);
            AmountCollected = deserializer.Deserialize<uint>(DataType.UNSIGNED_32_BIT_INTEGER);
            DebtType = deserializer.Deserialize<byte>(DataType.ENUMERATION_8_BIT);
            OutstandingDebt = deserializer.Deserialize<uint>(DataType.UNSIGNED_32_BIT_INTEGER);
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("DebtPayload [");
            builder.Append(base.ToString());
            builder.Append(", CollectionTime=");
            builder.Append(CollectionTime);
            builder.Append(", AmountCollected=");
            builder.Append(AmountCollected);
            builder.Append(", DebtType=");
            builder.Append(DebtType);
            builder.Append(", OutstandingDebt=");
            builder.Append(OutstandingDebt);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
