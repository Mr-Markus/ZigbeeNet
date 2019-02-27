using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.DAO;
using ZigBeeNet.ZDO.Field;

namespace ZigBeeNet.DAO
{
    /// <summary>
 /// This class provides a clean class to hold a data object for serialisation of a {@link ZigBeeNode}
 /// </summary>
    public class ZigBeeNodeDao
    {
        /// <summary>
         /// The extended {@link IeeeAddress} for the node
         /// </summary>
        public string IeeeAddress { get; set; }

        /// <summary>
         /// The 16 bit network address for the node
         /// </summary>
        public ushort NetworkAddress { get; set; }

        /// <summary>
         /// The {@link NodeDescriptor} for the node
         /// </summary>
        public NodeDescriptor NodeDescriptor { get; set; }

        /// <summary>
         /// The {@link PowerDescriptor} for the node
         /// </summary>
        public PowerDescriptor PowerDescriptor { get; set; }

        /// <summary>
         /// The list of endpoints for this node
         /// </summary>
        public List<ZigBeeEndpointDao> Endpoints { get; set; }

        public List<BindingTable> BindingTable { get; set; }
    }
}
