using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.ZDO.Field;

namespace ZigBeeNet.Database
{
    /// <summary>
    /// This class provides a clean class to hold a data object for serialisation of a <see cref="ZigBeeNode">
    /// </summary>
    public class ZigBeeNodeDao
    {
        /// <summary>
        /// The extended <see cref="IeeeAddress"> for the node
        /// </summary>
        public IeeeAddress IeeeAddress { get; set; }

        /// <summary>
        /// The 16 bit network address for the node
        /// </summary>
        public ushort NetworkAddress { get; set; }

        /// <summary>
        /// The <see cref="NodeDescriptor"> for the node
        /// </summary>
        public NodeDescriptor NodeDescriptor { get; set; }

        /// <summary>
        /// The <see cref="PowerDescriptor"> for the node
        /// </summary>
        public PowerDescriptor PowerDescriptor { get; set; }

        /// <summary>
        /// The list of endpoints for this node
        /// </summary>
        public List<ZigBeeEndpointDao> Endpoints { get; set; }

        public HashSet<BindingTable> BindingTable { get; set; }
    }
}
