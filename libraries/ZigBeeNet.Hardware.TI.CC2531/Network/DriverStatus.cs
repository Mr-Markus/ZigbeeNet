using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.Hardware.TI.CC2531.Network
{
    public enum DriverStatus
    {
        /// <summary>
        /// The driver has been created and it will start to initialize all the hardware resources
        /// and the ZigBee network (i.e.: it will either join or create a network). 
        /// </summary>
        CREATED,
        /// <summary>
        /// The driver has opened the hardware resources, and it is waiting for
        /// the hardware to complete the reset process
        /// </summary>
        HARDWARE_OPEN,
        /// <summary>
        /// The driver has already initialized all the hardware resources, and it is waiting for
        /// the hardware to complete the initialization process
        /// </summary>
        HARDWARE_INITIALIZING,
        /// <summary>
        /// The all the hardware resources have been initialized successfully, it will start to
        /// initialize the ZigBee network
        /// </summary>
        HARDWARE_READY,
        /// <summary>
        /// The driver has already initialized the ZigBee network, and it is waiting for
        /// the completion of process (i.e.: it joined to the network and it is waiting for
        /// a network address)
        /// </summary>
        NETWORK_INITIALIZING,
        /// <summary>
        /// The driver successfully joined to or create the ZigBee network
        /// </summary>
        NETWORK_READY,
        /// <summary>
        /// The driver is closed, no resources is in use
        /// </summary>
        CLOSED
    }
}
