using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.ZCL.Clusters.Thermostat
{
    /// <summary>
    /// Setpoint Change Source value enumeration
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public enum SetpointChangeSourceEnum
    {
        // Manual user-initiated
        MANUAL_USER_INITIATED = 0x0000,
        // Schedule/internal programming-initiated
        SCHEDULE_INTERNAL_PROGRAMMING_INITIATED = 0x0001,
        // Externally-initiated
        EXTERNALLY_INITIATED = 0x0002
    }
}
