using System;
using System.Collections.Generic;
using System.Text;
using ZigbeeNet.ZCL;

namespace ZigbeeNet
{
    public class ZigbeeEndpoint
    {
        public byte Id { get; set; }

        public ZigbeeProfileType ProfileId { get; set; }

        public ZigBeeNode Node { get; set; }

        public List<ZclClusterId> InClusters { get; set; }

        public List<ZclClusterId> OutClusters { get; set; }

        public List<ZclClusterId> ClusterList
        {
            get
            {
                List<ZclClusterId> clusterList = new List<ZclClusterId>();

                clusterList.AddRange(InClusters);
                clusterList.AddRange(OutClusters);

                clusterList.Sort();

                return clusterList;
            }
        }

        public ZigbeeEndpoint(ZigBeeNode node = null)
        {
            Node = node;
            InClusters = new List<ZclClusterId>();
            OutClusters = new List<ZclClusterId>();
        }
    }
}
