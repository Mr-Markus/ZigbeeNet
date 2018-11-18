using System;
using System.Collections.Generic;
using System.Text;
using ZigbeeNet.ZCL;

namespace ZigbeeNet
{
    public class ZigbeeEndpoint
    {
        public byte Id { get; set; }

        public ZclProfile ProfileId { get; set; }

        public ZigbeeNode Node { get; set; }

        public List<ZclCluster> InClusters { get; set; }

        public List<ZclCluster> OutClusters { get; set; }

        public List<ZclCluster> ClusterList
        {
            get
            {
                List<ZclCluster> clusterList = new List<ZclCluster>();

                clusterList.AddRange(InClusters);
                clusterList.AddRange(OutClusters);

                clusterList.Sort();

                return clusterList;
            }
        }

        public ZigbeeEndpoint(ZigbeeNode node)
        {
            Node = node;
            InClusters = new List<ZclCluster>();
            OutClusters = new List<ZclCluster>();
        }
    }
}
