using BinarySerialization;
using System;
using System.Collections.Generic;
using System.Text;
using ZigbeeNet.ZCL;

namespace ZigbeeNet.CC
{
    public class Network
    {
        /// <summary>
        ///  Part of 802.15.4, the PAN ID is a 16-bit identifier selected at random by the Coordinator when 
        ///  the network starts up and is used by the MAC layer to filter out packets that are not part of the same network
        /// </summary>
        [FieldOrder(0)]
        public ushort PanId { get; set; }
        /// <summary>
        ///  A ZigBee concept, the EPID is a 64-bit identifier which can be used for more fine grained uniqueness and identification 
        ///  of ZigBee network. This is not sent in all packets but can be used in some situations such as when resolving PAN ID conflicts
        /// </summary>
        [Ignore()]
        public double ExtendedPanId { get; set; }
        /// <summary>
        ///  Also sometimes called the “short address” this is a 16-bit address that is unique only within an individual ZigBee network. 
        ///  The NwkAddr is assigned when a device joins the network and can change if it leaves and re-joins the network
        /// </summary>
        [Ignore()]
        public ushort NetworkAddress { get; set; }
        /// <summary>
        ///  Sometimes referred to as the “extended address” this is a 64-bit address just like the MAC addresses you may be used to in the world of Ethernet
        /// </summary>
        [Ignore()]
        public double IeeeAddress { get; set; }
        /// <summary>
        /// The Status of the Network. It can be offline or online
        /// </summary>
        [Ignore()]
        public ZpiStatus Status { get; set; }
        
        [FieldOrder(1)]
        public byte Channel { get; set; }

        [FieldOrder(2)]
        [FieldBitLength(4)]
        public byte StackProfile { get; set; }

        [FieldOrder(3)]
        [FieldBitLength(4)]
        public byte ZigbeeVersion { get; set; }

        [FieldOrder(4)]
        [FieldBitLength(4)]
        public byte BeaconOrder { get; set; }

        [FieldOrder(5)]
        [FieldBitLength(4)]
        public byte SuperFrameOrder { get; set; }

        [FieldOrder(5)]
        [FieldLength(1)]
        public bool PermitJoining { get; set; }
    }
}
