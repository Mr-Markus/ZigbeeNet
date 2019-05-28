using System;
using System.Text;
using ZigBeeNet.Transaction;
using ZigBeeNet.ZCL;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.ZDO.Command
{
    /// <summary>
    /// Node Descriptor Store Response value object class.
    /// 
    /// The Node_store_rsp is provided to notify a Local Device of the request Status
    /// from a Primary Discovery Cache device. Included in the response is a Status code
    /// to notify the Local Device whether the request is successful (the Primary Cache
    /// Device has space to store the discovery cache data for the Local Device), whether
    /// the request is not supported (meaning the Remote Device is not a Primary
    /// Discovery Cache device), or insufficient space exists.
    /// 
    /// </summary>

    public class NodeDescriptorStoreResponse : ZdoResponse
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public NodeDescriptorStoreResponse()
        {
            ClusterId = 0x8017;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append("NodeDescriptorStoreResponse [")
                   .Append(base.ToString())
                   .Append(']');

            return builder.ToString();
        }
    }
}
