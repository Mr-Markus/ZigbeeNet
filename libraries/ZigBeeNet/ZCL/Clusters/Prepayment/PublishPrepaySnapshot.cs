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
    /// Publish Prepay Snapshot value object class.
    ///
    /// Cluster: Prepayment. Command ID 0x01 is sent FROM the server.
    /// This command is a specific command used for the Prepayment cluster.
    ///
    /// FIXME: This command is generated in response to a GetPrepaySnapshot command. It is used
    /// to return a single snapshot to the client.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class PublishPrepaySnapshot : ZclCommand
    {
        /// <summary>
        /// The cluster ID to which this command belongs.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0705;

        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x01;

        /// <summary>
        /// Snapshot ID command message field.
        /// </summary>
        public uint SnapshotId { get; set; }

        /// <summary>
        /// Snapshot Time command message field.
        /// </summary>
        public DateTime SnapshotTime { get; set; }

        /// <summary>
        /// Total Snapshots Found command message field.
        /// </summary>
        public byte TotalSnapshotsFound { get; set; }

        /// <summary>
        /// Command Index command message field.
        /// </summary>
        public byte CommandIndex { get; set; }

        /// <summary>
        /// Total Number Of Commands command message field.
        /// </summary>
        public byte TotalNumberOfCommands { get; set; }

        /// <summary>
        /// Snapshot Cause command message field.
        /// </summary>
        public int SnapshotCause { get; set; }

        /// <summary>
        /// Snapshot Payload Type command message field.
        /// </summary>
        public byte SnapshotPayloadType { get; set; }

        /// <summary>
        /// Snapshot Payload command message field.
        /// </summary>
        public byte SnapshotPayload { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public PublishPrepaySnapshot()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
            CommandDirection = ZclCommandDirection.SERVER_TO_CLIENT;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(SnapshotId, ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER));
            serializer.Serialize(SnapshotTime, ZclDataType.Get(DataType.UTCTIME));
            serializer.Serialize(TotalSnapshotsFound, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            serializer.Serialize(CommandIndex, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            serializer.Serialize(TotalNumberOfCommands, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            serializer.Serialize(SnapshotCause, ZclDataType.Get(DataType.BITMAP_32_BIT));
            serializer.Serialize(SnapshotPayloadType, ZclDataType.Get(DataType.ENUMERATION_8_BIT));
            serializer.Serialize(SnapshotPayload, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            SnapshotId = deserializer.Deserialize<uint>(ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER));
            SnapshotTime = deserializer.Deserialize<DateTime>(ZclDataType.Get(DataType.UTCTIME));
            TotalSnapshotsFound = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            CommandIndex = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            TotalNumberOfCommands = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            SnapshotCause = deserializer.Deserialize<int>(ZclDataType.Get(DataType.BITMAP_32_BIT));
            SnapshotPayloadType = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.ENUMERATION_8_BIT));
            SnapshotPayload = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("PublishPrepaySnapshot [");
            builder.Append(base.ToString());
            builder.Append(", SnapshotId=");
            builder.Append(SnapshotId);
            builder.Append(", SnapshotTime=");
            builder.Append(SnapshotTime);
            builder.Append(", TotalSnapshotsFound=");
            builder.Append(TotalSnapshotsFound);
            builder.Append(", CommandIndex=");
            builder.Append(CommandIndex);
            builder.Append(", TotalNumberOfCommands=");
            builder.Append(TotalNumberOfCommands);
            builder.Append(", SnapshotCause=");
            builder.Append(SnapshotCause);
            builder.Append(", SnapshotPayloadType=");
            builder.Append(SnapshotPayloadType);
            builder.Append(", SnapshotPayload=");
            builder.Append(SnapshotPayload);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
