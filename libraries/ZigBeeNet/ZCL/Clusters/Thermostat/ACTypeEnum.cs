using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.ZCL.Clusters.Thermostat
{
    /// <summary>
    /// AC Type value enumeration
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public enum ACTypeEnum
    {
        // Reserved
        RESERVED = 0x0000,
        // Cooling and Fixed Speed
        COOLING_AND_FIXED_SPEED = 0x0001,
        // Heat Pump and Fixed Speed
        HEAT_PUMP_AND_FIXED_SPEED = 0x0002,
        // Cooling and Inverter
        COOLING_AND_INVERTER = 0x0003,
        // Heat Pump and Inverter
        HEAT_PUMP_AND_INVERTER = 0x0004
    }
}
