using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.ZCL.Clusters.KeyEstablishment
{
    /// <summary>
    /// Key Establishment Status value enumeration
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public enum KeyEstablishmentStatusEnum
    {
        // Unknown Issuer
        UNKNOWN_ISSUER = 0x0001,
        // Bad Key Confirm
        BAD_KEY_CONFIRM = 0x0002,
        // Bad Message
        BAD_MESSAGE = 0x0003,
        // No Resources
        NO_RESOURCES = 0x0004,
        // Unsupported Suite
        UNSUPPORTED_SUITE = 0x0005,
        // Invalid Certificate
        INVALID_CERTIFICATE = 0x0006
    }
}
