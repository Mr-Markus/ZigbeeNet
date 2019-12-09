using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.ZCL;
using ZigBeeNet.ZCL.Protocol;
using ZigBeeNet.ZDO.Field;


namespace ZigBeeNet.ZDO.Command
{
    /// <summary>
    /// Backup Source Bind Request value object class.
    ///
    ///
    /// The Backup_Source_Bind_req is generated from a local primary binding table cache and
    /// sent to a remote backup binding table cache device to request backup storage of its
    /// entire source table. The destination addressing mode for this request is unicast.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class BackupSourceBindRequest : ZdoRequest
    {
        /// <summary>
        /// The ZDO cluster ID.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0029;

        /// <summary>
        /// Source Table Entries command message field.
        /// </summary>
        public ushort SourceTableEntries { get; set; }

        /// <summary>
        /// Start Index command message field.
        /// </summary>
        public ushort StartIndex { get; set; }

        /// <summary>
        /// Source Table List Count command message field.
        /// </summary>
        public ushort SourceTableListCount { get; set; }

        /// <summary>
        /// Source Table List command message field.
        /// </summary>
        public List<ulong> SourceTableList { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public BackupSourceBindRequest()
        {
            ClusterId = CLUSTER_ID;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            base.Serialize(serializer);

            serializer.Serialize(SourceTableEntries, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            serializer.Serialize(StartIndex, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            serializer.Serialize(SourceTableListCount, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            serializer.Serialize(SourceTableList, ZclDataType.Get(DataType.N_X_IEEE_ADDRESS));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            base.Deserialize(deserializer);

            SourceTableEntries = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            StartIndex = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            SourceTableListCount = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            SourceTableList = deserializer.Deserialize<List<ulong>>(ZclDataType.Get(DataType.N_X_IEEE_ADDRESS));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("BackupSourceBindRequest [");
            builder.Append(base.ToString());
            builder.Append(", SourceTableEntries=");
            builder.Append(SourceTableEntries);
            builder.Append(", StartIndex=");
            builder.Append(StartIndex);
            builder.Append(", SourceTableListCount=");
            builder.Append(SourceTableListCount);
            builder.Append(", SourceTableList=");
            builder.Append(SourceTableList == null? "" : string.Join(", ", SourceTableList));
            builder.Append(']');

            return builder.ToString();
        }
    }
}
