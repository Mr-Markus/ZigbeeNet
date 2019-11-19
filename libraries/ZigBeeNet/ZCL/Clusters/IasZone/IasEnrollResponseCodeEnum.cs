using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.ZCL.Clusters.IasZone
{
    /// <summary>
    /// IAS Enroll Response Code value enumeration
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public enum IasEnrollResponseCodeEnum
    {
        // Success
        SUCCESS = 0x0000,
        // Not Supported
        NOT_SUPPORTED = 0x0001,
        // No Enroll Permit
        NO_ENROLL_PERMIT = 0x0002,
        // Too Many Zones
        TOO_MANY_ZONES = 0x0003
    }
}
