using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.ZCL.Clusters.Messaging
{
    /// <summary>
    /// Messaging Control Mask value enumeration
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public enum MessagingControlMaskBitmap
    {
        // Trans Mechanism
        TRANS_MECHANISM = 0x0003,
        // Message Urgency
        MESSAGE_URGENCY = 0x000C,
        // Enhanced Confirmation Request
        ENHANCED_CONFIRMATION_REQUEST = 0x0020,
        // Message Confirmation
        MESSAGE_CONFIRMATION = 0x0080
    }
}
