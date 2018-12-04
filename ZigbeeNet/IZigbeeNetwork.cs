using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ZigbeeNet
{
    /// <summary>
    /// ZigBee network interface. It provides an interface for higher layers to receive information about the network and
    /// also provides services for the ZigBeeTransportTransmit to provide network updates and incoming commands.
    /// </summary>
    public interface IZigbeeNetwork
    {
        /// <summary>
        /// Sends ZigBee command without waiting for response.
        /// </summary>
        /// <param name="command"></param>
        void SendTransaction(ZigBeeCommand command);

        /// <summary>
        /// Adds ZigBee library command listener.
        /// </summary>
        /// <param name="commandListener"></param>
        void AddCommandListener(IZigbeeCommandListener commandListener);

        /// <summary>
        /// Removes ZigBee library command listener.
        /// </summary>
        /// <param name="commandListener"></param>
        void RemoveCommandListener(IZigbeeCommandListener commandListener);
    }
}
