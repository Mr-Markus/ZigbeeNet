using System;
using System.Text;
using ZigBeeNet.Transaction;
using ZigBeeNet.ZCL;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.ZDO.Command
{
    /// <summary>
    /// Remove Backup Bind Entry Response value object class.
    /// 
    /// The Remove_Bkup_Bind_Entry_rsp is generated from a backup binding table
    /// cache device in response to a Remove_Bkup_Bind_Entry_req from the primary
    /// binding table cache and contains the Status of the request. This command shall be
    /// unicast to the requesting device. If the remote device is not a backup binding table
    /// cache, it shall return a Status of NOT_SUPPORTED. If the originator of the
    /// request is not recognized as a primary binding table cache, it shall return a Status
    /// of INV_REQUESTTYPE. Otherwise, the backup binding table cache shall delete
    /// the binding entry from its binding table and return a Status of SUCCESS. If the
    /// entry is not found, it shall return a Status of NO_ENTRY.
    /// 
    /// </summary>
    public class RemoveBackupBindEntryResponse : ZdoResponse
    {
        /// <summary>
        /// EntryCount command message field.
/// </summary>
        public ushort EntryCount { get; set; }

        /// <summary>
        /// Default constructor.
/// </summary>
        public RemoveBackupBindEntryResponse()
        {
            ClusterId = 0x8026;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            base.Serialize(serializer);

            serializer.Serialize(Status, ZclDataType.Get(DataType.ZDO_STATUS));
            serializer.Serialize(EntryCount, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
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
            EntryCount = (ushort)deserializer.Deserialize(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append("RemoveBackupBindEntryResponse [")
                   .Append(base.ToString())
                   .Append(", Status=")
                   .Append(Status)
                   .Append(", entryCount=")
                   .Append(EntryCount)
                   .Append(']');

            return builder.ToString();
        }
    }
}
