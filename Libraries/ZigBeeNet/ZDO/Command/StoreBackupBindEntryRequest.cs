using System;
using System.Text;
using ZigBeeNet.Transaction;
using ZigBeeNet.ZCL;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.ZDO.Command
{
    /// <summary>
    /// Store Backup Bind Entry Request value object class.
    /// 
    /// The Store_Bkup_Bind_Entry_req is generated from a local primary binding table
    /// cache and sent to a remote backup binding table cache device to request backup
    /// storage of the entry. It will be generated whenever a new binding table entry has
    /// been created by the primary binding table cache. The destination addressing mode
    /// for this request is unicast.
    /// 
/// </summary>

    public class StoreBackupBindEntryRequest : ZdoRequest
    {
        /// <summary>
        /// Default constructor.
/// </summary>
        public StoreBackupBindEntryRequest()
        {
            ClusterId = 0x0025;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append("StoreBackupBindEntryRequest [")
                   .Append(base.ToString())
                   .Append(']');

            return builder.ToString();
        }
    }
}
