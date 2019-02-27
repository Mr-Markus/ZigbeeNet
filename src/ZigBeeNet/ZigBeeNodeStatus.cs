using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet
{
    public enum ZigBeeNodeStatus : byte
    {
        /// <summary>
      /// A device has joined the network without security
      /// </summary>
        UNSECURED_JOIN,

        /// <summary>
         /// A device has securely rejoined the network
         /// </summary>
        SECURED_REJOIN,

        /// <summary>
         /// A device has unsecurely rejoined the network
         /// </summary>
        UNSECURED_REJOIN,

        /// <summary>
         /// A device has left the network
         /// </summary>
        DEVICE_LEFT

    }
}
