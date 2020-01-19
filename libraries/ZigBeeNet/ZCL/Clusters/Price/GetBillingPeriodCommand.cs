using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.Security;
using ZigBeeNet.ZCL.Clusters.Price;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Protocol;


namespace ZigBeeNet.ZCL.Clusters.Price
{
    /// <summary>
    /// Get Billing Period Command value object class.
    ///
    /// Cluster: Price. Command ID 0x0B is sent TO the server.
    /// This command is a specific command used for the Price cluster.
    ///
    /// This command initiates one or more PublishBillingPeriod commands for currently
    /// scheduled billing periods.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class GetBillingPeriodCommand : ZclCommand
    {
        /// <summary>
        /// The cluster ID to which this command belongs.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0700;

        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x0B;

        /// <summary>
        /// Earliest Start Time command message field.
        /// 
        /// UTCTime stamp indicating the earliest start time of billing periods to be returned
        /// by the corresponding PublishBillingPeriod command. The first returned
        /// PublishBillingPeriod command shall be the instance which is active or becomes
        /// active at or after the stated EarliestStartTime. If more than one instance is
        /// requested, the active and scheduled instances shall be sent with ascending
        /// ordered StartTime.
        /// </summary>
        public DateTime EarliestStartTime { get; set; }

        /// <summary>
        /// Min . Issuer Event ID command message field.
        /// 
        /// A 32-bit integer representing the minimum Issuer Event ID of billing periods to be
        /// returned by the corresponding PublishBillingPeriod command. A value of
        /// 0xFFFFFFFF means not specified; the server shall return periods irrespective of
        /// the value of the Issuer Event ID.
        /// </summary>
        public uint MinIssuerEventId { get; set; }

        /// <summary>
        /// Number Of Commands command message field.
        /// 
        /// An 8 bit Integer which indicates the maximum number of PublishBillingPeriod
        /// commands that the CLIENT is willing to receive in response to this command. A value
        /// of 0 would indicate all available PublishBillingPeriod commands shall be
        /// returned.
        /// </summary>
        public byte NumberOfCommands { get; set; }

        /// <summary>
        /// Tariff Type command message field.
        /// 
        /// An 8-bit bitmap identifying the TariffType of the requested Billing Period
        /// information. The least significant nibble represents an enumeration of the
        /// tariff type (Generation Meters shall use the ‘Received’ Tariff). A value of 0xFF
        /// means not specified. If the TariffType is not specified, the server shall return
        /// Billing Period information regardless of its type. The most significant nibble is
        /// reserved.
        /// </summary>
        public byte TariffType { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public GetBillingPeriodCommand()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(EarliestStartTime, ZclDataType.Get(DataType.UTCTIME));
            serializer.Serialize(MinIssuerEventId, ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER));
            serializer.Serialize(NumberOfCommands, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            serializer.Serialize(TariffType, ZclDataType.Get(DataType.BITMAP_8_BIT));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            EarliestStartTime = deserializer.Deserialize<DateTime>(ZclDataType.Get(DataType.UTCTIME));
            MinIssuerEventId = deserializer.Deserialize<uint>(ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER));
            NumberOfCommands = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            TariffType = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.BITMAP_8_BIT));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("GetBillingPeriodCommand [");
            builder.Append(base.ToString());
            builder.Append(", EarliestStartTime=");
            builder.Append(EarliestStartTime);
            builder.Append(", MinIssuerEventId=");
            builder.Append(MinIssuerEventId);
            builder.Append(", NumberOfCommands=");
            builder.Append(NumberOfCommands);
            builder.Append(", TariffType=");
            builder.Append(TariffType);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
