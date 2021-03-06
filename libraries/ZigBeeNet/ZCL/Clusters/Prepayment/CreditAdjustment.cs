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
    /// Credit Adjustment value object class.
    ///
    /// Cluster: Prepayment. Command ID 0x05 is sent TO the server.
    /// This command is a specific command used for the Prepayment cluster.
    ///
    /// FIXME: The CreditAdjustment command is sent to update the accounting base for the
    /// Prepayment meter.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class CreditAdjustment : ZclCommand
    {
        /// <summary>
        /// The cluster ID to which this command belongs.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0705;

        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x05;

        /// <summary>
        /// Issuer Event ID command message field.
        /// </summary>
        public uint IssuerEventId { get; set; }

        /// <summary>
        /// Start Time command message field.
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// Credit Adjustment Type command message field.
        /// </summary>
        public byte CreditAdjustmentType { get; set; }

        /// <summary>
        /// Credit Adjustment Value command message field.
        /// </summary>
        public uint CreditAdjustmentValue { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public CreditAdjustment()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(IssuerEventId, DataType.UNSIGNED_32_BIT_INTEGER);
            serializer.Serialize(StartTime, DataType.UTCTIME);
            serializer.Serialize(CreditAdjustmentType, DataType.ENUMERATION_8_BIT);
            serializer.Serialize(CreditAdjustmentValue, DataType.UNSIGNED_32_BIT_INTEGER);
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            IssuerEventId = deserializer.Deserialize<uint>(DataType.UNSIGNED_32_BIT_INTEGER);
            StartTime = deserializer.Deserialize<DateTime>(DataType.UTCTIME);
            CreditAdjustmentType = deserializer.Deserialize<byte>(DataType.ENUMERATION_8_BIT);
            CreditAdjustmentValue = deserializer.Deserialize<uint>(DataType.UNSIGNED_32_BIT_INTEGER);
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("CreditAdjustment [");
            builder.Append(base.ToString());
            builder.Append(", IssuerEventId=");
            builder.Append(IssuerEventId);
            builder.Append(", StartTime=");
            builder.Append(StartTime);
            builder.Append(", CreditAdjustmentType=");
            builder.Append(CreditAdjustmentType);
            builder.Append(", CreditAdjustmentValue=");
            builder.Append(CreditAdjustmentValue);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
