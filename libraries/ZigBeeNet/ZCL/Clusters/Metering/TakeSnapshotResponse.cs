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
    /// Take Snapshot Response value object class.
    ///
    /// Cluster: Metering. Command ID 0x05 is sent FROM the server.
    /// This command is a specific command used for the Metering cluster.
    ///
    /// This command is generated in response to a TakeSnapshot command, and is sent to confirm
    /// whether the requested snapshot has been accepted and successfully taken.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class TakeSnapshotResponse : ZclCommand
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
        /// Snapshot ID command message field.
        /// 
        /// Unique identifier allocated by the device creating the snapshot. The value
        /// contained in this field indicates the TakeSnapshot command for which this
        /// response is generated.
        /// </summary>
        public uint SnapshotId { get; set; }

        /// <summary>
        /// Snapshot Confirmation command message field.
        /// 
        /// This is the acknowledgment from the device that it can support this required type of
        /// snapshot.
        /// </summary>
        public byte SnapshotConfirmation { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public TakeSnapshotResponse()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
            CommandDirection = ZclCommandDirection.SERVER_TO_CLIENT;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(SnapshotId, ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER));
            serializer.Serialize(SnapshotConfirmation, ZclDataType.Get(DataType.ENUMERATION_8_BIT));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            SnapshotId = deserializer.Deserialize<uint>(ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER));
            SnapshotConfirmation = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.ENUMERATION_8_BIT));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("TakeSnapshotResponse [");
            builder.Append(base.ToString());
            builder.Append(", SnapshotId=");
            builder.Append(SnapshotId);
            builder.Append(", SnapshotConfirmation=");
            builder.Append(SnapshotConfirmation);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
