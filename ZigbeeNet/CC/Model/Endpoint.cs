using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet.CC
{
    public class Endpoint
    {
        public byte Id { get; set; }

        public DoubleByte ProfileId { get; set; }

        public Device Device { get; set; }

        public List<DoubleByte> InClusters { get; set; }

        public List<DoubleByte> OutClusters { get; set; }

        public List<DoubleByte> ClusterList
        {
            get
            {
                List<DoubleByte> clusterList = new List<DoubleByte>();

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
                if (ProfileId .Get16BitValue()< 0x8000 && Device.Id < 0xc000)
                {
                    return true;
                } 

                return false;
            }
        }

        public Endpoint(Device device)
        {
            Device = device;
            InClusters = new List<DoubleByte>();
            OutClusters = new List<DoubleByte>();
        }
    }
}
