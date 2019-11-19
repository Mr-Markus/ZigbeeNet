using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.ZCL.Clusters.AnalogInputBasic
{
    /// <summary>
    /// Status Flags value enumeration
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public enum AnalogInputStatusFlagsBitmap
    {
        // In_Alarm
        IN_ALARM = 0x0001,
        // Fault
        FAULT = 0x0002,
        // Overridden
        OVERRIDDEN = 0x0004,
        // Out Of Service
        OUT_OF_SERVICE = 0x0008
    }
}
