using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.ZCL.Clusters.IasAce
{
    /// <summary>
    /// IAS ACE Arm Notification value enumeration
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public enum IasAceArmNotificationEnum
    {
        // All Zones Disarmed
        ALL_ZONES_DISARMED = 0x0000,
        // Only Day Home Zones Armed
        ONLY_DAY_HOME_ZONES_ARMED = 0x0001,
        // Only Night Sleep Zones Armed
        ONLY_NIGHT_SLEEP_ZONES_ARMED = 0x0002,
        // All Zones Armed
        ALL_ZONES_ARMED = 0x0003,
        // Invalid Arm Disarm Code
        INVALID_ARM_DISARM_CODE = 0x0004,
        // Not Ready To Arm
        NOT_READY_TO_ARM = 0x0005,
        // Already Disarmed
        ALREADY_DISARMED = 0x0006
    }
}
