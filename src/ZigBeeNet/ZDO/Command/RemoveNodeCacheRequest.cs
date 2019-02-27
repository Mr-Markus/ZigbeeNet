using System;
using System.Text;
using ZigBeeNet.Transaction;
using ZigBeeNet.ZCL;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.ZDO.Command
{
    /// <summary>
    /// Remove Node Cache Request value object class.
    /// 
    /// The Remove_node_cache_req is provided to enable ZigBee devices on the
    /// network to request removal of discovery cache information for a specified ZigBee
    /// end device from a Primary Discovery Cache device. The effect of a successful
    /// Remove_node_cache_req is to undo a previously successful Discovery_store_req
    /// and additionally remove any cache information stored on behalf of the specified
    /// ZigBee end device on the Primary Discovery Cache device.
    /// 
/// </summary>

    public class RemoveNodeCacheRequest : ZdoRequest
    {
        /// <summary>
        /// Default constructor.
/// </summary>
        public RemoveNodeCacheRequest()
        {
            ClusterId = 0x001B;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append("RemoveNodeCacheRequest [")
                   .Append(base.ToString())
                   .Append(']');

            return builder.ToString();
        }
    }
}
