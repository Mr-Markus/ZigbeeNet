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
    /// Set Maximum Credit Limit value object class.
    ///
    /// Cluster: Prepayment. Command ID 0x0B is sent TO the server.
    /// This command is a specific command used for the Prepayment cluster.
    ///
    /// FIXME: This command is sent from a client to the Prepayment server to set the maximum
    /// credit level allowed in the meter.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class SetMaximumCreditLimit : ZclCommand
    {
        /// <summary>
        /// The cluster ID to which this command belongs.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0705;

        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x0B;

        /// <summary>
        /// Provider ID command message field.
        /// </summary>
        public uint ProviderId { get; set; }

        /// <summary>
        /// Issuer Event ID command message field.
        /// </summary>
        public uint IssuerEventId { get; set; }

        /// <summary>
        /// Implementation Date Time command message field.
        /// </summary>
        public DateTime ImplementationDateTime { get; set; }

        /// <summary>
        /// Maximum Credit Level command message field.
        /// </summary>
        public uint MaximumCreditLevel { get; set; }

        /// <summary>
        /// Maximum Credit Per Top Up command message field.
        /// </summary>
        public uint MaximumCreditPerTopUp { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public SetMaximumCreditLimit()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(ProviderId, ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER));
            serializer.Serialize(IssuerEventId, ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER));
            serializer.Serialize(ImplementationDateTime, ZclDataType.Get(DataType.UTCTIME));
            serializer.Serialize(MaximumCreditLevel, ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER));
            serializer.Serialize(MaximumCreditPerTopUp, ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            ProviderId = deserializer.Deserialize<uint>(ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER));
            IssuerEventId = deserializer.Deserialize<uint>(ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER));
            ImplementationDateTime = deserializer.Deserialize<DateTime>(ZclDataType.Get(DataType.UTCTIME));
            MaximumCreditLevel = deserializer.Deserialize<uint>(ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER));
            MaximumCreditPerTopUp = deserializer.Deserialize<uint>(ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("SetMaximumCreditLimit [");
            builder.Append(base.ToString());
            builder.Append(", ProviderId=");
            builder.Append(ProviderId);
            builder.Append(", IssuerEventId=");
            builder.Append(IssuerEventId);
            builder.Append(", ImplementationDateTime=");
            builder.Append(ImplementationDateTime);
            builder.Append(", MaximumCreditLevel=");
            builder.Append(MaximumCreditLevel);
            builder.Append(", MaximumCreditPerTopUp=");
            builder.Append(MaximumCreditPerTopUp);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
