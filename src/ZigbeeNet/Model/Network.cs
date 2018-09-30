using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet
{
    public class Network
    {
        /// <summary>
        ///  Part of 802.15.4, the PAN ID is a 16-bit identifier selected at random by the Coordinator when 
        ///  the network starts up and is used by the MAC layer to filter out packets that are not part of the same network
        /// </summary>
        public ushort PanId { get; set; }
        /// <summary>
        ///  A ZigBee concept, the EPID is a 64-bit identifier which can be used for more fine grained uniqueness and identification 
        ///  of ZigBee network. This is not sent in all packets but can be used in some situations such as when resolving PAN ID conflicts
        /// </summary>
        public double ExtendedPanId { get; set; }
        /// <summary>
        ///  Also sometimes called the “short address” this is a 16-bit address that is unique only within an individual ZigBee network. 
        ///  The NwkAddr is assigned when a device joins the network and can change if it leaves and re-joins the network
        /// </summary>
        public ushort NetworkAddress { get; set; }
        /// <summary>
        ///  Sometimes referred to as the “extended address” this is a 64-bit address just like the MAC addresses you may be used to in the world of Ethernet
        /// </summary>
        public double IeeeAddress { get; set; }
        /// <summary>
        /// The Status of the Network. It can be offline or online
        /// </summary>
        public Status Status { get; set; }

        public byte Channel { get; set; }
    }
}
