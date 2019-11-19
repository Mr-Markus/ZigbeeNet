using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.ZCL.Clusters.Metering
{
    /// <summary>
    /// Metering Device Type value enumeration
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public enum MeteringDeviceTypeEnum
    {
        // Electric Metering
        ELECTRIC_METERING = 0x0000,
        // Gas Metering
        GAS_METERING = 0x0001,
        // Water Metering
        WATER_METERING = 0x0002,
        // Thermal Metering
        THERMAL_METERING = 0x0003,
        // Pressure Metering
        PRESSURE_METERING = 0x0004,
        // Heat Metering
        HEAT_METERING = 0x0005,
        // Cooling Metering
        COOLING_METERING = 0x0006,
        // Mirrored Gas Metering
        MIRRORED_GAS_METERING = 0x0080,
        // Mirrored Water Metering
        MIRRORED_WATER_METERING = 0x0081,
        // Mirrored Thermal Metering
        MIRRORED_THERMAL_METERING = 0x0082,
        // Mirrored Pressure Metering
        MIRRORED_PRESSURE_METERING = 0x0083,
        // Mirrored Heat Metering
        MIRRORED_HEAT_METERING = 0x0084,
        // Mirrored Cooling Metering
        MIRRORED_COOLING_METERING = 0x0085
    }
}
