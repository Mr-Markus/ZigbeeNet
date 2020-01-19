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
    /// Get Debt Repayment Log value object class.
    ///
    /// Cluster: Prepayment. Command ID 0x0A is sent TO the server.
    /// This command is a specific command used for the Prepayment cluster.
    ///
    /// FIXME: This command is used to request the contents of the repayment log.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class GetDebtRepaymentLog : ZclCommand
    {
        /// <summary>
        /// The cluster ID to which this command belongs.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0705;

        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x0A;

        /// <summary>
        /// Latest End Time command message field.
        /// </summary>
        public DateTime LatestEndTime { get; set; }

        /// <summary>
        /// Number Of Debts command message field.
        /// </summary>
        public byte NumberOfDebts { get; set; }

        /// <summary>
        /// Debt Type command message field.
        /// </summary>
        public byte DebtType { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public GetDebtRepaymentLog()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(LatestEndTime, ZclDataType.Get(DataType.UTCTIME));
            serializer.Serialize(NumberOfDebts, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            serializer.Serialize(DebtType, ZclDataType.Get(DataType.ENUMERATION_8_BIT));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            LatestEndTime = deserializer.Deserialize<DateTime>(ZclDataType.Get(DataType.UTCTIME));
            NumberOfDebts = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            DebtType = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.ENUMERATION_8_BIT));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("GetDebtRepaymentLog [");
            builder.Append(base.ToString());
            builder.Append(", LatestEndTime=");
            builder.Append(LatestEndTime);
            builder.Append(", NumberOfDebts=");
            builder.Append(NumberOfDebts);
            builder.Append(", DebtType=");
            builder.Append(DebtType);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
