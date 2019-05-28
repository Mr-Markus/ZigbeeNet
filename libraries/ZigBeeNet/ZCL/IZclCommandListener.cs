using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.ZCL
{
    /// <summary>
    /// Command listener. Listeners are called when an ZclCommand for the Cluster is received.
    /// </summary>
    public interface IZclCommandListener
    {
        /// <summary>
         /// Called when a ZclCommand is received for this cluster.
         /// </summary>
        void CommandReceived(ZclCommand command);
    }
}
