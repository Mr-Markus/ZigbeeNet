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
    /// Recover Bind Table Request value object class.
    ///
    ///
    /// The Recover_Bind_Table_req is generated from a local primary binding table cache and
    /// sent to a remote backup binding table cache device when it wants a complete restore of the
    /// binding table. The destination addressing mode for this request is unicast.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class RecoverBindTableRequest : ZdoRequest
    {
        /// <summary>
        /// The ZDO cluster ID.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0028;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public RecoverBindTableRequest()
        {
            ClusterId = CLUSTER_ID;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("RecoverBindTableRequest [");
            builder.Append(base.ToString());
            builder.Append(']');

            return builder.ToString();
        }
    }
}
