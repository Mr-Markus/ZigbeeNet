using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.Hardware.Digi.XBee.Internal.Protocol;

namespace ZigBeeNet.Hardware.Digi.XBee.Internal
{
    /// <summary>
    /// Interface to receive notifications of received <see cref="IXBeeEvent"/> from the <see cref="XBeeFrameHandler"/>
    /// </summary>
    public interface IXBeeEventListener
    {
        /// <summary>
        /// Listeners are called when a new <see cref="IXBeeEvent"/> is received
        /// </summary>
        /// <typeparam name="T">Expected type of xbee event</typeparam>
        /// <param name="xbeeEvent">The received <see cref="IXBeeEvent"/>.</param>
        void XbeeEventReceived(IXBeeEvent xbeeEvent);
    }
}
