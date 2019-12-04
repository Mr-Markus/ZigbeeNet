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
    /// Power Descriptor Store Request value object class.
    ///
    ///
    /// The Power_Desc_store_req is provided to enable ZigBee end devices on the network to
    /// request storage of their Power Descriptor on a Primary Discovery Cache device which has
    /// previously received a SUCCESS status from a Discovery_store_req to the same Primary
    /// Discovery Cache device. Included in this request is the Power Descriptor the Local
    /// Device wishes to cache.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class PowerDescriptorStoreRequest : ZdoRequest
    {
        /// <summary>
        /// The ZDO cluster ID.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0018;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public PowerDescriptorStoreRequest()
        {
            ClusterId = CLUSTER_ID;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("PowerDescriptorStoreRequest [");
            builder.Append(base.ToString());
            builder.Append(']');

            return builder.ToString();
        }
    }
}
