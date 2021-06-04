using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet
{
    /// <summary>
    /// Defines the broadcast destination addresses defined in the ZigBee protocol.
    /// 
    /// Broadcast transmissions shall not use the MAC sub-layer
    /// acknowledgement; instead, a passive acknowledgement mechanism may be used.
    /// Passive acknowledgement means that every ZigBee router and ZigBee
    /// coordinator keeps track of which of its neighboring devices have successfully
    /// relayed the broadcast transmission. The MAC sub-layer acknowledgement is
    /// disabled by setting the acknowledged transmission flag of the TxOptions
    /// parameter to FALSE. All other flags of the TxOptions parameter shall be set based
    /// on the network configuration
    /// </summary>
    public enum ZigBeeBroadcastDestination : ushort
    {
        /// <summary>
        /// All devices in PAN
        /// </summary>
        BROADCAST_ALL_DEVICES = 0xFFFF,

        /// <summary>
        /// macRxOnWhenIdle = TRUE
        /// </summary>
        BROADCAST_RX_ON = 0xFFFD,

        /// <summary>
        /// All routers and coordinator
        /// </summary>
        BROADCAST_ROUTERS_AND_COORD = 0xFFFC,

        /// <summary>
        /// Low power routers only
        /// </summary>
        BROADCAST_LOW_POWER_ROUTERS = 0xFFFB,

        // Reserved values
        BROADCAST_RESERVED_FFFE = 0xFFFE,
        BROADCAST_RESERVED_FFFA = 0xFFFA,
        BROADCAST_RESERVED_FFF9 = 0xFFF9,
        BROADCAST_RESERVED_FFF8 = 0xFFF8,
    }

    public static class ZigBeeBroadcastDestinationHelper
    {
        public static bool IsBroadcast(this ZigBeeBroadcastDestination address) => IsBroadcast((ushort)address);
        public static bool IsBroadcast(ushort address) => (address >= (ushort)ZigBeeBroadcastDestination.BROADCAST_RESERVED_FFF8 && address <= (ushort)ZigBeeBroadcastDestination.BROADCAST_ALL_DEVICES);
    }
}
