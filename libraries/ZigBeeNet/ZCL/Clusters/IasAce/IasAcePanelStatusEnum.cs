using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.ZCL.Clusters.IasAce
{
    /// <summary>
    /// IAS ACE Panel Status value enumeration
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public enum IasAcePanelStatusEnum
    {
        // Panel Disarmed
        PANEL_DISARMED = 0x0000,
        // Armed Stay
        ARMED_STAY = 0x0001,
        // Armed Night
        ARMED_NIGHT = 0x0002,
        // Armed Away
        ARMED_AWAY = 0x0003,
        // Exit Delay
        EXIT_DELAY = 0x0004,
        // Entry Delay
        ENTRY_DELAY = 0x0005,
        // Not Ready To Arm
        NOT_READY_TO_ARM = 0x0006,
        // In Alarm
        IN_ALARM = 0x0007,
        // Arming Stay
        ARMING_STAY = 0x0008,
        // Arming Night
        ARMING_NIGHT = 0x0009,
        // Arming Away
        ARMING_AWAY = 0x000A
    }
}
