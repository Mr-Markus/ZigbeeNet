using System;
using System.Text;
using ZigBeeNet.Transaction;
using ZigBeeNet.ZCL;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.ZDO.Command
{
    /// <summary>
   /// Backup Bind Table Request value object class.
   /// 
   /// The Backup_Bind_Table_req is generated from a local primary binding table
   /// cache and sent to the remote backup binding table cache device to request backup
   /// storage of its entire binding table. The destination addressing mode for this
   /// request is unicast.
   /// </summary>
    public class BackupBindTableRequest : ZdoRequest
    {
        /// <summary>
         /// Default constructor.
         /// </summary>
        public BackupBindTableRequest()
        {
            ClusterId = 0x0027;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append("BackupBindTableRequest [")
                   .Append(base.ToString())
                   .Append(']');

            return builder.ToString();
        }

    }
}