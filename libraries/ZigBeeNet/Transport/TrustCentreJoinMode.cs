using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.Transport
{
    /// <summary>
    /// An enumeration with the Trust Center join mode.
    /// </summary>
    public enum TrustCentreJoinMode
    {
        /// <summary>
        /// The TC should deny joins. Even if a router allows a device to join the network, the trust center will not support
        /// the request, and will not deliver the network key to the device.
        /// </summary>
        TC_JOIN_DENY,
        /// <summary>
        /// The TC should allow joins using the link key, or a preconfigured link key / device specific install code.
        /// </summary>
        TC_JOIN_INSECURE,
        /// <summary>
        /// The TC should allow joins only with a preconfigured link key / device specific install code.
        /// </summary>
        TC_JOIN_SECURE
    }
}
