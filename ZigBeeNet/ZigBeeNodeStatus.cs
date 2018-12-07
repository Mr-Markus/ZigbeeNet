using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet
{
    public enum ZigBeeNodeStatus : byte
    {
        /**
      * A device has joined the network without security
      */
        UNSECURED_JOIN,

        /**
         * A device has securely rejoined the network
         */
        SECURED_REJOIN,

        /**
         * A device has unsecurely rejoined the network
         */
        UNSECURED_REJOIN,

        /**
         * A device has left the network
         */
        DEVICE_LEFT

    }
}
