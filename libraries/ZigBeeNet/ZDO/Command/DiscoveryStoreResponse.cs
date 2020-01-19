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
    /// Discovery Store Response value object class.
    ///
    ///
    /// The Discovery_store_rsp is provided to notify a Local Device of the request status from
    /// a Primary Discovery Cache device. Included in the response is a status code to notify the
    /// Local Device whether the request is successful (the Primary Cache Device has space to
    /// store the discovery cache data for the Local Device), whether the request is
    /// unsupported (meaning the Remote Device is not a Primary Discovery Cache device), or
    /// insufficient space exists.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class DiscoveryStoreResponse : ZdoResponse
    {
        /// <summary>
        /// The ZDO cluster ID.
        /// </summary>
        public const ushort CLUSTER_ID = 0x8016;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public DiscoveryStoreResponse()
        {
            ClusterId = CLUSTER_ID;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("DiscoveryStoreResponse [");
            builder.Append(base.ToString());
            builder.Append(']');

            return builder.ToString();
        }
    }
}
