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
    /// Find Node Cache Request value object class.
    ///
    ///
    /// The Find_node_cache_req is provided to enable ZigBee devices on the network to
    /// broadcast to all devices for which macRxOnWhenIdle = TRUE a request to find a device on
    /// the network that holds discovery information for the device of interest, as specified
    /// in the request parameters. The effect of a successful Find_node_cache_req is to have
    /// the Primary Discovery Cache device, holding discovery information for the device of
    /// interest, unicast a Find_node_cache_rsp back to the Local Device. Note that, like the
    /// NWK_addr_req, only the device meeting this criteria shall respond to the request
    /// generated by Find_node_cache_req.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class FindNodeCacheRequest : ZdoRequest
    {
        /// <summary>
        /// The ZDO cluster ID.
        /// </summary>
        public const ushort CLUSTER_ID = 0x001C;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public FindNodeCacheRequest()
        {
            ClusterId = CLUSTER_ID;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("FindNodeCacheRequest [");
            builder.Append(base.ToString());
            builder.Append(']');

            return builder.ToString();
        }
    }
}
