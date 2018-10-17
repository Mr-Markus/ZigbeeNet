using System;
using System.Collections.Generic;
using System.Text;
using ZigbeeNet.ZCL;

namespace ZigbeeNet.CC
{
    public class Endpoint
    {
        public byte Id { get; set; }

        public ushort ProfileId { get; set; }

        public Device Device { get; set; }

        public List<Cluster> InClusters { get; set; }

        public List<Cluster> OutClusters { get; set; }

        public List<Cluster> ClusterList
        {
            get
            {
                List<Cluster> clusterList = new List<Cluster>();

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
                if (this.ProfileId < 0x8000 && this.Device.Id < 0xc000)
                {
                    return true;
                } 

                return false;
            }
        }

        public Endpoint(Device device)
        {
            Device = device;
            InClusters = new List<Cluster>();
            OutClusters = new List<Cluster>();
        }
    }
}
