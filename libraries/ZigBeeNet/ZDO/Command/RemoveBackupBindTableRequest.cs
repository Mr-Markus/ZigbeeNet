using System;
using System.Text;
using ZigBeeNet.Transaction;
using ZigBeeNet.ZCL;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.ZDO.Command
{
    /// <summary>
    /// Remove Backup Bind Table Request value object class.
    /// 
    /// The Remove_Bkup_Bind_Entry_req is generated from a local primary binding
    /// table cache and sent to a remote backup binding table cache device to request
    /// removal of the entry from backup storage. It will be generated whenever a binding
    /// table entry has been unbound by the primary binding table cache. The destination
    /// addressing mode for this request is unicast.
    /// 
    /// </summary>

    public class RemoveBackupBindTableRequest : ZdoRequest
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public RemoveBackupBindTableRequest()
        {
            ClusterId = 0x0026;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append("RemoveBackupBindTableRequest [")
                   .Append(base.ToString())
                   .Append(']');

            return builder.ToString();
        }
    }
}
