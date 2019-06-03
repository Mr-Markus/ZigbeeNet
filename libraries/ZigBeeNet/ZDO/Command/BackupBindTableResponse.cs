using System;
using System.Text;
using ZigBeeNet.Transaction;
using ZigBeeNet.ZCL;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.ZDO.Command
{
    /// <summary>
   /// Backup Bind Table Response value object class.
   /// 
   /// The Backup_Bind_Table_rsp is generated from a backup binding table cache
   /// device in response to a Backup_Bind_Table_req from a primary binding table
   /// cache and contains the status of the request. This command shall be unicast to the
   /// requesting device. If the remote device is not a backup binding table cache, it shall
   /// return a status of NOT_SUPPORTED. If the originator of the request is not
   /// recognized as a primary binding table cache, it shall return a status of
   /// INV_REQUESTTYPE. Otherwise, the backup binding table cache shall
   /// overwrite the binding entries in its binding table starting with StartIndex and
   /// continuing for BindingTableListCount entries. If this exceeds its table size, it shall
   /// fill in as many entries as possible and return a status of TABLE_FULL and the
   /// EntryCount parameter will be the number of entries in the table. Otherwise, it
   /// shall return a status of SUCCESS and EntryCount will be equal to StartIndex +
   /// BindingTableListCount from Backup_Bind_Table_req.
   /// </summary>
    public class BackupBindTableResponse : ZdoResponse
    {
        /// <summary>
         /// Default constructor.
         /// </summary>
        public BackupBindTableResponse()
        {
            ClusterId = 0x8027;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            base.Serialize(serializer);

            serializer.Serialize(Status, ZclDataType.Get(DataType.ZDO_STATUS));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            base.Deserialize(deserializer);

            Status = (ZdoStatus)deserializer.Deserialize(ZclDataType.Get(DataType.ZDO_STATUS));
            if (Status != ZdoStatus.SUCCESS)
            {
                // Don't read the full response if we have an error
                return;
            }
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append("BackupBindTableResponse [")
                   .Append(base.ToString())
                   .Append(", status=")
                   .Append(Status)
                   .Append(']');

            return builder.ToString();
        }

    }
}