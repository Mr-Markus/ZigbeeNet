// License text here

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ZigBeeNet.DAO;
using ZigBeeNet.ZCL.Protocol;
using ZigBeeNet.ZCL.Field;

namespace ZigBeeNet.ZCL.Clusters
{
    /// <summary>
    /// Multistate Value (BACnet Extended)cluster implementation (Cluster ID 0x0613).
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class ZclMultistateValueBACnetExtendedCluster : ZclCluster
    {
        /// <summary>
        /// The ZigBee Cluster Library Cluster ID
        /// </summary>
        public const ushort CLUSTER_ID = 0x0613;

        /// <summary>
        /// The ZigBee Cluster Library Cluster Name
        /// </summary>
        public const string CLUSTER_NAME = "Multistate Value (BACnet Extended)";

        // Attribute initialisation
        protected override Dictionary<ushort, ZclAttribute> InitializeAttributes()
        {
            Dictionary<ushort, ZclAttribute> attributeMap = new Dictionary<ushort, ZclAttribute>(0);

            return attributeMap;
        }

        /// <summary>
        /// Default constructor to create a Multistate Value (BACnet Extended) cluster.
        ///
        /// <param name ="zigbeeEndpoint">The ZigBeeEndpoint</param>
        /// </summary>
        public ZclMultistateValueBACnetExtendedCluster(ZigBeeEndpoint zigbeeEndpoint)
            : base(zigbeeEndpoint, CLUSTER_ID, CLUSTER_NAME)
        {
        }

    }
}
