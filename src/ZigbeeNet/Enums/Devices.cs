using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet
{
    public enum Devices : ushort
    {
        //Generic
        OnOffSwitch = 0x0000,
        LevelControlSwitch = 0x0001,
        OnOffOutput = 0x0002,
        LevelControllableOutput = 0x0003,
        SceneSelector = 0x0004,
        ConfigurationTool = 0x0005,
        RemoteControl = 0x0006,
        CombinedInterface = 0x0007,
        RangeExtender = 0x0008,
        MainsPowerOutlet = 0x0009,
        //Lighting
        OnOffLight = 0x0100,
        DimmableLight = 0x0101,
        ColorDimmableLight = 0x0102,
        OnOffLightSwitch = 0x0103,
        DimmerSwitch = 0x0104,
        ColorDimmerSwitch = 0x0105,
        LightSensor = 0x0106,
        OccupancySensor = 0x0107,
        //Closures
        Shade = 0x0200,
        ShadeController = 0x0201,
        //HVAC
        HeatingCoolingUnit = 0x0300,
        Thermostat = 0x0301,
        TemperatureSensor = 0x0302,
        Pump = 0x0303,
        PumpController = 0x0304,
        PressureSensor = 0x0305,
        FlowSensor = 0x0306,
        //Intruder Alarm Systems
        IASControlandIndicatingEquipment = 0x0400,
        IASAncillaryControlEquipment = 0x0401,
        IASZone = 0x0402,
        IASWarningDevice = 0x0403 
    }

    public enum eDeviceStatus : byte
    {
        Offline = 0x00,
        Online = 0x01
    }

    public enum eDeviceEnabled : byte
    {
        Disabled = 0x00,
        Enabled = 0x01
    }
}
