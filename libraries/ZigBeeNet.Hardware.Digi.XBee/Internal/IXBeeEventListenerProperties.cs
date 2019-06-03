using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.Hardware.Digi.XBee.Internal.Protocol;

namespace ZigBeeNet.Hardware.Digi.XBee.Internal
{
    public interface IXBeeEventListenerProperties : IXBeeEventListener
    {
        /// <summary>
        /// Gets or sets the received event.
        /// </summary>
        /// <value>
        /// The received event.
        /// </value>
        IXBeeEvent ReceivedEvent { get; set; }

        /// <summary>
        /// Gets a value indicating whether this <see cref="IXBeeEventListener"/> is complete.
        /// </summary>
        /// <value>
        ///   <c>true</c> if complete; otherwise, <c>false</c>.
        /// </value>
        bool Complete { get; set; }
    }
}
