using System;
using System.Text;
using ZigBeeNet.Transaction;
using ZigBeeNet.ZCL;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.ZDO.Command
{
    /// <summary>
    /// Recover Bind Table Response value object class.
    /// 
    /// The Recover_Bind_Table_rsp is generated from a backup binding table cache
    /// device in response to a Recover_Bind_Table_req from a primary binding table
    /// cache and contains the Status of the request. This command shall be unicast to the
    /// requesting device. If the responding device is not a backup binding table cache, it
    /// shall return a Status of NOT_SUPPORTED. If the originator of the request is not
    /// recognized as a primary binding table cache it shall return a Status of
    /// INV_REQUESTTYPE. Otherwise, the backup binding table cache shall prepare a
    /// list of binding table entries from its backup beginning with StartIndex. It will fit in
    /// as many entries as possible into a Recover_Bind_Table_rsp command and return a
    /// Status of SUCCESS. If StartIndex is more than the number of entries in the
    /// Binding table, a Status of NO_ENTRY is returned. For a successful response,
    /// BindingTableEntries is the total number of entries in the backup binding table, and
    /// BindingTableListCount is the number of entries which is being returned in the
    /// response.
    /// 
    /// </summary>
    public class RecoverBindTableResponse : ZdoResponse
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public RecoverBindTableResponse()
        {
            ClusterId = 0x8028;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append("RecoverBindTableResponse [")
                   .Append(base.ToString())
                   .Append(']');

            return builder.ToString();
        }
    }
}
