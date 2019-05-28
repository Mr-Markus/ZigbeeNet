using System;
using System.Text;
using ZigBeeNet.Transaction;
using ZigBeeNet.ZCL;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.ZDO.Command
{
    /// <summary>
    /// Recover Bind Table Request value object class.
    /// 
    /// The Recover_Bind_Table_req is generated from a local primary binding table
    /// cache and sent to a remote backup binding table cache device when it wants a
    /// complete restore of the binding table. The destination addressing mode for this
    /// request is unicast.
    /// 
    /// </summary>
    public class RecoverBindTableRequest : ZdoRequest
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public RecoverBindTableRequest()
        {
            ClusterId = 0x0028;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append("RecoverBindTableRequest [")
                   .Append(base.ToString())
                   .Append(']');

            return builder.ToString();
        }
    }
}
