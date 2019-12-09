using System.Collections.Generic;

namespace ZigBeeNet.DAO
{
    public class ZclClusterDao
    {
        public string ClusterName { get; set; }

        public ushort ClusterId { get; set; }

        public bool IsClient { get; set; }

        public Dictionary<ushort, ZclAttributeDao> Attributes { get; set; }

        public List<byte> SupportedCommandsReceived { get; set; }

        public List<byte> SupportedCommandsGenerated { get; set; }

        public List<ushort> SupportedAttributes { get; set; }
    }
}