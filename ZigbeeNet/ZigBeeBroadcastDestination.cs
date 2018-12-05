using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet
{
    /**
     * Defines the broadcast destination addresses defined in the ZigBee protocol.
     * 
     * Broadcast transmissions shall not use the MAC sub-layer
     * acknowledgement; instead, a passive acknowledgement mechanism may be used.
     * Passive acknowledgement means that every ZigBee router and ZigBee
     * coordinator keeps track of which of its neighboring devices have successfully
     * relayed the broadcast transmission. The MAC sub-layer acknowledgement is
     * disabled by setting the acknowledged transmission flag of the TxOptions
     * parameter to FALSE. All other flags of the TxOptions parameter shall be set based
     * on the network configuration
     */
    public enum BroadcastDestination : ushort
    {
        /**
         * All devices in PAN
         */
        BROADCAST_ALL_DEVICES = 0xFFFF,

        /**
         * macRxOnWhenIdle = TRUE
         */
        BROADCAST_RX_ON = 0xFFFD,

        /**
         * All routers and coordinator
         */
        BROADCAST_ROUTERS_AND_COORD = 0xFFFC,

        /**
         * Low power routers only
         */
        BROADCAST_LOW_POWER_ROUTERS = 0xFFFB,

        // Reserved values
        BROADCAST_RESERVED_FFFE = 0xFFFE,
        BROADCAST_RESERVED_FFFA = 0xFFFA,
        BROADCAST_RESERVED_FFF9 = 0xFFF9,
        BROADCAST_RESERVED_FFF8 = 0xFFF8,
    }


    public class ZigBeeBroadcastDestination
    {
        /**
        * A mapping between the integer code and its corresponding type to
        * facilitate lookup by code.
        */
        private static Dictionary</*int*/BroadcastDestination, ZigBeeBroadcastDestination> _codeMapping;

        public ushort Key { get; private set; }

        public BroadcastDestination Destination { get; private set; }

        private ZigBeeBroadcastDestination(/*int key, */ BroadcastDestination destination)
        {
            this.Key = (ushort)destination;
            this.Destination = destination;
        }

        private static void InitMapping()
        {
            _codeMapping = new Dictionary</*int*/BroadcastDestination, ZigBeeBroadcastDestination>
            {
                { BroadcastDestination.BROADCAST_ALL_DEVICES,  new ZigBeeBroadcastDestination(BroadcastDestination.BROADCAST_ALL_DEVICES) },
                { BroadcastDestination.BROADCAST_RX_ON,  new ZigBeeBroadcastDestination(BroadcastDestination.BROADCAST_RX_ON) },
                { BroadcastDestination.BROADCAST_ROUTERS_AND_COORD,  new ZigBeeBroadcastDestination(BroadcastDestination.BROADCAST_ROUTERS_AND_COORD) },
                { BroadcastDestination.BROADCAST_LOW_POWER_ROUTERS,  new ZigBeeBroadcastDestination(BroadcastDestination.BROADCAST_LOW_POWER_ROUTERS) },
                { BroadcastDestination.BROADCAST_RESERVED_FFFE,  new ZigBeeBroadcastDestination(BroadcastDestination.BROADCAST_RESERVED_FFFE) },
                { BroadcastDestination.BROADCAST_RESERVED_FFFA,  new ZigBeeBroadcastDestination(BroadcastDestination.BROADCAST_RESERVED_FFFA) },
                { BroadcastDestination.BROADCAST_RESERVED_FFF9,  new ZigBeeBroadcastDestination(BroadcastDestination.BROADCAST_RESERVED_FFF9) },
                { BroadcastDestination.BROADCAST_RESERVED_FFF8,  new ZigBeeBroadcastDestination(BroadcastDestination.BROADCAST_RESERVED_FFF8) },
            };
        }

        /**
         * Lookup function based on the EmberApsOption type code. Returns null
         * if the code does not exist.
         */
        public static ZigBeeBroadcastDestination GetBroadcastDestination(ushort key)
        {
            if (_codeMapping == null)
            {
                InitMapping();
            }

            var i = (BroadcastDestination)key;

            if (_codeMapping.ContainsKey(i))
                return _codeMapping[i];
            else
                return null;
        }

        public static ZigBeeBroadcastDestination GetBroadcastDestination(BroadcastDestination dest)
        {
            return GetBroadcastDestination((ushort)dest);
        }
    }
}
