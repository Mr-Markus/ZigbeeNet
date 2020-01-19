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
    /// Display Protected Message Command value object class.
    ///
    /// Cluster: Messaging. Command ID 0x02 is sent TO the server.
    /// This command is a specific command used for the Messaging cluster.
    ///
    /// The Display Protected Message command is for use with messages that are protected by a
    /// password or PIN
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class DisplayProtectedMessageCommand : ZclCommand
    {
        /// <summary>
        /// The cluster ID to which this command belongs.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0703;

        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x02;

        /// <summary>
        /// Message ID command message field.
        /// 
        /// A unique unsigned 32-bit number identifier for this message. It’s expected the
        /// value contained in this field is a unique number managed by upstream systems or a UTC
        /// based time stamp (UTCTime data type) identifying when the message was issued.
        /// </summary>
        public uint MessageId { get; set; }

        /// <summary>
        /// Message Control command message field.
        /// 
        /// An 8-bit BitMap field indicating control information related to the message.
        /// </summary>
        public byte MessageControl { get; set; }

        /// <summary>
        /// Start Time command message field.
        /// 
        /// A UTCTime field to denote the time at which the message becomes valid. A Start Time of
        /// 0x00000000 is a special time denoting “now.” If the device would send an event with a
        /// Start Time of now, adjust the Duration In Minutes field to correspond to the
        /// remainder of the event.
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// Duration In Minutes command message field.
        /// 
        /// An unsigned 16-bit field is used to denote the amount of time in minutes after the
        /// Start Time during which the message is displayed. A Maximum value of 0xFFFF means
        /// “until changed”.
        /// </summary>
        public ushort DurationInMinutes { get; set; }

        /// <summary>
        /// Message command message field.
        /// 
        /// A ZCL String containing the message to be delivered. The String shall be encoded in
        /// the UTF-8 format. Devices will have the ability to choose the methods for managing
        /// messages that are larger than can be displayed (truncation, scrolling, etc.). For
        /// supporting larger messages sent over the network, both devices must agree upon a
        /// common Fragmentation ASDU Maximum Incoming Transfer Size. Any message that needs
        /// truncation shall truncate on a UTF-8 character boundary. The SE secure payload is
        /// 59 bytes for the Message field in a non- fragmented, non-source routed Display
        /// Message packet (11 bytes for other Display Message fields). Devices using
        /// fragmentation can send a message larger than this. Reserving bytes for source
        /// route will reduce this.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Extended Message Control command message field.
        /// 
        /// An 8-bit BitMap field indicating additional control and status information for a
        /// given message. A UTC Time field to indicate the date/time at which all existing
        /// display messages should be cleared.
        /// </summary>
        public byte ExtendedMessageControl { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public DisplayProtectedMessageCommand()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(MessageId, ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER));
            serializer.Serialize(MessageControl, ZclDataType.Get(DataType.BITMAP_8_BIT));
            serializer.Serialize(StartTime, ZclDataType.Get(DataType.UTCTIME));
            serializer.Serialize(DurationInMinutes, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            serializer.Serialize(Message, ZclDataType.Get(DataType.CHARACTER_STRING));
            serializer.Serialize(ExtendedMessageControl, ZclDataType.Get(DataType.BITMAP_8_BIT));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            MessageId = deserializer.Deserialize<uint>(ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER));
            MessageControl = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.BITMAP_8_BIT));
            StartTime = deserializer.Deserialize<DateTime>(ZclDataType.Get(DataType.UTCTIME));
            DurationInMinutes = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            Message = deserializer.Deserialize<string>(ZclDataType.Get(DataType.CHARACTER_STRING));
            ExtendedMessageControl = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.BITMAP_8_BIT));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("DisplayProtectedMessageCommand [");
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
            builder.Append(", ExtendedMessageControl=");
            builder.Append(ExtendedMessageControl);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
