using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ZigBeeNet.Hardware.Ember.Ezsp;
using ZigBeeNet.Hardware.Ember.Transaction;
using ZigBeeNet.Transport;

namespace ZigBeeNet.Hardware.Ember.Internal
{
    /// <summary>
    /// Interface to exchange asynchronous packets and link state changes from the low level protocol handlers
    /// (ASH/SPI) to the EZSP layer.
    /// </summary>
    public interface IEzspFrameHandler
    {
        /**
         * Passes received asynchronous frames from the ASH handler to the EZSP layer
         *
         * @param response incoming {@link EzspFrame} response frame
         */
        void HandlePacket(EzspFrame response);

        /**
         * Called when the ASH link state changes
         *
         * @param state true if the link is UP, false if the link is DOWN
         */
        void HandleLinkStateChange(bool state);
    }
}
