using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.ZCL.Clusters.ElectricalMeasurement
{
    /// <summary>
    /// Measurement Type value enumeration
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public enum MeasurementTypeEnum
    {
        // AC Active Measurement
        AC_ACTIVE_MEASUREMENT = 0x0000,
        // AC Reactive Measurement
        AC_REACTIVE_MEASUREMENT = 0x0001,
        // AC Apparent Measurement
        AC_APPARENT_MEASUREMENT = 0x0002,
        // Phase A Measurement
        PHASE_A_MEASUREMENT = 0x0004,
        // Phase B Measurement
        PHASE_B_MEASUREMENT = 0x0008,
        // Phase C Measurement
        PHASE_C_MEASUREMENT = 0x0010,
        // DC Measurement
        DC_MEASUREMENT = 0x0020,
        // Harmonics Measurement
        HARMONICS_MEASUREMENT = 0x0040,
        // Power Quality Measurement
        POWER_QUALITY_MEASUREMENT = 0x0080
    }
}
