using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.Transport
{
    public enum ZigBeeTransportState
    {
        /// <summary>
        /// Transport has not yet been initialised
        /// </summary>
        UNINITIALISED,
        /// <summary>
        /// Transport is currently initialising
        /// </summary>
        INITIALISING,
        /// <summary>
        /// Transport is online and able to be used
        /// </summary>
        ONLINE,
        /// <summary>
        /// Transport is offline and not able to be used
        /// </summary>
        OFFLINE
    }
}
