using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.ZCL.Clusters.DemandResponseAndLoadControl
{
    /// <summary>
    /// Device Class value enumeration
    ///
    /// Although, for backwards compatibility, the Type cannot be changed, this 16-bit
    /// Integer should be treated as if it were a 16-bit BitMap.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public enum DeviceClassBitmap
    {
        // Hvac Compressor Or Furnace
        HVAC_COMPRESSOR_OR_FURNACE = 0x0001,
        // Strip Heat Baseboard Heat
        STRIP_HEAT_BASEBOARD_HEAT = 0x0002,
        // Water Heater
        WATER_HEATER = 0x0004,
        // Pool Pump Spa Jacuzzi
        POOL_PUMP_SPA_JACUZZI = 0x0008,
        // Smart Appliances
        SMART_APPLIANCES = 0x0010,
        // Irrigation Pump
        IRRIGATION_PUMP = 0x0020,
        // Managed C And I Loads
        MANAGED_C_AND_I_LOADS = 0x0040,
        // Simple Misc Loads
        SIMPLE_MISC_LOADS = 0x0080,
        // Exterior Lighting
        EXTERIOR_LIGHTING = 0x0100,
        // Interior Lighting
        INTERIOR_LIGHTING = 0x0200,
        // Electric Vehicle
        ELECTRIC_VEHICLE = 0x0400,
        // Generation Systems
        GENERATION_SYSTEMS = 0x0800
    }
}
