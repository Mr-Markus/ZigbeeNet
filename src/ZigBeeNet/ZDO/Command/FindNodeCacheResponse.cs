using System;
using System.Text;
using ZigBeeNet.Transaction;
using ZigBeeNet.ZCL;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.ZDO.Command
{
    /// <summary>
    /// Find Node Cache Response value object class.
    /// 
    /// The Find_node_cache_rsp is provided to notify a Local Device of the successful
    /// discovery of the Primary Discovery Cache device for the given NWKAddr and
    /// IEEEAddr fields supplied in the request, or to signify that the device of interest is
    /// capable of responding to discovery requests. The Find_node_cache_rsp shall be
    /// generated only by Primary Discovery Cache devices holding discovery
    /// information for the NWKAddr and IEEEAddr in the request or the device of
    /// interest itself and all other Remote Devices shall not supply a response.
    /// 
    /// </summary>
    public class FindNodeCacheResponse : ZdoResponse
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public FindNodeCacheResponse()
        {
            ClusterId = 0x801C;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append("FindNodeCacheResponse [")
                   .Append(base.ToString())
                   .Append(']');

            return builder.ToString();
        }

    }
}
