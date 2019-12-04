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
    /// Simple Descriptor Store value object class.
    ///
    ///
    /// The Simple_desc_store_req is provided to enable ZigBee end devices on the network to
    /// request storage of their list of Simple Descriptors on a Primary Discovery Cache device
    /// which has previously received a SUCCESS status from a Discovery_store_req to the same
    /// Primary Discovery Cache device. Note that each Simple Descriptor for every active
    /// endpoint on the Local Device must be individually uploaded to the Primary Discovery
    /// Cache device via this command to enable cached discovery. Included in this request is
    /// the length of the Simple Descriptor the Local Device wishes to cache and the Simple
    /// Descriptor itself. The endpoint is a field within the Simple Descriptor and is accessed
    /// by the Remote Device to manage the discovery cache information for the Local Device.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class SimpleDescriptorStore : ZdoRequest
    {
        /// <summary>
        /// The ZDO cluster ID.
        /// </summary>
        public const ushort CLUSTER_ID = 0x001A;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public SimpleDescriptorStore()
        {
            ClusterId = CLUSTER_ID;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("SimpleDescriptorStore [");
            builder.Append(base.ToString());
            builder.Append(']');

            return builder.ToString();
        }
    }
}
