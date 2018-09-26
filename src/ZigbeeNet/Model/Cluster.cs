using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet
{
    public class Cluster
    {
        public ushort Identifier { get; set; }
        public string Name { get; set; }
        public List<ClusterAttribute> Attributes { get; set; }
    }
}
