using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.ZCL.Clusters.Basic
{
    /// <summary>
    /// Power Source value enumeration
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public enum PowerSourceEnum
    {
        // Unknown
        UNKNOWN = 0x0000,
        // Mains Single Phase
        MAINS_SINGLE_PHASE = 0x0001,
        // Mains Three Phase
        MAINS_THREE_PHASE = 0x0002,
        // Battery
        BATTERY = 0x0003,
        // DC Source
        DC_SOURCE = 0x0004,
        // Emergency Mains Constant
        EMERGENCY_MAINS_CONSTANT = 0x0005,
        // Emergency Mains Changeover
        EMERGENCY_MAINS_CHANGEOVER = 0x0006
    }
}
