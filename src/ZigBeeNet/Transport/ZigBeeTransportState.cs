using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.Transport
{
    public enum ZigBeeTransportState
    {
        /// <summary>
        /// Network has not yet been initialised
        /// </summary>
        UNINITIALISED,
        /// <summary>
        /// Network is currently initialising
        /// </summary>
        INITIALISING,
        /// <summary>
        /// Network is online and able to be used
        /// </summary>
        ONLINE,
        /// <summary>
        /// Network is offline and not able to be used
        /// </summary>
        OFFLINE,
        /// <summary>
        /// The network has been closed and may not be restarted
        /// </summary>
        SHUTDOWN
    }
}
