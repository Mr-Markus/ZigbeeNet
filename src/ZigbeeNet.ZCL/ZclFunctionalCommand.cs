using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet.ZCL
{
    public class ZclFunctionalCommand : ZclCommand
    {
        public string Cluster { get; set; }

        public Direction Direction { get; set; }
    }
}
