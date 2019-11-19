using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.ZCL.Clusters.GreenPower
{
    /// <summary>
    /// Gp Notification Option value enumeration
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public enum GpNotificationOptionBitmap
    {
        // Application ID
        APPLICATION_ID = 0x0007,
        // Also Unicast
        ALSO_UNICAST = 0x0008,
        // Also Derived Group
        ALSO_DERIVED_GROUP = 0x0010,
        // Also Commissioned Group
        ALSO_COMMISSIONED_GROUP = 0x0020,
        // Security Level
        SECURITY_LEVEL = 0x00C0,
        // Security Key Type
        SECURITY_KEY_TYPE = 0x0700,
        // Rx After Tx
        RX_AFTER_TX = 0x0800,
        // Gp Tx Queue Full
        GP_TX_QUEUE_FULL = 0x1000,
        // Bidirectional Capability
        BIDIRECTIONAL_CAPABILITY = 0x2000,
        // Proxy Info Present
        PROXY_INFO_PRESENT = 0x4000
    }
}
