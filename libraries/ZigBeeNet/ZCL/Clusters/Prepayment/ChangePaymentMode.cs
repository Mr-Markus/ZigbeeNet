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
    /// Change Payment Mode value object class.
    ///
    /// Cluster: Prepayment. Command ID 0x06 is sent TO the server.
    /// This command is a specific command used for the Prepayment cluster.
    ///
    /// FIXME: This command is sent to a Metering Device to instruct it to change its mode of
    /// operation. i.e. from Credit to Prepayment.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class ChangePaymentMode : ZclCommand
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
        /// Proposed Payment Control Configuration command message field.
        /// </summary>
        public ushort ProposedPaymentControlConfiguration { get; set; }

        /// <summary>
        /// Cut Off Value command message field.
        /// </summary>
        public uint CutOffValue { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ChangePaymentMode()
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
            serializer.Serialize(ProposedPaymentControlConfiguration, ZclDataType.Get(DataType.BITMAP_16_BIT));
            serializer.Serialize(CutOffValue, ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            ProviderId = deserializer.Deserialize<uint>(ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER));
            IssuerEventId = deserializer.Deserialize<uint>(ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER));
            ImplementationDateTime = deserializer.Deserialize<DateTime>(ZclDataType.Get(DataType.UTCTIME));
            ProposedPaymentControlConfiguration = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.BITMAP_16_BIT));
            CutOffValue = deserializer.Deserialize<uint>(ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("ChangePaymentMode [");
            builder.Append(base.ToString());
            builder.Append(", ProviderId=");
            builder.Append(ProviderId);
            builder.Append(", IssuerEventId=");
            builder.Append(IssuerEventId);
            builder.Append(", ImplementationDateTime=");
            builder.Append(ImplementationDateTime);
            builder.Append(", ProposedPaymentControlConfiguration=");
            builder.Append(ProposedPaymentControlConfiguration);
            builder.Append(", CutOffValue=");
            builder.Append(CutOffValue);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
