using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZigBeeNet.Transaction;

namespace ZigBeeNet
{
    /// <summary>
     /// ZigBee network interface. It provides an interface for higher layers to receive information about the network and
     /// also provides services for the <see cref="ZigBeeTransportTransmit"> to provide network updates and incoming commands.
     /// </summary>
    public interface IZigBeeNetwork
    {
        /// <summary>
         /// Sends ZigBee command without waiting for response.
         ///
         /// <param name="command">the <see cref="ZigBeeCommand"> to send</param>
         /// </summary>
        void SendTransaction(ZigBeeCommand command);

        /// <summary>
         /// Sends <see cref="ZigBeeCommand"> command and uses the <see cref="ZigBeeTransactionMatcher"> to match the response.
         ///
         /// <param name="command">the <see cref="ZigBeeCommand"> to send</param>
         /// <param name="responseMatcher">the <see cref="ZigBeeTransactionMatcher"> used to match the response to the request</param>
         /// <returns>the <see cref="CommandResult"> future.</returns>
         /// </summary>
        Task<CommandResult> SendTransaction(ZigBeeCommand command, IZigBeeTransactionMatcher responseMatcher);

        /// <summary>
         /// Adds ZigBee library command listener.
         ///
         /// <param name="commandListener">the <see cref="ZigBeeCommandListener"></param>
         /// </summary>
        void AddCommandListener(IZigBeeCommandListener commandListener);

        /// <summary>
         /// Removes ZigBee library command listener.
         ///
         /// <param name="commandListener">the <see cref="ZigBeeCommandListener"></param>
         /// </summary>
        void RemoveCommandListener(IZigBeeCommandListener commandListener);
    }
}
