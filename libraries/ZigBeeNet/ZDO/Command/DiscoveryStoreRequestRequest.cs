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
    /// Discovery Store Request Request value object class.
    ///
    ///
    /// The Discovery_store_req is provided to enable ZigBee end devices on the network to
    /// request storage of their discovery cache information on a Primary Discovery Cache
    /// device. Included in the request is the amount of storage space the Local Device
    /// requires.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class DiscoveryStoreRequestRequest : ZdoRequest
    {
        /// <summary>
        /// The ZDO cluster ID.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0016;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public DiscoveryStoreRequestRequest()
        {
            ClusterId = CLUSTER_ID;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("DiscoveryStoreRequestRequest [");
            builder.Append(base.ToString());
            builder.Append(']');

            return builder.ToString();
        }
    }
}
