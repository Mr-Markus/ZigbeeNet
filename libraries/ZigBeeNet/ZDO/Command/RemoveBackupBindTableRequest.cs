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
    /// Remove Backup Bind Table Request value object class.
    ///
    ///
    /// The Remove_Bkup_Bind_Entry_req is generated from a local primary binding table cache
    /// and sent to a remote backup binding table cache device to request removal of the entry
    /// from backup storage. It will be generated whenever a binding table entry has been
    /// unbound by the primary binding table cache. The destination addressing mode for this
    /// request is unicast.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class RemoveBackupBindTableRequest : ZdoRequest
    {
        /// <summary>
        /// The ZDO cluster ID.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0026;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public RemoveBackupBindTableRequest()
        {
            ClusterId = CLUSTER_ID;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("RemoveBackupBindTableRequest [");
            builder.Append(base.ToString());
            builder.Append(']');

            return builder.ToString();
        }
    }
}
