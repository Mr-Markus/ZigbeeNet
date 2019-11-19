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
    /// Get Calorific Value Command value object class.
    ///
    /// Cluster: Price. Command ID 0x05 is sent TO the server.
    /// This command is a specific command used for the Price cluster.
    ///
    /// This command initiates a PublishCalorificValue command(s) for scheduled calorific
    /// value updates. A server device shall be capable of storing at least two instances, the
    /// current and (if available) next instance to be activated in the future. <br> A ZCL
    /// Default response with status NOT_FOUND shall be returned if there are no conversion
    /// factor updates available
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class GetCalorificValueCommand : ZclCommand
    {
        /// <summary>
        /// The cluster ID to which this command belongs.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0700;

        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x05;

        /// <summary>
        /// Earliest Start Time command message field.
        /// 
        /// UTCTime stamp indicating the earliest start time of values to be returned by the
        /// corresponding PublishCalorificValue command. The first returned
        /// PublishCalorificValue command shall be the instance which is active or becomes
        /// active at or after the stated Earliest Start Time. If more than one instance is
        /// requested, the active and scheduled instances shall be sent with ascending
        /// ordered Start Time.
        /// </summary>
        public DateTime EarliestStartTime { get; set; }

        /// <summary>
        /// Min . Issuer Event ID command message field.
        /// 
        /// A 32-bit integer representing the minimum Issuer Event ID of values to be returned
        /// by the corresponding PublishCalorificValue command. A value of 0xFFFFFFFF means
        /// not specified; the server shall return values irrespective of the value of the
        /// Issuer Event ID.
        /// </summary>
        public uint MinIssuerEventId { get; set; }

        /// <summary>
        /// Number Of Commands command message field.
        /// 
        /// An 8-bit Integer which represents the maximum number of PublishCalorificValue
        /// commands that the CLIENT is willing to receive in response to this command. A value
        /// of 0 would indicate all available PublishCalorificValue commands shall be
        /// returned.
        /// </summary>
        public byte NumberOfCommands { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public GetCalorificValueCommand()
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
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            EarliestStartTime = deserializer.Deserialize<DateTime>(ZclDataType.Get(DataType.UTCTIME));
            MinIssuerEventId = deserializer.Deserialize<uint>(ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER));
            NumberOfCommands = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("GetCalorificValueCommand [");
            builder.Append(base.ToString());
            builder.Append(", EarliestStartTime=");
            builder.Append(EarliestStartTime);
            builder.Append(", MinIssuerEventId=");
            builder.Append(MinIssuerEventId);
            builder.Append(", NumberOfCommands=");
            builder.Append(NumberOfCommands);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
