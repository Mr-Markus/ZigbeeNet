using System;
using System.Collections.Generic;

namespace ZigBeeNet.ZCL
{
    /// <summary>
    /// Custom cluster used as a placeholder for unknown clusters
    /// </summary>
    public class ZclCustomCluster : ZclCluster
    {
        protected override Dictionary<ushort, ZclAttribute> InitializeClientAttributes()
        {
            return new Dictionary<ushort, ZclAttribute>(0);
        }

        protected override Dictionary<ushort, ZclAttribute> InitializeServerAttributes()
        {
            return new Dictionary<ushort, ZclAttribute>(0);
        }

        /// <summary>
        /// Default constructor to create a Basic cluster.
        ///
        /// <param name="zigbeeEndpoint"> the ZigBeeEndpoint this cluster is contained within </param>
        /// </summary>
        public ZclCustomCluster(ZigBeeEndpoint zigbeeEndpoint, ushort clusterId)
            : base(zigbeeEndpoint, clusterId, $"Custom Cluster #{clusterId}")
        {
        }
    }
}
