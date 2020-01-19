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
    /// Discovery Cache Request value object class.
    ///
    ///
    /// The Discovery_Cache_req is provided to enable devices on the network to locate a
    /// Primary Discovery Cache device on the network. The destination addressing on this
    /// primitive shall be broadcast to all devices for which macRxOnWhenIdle = TRUE.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class DiscoveryCacheRequest : ZdoRequest
    {
        /// <summary>
        /// The ZDO cluster ID.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0012;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public DiscoveryCacheRequest()
        {
            ClusterId = CLUSTER_ID;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("DiscoveryCacheRequest [");
            builder.Append(base.ToString());
            builder.Append(']');

            return builder.ToString();
        }
    }
}
