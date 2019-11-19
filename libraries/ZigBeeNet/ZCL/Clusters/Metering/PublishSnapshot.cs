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
    /// Publish Snapshot value object class.
    ///
    /// Cluster: Metering. Command ID 0x06 is sent FROM the server.
    /// This command is a specific command used for the Metering cluster.
    ///
    /// This command is generated in response to a GetSnapshot command. It is used to return a
    /// single snapshot to the client.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class PublishSnapshot : ZclCommand
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
        /// Snapshot ID command message field.
        /// 
        /// Unique identifier allocated by the device creating the snapshot.
        /// </summary>
        public uint SnapshotId { get; set; }

        /// <summary>
        /// Snapshot Time command message field.
        /// 
        /// This is a 32 bit value (in UTC Time) representing the time at which the data snapshot
        /// was taken.
        /// </summary>
        public DateTime SnapshotTime { get; set; }

        /// <summary>
        /// Total Snapshots Found command message field.
        /// 
        /// An 8-bit Integer indicating the number of snapshots found, based on the search
        /// criteria defined in the associated GetSnapshot command. If the value is greater
        /// than 1, the client is able to request the next snapshot by incrementing the Snapshot
        /// Offset field in an otherwise repeated GetSnapshot command.
        /// </summary>
        public byte TotalSnapshotsFound { get; set; }

        /// <summary>
        /// Command Index command message field.
        /// 
        /// The CommandIndex is used to count the payload fragments in the case where the entire
        /// payload (snapshot) does not fit into one message. The CommandIndex starts at 0 and
        /// is incremented for each fragment belonging to the same command.
        /// </summary>
        public byte CommandIndex { get; set; }

        /// <summary>
        /// Total Number Of Commands command message field.
        /// 
        /// In the case where the entire payload (snapshot) does not fit into one message, the
        /// Total Number of Commands field indicates the total number of sub-commands that
        /// will be returned.
        /// </summary>
        public byte TotalNumberOfCommands { get; set; }

        /// <summary>
        /// Snapshot Cause command message field.
        /// 
        /// A 32-bit BitMap indicating the cause of the snapshot.
        /// </summary>
        public int SnapshotCause { get; set; }

        /// <summary>
        /// Snapshot Payload Type command message field.
        /// 
        /// The SnapshotPayloadType is an 8-bit enumerator defining the format of the
        /// SnapshotSubPayload in this message. The server selects the SnapshotPayloadType
        /// based on the charging scheme in use.
        /// If the snapshot is taken by the server due to a change of Tariff Information (cause =
        /// 3) which involves a change in charging scheme then two snapshots shall be taken, the
        /// first according to the charging scheme being dismissed, the second to the scheme
        /// being introduced.
        /// </summary>
        public byte SnapshotPayloadType { get; set; }

        /// <summary>
        /// Snapshot Payload command message field.
        /// 
        /// The format of the SnapshotSub-Payload differs depending on the
        /// SnapshotPayloadType, as shown below. Note that, where the entire payload
        /// (snapshot) does not fit into one message, only the leading (non-Sub-Payload)
        /// fields of the Snapshot payload are repeated in each command; the
        /// SnapshotSub-Payload is divided over the required number of commands.
        /// </summary>
        public byte SnapshotPayload { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public PublishSnapshot()
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

            builder.Append("PublishSnapshot [");
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
