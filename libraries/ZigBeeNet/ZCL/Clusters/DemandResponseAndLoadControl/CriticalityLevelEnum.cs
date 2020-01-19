using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.ZCL.Clusters.DemandResponseAndLoadControl
{
    /// <summary>
    /// Criticality Level value enumeration
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public enum CriticalityLevelEnum
    {
        // Green
        GREEN = 0x0001,
        // Level 1
        LEVEL_1 = 0x0002,
        // Level 2
        LEVEL_2 = 0x0003,
        // Level 3
        LEVEL_3 = 0x0004,
        // Level 4
        LEVEL_4 = 0x0005,
        // Level 5
        LEVEL_5 = 0x0006,
        // Emergency
        EMERGENCY = 0x0007,
        // Planned Outage
        PLANNED_OUTAGE = 0x0008,
        // Service Disconnect
        SERVICE_DISCONNECT = 0x0009,
        // Utility Defined 1
        UTILITY_DEFINED_1 = 0x000A,
        // Utility Defined 2
        UTILITY_DEFINED_2 = 0x000B,
        // Utility Defined 3
        UTILITY_DEFINED_3 = 0x000C,
        // Utility Defined 4
        UTILITY_DEFINED_4 = 0x000D,
        // Utility Defined 5
        UTILITY_DEFINED_5 = 0x000E,
        // Utility Defined 6
        UTILITY_DEFINED_6 = 0x000F
    }
}
