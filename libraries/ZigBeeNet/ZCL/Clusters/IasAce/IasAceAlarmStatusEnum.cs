using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.ZCL.Clusters.IasAce
{
    /// <summary>
    /// IAS ACE Alarm Status value enumeration
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public enum IasAceAlarmStatusEnum
    {
        // No Alarm
        NO_ALARM = 0x0000,
        // Burglar
        BURGLAR = 0x0001,
        // Fire
        FIRE = 0x0002,
        // Emergency
        EMERGENCY = 0x0003,
        // Police Panic
        POLICE_PANIC = 0x0004,
        // Fire Panic
        FIRE_PANIC = 0x0005,
        // Emergency Panic
        EMERGENCY_PANIC = 0x0006
    }
}
