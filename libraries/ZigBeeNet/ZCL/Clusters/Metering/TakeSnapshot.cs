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
    /// Take Snapshot value object class.
    ///
    /// Cluster: Metering. Command ID 0x05 is sent TO the server.
    /// This command is a specific command used for the Metering cluster.
    ///
    /// This command is used to instruct the cluster server to take a single snapshot.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class TakeSnapshot : ZclCommand
    {
        /// <summary>
        /// The cluster ID to which this command belongs.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0702;

        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x05;

        /// <summary>
        /// Snapshot Cause command message field.
        /// 
        /// A 32-bit BitMap indicating the cause of the snapshot. Note that the Manually
        /// Triggered from Client flag shall additionally be set for all Snapshots triggered
        /// in this manner.
        /// </summary>
        public int SnapshotCause { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public TakeSnapshot()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(SnapshotCause, ZclDataType.Get(DataType.BITMAP_32_BIT));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            SnapshotCause = deserializer.Deserialize<int>(ZclDataType.Get(DataType.BITMAP_32_BIT));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("TakeSnapshot [");
            builder.Append(base.ToString());
            builder.Append(", SnapshotCause=");
            builder.Append(SnapshotCause);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
