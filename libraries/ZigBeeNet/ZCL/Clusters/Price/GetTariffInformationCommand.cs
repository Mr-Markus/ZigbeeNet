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
    /// Get Tariff Information Command value object class.
    ///
    /// Cluster: Price. Command ID 0x06 is sent TO the server.
    /// This command is a specific command used for the Price cluster.
    ///
    /// This command initiates PublishTariffInformation command(s) for scheduled tariff
    /// updates. A server device shall be capable of storing at least two instances, current and
    /// the next instance to be activated in the future. <br> One or more
    /// PublishTariffInformation commands are sent in response to this command. To obtain the
    /// complete tariff details, further GetPriceMatrix and GetBlockThesholds commands
    /// must be sent using the start time and IssuerTariffID obtained from the appropriate
    /// PublishTariffInformation command.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class GetTariffInformationCommand : ZclCommand
    {
        /// <summary>
        /// The cluster ID to which this command belongs.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0700;

        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x06;

        /// <summary>
        /// Earliest Start Time command message field.
        /// 
        /// UTCTime stamp indicating the earliest start time of tariffs to be returned by the
        /// corresponding PublishTariffInformation command. The first returned
        /// PublishTariffInformation command shall be the instance which is active or
        /// becomes active at or after the stated EarliestStartTime. If more than one command
        /// is requested, the active and scheduled commands shall be sent with ascending
        /// ordered StartTime.
        /// </summary>
        public DateTime EarliestStartTime { get; set; }

        /// <summary>
        /// Min . Issuer Event ID command message field.
        /// 
        /// A 32-bit integer representing the minimum Issuer Event ID of tariffs to be returned
        /// by the corresponding PublishTariffInformation command. A value of 0xFFFFFFFF
        /// means not specified; the server shall return tariffs irrespective of the value of
        /// the Issuer Event ID.
        /// </summary>
        public uint MinIssuerEventId { get; set; }

        /// <summary>
        /// Number Of Commands command message field.
        /// 
        /// An 8-bit integer which represents the maximum number of
        /// PublishTariffInformation commands that the CLIENT is willing to receive in
        /// response to this command. A value of 0 would indicate all available
        /// PublishTariffInformation commands shall be returned.
        /// </summary>
        public byte NumberOfCommands { get; set; }

        /// <summary>
        /// Tariff Type command message field.
        /// 
        /// An 8-bit bitmap identifying the type of tariff published in this command. The least
        /// significant nibble represents an enumeration of the tariff type (Generation
        /// Meters shall use the ‘Received’ Tariff.). The most significant nibble is
        /// reserved.
        /// </summary>
        public byte TariffType { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public GetTariffInformationCommand()
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

            builder.Append("GetTariffInformationCommand [");
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
