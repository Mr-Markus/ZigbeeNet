using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet.ZCL
{
    public abstract class ZclCluster
    {
        public ZigbeeEndpoint ZigbeeEndpoint { get; set; }

        private bool _isClient = false;

        protected ushort ClusterId { get; set; }

        protected string Name { get; set; }

        public Dictionary<ushort, ZclAttribute> Attributes { get; set; }

        public ZclCluster()
        {
            Attributes = new Dictionary<ushort, ZclAttribute>();
        }
    }
}
