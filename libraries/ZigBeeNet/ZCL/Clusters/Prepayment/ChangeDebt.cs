using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.Security;
using ZigBeeNet.ZCL.Clusters.Prepayment;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Protocol;


namespace ZigBeeNet.ZCL.Clusters.Prepayment
{
    /// <summary>
    /// Change Debt value object class.
    ///
    /// Cluster: Prepayment. Command ID 0x02 is sent TO the server.
    /// This command is a specific command used for the Prepayment cluster.
    ///
    /// FIXME: The ChangeDebt command is send to the Metering Device to change the fuel or Non
    /// fuel debt values.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class ChangeDebt : ZclCommand
    {
        /// <summary>
        /// The cluster ID to which this command belongs.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0705;

        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x02;

        /// <summary>
        /// Issuer Event ID command message field.
        /// </summary>
        public uint IssuerEventId { get; set; }

        /// <summary>
        /// Debt Label command message field.
        /// </summary>
        public ByteArray DebtLabel { get; set; }

        /// <summary>
        /// Debt Amount command message field.
        /// </summary>
        public uint DebtAmount { get; set; }

        /// <summary>
        /// Debt Recovery Method command message field.
        /// </summary>
        public byte DebtRecoveryMethod { get; set; }

        /// <summary>
        /// Debt Amount Type command message field.
        /// </summary>
        public byte DebtAmountType { get; set; }

        /// <summary>
        /// Debt Recovery Start Time command message field.
        /// </summary>
        public DateTime DebtRecoveryStartTime { get; set; }

        /// <summary>
        /// Debt Recovery Collection Time command message field.
        /// </summary>
        public ushort DebtRecoveryCollectionTime { get; set; }

        /// <summary>
        /// Debt Recovery Frequency command message field.
        /// </summary>
        public byte DebtRecoveryFrequency { get; set; }

        /// <summary>
        /// Debt Recovery Amount command message field.
        /// </summary>
        public uint DebtRecoveryAmount { get; set; }

        /// <summary>
        /// Debt Recovery Balance Percentage command message field.
        /// </summary>
        public ushort DebtRecoveryBalancePercentage { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ChangeDebt()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(IssuerEventId, ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER));
            serializer.Serialize(DebtLabel, ZclDataType.Get(DataType.OCTET_STRING));
            serializer.Serialize(DebtAmount, ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER));
            serializer.Serialize(DebtRecoveryMethod, ZclDataType.Get(DataType.ENUMERATION_8_BIT));
            serializer.Serialize(DebtAmountType, ZclDataType.Get(DataType.ENUMERATION_8_BIT));
            serializer.Serialize(DebtRecoveryStartTime, ZclDataType.Get(DataType.UTCTIME));
            serializer.Serialize(DebtRecoveryCollectionTime, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            serializer.Serialize(DebtRecoveryFrequency, ZclDataType.Get(DataType.ENUMERATION_8_BIT));
            serializer.Serialize(DebtRecoveryAmount, ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER));
            serializer.Serialize(DebtRecoveryBalancePercentage, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            IssuerEventId = deserializer.Deserialize<uint>(ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER));
            DebtLabel = deserializer.Deserialize<ByteArray>(ZclDataType.Get(DataType.OCTET_STRING));
            DebtAmount = deserializer.Deserialize<uint>(ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER));
            DebtRecoveryMethod = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.ENUMERATION_8_BIT));
            DebtAmountType = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.ENUMERATION_8_BIT));
            DebtRecoveryStartTime = deserializer.Deserialize<DateTime>(ZclDataType.Get(DataType.UTCTIME));
            DebtRecoveryCollectionTime = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            DebtRecoveryFrequency = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.ENUMERATION_8_BIT));
            DebtRecoveryAmount = deserializer.Deserialize<uint>(ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER));
            DebtRecoveryBalancePercentage = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("ChangeDebt [");
            builder.Append(base.ToString());
            builder.Append(", IssuerEventId=");
            builder.Append(IssuerEventId);
            builder.Append(", DebtLabel=");
            builder.Append(DebtLabel);
            builder.Append(", DebtAmount=");
            builder.Append(DebtAmount);
            builder.Append(", DebtRecoveryMethod=");
            builder.Append(DebtRecoveryMethod);
            builder.Append(", DebtAmountType=");
            builder.Append(DebtAmountType);
            builder.Append(", DebtRecoveryStartTime=");
            builder.Append(DebtRecoveryStartTime);
            builder.Append(", DebtRecoveryCollectionTime=");
            builder.Append(DebtRecoveryCollectionTime);
            builder.Append(", DebtRecoveryFrequency=");
            builder.Append(DebtRecoveryFrequency);
            builder.Append(", DebtRecoveryAmount=");
            builder.Append(DebtRecoveryAmount);
            builder.Append(", DebtRecoveryBalancePercentage=");
            builder.Append(DebtRecoveryBalancePercentage);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
