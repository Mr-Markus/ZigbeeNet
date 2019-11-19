using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.ZCL.Clusters.IasZone
{
    /// <summary>
    /// IAS Zone Status value enumeration
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public enum IasZoneStatusBitmap
    {
        // Alarm 1
        ALARM_1 = 0x0001,
        // Alarm 2
        ALARM_2 = 0x0002,
        // Tamper
        TAMPER = 0x0004,
        // Battery
        BATTERY = 0x0008,
        // Supervision Reports
        SUPERVISION_REPORTS = 0x0010,
        // Restore Reports
        RESTORE_REPORTS = 0x0020,
        // Trouble
        TROUBLE = 0x0040,
        // AC
        AC = 0x0080,
        // Test
        TEST = 0x0100,
        // Battery Defect
        BATTERY_DEFECT = 0x0200
    }
}
