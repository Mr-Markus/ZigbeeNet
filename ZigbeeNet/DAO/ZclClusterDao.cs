using System;
using System.Collections.Generic;
using System.Text;
using ZigbeeNet.ZCL;

namespace ZigbeeNet.DAO
{
    public class ZclClusterDao
    {
        public string Label { get; set; }

        public int ClusterId { get; set; }

        public bool IsClient { get; set; }

        public Dictionary<int, ZclAttribute> Attributes { get; set; }

        public List<int> SupportedCommandsReceived { get; set; }

        public List<int> SupportedCommandsGenerated { get; set; }

        public List<int> SupportedAttributes { get; set; }
    }
}