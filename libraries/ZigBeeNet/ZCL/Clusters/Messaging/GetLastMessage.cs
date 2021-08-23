using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.Security;
using ZigBeeNet.ZCL.Clusters.Messaging;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Protocol;


namespace ZigBeeNet.ZCL.Clusters.Messaging
{
    /// <summary>
    /// Get Last Message value object class.
    ///
    /// Cluster: Messaging. Command ID 0x00 is sent FROM the server.
    /// This command is a specific command used for the Messaging cluster.
    ///
    /// On receipt of this command, the device shall send a Display Message or Display Protected
    /// Message command as appropriate. A ZCL Default Response with status NOT_FOUND shall be
    /// returned if no message is available.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class GetLastMessage : ZclCommand
    {
        /// <summary>
        /// The cluster ID to which this command belongs.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0703;

        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x00;

        /// <summary>
        /// Message ID command message field.
        /// </summary>
        public uint MessageId { get; set; }

        /// <summary>
        /// Message Control command message field.
        /// </summary>
        public byte MessageControl { get; set; }

        /// <summary>
        /// Start Time command message field.
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// Duration In Minutes command message field.
        /// </summary>
        public ushort DurationInMinutes { get; set; }

        /// <summary>
        /// Message command message field.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Optional Extended Message Control command message field.
        /// </summary>
        public byte OptionalExtendedMessageControl { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public GetLastMessage()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
            CommandDirection = ZclCommandDirection.SERVER_TO_CLIENT;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(MessageId, DataType.UNSIGNED_32_BIT_INTEGER);
            serializer.Serialize(MessageControl, DataType.BITMAP_8_BIT);
            serializer.Serialize(StartTime, DataType.UTCTIME);
            serializer.Serialize(DurationInMinutes, DataType.UNSIGNED_16_BIT_INTEGER);
            serializer.Serialize(Message, DataType.CHARACTER_STRING);
            serializer.Serialize(OptionalExtendedMessageControl, DataType.BITMAP_8_BIT);
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            MessageId = deserializer.Deserialize<uint>(DataType.UNSIGNED_32_BIT_INTEGER);
            MessageControl = deserializer.Deserialize<byte>(DataType.BITMAP_8_BIT);
            StartTime = deserializer.Deserialize<DateTime>(DataType.UTCTIME);
            DurationInMinutes = deserializer.Deserialize<ushort>(DataType.UNSIGNED_16_BIT_INTEGER);
            Message = deserializer.Deserialize<string>(DataType.CHARACTER_STRING);
            OptionalExtendedMessageControl = deserializer.Deserialize<byte>(DataType.BITMAP_8_BIT);
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("GetLastMessage [");
            builder.Append(base.ToString());
            builder.Append(", MessageId=");
            builder.Append(MessageId);
            builder.Append(", MessageControl=");
            builder.Append(MessageControl);
            builder.Append(", StartTime=");
            builder.Append(StartTime);
            builder.Append(", DurationInMinutes=");
            builder.Append(DurationInMinutes);
            builder.Append(", Message=");
            builder.Append(Message);
            builder.Append(", OptionalExtendedMessageControl=");
            builder.Append(OptionalExtendedMessageControl);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
