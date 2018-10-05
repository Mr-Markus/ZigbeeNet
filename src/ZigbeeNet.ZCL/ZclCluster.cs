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
        public List<ZclFunctionalCommand> Requests { get; }
        public List<ZclFunctionalCommand> Responses { get; }

        public ZclCluster()
        {
            Attributes = new List<ZclAttribute>();
            Requests = new List<ZclFunctionalCommand>();
            Responses = new List<ZclFunctionalCommand>();
        }
    }
}
