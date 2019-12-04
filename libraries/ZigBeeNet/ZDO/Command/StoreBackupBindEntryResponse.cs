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
    /// Store Backup Bind Entry Response value object class.
    ///
    ///
    /// The Store_Bkup_Bind_Entry_rsp is generated from a backup binding table cache device
    /// in response to a Store_Bkup_Bind_Entry_req from a primary binding table cache, and
    /// contains the status of the request. This command shall be unicast to the requesting
    /// device. If the remote device is not a backup binding table cache, it shall return a status
    /// of NOT_SUPPORTED. If the originator of the request is not recognized as a primary
    /// binding table cache, it shall return a status of INV_REQUESTTYPE. Otherwise, the
    /// backup binding table cache shall add the binding entry to its binding table and return a
    /// status of SUCCESS. If there is no room, it shall return a status of TABLE_FULL.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class StoreBackupBindEntryResponse : ZdoResponse
    {
        /// <summary>
        /// The ZDO cluster ID.
        /// </summary>
        public const ushort CLUSTER_ID = 0x8025;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public StoreBackupBindEntryResponse()
        {
            ClusterId = CLUSTER_ID;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            base.Serialize(serializer);

            serializer.Serialize(Status, ZclDataType.Get(DataType.ZDO_STATUS));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            base.Deserialize(deserializer);

            Status = deserializer.Deserialize<ZdoStatus>(ZclDataType.Get(DataType.ZDO_STATUS));
            if (Status != ZdoStatus.SUCCESS)
            {
                // Don't read the full response if we have an error
                return;
            }
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("StoreBackupBindEntryResponse [");
            builder.Append(base.ToString());
            builder.Append(", Status=");
            builder.Append(Status);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
