using System;
using System.Text;
using ZigBeeNet.Transaction;
using ZigBeeNet.ZCL;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.ZDO.Command
{
    /// <summary>
    /// Node Descriptor Store Request value object class.
    /// 
    /// The Node_Desc_store_req is provided to enable ZigBee end devices on the
    /// network to request storage of their Node Descriptor on a Primary Discovery
    /// Cache device which has previously received a SUCCESS Status from a
    /// Discovery_store_req to the same Primary Discovery Cache device. Included in
    /// this request is the Node Descriptor the Local Device wishes to cache.
    /// 
    /// </summary>

    public class NodeDescriptorStoreRequest : ZdoRequest
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public NodeDescriptorStoreRequest()
        {
            ClusterId = 0x0017;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append("NodeDescriptorStoreRequest [")
                   .Append(base.ToString())
                   .Append(']');

            return builder.ToString();
        }
    }
}
