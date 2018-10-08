using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet.ZCL
{
    public class ZclCluster
    {
        public ushort Id { get; set; }
        public string Name { get; set; }

        public List<ZclAttribute> Attributes { get; }
        public List<ZclClusterCommand> Requests { get; }
        public List<ZclClusterCommand> Responses { get; }

        public ZclCluster()
        {
            Attributes = new List<ZclAttribute>();
            Requests = new List<ZclClusterCommand>();
            Responses = new List<ZclClusterCommand>();
        }
    }
}
