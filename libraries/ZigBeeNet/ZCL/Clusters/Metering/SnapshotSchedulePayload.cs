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
    /// Snapshot Schedule Payload structure implementation.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class SnapshotSchedulePayload : IZigBeeSerializable
    {
        /// <summary>
        /// Snapshot Schedule ID structure field.
        /// </summary>
        public byte SnapshotScheduleId { get; set; }

        /// <summary>
        /// Snapshot Start Time structure field.
        /// </summary>
        public DateTime SnapshotStartTime { get; set; }

        /// <summary>
        /// Snapshot Schedule structure field.
        /// </summary>
        public int SnapshotSchedule { get; set; }

        /// <summary>
        /// Snapshot Payload Type structure field.
        /// </summary>
        public byte SnapshotPayloadType { get; set; }

        /// <summary>
        /// Snapshot Cause structure field.
        /// </summary>
        public int SnapshotCause { get; set; }



        public void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(SnapshotScheduleId, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            serializer.Serialize(SnapshotStartTime, ZclDataType.Get(DataType.UTCTIME));
            serializer.Serialize(SnapshotSchedule, ZclDataType.Get(DataType.BITMAP_24_BIT));
            serializer.Serialize(SnapshotPayloadType, ZclDataType.Get(DataType.ENUMERATION_8_BIT));
            serializer.Serialize(SnapshotCause, ZclDataType.Get(DataType.BITMAP_32_BIT));
        }

        public void Deserialize(ZclFieldDeserializer deserializer)
        {
            SnapshotScheduleId = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            SnapshotStartTime = deserializer.Deserialize<DateTime>(ZclDataType.Get(DataType.UTCTIME));
            SnapshotSchedule = deserializer.Deserialize<int>(ZclDataType.Get(DataType.BITMAP_24_BIT));
            SnapshotPayloadType = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.ENUMERATION_8_BIT));
            SnapshotCause = deserializer.Deserialize<int>(ZclDataType.Get(DataType.BITMAP_32_BIT));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("SnapshotSchedulePayload [");
            builder.Append(base.ToString());
            builder.Append(", SnapshotScheduleId=");
            builder.Append(SnapshotScheduleId);
            builder.Append(", SnapshotStartTime=");
            builder.Append(SnapshotStartTime);
            builder.Append(", SnapshotSchedule=");
            builder.Append(SnapshotSchedule);
            builder.Append(", SnapshotPayloadType=");
            builder.Append(SnapshotPayloadType);
            builder.Append(", SnapshotCause=");
            builder.Append(SnapshotCause);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
