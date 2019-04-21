using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.Hardware.Digi.XBee.Internal.Protocol;

namespace ZigBeeNet.Hardware.Digi.XBee.Internal
{
    public interface IXBeeListener
    {
        /// <summary>
        /// Transactions the event.
        /// </summary>
        /// <param name="response">The response.</param>
        /// <returns></returns>
        bool TransactionEvent(IXBeeResponse response);
    }
}
