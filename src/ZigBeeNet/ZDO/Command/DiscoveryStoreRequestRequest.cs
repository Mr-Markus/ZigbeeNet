using System;
using System.Text;
using ZigBeeNet.Transaction;
using ZigBeeNet.ZCL;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.ZDO.Command
{
    /// <summary>
    /// Discovery Store Request Request value object class.
    /// 
    /// The Discovery_store_req is provided to enable ZigBee end devices on the
    /// network to request storage of their discovery cache information on a Primary
    /// Discovery Cache device. Included in the request is the amount of storage space
    /// the Local Device requires.
    /// 
    /// </summary>
    public class DiscoveryStoreRequestRequest : ZdoRequest
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public DiscoveryStoreRequestRequest()
        {
            ClusterId = 0x0016;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append("DiscoveryStoreRequestRequest [")
                   .Append(base.ToString())
                   .Append(']');

            return builder.ToString();
        }

    }
}
