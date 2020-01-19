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
    /// Consumer Top Up Response value object class.
    ///
    /// Cluster: Prepayment. Command ID 0x03 is sent FROM the server.
    /// This command is a specific command used for the Prepayment cluster.
    ///
    /// FIXME: This command is send in response to the ConsumerTopUp Command.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class ConsumerTopUpResponse : ZclCommand
    {
        /// <summary>
        /// The cluster ID to which this command belongs.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0705;

        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x03;

        /// <summary>
        /// Result Type command message field.
        /// </summary>
        public byte ResultType { get; set; }

        /// <summary>
        /// Top Up Value command message field.
        /// </summary>
        public uint TopUpValue { get; set; }

        /// <summary>
        /// Source Of Top Up command message field.
        /// </summary>
        public byte SourceOfTopUp { get; set; }

        /// <summary>
        /// Credit Remaining command message field.
        /// </summary>
        public uint CreditRemaining { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ConsumerTopUpResponse()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
            CommandDirection = ZclCommandDirection.SERVER_TO_CLIENT;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(ResultType, ZclDataType.Get(DataType.ENUMERATION_8_BIT));
            serializer.Serialize(TopUpValue, ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER));
            serializer.Serialize(SourceOfTopUp, ZclDataType.Get(DataType.ENUMERATION_8_BIT));
            serializer.Serialize(CreditRemaining, ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            ResultType = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.ENUMERATION_8_BIT));
            TopUpValue = deserializer.Deserialize<uint>(ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER));
            SourceOfTopUp = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.ENUMERATION_8_BIT));
            CreditRemaining = deserializer.Deserialize<uint>(ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("ConsumerTopUpResponse [");
            builder.Append(base.ToString());
            builder.Append(", ResultType=");
            builder.Append(ResultType);
            builder.Append(", TopUpValue=");
            builder.Append(TopUpValue);
            builder.Append(", SourceOfTopUp=");
            builder.Append(SourceOfTopUp);
            builder.Append(", CreditRemaining=");
            builder.Append(CreditRemaining);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
