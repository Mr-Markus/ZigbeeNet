using System;
using System.Collections.Generic;
using System.Text;
using ZigbeeNet.ZCL;

namespace ZigbeeNet.CC
{
    public class Endpoint
    {
        public byte Id { get; set; }

        public Device Device { get; set; }

        public List<Clusters> InClusters { get; set; }

        public List<Clusters> OutClusters { get; set; }

        public Endpoint(Device device)
        {
            Device = device;
            InClusters = new List<Clusters>();
            OutClusters = new List<Clusters>();
        }
    }
}
