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
    /// Get Snapshot value object class.
    ///
    /// Cluster: Metering. Command ID 0x06 is sent TO the server.
    /// This command is a specific command used for the Metering cluster.
    ///
    /// This command is used to request snapshot data from the cluster server.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class GetSnapshot : ZclCommand
    {
        /// <summary>
        /// The cluster ID to which this command belongs.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0702;

        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x06;

        /// <summary>
        /// Earliest Start Time command message field.
        /// 
        /// A UTC Timestamp indicating the earliest time of a snapshot to be returned by a
        /// corresponding Publish Snapshot command. Snapshots with a time stamp equal to or
        /// greater than the specified Earliest Start Time shall be returned.
        /// </summary>
        public DateTime EarliestStartTime { get; set; }

        /// <summary>
        /// Latest End Time command message field.
        /// 
        /// A UTC Timestamp indicating the latest time of a snapshot to be returned by a
        /// corresponding Publish Snapshot command. Snapshots with a time stamp less than the
        /// specified Latest End Time shall be returned.
        /// </summary>
        public DateTime LatestEndTime { get; set; }

        /// <summary>
        /// Snapshot Offset command message field.
        /// 
        /// Where multiple snapshots satisfy the selection criteria specified by the other
        /// fields in this command, this field identifies the individual snapshot to be
        /// returned. An offset of zero (0x00) indicates that the first snapshot satisfying
        /// the selection criteria should be returned, 0x01 the second, and so on.
        /// </summary>
        public byte SnapshotOffset { get; set; }

        /// <summary>
        /// Snapshot Cause command message field.
        /// 
        /// This field is used to select only snapshots that were taken due to a specific cause.
        /// Setting this field to 0xFFFFFFFF indicates that all snapshots should be selected,
        /// irrespective of the cause.
        /// </summary>
        public int SnapshotCause { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public GetSnapshot()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(EarliestStartTime, ZclDataType.Get(DataType.UTCTIME));
            serializer.Serialize(LatestEndTime, ZclDataType.Get(DataType.UTCTIME));
            serializer.Serialize(SnapshotOffset, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            serializer.Serialize(SnapshotCause, ZclDataType.Get(DataType.BITMAP_32_BIT));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            EarliestStartTime = deserializer.Deserialize<DateTime>(ZclDataType.Get(DataType.UTCTIME));
            LatestEndTime = deserializer.Deserialize<DateTime>(ZclDataType.Get(DataType.UTCTIME));
            SnapshotOffset = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            SnapshotCause = deserializer.Deserialize<int>(ZclDataType.Get(DataType.BITMAP_32_BIT));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("GetSnapshot [");
            builder.Append(base.ToString());
            builder.Append(", EarliestStartTime=");
            builder.Append(EarliestStartTime);
            builder.Append(", LatestEndTime=");
            builder.Append(LatestEndTime);
            builder.Append(", SnapshotOffset=");
            builder.Append(SnapshotOffset);
            builder.Append(", SnapshotCause=");
            builder.Append(SnapshotCause);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
