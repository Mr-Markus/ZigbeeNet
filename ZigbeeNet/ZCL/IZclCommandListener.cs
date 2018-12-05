using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.ZCL
{
    /**
    * Command listener. Listeners are called when an ZclCommand for the Cluster is received.
    */
    public interface IZclCommandListener
    {
        /**
         * Called when a ZclCommand is received for this cluster.
         */
        void CommandReceived(ZclCommand command);
    }
}
