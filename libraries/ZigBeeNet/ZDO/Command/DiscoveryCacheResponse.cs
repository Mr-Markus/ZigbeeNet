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
    /// Discovery Cache Response value object class.
    ///
    ///
    /// The Discovery_Cache_rsp is generated by Primary Discovery Cache devices receiving
    /// the Discovery_Cache_req. Remote Devices which are not Primary Discovery Cache
    /// devices (as designated in its Node Descriptor) should not respond to the
    /// Discovery_Cache_req command.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class DiscoveryCacheResponse : ZdoResponse
    {
        /// <summary>
        /// The ZDO cluster ID.
        /// </summary>
        public const ushort CLUSTER_ID = 0x8012;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public DiscoveryCacheResponse()
        {
            ClusterId = CLUSTER_ID;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("DiscoveryCacheResponse [");
            builder.Append(base.ToString());
            builder.Append(']');

            return builder.ToString();
        }
    }
}
