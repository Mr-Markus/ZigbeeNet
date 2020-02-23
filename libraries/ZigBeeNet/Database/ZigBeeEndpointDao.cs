using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.Database
{
    public class ZigBeeEndpointDao
    {
        public ushort ProfileId { get; set; }
        public byte EndpointId { get; set; }
        public ushort DeviceId { get; set; }
        public int DeviceVersion { get; set; }

        public List<ZclClusterDao> InputClusters { get; set; }

        public List<ZclClusterDao> OutputClusters { get; set; }


        public void SetInputClusters(List<ZclClusterDao> clusters)
        {
            InputClusters.AddRange(clusters);
        }

        public void SetOutputClusters(List<ZclClusterDao> clusters)
        {
            OutputClusters.AddRange(clusters);
        }

        public ZigBeeEndpointDao()
        {
            InputClusters = new List<ZclClusterDao>();
            OutputClusters = new List<ZclClusterDao>();
        }
    }
}
