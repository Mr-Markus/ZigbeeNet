using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.ZCL.Clusters.GreenPower
{
    /// <summary>
    /// Gp Pairing Configuration Option value enumeration
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public enum GpPairingConfigurationOptionBitmap
    {
        // Application ID
        APPLICATION_ID = 0x0007,
        // Communication Mode
        COMMUNICATION_MODE = 0x0018,
        // Sequence Number Capabilities
        SEQUENCE_NUMBER_CAPABILITIES = 0x0020,
        // Rx On Capability
        RX_ON_CAPABILITY = 0x0040,
        // Fixed Location
        FIXED_LOCATION = 0x0080,
        // Assigned Alias
        ASSIGNED_ALIAS = 0x0100,
        // Security Use
        SECURITY_USE = 0x0200,
        // Application Information Present
        APPLICATION_INFORMATION_PRESENT = 0x0400
    }
}
