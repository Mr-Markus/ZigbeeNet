using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.Security;
using ZigBeeNet.ZCL.Clusters.Metering;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Protocol;


namespace ZigBeeNet.ZCL.Clusters.Metering
{
    /// <summary>
    /// Get Notified Message value object class.
    ///
    /// Cluster: Metering. Command ID 0x0B is sent FROM the server.
    /// This command is a specific command used for the Metering cluster.
    ///
    /// The GetNotifiedMessage command is used only when a BOMD is being mirrored. This command
    /// provides a method for the BOMD to notify the Mirror message queue that it wants to receive
    /// commands that the Mirror has queued. The Notification flags set within the command
    /// shall inform the mirror of the commands that the BOMD is requesting.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class GetNotifiedMessage : ZclCommand
    {
        /// <summary>
        /// The cluster ID to which this command belongs.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0702;

        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x0B;

        /// <summary>
        /// Notification Scheme command message field.
        /// 
        /// An unsigned 8-bit integer that allows for the pre-loading of the Notification
        /// Flags bit mapping to ZCL or Smart Energy Standard commands.
        /// </summary>
        public byte NotificationScheme { get; set; }

        /// <summary>
        /// Notification Flag Attribute ID command message field.
        /// 
        /// An unsigned 16-bit integer that denotes the attribute ID of the notification flag
        /// (1-8) that is included in this command.
        /// </summary>
        public ushort NotificationFlagAttributeId { get; set; }

        /// <summary>
        /// Notification Flags N command message field.
        /// 
        /// The Notification Flags attribute/parameter indicating the command being
        /// requested.
        /// </summary>
        public int NotificationFlagsN { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public GetNotifiedMessage()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
            CommandDirection = ZclCommandDirection.SERVER_TO_CLIENT;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(NotificationScheme, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            serializer.Serialize(NotificationFlagAttributeId, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            serializer.Serialize(NotificationFlagsN, ZclDataType.Get(DataType.BITMAP_32_BIT));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            NotificationScheme = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            NotificationFlagAttributeId = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            NotificationFlagsN = deserializer.Deserialize<int>(ZclDataType.Get(DataType.BITMAP_32_BIT));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("GetNotifiedMessage [");
            builder.Append(base.ToString());
            builder.Append(", NotificationScheme=");
            builder.Append(NotificationScheme);
            builder.Append(", NotificationFlagAttributeId=");
            builder.Append(NotificationFlagAttributeId);
            builder.Append(", NotificationFlagsN=");
            builder.Append(NotificationFlagsN);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
