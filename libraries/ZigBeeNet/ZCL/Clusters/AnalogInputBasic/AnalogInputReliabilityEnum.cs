using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.ZCL.Clusters.AnalogInputBasic
{
    /// <summary>
    /// Reliability value enumeration
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public enum AnalogInputReliabilityEnum
    {
        // No - Fault - Detected
        NO_FAULT_DETECTED = 0x0000,
        // Over - Range
        OVER_RANGE = 0x0002,
        // Under - Range
        UNDER_RANGE = 0x0003,
        // Open - Loop
        OPEN_LOOP = 0x0004,
        // Shorted - Loop
        SHORTED_LOOP = 0x0005,
        // Unreliable - Other
        UNRELIABLE_OTHER = 0x0007,
        // Process - Error
        PROCESS_ERROR = 0x0008,
        // Multi - State - Fault
        MULTI_STATE_FAULT = 0x0009,
        // Configuration - Error
        CONFIGURATION_ERROR = 0x000A
    }
}
