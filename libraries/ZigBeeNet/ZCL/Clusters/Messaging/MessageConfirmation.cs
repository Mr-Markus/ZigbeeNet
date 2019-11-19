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
    /// Message Confirmation value object class.
    ///
    /// Cluster: Messaging. Command ID 0x01 is sent FROM the server.
    /// This command is a specific command used for the Messaging cluster.
    ///
    /// The Message Confirmation command provides an indication that a Utility Customer has
    /// acknowledged and/or accepted the contents of a previously sent message. Enhanced
    /// Message Confirmation commands shall contain an answer of ‘NO’, ‘YES’ and/or a message
    /// confirmation string.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class MessageConfirmation : ZclCommand
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
        /// A unique unsigned 32-bit number identifier for the message being confirmed.
        /// </summary>
        public uint MessageId { get; set; }

        /// <summary>
        /// Confirmation Time command message field.
        /// 
        /// UTCTime of user confirmation of message.
        /// </summary>
        public DateTime ConfirmationTime { get; set; }

        /// <summary>
        /// Message Confirmation Control command message field.
        /// 
        /// An 8-bit BitMap field indicating the simple confirmation that is contained within
        /// the response.
        /// </summary>
        public byte MessageConfirmationControl { get; set; }

        /// <summary>
        /// Message Confirmation Response command message field.
        /// 
        /// A ZCL Octet String containing the message to be returned. The first Octet indicates
        /// length. The string shall be encoded in the UTF-8 format. If this optional field is
        /// not available, a default value of 0x00 shall be used.
        /// </summary>
        public ByteArray MessageConfirmationResponse { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public MessageConfirmation()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
            CommandDirection = ZclCommandDirection.SERVER_TO_CLIENT;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(MessageId, ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER));
            serializer.Serialize(ConfirmationTime, ZclDataType.Get(DataType.UTCTIME));
            serializer.Serialize(MessageConfirmationControl, ZclDataType.Get(DataType.BITMAP_8_BIT));
            serializer.Serialize(MessageConfirmationResponse, ZclDataType.Get(DataType.OCTET_STRING));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            MessageId = deserializer.Deserialize<uint>(ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER));
            ConfirmationTime = deserializer.Deserialize<DateTime>(ZclDataType.Get(DataType.UTCTIME));
            MessageConfirmationControl = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.BITMAP_8_BIT));
            MessageConfirmationResponse = deserializer.Deserialize<ByteArray>(ZclDataType.Get(DataType.OCTET_STRING));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("MessageConfirmation [");
            builder.Append(base.ToString());
            builder.Append(", MessageId=");
            builder.Append(MessageId);
            builder.Append(", ConfirmationTime=");
            builder.Append(ConfirmationTime);
            builder.Append(", MessageConfirmationControl=");
            builder.Append(MessageConfirmationControl);
            builder.Append(", MessageConfirmationResponse=");
            builder.Append(MessageConfirmationResponse);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
