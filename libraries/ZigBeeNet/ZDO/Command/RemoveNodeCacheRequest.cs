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
    /// Remove Node Cache Request value object class.
    ///
    ///
    /// The Remove_node_cache_req is provided to enable ZigBee devices on the network to
    /// request removal of discovery cache information for a specified ZigBee end device from a
    /// Primary Discovery Cache device. The effect of a successful Remove_node_cache_req is
    /// to undo a previously successful Discovery_store_req and additionally remove any
    /// cache information stored on behalf of the specified ZigBee end device on the Primary
    /// Discovery Cache device.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class RemoveNodeCacheRequest : ZdoRequest
    {
        /// <summary>
        /// The ZDO cluster ID.
        /// </summary>
        public const ushort CLUSTER_ID = 0x001B;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public RemoveNodeCacheRequest()
        {
            ClusterId = CLUSTER_ID;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("RemoveNodeCacheRequest [");
            builder.Append(base.ToString());
            builder.Append(']');

            return builder.ToString();
        }
    }
}
