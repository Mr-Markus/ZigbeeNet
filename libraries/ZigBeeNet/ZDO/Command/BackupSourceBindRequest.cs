using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.Transaction;
using ZigBeeNet.ZCL;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.ZDO.Command
{
    /// <summary>
    /// Backup Source Bind Request value object class.
    /// 
    /// The Backup_Source_Bind_req is generated from a local primary binding table
    /// cache and sent to a remote backup binding table cache device to request backup
    /// storage of its entire source table. The destination addressing mode for this request
    /// is unicast.
    /// </summary>
    public class BackupSourceBindRequest : ZdoRequest
    {
        /// <summary>
         /// SourceTableEntries command message field.
         /// </summary>
        public ushort SourceTableEntries { get; set; }

        /// <summary>
         /// StartIndex command message field.
         /// </summary>
        public ushort StartIndex { get; set; }

        /// <summary>
         /// SourceTableListCount command message field.
         /// </summary>
        public ushort SourceTableListCount { get; set; }

        /// <summary>
         /// SourceTableList command message field.
         /// </summary>
        public List<ulong> SourceTableList { get; set; }

        /// <summary>
         /// Default constructor.
         /// </summary>
        public BackupSourceBindRequest()
        {
            ClusterId = 0x0029;
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

            SourceTableEntries = (ushort)deserializer.Deserialize(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            StartIndex = (ushort)deserializer.Deserialize(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            SourceTableListCount = (ushort)deserializer.Deserialize(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            SourceTableList = (List<ulong>)deserializer.Deserialize(ZclDataType.Get(DataType.N_X_IEEE_ADDRESS));
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append("BackupSourceBindRequest [")
                   .Append(base.ToString())
                   .Append(", sourceTableEntries=")
                   .Append(SourceTableEntries)
                   .Append(", startIndex=")
                   .Append(StartIndex)
                   .Append(", sourceTableListCount=")
                   .Append(SourceTableListCount)
                   .Append(", sourceTableList=")
                   .Append(SourceTableList)
                   .Append(']');

            return builder.ToString();
        }

    }
}