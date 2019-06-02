using System;
using System.Text;
using ZigBeeNet.Transaction;
using ZigBeeNet.ZCL;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.ZDO.Command
{
    /// <summary>
    /// Store Backup Bind Entry Response value object class.
    /// 
    /// The Store_Bkup_Bind_Entry_rsp is generated from a backup binding table cache
    /// device in response to a Store_Bkup_Bind_Entry_req from a primary binding table
    /// cache, and contains the Status of the request. This command shall be unicast to the
    /// requesting device. If the remote device is not a backup binding table cache, it shall
    /// return a Status of NOT_SUPPORTED. If the originator of the request is not
    /// recognized as a primary binding table cache, it shall return a Status of
    /// INV_REQUESTTYPE. Otherwise, the backup binding table cache shall add the
    /// binding entry to its binding table and return a Status of SUCCESS. If there is no
    /// room, it shall return a Status of TABLE_FULL.
    /// 
    /// </summary>

    public class StoreBackupBindEntryResponse : ZdoResponse
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public StoreBackupBindEntryResponse()
        {
            ClusterId = 0x8025;
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

            builder.Append("StoreBackupBindEntryResponse [")
                   .Append(base.ToString())
                   .Append(", Status=")
                   .Append(Status)
                   .Append(']');

            return builder.ToString();
        }
    }
}
