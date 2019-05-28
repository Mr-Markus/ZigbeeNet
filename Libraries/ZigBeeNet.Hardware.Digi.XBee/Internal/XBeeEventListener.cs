using System;
using ZigBeeNet.Hardware.Digi.XBee.Internal.Protocol;

namespace ZigBeeNet.Hardware.Digi.XBee.Internal
{
    public class XBeeEventListener : IXBeeEventListenerProperties
    {
        #region fields

        private readonly object _lockObject = new object();
        private readonly Type _eventClass;

        #endregion fields

        #region constructor

        public XBeeEventListener(Type eventClass)
        {
            _eventClass = eventClass;
        }

        #endregion constructor

        #region properties

        /// <summary>
        /// Gets or sets the received event.
        /// </summary>
        /// <value>
        /// The received event.
        /// </value>
        public IXBeeEvent ReceivedEvent { get; set; }

        /// <summary>
        /// Gets a value indicating whether this <see cref="IXBeeEventListener"/> is complete.
        /// </summary>
        /// <value>
        ///   <c>true</c> if complete; otherwise, <c>false</c>.
        /// </value>
        public bool Complete { get; set; }

        #endregion properties

        #region methods

        /// <summary>
        /// Listeners are called when a new <see cref="IXBeeEvent"/> is received
        /// </summary>
        /// <typeparam name="T">Expected type of xbee event</typeparam>
        /// <param name="xbeeEvent">The received <see cref="IXBeeEvent"/>.</param>
        public void XbeeEventReceived(IXBeeEvent xbeeEvent)
        {
            if (xbeeEvent.GetType() != _eventClass)
            {
                return;
            }

            ReceivedEvent = xbeeEvent;

            lock (_lockObject)
            {
                Complete = true;
            }
        }

        #endregion methods
    }
}
