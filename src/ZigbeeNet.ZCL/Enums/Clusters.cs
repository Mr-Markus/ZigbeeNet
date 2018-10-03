using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet.ZCL
{
    public enum Clusters
    {
        //General
        Basic = 0,
        PowerConfiguration = 1,
        DeviceTemperaturConfiguration = 2,
        Identify = 3,
        Groups = 4,
        Scenes = 5,
        OnOff = 6,
        OnOffSwitchConfiguration = 7,
        LevelControl = 8,
        Alarms = 9,
        Time = 10,
        RSSILocation = 11,
        //Closures
        ShadeConfiguration = 0x0100,
        //HVAC
        PumpConfigurationAndControl = 512,
        Thermostat = 513,
        FanControl = 514,
        DehumidificationControl = 515,
        ThermostatUserInterfaceConfiguration = 516,
        //Lighting
        ColorControl = 768,
        BallastConfiguration = 769,
        //Measurement and sensing 
        LuminanceMeasurement = 1024,
        LuminanceLevelSensing = 1025,
        TemperatureMeasurement = 1026,
        PressureMeasurement = 1027,
        FlowMeasurement = 1028,
        RelativeHumidityMeasurement = 1029,
        Occupancysensing = 103,
        //Security and safety 
        IASZone = 1280,
        IASACE = 1281,
        IASWD = 1282 
    }
}
