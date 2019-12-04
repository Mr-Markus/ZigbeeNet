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
    /// Active Endpoint Store Request value object class.
    ///
    ///
    /// The Active_EP_store_req is provided to enable ZigBee end devices on the network to
    /// request storage of their list of Active Endpoints on a Primary Discovery Cache device
    /// which has previously received a SUCCESS status from a Discovery_store_req to the same
    /// Primary Discovery Cache device. Included in this request is the count of Active
    /// Endpoints the Local Device wishes to cache and the endpoint list itself.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class ActiveEndpointStoreRequest : ZdoRequest
    {
        /// <summary>
        /// The ZDO cluster ID.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0019;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ActiveEndpointStoreRequest()
        {
            ClusterId = CLUSTER_ID;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("ActiveEndpointStoreRequest [");
            builder.Append(base.ToString());
            builder.Append(']');

            return builder.ToString();
        }
    }
}
