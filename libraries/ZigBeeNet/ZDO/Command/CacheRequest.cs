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
    /// Cache Request value object class.
    ///
    ///
    /// The Mgmt_Cache_req is provided to enable ZigBee devices on the network to retrieve a
    /// list of ZigBee End Devices registered with a Primary Discovery Cache device. The
    /// destination addressing on this primitive shall be unicast.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class CacheRequest : ZdoRequest
    {
        /// <summary>
        /// The ZDO cluster ID.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0037;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public CacheRequest()
        {
            ClusterId = CLUSTER_ID;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("CacheRequest [");
            builder.Append(base.ToString());
            builder.Append(']');

            return builder.ToString();
        }
    }
}
