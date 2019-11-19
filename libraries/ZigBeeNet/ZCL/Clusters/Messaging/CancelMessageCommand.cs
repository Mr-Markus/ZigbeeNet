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
    /// Cancel Message Command value object class.
    ///
    /// Cluster: Messaging. Command ID 0x01 is sent TO the server.
    /// This command is a specific command used for the Messaging cluster.
    ///
    /// The Cancel Message command provides the ability to cancel the sending or acceptance of
    /// previously sent messages. When this message is received the recipient device has the
    /// option of clearing any display or user interfaces it supports, or has the option of
    /// logging the message for future reference.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class CancelMessageCommand : ZclCommand
    {
        /// <summary>
        /// The cluster ID to which this command belongs.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0703;

        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x01;

        /// <summary>
        /// Message ID command message field.
        /// 
        /// A unique unsigned 32-bit number identifier for the message being cancelled. It’s
        /// expected the value contained in this field is a unique number managed by upstream
        /// systems or a UTC based time stamp (UTCTime data type) identifying when the message
        /// was originally issued.
        /// </summary>
        public uint MessageId { get; set; }

        /// <summary>
        /// Message Control command message field.
        /// 
        /// This field is deprecated and should be set to 0x00.
        /// </summary>
        public byte MessageControl { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public CancelMessageCommand()
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
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            MessageId = deserializer.Deserialize<uint>(ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER));
            MessageControl = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.BITMAP_8_BIT));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("CancelMessageCommand [");
            builder.Append(base.ToString());
            builder.Append(", MessageId=");
            builder.Append(MessageId);
            builder.Append(", MessageControl=");
            builder.Append(MessageControl);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
