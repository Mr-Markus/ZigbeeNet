using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.ZCL.Clusters.PowerConfiguration
{
    /// <summary>
    /// Battery Size value enumeration
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public enum BatterySizeEnum
    {
        // No Battery
        NO_BATTERY = 0x0000,
        // Build In
        BUILD_IN = 0x0001,
        // Other
        OTHER = 0x0002,
        // AA Cell
        AA_CELL = 0x0003,
        // AAA Cell
        AAA_CELL = 0x0004,
        // C Cell
        C_CELL = 0x0005,
        // D Cell
        D_CELL = 0x0006,
        // CR 2 Cell
        CR_2_CELL = 0x0007,
        // CR 123 A Cell
        CR_123_A_CELL = 0x0008,
        // Unknown
        UNKNOWN = 0x00FF
    }
}
