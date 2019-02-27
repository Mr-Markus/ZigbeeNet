using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZigBeeNet.Transaction;

namespace ZigBeeNet
{
    /// <summary>
     /// ZigBee network interface. It provides an interface for higher layers to receive information about the network and
     /// also provides services for the {@link ZigBeeTransportTransmit} to provide network updates and incoming commands.
     /// </summary>
    public interface IZigBeeNetwork
    {
        /// <summary>
         /// Sends ZigBee command without waiting for response.
         ///
         /// @param command the {@link ZigBeeCommand} to send
         /// </summary>
        void SendTransaction(ZigBeeCommand command);

        /// <summary>
         /// Sends {@link ZigBeeCommand} command and uses the {@link ZigBeeTransactionMatcher} to match the response.
         ///
         /// @param command the {@link ZigBeeCommand} to send
         /// @param responseMatcher the {@link ZigBeeTransactionMatcher} used to match the response to the request
         /// @return the {@link CommandResult} future.
         /// </summary>
        Task<CommandResult> SendTransaction(ZigBeeCommand command, IZigBeeTransactionMatcher responseMatcher);

        /// <summary>
         /// Adds ZigBee library command listener.
         ///
         /// @param commandListener the {@link ZigBeeCommandListener}
         /// </summary>
        void AddCommandListener(IZigBeeCommandListener commandListener);

        /// <summary>
         /// Removes ZigBee library command listener.
         ///
         /// @param commandListener the {@link ZigBeeCommandListener}
         /// </summary>
        void RemoveCommandListener(IZigBeeCommandListener commandListener);
    }
}
