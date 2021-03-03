using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.ZCL.Clusters.MeterIdentification
{
    /// <summary>
    /// Meter Type value enumeration
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public enum MeterTypeEnum
    {
        // Utility Primary Meter
        UTILITY_PRIMARY_METER = 0x0000,
        // Utility Production Meter
        UTILITY_PRODUCTION_METER = 0x0001,
        // Utility Secondary Meter
        UTILITY_SECONDARY_METER = 0x0002,
        // Private Primary Meter
        PRIVATE_PRIMARY_METER = 0x0100,
        // Private Production Meter
        PRIVATE_PRODUCTION_METER = 0x0101,
        // Private Secondary Meters
        PRIVATE_SECONDARY_METERS = 0x0102,
        // Generic Meter
        GENERIC_METER = 0x0110
    }
}
