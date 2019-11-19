using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.ZCL.Clusters.IasZone
{
    /// <summary>
    /// IAS Zone Type value enumeration
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public enum IasZoneTypeEnum
    {
        // Standard CIE
        STANDARD_CIE = 0x0000,
        // Motion Sensor
        MOTION_SENSOR = 0x000D,
        // Contact Switch
        CONTACT_SWITCH = 0x0015,
        // Fire Sensor
        FIRE_SENSOR = 0x0028,
        // Water Sensor
        WATER_SENSOR = 0x002A,
        // Gas Sensor
        GAS_SENSOR = 0x002B,
        // Personal Emergency Device
        PERSONAL_EMERGENCY_DEVICE = 0x002C,
        // Vibration Movement Sensor
        VIBRATION_MOVEMENT_SENSOR = 0x002D,
        // Remote Control
        REMOTE_CONTROL = 0x010F,
        // Key Fob
        KEY_FOB = 0x0115,
        // Keypad
        KEYPAD = 0x021D,
        // Standard Warning Device
        STANDARD_WARNING_DEVICE = 0x0225,
        // Glass Break Sensor
        GLASS_BREAK_SENSOR = 0x0226,
        // Carbon Monoxide Sensor
        CARBON_MONOXIDE_SENSOR = 0x0227,
        // Security Repeater
        SECURITY_REPEATER = 0x0229,
        // Invalid Zone Type
        INVALID_ZONE_TYPE = 0x0FFFF
    }
}
