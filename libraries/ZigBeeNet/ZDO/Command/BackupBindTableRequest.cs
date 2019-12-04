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
    /// Backup Bind Table Request value object class.
    ///
    ///
    /// The Backup_Bind_Table_req is generated from a local primary binding table cache and
    /// sent to the remote backup binding table cache device to request backup storage of its
    /// entire binding table. The destination addressing mode for this request is unicast.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class BackupBindTableRequest : ZdoRequest
    {
        /// <summary>
        /// The ZDO cluster ID.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0027;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public BackupBindTableRequest()
        {
            ClusterId = CLUSTER_ID;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("BackupBindTableRequest [");
            builder.Append(base.ToString());
            builder.Append(']');

            return builder.ToString();
        }
    }
}
