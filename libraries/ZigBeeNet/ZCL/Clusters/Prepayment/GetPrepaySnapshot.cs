using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.Security;
using ZigBeeNet.ZCL.Clusters.Prepayment;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Protocol;


namespace ZigBeeNet.ZCL.Clusters.Prepayment
{
    /// <summary>
    /// Get Prepay Snapshot value object class.
    ///
    /// Cluster: Prepayment. Command ID 0x07 is sent TO the server.
    /// This command is a specific command used for the Prepayment cluster.
    ///
    /// FIXME: This command is used to request the cluster server for snapshot data.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class GetPrepaySnapshot : ZclCommand
    {
        /// <summary>
        /// The cluster ID to which this command belongs.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0705;

        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x07;

        /// <summary>
        /// Earliest Start Time command message field.
        /// </summary>
        public DateTime EarliestStartTime { get; set; }

        /// <summary>
        /// Latest End Time command message field.
        /// </summary>
        public DateTime LatestEndTime { get; set; }

        /// <summary>
        /// Snapshot Offset command message field.
        /// </summary>
        public byte SnapshotOffset { get; set; }

        /// <summary>
        /// Snapshot Cause command message field.
        /// </summary>
        public int SnapshotCause { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public GetPrepaySnapshot()
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

            builder.Append("GetPrepaySnapshot [");
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
