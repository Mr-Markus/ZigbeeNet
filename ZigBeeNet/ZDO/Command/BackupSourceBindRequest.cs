using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.Transaction;
using ZigBeeNet.ZCL;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.ZDO.Command
{
    /**
    * Backup Source Bind Request value object class.
    * 
    * The Backup_Source_Bind_req is generated from a local primary binding table
    * cache and sent to a remote backup binding table cache device to request backup
    * storage of its entire source table. The destination addressing mode for this request
    * is unicast.
    */
    public class BackupSourceBindRequest : ZdoRequest
    {
        /**
         * SourceTableEntries command message field.
         */
        public ushort SourceTableEntries { get; set; }

        /**
         * StartIndex command message field.
         */
        public ushort StartIndex { get; set; }

        /**
         * SourceTableListCount command message field.
         */
        public ushort SourceTableListCount { get; set; }

        /**
         * SourceTableList command message field.
         */
        public List<ulong> SourceTableList { get; set; }

        /**
         * Default constructor.
         */
        public BackupSourceBindRequest()
        {
            ClusterId = 0x0029;
        }

        public override void Serialize(ZclFieldSerializer serializer)
        {
            base.Serialize(serializer);

            serializer.Serialize(SourceTableEntries, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            serializer.Serialize(StartIndex, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            serializer.Serialize(SourceTableListCount, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            serializer.Serialize(SourceTableList, ZclDataType.Get(DataType.N_X_IEEE_ADDRESS));
        }

        public override void Deserialize(ZclFieldDeserializer deserializer)
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