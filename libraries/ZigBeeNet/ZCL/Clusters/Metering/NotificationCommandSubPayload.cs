using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.Serialization;
using ZigBeeNet.ZCL.Clusters.Metering;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.ZCL.Clusters.Metering
{
    /// <summary>
    ///  structure implementation.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class NotificationCommandSubPayload : IZigBeeSerializable
    {
        /// <summary>
        /// Cluster ID structure field.
        /// 
        /// An unsigned 16-bit integer that denotes the Cluster ID of the Notification flag
        /// that will be configured for this Notification scheme.
        /// </summary>
        public ushort ClusterId { get; set; }

        /// <summary>
        /// Manufacturer Code structure field.
        /// 
        /// An unsigned 16-bit integer that denotes the Manufacturer Code to be used with these
        /// command IDs, that are configured for this Notification flag within this
        /// Notification scheme.
        /// </summary>
        public ushort ManufacturerCode { get; set; }

        /// <summary>
        /// Number Of Commands structure field.
        /// 
        /// An unsigned 8-bit integer that indicates the number of command identifiers
        /// contained within this sub payload.
        /// </summary>
        public byte NumberOfCommands { get; set; }

        /// <summary>
        /// Command IDs structure field.
        /// 
        /// An unsigned 8-bit integer that denotes the command that is to be used. The command ID
        /// should be used with the cluster ID to reference the command(s).
        /// </summary>
        public byte CommandIds { get; set; }



        public void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(ClusterId, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            serializer.Serialize(ManufacturerCode, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            serializer.Serialize(NumberOfCommands, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            serializer.Serialize(CommandIds, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
        }

        public void Deserialize(ZclFieldDeserializer deserializer)
        {
            ClusterId = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            ManufacturerCode = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            NumberOfCommands = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            CommandIds = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("NotificationCommandSubPayload [");
            builder.Append(base.ToString());
            builder.Append(", ClusterId=");
            builder.Append(ClusterId);
            builder.Append(", ManufacturerCode=");
            builder.Append(ManufacturerCode);
            builder.Append(", NumberOfCommands=");
            builder.Append(NumberOfCommands);
            builder.Append(", CommandIds=");
            builder.Append(CommandIds);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
