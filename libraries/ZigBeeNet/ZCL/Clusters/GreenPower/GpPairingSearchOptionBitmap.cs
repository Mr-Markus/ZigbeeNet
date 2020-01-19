using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.ZCL.Clusters.GreenPower
{
    /// <summary>
    /// Gp Pairing Search Option value enumeration
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public enum GpPairingSearchOptionBitmap
    {
        // Application ID
        APPLICATION_ID = 0x0007,
        // Request Unicast Sinks
        REQUEST_UNICAST_SINKS = 0x0008,
        // Request Derived Groupcast Sinks
        REQUEST_DERIVED_GROUPCAST_SINKS = 0x0010,
        // Request Commissioned Groupcast Sinks
        REQUEST_COMMISSIONED_GROUPCAST_SINKS = 0x0020,
        // Request Gpd Security Frame Counter
        REQUEST_GPD_SECURITY_FRAME_COUNTER = 0x0040,
        // Request Gpd Security Key
        REQUEST_GPD_SECURITY_KEY = 0x0080
    }
}
