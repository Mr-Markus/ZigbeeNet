using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.ZCL.Clusters
{
    public class ZclTouchlinkCluster : ZclCluster
    {
        /// <summary>
         /// The ZigBee Cluster Library Cluster ID
         /// </summary>
        public static ushort CLUSTER_ID = 0x1000;

        /// <summary>
         /// The ZigBee Cluster Library Cluster Name
         /// </summary>
        public static string CLUSTER_NAME = "Touchlink";

        // Attribute initialisation
        protected override Dictionary<ushort, ZclAttribute> InitializeAttributes()
        {
            Dictionary<ushort, ZclAttribute> attributeMap = new Dictionary<ushort, ZclAttribute>(0);

            return attributeMap;
        }

        /// <summary>
         /// Default constructor to create a Touchlink cluster.
         ///
         /// <param name="zigbeeEndpoint">the <see cref="ZigBeeEndpoint"></param>
         /// </summary>
        public ZclTouchlinkCluster(ZigBeeEndpoint zigbeeEndpoint)
            : base(zigbeeEndpoint, CLUSTER_ID, CLUSTER_NAME)
        {
            
        }
    }
}
