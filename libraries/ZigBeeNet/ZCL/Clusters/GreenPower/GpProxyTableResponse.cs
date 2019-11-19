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
    /// Gp Proxy Table Response value object class.
    ///
    /// Cluster: Green Power. Command ID 0x0B is sent TO the server.
    /// This command is a specific command used for the Green Power cluster.
    ///
    /// To reply with read-out Proxy Table entries, by index or by GPD ID.
    /// Upon reception of the GP Proxy Table Request command, the device shall check if it
    /// implements a Proxy Table. If not, it shall generate a ZCL Default Response command, with
    /// the Status code field carrying UNSUP_CLUSTER_COMMAND. If the device implements the
    /// Proxy Table, it shall prepare a GP Proxy Table Response.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class GpProxyTableResponse : ZclCommand
    {
        /// <summary>
        /// The cluster ID to which this command belongs.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0021;

        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x0B;

        /// <summary>
        /// Status command message field.
        /// </summary>
        public byte Status { get; set; }

        /// <summary>
        /// Total Number Of Non Empty Proxy Table Entries command message field.
        /// </summary>
        public byte TotalNumberOfNonEmptyProxyTableEntries { get; set; }

        /// <summary>
        /// Start Index command message field.
        /// </summary>
        public byte StartIndex { get; set; }

        /// <summary>
        /// Entries Count command message field.
        /// </summary>
        public byte EntriesCount { get; set; }

        /// <summary>
        /// Proxy Table Entries command message field.
        /// </summary>
        public byte ProxyTableEntries { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public GpProxyTableResponse()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(Status, ZclDataType.Get(DataType.ENUMERATION_8_BIT));
            serializer.Serialize(TotalNumberOfNonEmptyProxyTableEntries, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            serializer.Serialize(StartIndex, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            serializer.Serialize(EntriesCount, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            serializer.Serialize(ProxyTableEntries, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            Status = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.ENUMERATION_8_BIT));
            TotalNumberOfNonEmptyProxyTableEntries = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            StartIndex = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            EntriesCount = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            ProxyTableEntries = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("GpProxyTableResponse [");
            builder.Append(base.ToString());
            builder.Append(", Status=");
            builder.Append(Status);
            builder.Append(", TotalNumberOfNonEmptyProxyTableEntries=");
            builder.Append(TotalNumberOfNonEmptyProxyTableEntries);
            builder.Append(", StartIndex=");
            builder.Append(StartIndex);
            builder.Append(", EntriesCount=");
            builder.Append(EntriesCount);
            builder.Append(", ProxyTableEntries=");
            builder.Append(ProxyTableEntries);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
