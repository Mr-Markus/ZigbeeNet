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
    /// Analog Input (BACnet Regular)cluster implementation (Cluster ID 0x0602).
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class ZclAnalogInputBACnetRegularCluster : ZclCluster
    {
        /// <summary>
        /// The ZigBee Cluster Library Cluster ID
        /// </summary>
        public const ushort CLUSTER_ID = 0x0602;

        /// <summary>
        /// The ZigBee Cluster Library Cluster Name
        /// </summary>
        public const string CLUSTER_NAME = "Analog Input (BACnet Regular)";

        // Attribute initialisation
        protected override Dictionary<ushort, ZclAttribute> InitializeAttributes()
        {
            Dictionary<ushort, ZclAttribute> attributeMap = new Dictionary<ushort, ZclAttribute>(0);

            return attributeMap;
        }

        /// <summary>
        /// Default constructor to create a Analog Input (BACnet Regular) cluster.
        ///
        /// <param name ="zigbeeEndpoint">The ZigBeeEndpoint</param>
        /// </summary>
        public ZclAnalogInputBACnetRegularCluster(ZigBeeEndpoint zigbeeEndpoint)
            : base(zigbeeEndpoint, CLUSTER_ID, CLUSTER_NAME)
        {
        }

    }
}
