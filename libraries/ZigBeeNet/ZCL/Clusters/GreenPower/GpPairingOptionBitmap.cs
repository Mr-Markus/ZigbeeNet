using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.ZCL.Clusters.GreenPower
{
    /// <summary>
    /// Gp Pairing Option value enumeration
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public enum GpPairingOptionBitmap
    {
        // Application ID
        APPLICATION_ID = 0x0007,
        // Add Sink
        ADD_SINK = 0x0008,
        // Remove Gpd
        REMOVE_GPD = 0x0010,
        // Communication Mode
        COMMUNICATION_MODE = 0x0060,
        // Gpd Fixed
        GPD_FIXED = 0x0080,
        // Gpd MAC Sequence Number Capabilities
        GPD_MAC_SEQUENCE_NUMBER_CAPABILITIES = 0x0100,
        // Security Level
        SECURITY_LEVEL = 0x0600,
        // Security Key Type
        SECURITY_KEY_TYPE = 0x3800,
        // Gpd Security Frame Counter Present
        GPD_SECURITY_FRAME_COUNTER_PRESENT = 0x4000,
        // Gpd Security Key Present
        GPD_SECURITY_KEY_PRESENT = 0x08000,
        // Assigned Alias Present
        ASSIGNED_ALIAS_PRESENT = 0x10000,
        // Forwarding Radius Present
        FORWARDING_RADIUS_PRESENT = 0x20000
    }
}
