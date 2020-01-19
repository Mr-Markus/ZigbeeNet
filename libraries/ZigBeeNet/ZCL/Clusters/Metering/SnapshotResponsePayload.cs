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
    /// Snapshot Response Payload structure implementation.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class SnapshotResponsePayload : IZigBeeSerializable
    {
        /// <summary>
        /// Snapshot Schedule ID structure field.
        /// </summary>
        public byte SnapshotScheduleId { get; set; }

        /// <summary>
        /// Snapshot Schedule Confirmation structure field.
        /// </summary>
        public byte SnapshotScheduleConfirmation { get; set; }



        public void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(SnapshotScheduleId, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            serializer.Serialize(SnapshotScheduleConfirmation, ZclDataType.Get(DataType.ENUMERATION_8_BIT));
        }

        public void Deserialize(ZclFieldDeserializer deserializer)
        {
            SnapshotScheduleId = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            SnapshotScheduleConfirmation = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.ENUMERATION_8_BIT));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("SnapshotResponsePayload [");
            builder.Append(base.ToString());
            builder.Append(", SnapshotScheduleId=");
            builder.Append(SnapshotScheduleId);
            builder.Append(", SnapshotScheduleConfirmation=");
            builder.Append(SnapshotScheduleConfirmation);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
