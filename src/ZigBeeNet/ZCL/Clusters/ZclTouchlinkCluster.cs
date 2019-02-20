using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.ZCL.Clusters
{
    public class ZclTouchlinkCluster : ZclCluster
    {
        /**
         * The ZigBee Cluster Library Cluster ID
         */
        public static ushort CLUSTER_ID = 0x1000;

        /**
         * The ZigBee Cluster Library Cluster Name
         */
        public static string CLUSTER_NAME = "Touchlink";

        // Attribute initialisation
        protected override Dictionary<ushort, ZclAttribute> InitializeAttributes()
        {
            Dictionary<ushort, ZclAttribute> attributeMap = new Dictionary<ushort, ZclAttribute>(0);

            return attributeMap;
        }

        /**
         * Default constructor to create a Touchlink cluster.
         *
         * @param zigbeeEndpoint the {@link ZigBeeEndpoint}
         */
        public ZclTouchlinkCluster(ZigBeeEndpoint zigbeeEndpoint)
            : base(zigbeeEndpoint, CLUSTER_ID, CLUSTER_NAME)
        {
            
        }
    }
}
