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
    /// Find Node Cache Response value object class.
    ///
    ///
    /// The Find_node_cache_rsp is provided to notify a Local Device of the successful
    /// discovery of the Primary Discovery Cache device for the given NWKAddr and IEEEAddr
    /// fields supplied in the request, or to signify that the device of interest is capable of
    /// responding to discovery requests. The Find_node_cache_rsp shall be generated only by
    /// Primary Discovery Cache devices holding discovery information for the NWKAddr and
    /// IEEEAddr in the request or the device of interest itself and all other Remote Devices
    /// shall not supply a response.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class FindNodeCacheResponse : ZdoResponse
    {
        /// <summary>
        /// The ZDO cluster ID.
        /// </summary>
        public const ushort CLUSTER_ID = 0x801C;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public FindNodeCacheResponse()
        {
            ClusterId = CLUSTER_ID;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("FindNodeCacheResponse [");
            builder.Append(base.ToString());
            builder.Append(']');

            return builder.ToString();
        }
    }
}
