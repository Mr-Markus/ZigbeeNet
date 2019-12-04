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
    /// Store Backup Bind Entry Request value object class.
    ///
    ///
    /// The Store_Bkup_Bind_Entry_req is generated from a local primary binding table cache
    /// and sent to a remote backup binding table cache device to request backup storage of the
    /// entry. It will be generated whenever a new binding table entry has been created by the
    /// primary binding table cache. The destination addressing mode for this request is
    /// unicast.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class StoreBackupBindEntryRequest : ZdoRequest
    {
        /// <summary>
        /// The ZDO cluster ID.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0025;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public StoreBackupBindEntryRequest()
        {
            ClusterId = CLUSTER_ID;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("StoreBackupBindEntryRequest [");
            builder.Append(base.ToString());
            builder.Append(']');

            return builder.ToString();
        }
    }
}
