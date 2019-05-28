using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.Hardware.TI.CC2531.Packet.AF;

namespace ZigBeeNet.Hardware.TI.CC2531.Network
{
    /// <summary>
    /// This class represent the callback invoked by the ZigBeeNetworkManager whenever a message
    /// Application Framework arrives from the ZigBee Network
    /// </summary>
    public interface IApplicationFrameworkMessageListener
    {
        /// <summary>
        /// This method is invoked by the ZigBeeNetworkManager on all the ApplicationFrameworkMessageListener
        /// when a AF_INCOMING_MSG command arrive from the ZigBee NIC
        /// </summary>
        /// <param name="msg">The AF_INCOMING_MSG arrived that has to be handled</param>
        /// <returns></returns>
        bool Notify(AF_INCOMING_MSG msg);
    }
}
