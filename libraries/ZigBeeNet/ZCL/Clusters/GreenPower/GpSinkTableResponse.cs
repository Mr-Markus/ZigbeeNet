using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.Security;
using ZigBeeNet.ZCL.Clusters.GreenPower;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Protocol;


namespace ZigBeeNet.ZCL.Clusters.GreenPower
{
    /// <summary>
    /// Gp Sink Table Response value object class.
    ///
    /// Cluster: Green Power. Command ID 0x0A is sent FROM the server.
    /// This command is a specific command used for the Green Power cluster.
    ///
    /// To selected Proxy Table entries, by index or by GPD ID.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class GpSinkTableResponse : ZclCommand
    {
        /// <summary>
        /// The cluster ID to which this command belongs.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0021;

        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x0A;

        /// <summary>
        /// Status command message field.
        /// </summary>
        public byte Status { get; set; }

        /// <summary>
        /// Total Numberof Non Empty Sink Table Entries command message field.
        /// </summary>
        public byte TotalNumberofNonEmptySinkTableEntries { get; set; }

        /// <summary>
        /// Start Index command message field.
        /// </summary>
        public byte StartIndex { get; set; }

        /// <summary>
        /// Sink Table Entries Count command message field.
        /// </summary>
        public byte SinkTableEntriesCount { get; set; }

        /// <summary>
        /// Sink Table Entries command message field.
        /// </summary>
        public byte SinkTableEntries { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public GpSinkTableResponse()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
            CommandDirection = ZclCommandDirection.SERVER_TO_CLIENT;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(Status, ZclDataType.Get(DataType.ENUMERATION_8_BIT));
            serializer.Serialize(TotalNumberofNonEmptySinkTableEntries, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            serializer.Serialize(StartIndex, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            serializer.Serialize(SinkTableEntriesCount, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            serializer.Serialize(SinkTableEntries, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            Status = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.ENUMERATION_8_BIT));
            TotalNumberofNonEmptySinkTableEntries = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            StartIndex = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            SinkTableEntriesCount = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            SinkTableEntries = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("GpSinkTableResponse [");
            builder.Append(base.ToString());
            builder.Append(", Status=");
            builder.Append(Status);
            builder.Append(", TotalNumberofNonEmptySinkTableEntries=");
            builder.Append(TotalNumberofNonEmptySinkTableEntries);
            builder.Append(", StartIndex=");
            builder.Append(StartIndex);
            builder.Append(", SinkTableEntriesCount=");
            builder.Append(SinkTableEntriesCount);
            builder.Append(", SinkTableEntries=");
            builder.Append(SinkTableEntries);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
