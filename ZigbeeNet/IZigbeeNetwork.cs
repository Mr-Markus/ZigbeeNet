using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZigBeeNet.Transaction;

namespace ZigBeeNet
{
    /**
     * ZigBee network interface. It provides an interface for higher layers to receive information about the network and
     * also provides services for the {@link ZigBeeTransportTransmit} to provide network updates and incoming commands.
     */
    public interface IZigBeeNetwork
    {
        /**
         * Sends ZigBee command without waiting for response.
         *
         * @param command the {@link ZigBeeCommand} to send
         */
        void SendTransaction(ZigBeeCommand command);

        /**
         * Sends {@link ZigBeeCommand} command and uses the {@link ZigBeeTransactionMatcher} to match the response.
         *
         * @param command the {@link ZigBeeCommand} to send
         * @param responseMatcher the {@link ZigBeeTransactionMatcher} used to match the response to the request
         * @return the {@link CommandResult} future.
         */
        Task<CommandResult> SendTransaction(ZigBeeCommand command, IZigBeeTransactionMatcher responseMatcher);

        /**
         * Adds ZigBee library command listener.
         *
         * @param commandListener the {@link ZigBeeCommandListener}
         */
        void AddCommandListener(IZigBeeCommandListener commandListener);

        /**
         * Removes ZigBee library command listener.
         *
         * @param commandListener the {@link ZigBeeCommandListener}
         */
        void RemoveCommandListener(IZigBeeCommandListener commandListener);
    }
}
