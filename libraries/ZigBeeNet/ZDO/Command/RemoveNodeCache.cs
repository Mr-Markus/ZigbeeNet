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
    /// Remove Node Cache value object class.
    ///
    ///
    /// The Remove_node_cache_rsp is provided to notify a Local Device of the request status
    /// from a Primary Discovery Cache device. Included in the response is a status code to
    /// notify the Local Device whether the request is successful (the Primary Cache Device has
    /// removed the discovery cache data for the indicated device of interest), or the request
    /// is not supported (meaning the Remote Device is not a Primary Discovery Cache device).
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class RemoveNodeCache : ZdoRequest
    {
        /// <summary>
        /// The ZDO cluster ID.
        /// </summary>
        public const ushort CLUSTER_ID = 0x801B;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public RemoveNodeCache()
        {
            ClusterId = CLUSTER_ID;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("RemoveNodeCache [");
            builder.Append(base.ToString());
            builder.Append(']');

            return builder.ToString();
        }
    }
}
