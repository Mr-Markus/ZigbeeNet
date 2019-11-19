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
    /// Publish Calorific Value Command value object class.
    ///
    /// Cluster: Price. Command ID 0x03 is sent FROM the server.
    /// This command is a specific command used for the Price cluster.
    ///
    /// The PublishCalorificValue command is sent in response to a GetCalorificValue command
    /// or if a new calorific value is available. Clients shall be capable of storing at least two
    /// instances of the Calorific Value, the currently active one and the next one.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class PublishCalorificValueCommand : ZclCommand
    {
        /// <summary>
        /// The cluster ID to which this command belongs.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0700;

        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x03;

        /// <summary>
        /// Issuer Event ID command message field.
        /// </summary>
        public uint IssuerEventId { get; set; }

        /// <summary>
        /// Start Time command message field.
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// Calorific Value command message field.
        /// </summary>
        public uint CalorificValue { get; set; }

        /// <summary>
        /// Calorific Value Unit command message field.
        /// </summary>
        public byte CalorificValueUnit { get; set; }

        /// <summary>
        /// Calorific Value Trailing Digit command message field.
        /// </summary>
        public byte CalorificValueTrailingDigit { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public PublishCalorificValueCommand()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
            CommandDirection = ZclCommandDirection.SERVER_TO_CLIENT;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(IssuerEventId, ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER));
            serializer.Serialize(StartTime, ZclDataType.Get(DataType.UTCTIME));
            serializer.Serialize(CalorificValue, ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER));
            serializer.Serialize(CalorificValueUnit, ZclDataType.Get(DataType.ENUMERATION_8_BIT));
            serializer.Serialize(CalorificValueTrailingDigit, ZclDataType.Get(DataType.BITMAP_8_BIT));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            IssuerEventId = deserializer.Deserialize<uint>(ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER));
            StartTime = deserializer.Deserialize<DateTime>(ZclDataType.Get(DataType.UTCTIME));
            CalorificValue = deserializer.Deserialize<uint>(ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER));
            CalorificValueUnit = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.ENUMERATION_8_BIT));
            CalorificValueTrailingDigit = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.BITMAP_8_BIT));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("PublishCalorificValueCommand [");
            builder.Append(base.ToString());
            builder.Append(", IssuerEventId=");
            builder.Append(IssuerEventId);
            builder.Append(", StartTime=");
            builder.Append(StartTime);
            builder.Append(", CalorificValue=");
            builder.Append(CalorificValue);
            builder.Append(", CalorificValueUnit=");
            builder.Append(CalorificValueUnit);
            builder.Append(", CalorificValueTrailingDigit=");
            builder.Append(CalorificValueTrailingDigit);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
