using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.ZCL;

namespace ZigBeeNet.DAO
{
    public class ZclClusterDao
    {
        public string Label { get; set; }

        public ushort ClusterId { get; set; }

        public bool IsClient { get; set; }

        public Dictionary<ushort, ZclAttribute> Attributes { get; set; }

        public List<byte> SupportedCommandsReceived { get; set; }

        public List<byte> SupportedCommandsGenerated { get; set; }

        public List<ushort> SupportedAttributes { get; set; }
    }
}