using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet
{
    public enum ZigBeeNodeState : byte
    {
        ///
        /// Node state is not currently known.
        ///
        UNKNOWN,

        ///
        ///The node is online and believed to be connected to the network.
        ///
        ONLINE,

        ///
        /// The node is offline - it has left the network.
        ///
        OFFLINE
    }
}
