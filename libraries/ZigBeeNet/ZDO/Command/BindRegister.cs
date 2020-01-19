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
    /// Bind Register value object class.
    ///
    ///
    /// The Bind_Register_req is generated from a Local Device and sent to a primary binding
    /// table cache device to register that the local device wishes to hold its own binding table
    /// entries. The destination addressing mode for this request is unicast.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class BindRegister : ZdoRequest
    {
        /// <summary>
        /// The ZDO cluster ID.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0023;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public BindRegister()
        {
            ClusterId = CLUSTER_ID;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("BindRegister [");
            builder.Append(base.ToString());
            builder.Append(']');

            return builder.ToString();
        }
    }
}
