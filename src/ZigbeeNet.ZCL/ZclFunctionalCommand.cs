using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet.ZCL
{
    public class ZclFunctionalCommand : ZclCommand
    {
        internal string ClusterName { get; set; }

        public ZclCluster Cluster { get; set; }

        public Direction Direction { get; set; }
    }
}
