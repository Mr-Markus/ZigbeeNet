using System;
using System.Text;
using ZigBeeNet.Transaction;
using ZigBeeNet.ZCL;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.ZDO.Command
{
    /// <summary>
    /// Remove Node Cache value object class.
    /// 
    /// The Remove_node_cache_rsp is provided to notify a Local Device of the request
    /// Status from a Primary Discovery Cache device. Included in the response is a Status
    /// code to notify the Local Device whether the request is successful (the Primary
    /// Cache Device has removed the discovery cache data for the indicated device of
    /// interest), or the request is not supported (meaning the Remote Device is not a
    /// Primary Discovery Cache device).
    /// 
    /// </summary>
    public class RemoveNodeCache : ZdoResponse
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public RemoveNodeCache()
        {
            ClusterId = 0x801B;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append("RemoveNodeCache [")
                   .Append(base.ToString())
                   .Append(']');

            return builder.ToString();
        }
    }
}
