using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.DAO;
using ZigBeeNet.ZDO.Field;

namespace ZigBeeNet.DAO
{
    /**
 * This class provides a clean class to hold a data object for serialisation of a {@link ZigBeeNode}
 */
    public class ZigBeeNodeDao
    {
        /**
         * The extended {@link IeeeAddress} for the node
         */
        public string IeeeAddress { get; set; }

        /**
         * The 16 bit network address for the node
         */
        public ushort NetworkAddress { get; set; }

        /**
         * The {@link NodeDescriptor} for the node
         */
        public NodeDescriptor NodeDescriptor { get; set; }

        /**
         * The {@link PowerDescriptor} for the node
         */
        public PowerDescriptor PowerDescriptor { get; set; }

        /**
         * The list of endpoints for this node
         */
        public List<ZigBeeEndpointDao> Endpoints { get; set; }

        public List<BindingTable> BindingTable { get; set; }
    }
}
