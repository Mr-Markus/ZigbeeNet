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
    /// Publish Debt Log value object class.
    ///
    /// Cluster: Prepayment. Command ID 0x06 is sent FROM the server.
    /// This command is a specific command used for the Prepayment cluster.
    ///
    /// FIXME: This command is used to send the contents of the Repayment Log.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class PublishDebtLog : ZclCommand
    {
        /// <summary>
        /// The cluster ID to which this command belongs.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0705;

        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x06;

        /// <summary>
        /// Command Index command message field.
        /// </summary>
        public byte CommandIndex { get; set; }

        /// <summary>
        /// Total Number Of Commands command message field.
        /// </summary>
        public byte TotalNumberOfCommands { get; set; }

        /// <summary>
        /// Debt Payload command message field.
        /// </summary>
        public DebtPayload DebtPayload { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public PublishDebtLog()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
            CommandDirection = ZclCommandDirection.SERVER_TO_CLIENT;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(CommandIndex, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            serializer.Serialize(TotalNumberOfCommands, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            DebtPayload.Serialize(serializer);
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            CommandIndex = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            TotalNumberOfCommands = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            DebtPayload = new DebtPayload();
            DebtPayload.Deserialize(deserializer);
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("PublishDebtLog [");
            builder.Append(base.ToString());
            builder.Append(", CommandIndex=");
            builder.Append(CommandIndex);
            builder.Append(", TotalNumberOfCommands=");
            builder.Append(TotalNumberOfCommands);
            builder.Append(", DebtPayload=");
            builder.Append(DebtPayload);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
