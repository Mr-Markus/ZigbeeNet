using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.ZCL.Clusters.GreenPower
{
    /// <summary>
    /// Gp Commissioning Notification Option value enumeration
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public enum GpCommissioningNotificationOptionBitmap
    {
        // Application ID
        APPLICATION_ID = 0x0007,
        // Rx After Tx
        RX_AFTER_TX = 0x0008,
        // Security Level
        SECURITY_LEVEL = 0x0030,
        // Security Key Type
        SECURITY_KEY_TYPE = 0x01C0,
        // Security Processing Failed
        SECURITY_PROCESSING_FAILED = 0x0200,
        // Bidirectional Capability
        BIDIRECTIONAL_CAPABILITY = 0x0400,
        // Proxy Info Present
        PROXY_INFO_PRESENT = 0x0800
    }
}
