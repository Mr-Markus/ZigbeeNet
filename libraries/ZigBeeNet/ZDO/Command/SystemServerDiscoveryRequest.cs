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
    /// System Server Discovery Request value object class.
    ///
    ///
    /// The System_Server_Discovery_req is generated from a Local Device wishing to discover
    /// the location of a particular system server or servers as indicated by the ServerMask
    /// parameter. The destination addressing on this request is "broadcast to all devices for
    /// which macRxOnWhenIdle = TRUE".
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class SystemServerDiscoveryRequest : ZdoRequest
    {
        /// <summary>
        /// The ZDO cluster ID.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0015;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public SystemServerDiscoveryRequest()
        {
            ClusterId = CLUSTER_ID;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("SystemServerDiscoveryRequest [");
            builder.Append(base.ToString());
            builder.Append(']');

            return builder.ToString();
        }
    }
}
