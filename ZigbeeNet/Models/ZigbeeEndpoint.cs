using System;
using System.Collections.Generic;
using System.Text;
using ZigbeeNet.ZCL;

namespace ZigbeeNet
{
    public class ZigbeeEndpoint
    {
        public byte Id { get; set; }

        public DoubleByte ProfileId { get; set; }

        public ZigbeeNode Device { get; set; }

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

        public bool IsZclSupported
        {
            get
            {
                if (ProfileId.Value < 0x8000 && Device.Id < 0xc000)
                {
                    return true;
                } 

                return false;
            }
        }

        public ZigbeeEndpoint(ZigbeeNode device)
        {
            Device = device;
            InClusters = new List<ZclCluster>();
            OutClusters = new List<ZclCluster>();
        }
    }
}
