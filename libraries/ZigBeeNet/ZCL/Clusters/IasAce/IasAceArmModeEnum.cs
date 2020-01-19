using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.ZCL.Clusters.IasAce
{
    /// <summary>
    /// IAS ACE Arm Mode value enumeration
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public enum IasAceArmModeEnum
    {
        // Disarm
        DISARM = 0x0000,
        // Arm Day Home Zones Only
        ARM_DAY_HOME_ZONES_ONLY = 0x0001,
        // Arm Night Sleep Zones Only
        ARM_NIGHT_SLEEP_ZONES_ONLY = 0x0002,
        // Arm All Zones
        ARM_ALL_ZONES = 0x0003
    }
}
