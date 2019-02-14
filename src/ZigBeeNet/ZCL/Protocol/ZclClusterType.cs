using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.Logging;
using ZigBeeNet.ZCL.Clusters;

namespace ZigBeeNet.ZCL.Protocol
{
    public enum ClusterType
    {
        BASIC,
        POWER_CONFIGURATION,
        DEVICE_TEMPERATURE_CONFIGURATION,
        IDENTIFY,
        GROUPS,
        SCENES,
        ON_OFF,
        ON_OFF_SWITCH_CONFIGURATION,
        LEVEL_CONTROL,
        ALARMS,
        TIME,
        RSSI_LOCATION,
        ANALOG_INPUT__BASIC,
        ANALOG_OUTPUT__BASIC,
        ANALOG_VALUE__BASIC,
        BINARY_INPUT__BASIC,
        BINARY_OUTPUT__BASIC,
        BINARY_VALUE__BASIC,
        MULTISTATE_INPUT__BASIC,
        MULTISTATE_OUTPUT__BASIC,
        MULTISTATE_VALUE__BASIC,
        COMMISSIONING,
        OTA_UPGRADE,
        APPLIANCE_CONTROL,
        POLL_CONTROL,
        SHADE_CONFIGURATION,
        DOOR_LOCK,
        PUMP_CONFIGURATION_AND_CONTROL,
        THERMOSTAT,
        FAN_CONTROL,
        DEHUMIDIFICATION_CONTROL,
        THERMOSTAT_USER_INTERFACE_CONFIGURATION,
        COLOR_CONTROL,
        BALLAST_CONFIGURATION,
        ILLUMINANCE_MEASUREMENT,
        ILLUMINANCE_LEVEL_SENSING,
        TEMPERATURE_MEASUREMENT,
        PRESSURE_MEASUREMENT,
        FLOW_MEASUREMENT,
        RELATIVE_HUMIDITY_MEASUREMENT,
        OCCUPANCY_SENSING,
        IAS_ZONE,
        IAS_ACE,
        IAS_WD,
        GENERIC_TUNNEL,
        BACNET_PROTOCOL_TUNNEL,
        ANALOG_INPUT__BACNET_REGULAR,
        ANALOG_INPUT__BACNET_EXTENDED,
        ANALOG_OUTPUT__BACNET_REGULAR,
        ANALOG_OUTPUT__BACNET_EXTENDED,
        ANALOG_VALUE__BACNET_REGULAR,
        ANALOG_VALUE__BACNET_EXTENDED,
        BINARY_INPUT__BACNET_REGULAR,
        BINARY_INPUT__BACNET_EXTENDED,
        BINARY_OUTPUT__BACNET_REGULAR,
        BINARY_OUTPUT__BACNET_EXTENDED,
        BINARY_VALUE__BACNET_REGULAR,
        BINARY_VALUE__BACNET_EXTENDED,
        MULTISTATE_INPUT__BACNET_REGULAR,
        MULTISTATE_INPUT__BACNET_EXTENDED,
        MULTISTATE_OUTPUT__BACNET_REGULAR,
        MULTISTATE_OUTPUT__BACNET_EXTENDED,
        MULTISTATE_VALUE__BACNET_REGULAR,
        MULTISTATE_VALUE__BACNET_EXTENDED,
        PRICE,
        DEMAND_RESPONSE_AND_LOAD_CONTROL,
        METERING,
        MESSAGING,
        TUNNELING,
        KEY_ESTABLISHMENT,
        APPLIANCE_IDENTIFICATION,
        APPLIANCE_EVENTS_AND_ALERTS,
        APPLIANCE_STATISTICS,
        ELECTRICAL_MEASUREMENT,
        DIAGNOSTICS,
        GENERAL,
        TOUCHLINK,
    }

    public class ZclClusterType
    {
        private ILog _logger = LogProvider.For<ZclClusterType>();

        private static readonly Dictionary<ushort, ZclClusterType> _idValueMap;

        public ClusterType Type { get; private set; }

        public int ClusterId { get; private set; }

        public ProfileType ProfileType { get; private set; }

        public string Label { get; private set; }


        public Type ClusterClass
        {
            get; private set;
        }

        private ZclClusterType(int clusterId, ProfileType profileType, string label, ClusterType clusterType, Type clusterClass = null)
        {
            this.ClusterId = clusterId;
            this.ProfileType = profileType;
            this.Label = label;
            this.Type = clusterType;
            this.ClusterClass = clusterClass;
        }

        static ZclClusterType()
        {
            _idValueMap = new Dictionary<ushort, ZclClusterType>
            {
                { 0x0000, new ZclClusterType(0x0000, ProfileType.ZIGBEE_HOME_AUTOMATION, "Basic", ClusterType.BASIC, typeof(ZclBasicCluster)) },
                { 0x0001, new ZclClusterType(0x0001, ProfileType.ZIGBEE_HOME_AUTOMATION, "Power configuration", ClusterType.POWER_CONFIGURATION, typeof(ZclPowerConfigurationCluster)) },
                { 0x0002, new ZclClusterType(0x0002, ProfileType.ZIGBEE_HOME_AUTOMATION, "Device Temperature Configuration", ClusterType.DEVICE_TEMPERATURE_CONFIGURATION, typeof(ZclDeviceTemperatureConfigurationCluster)) },
                { 0x0003, new ZclClusterType(0x0003, ProfileType.ZIGBEE_HOME_AUTOMATION, "Identify", ClusterType.IDENTIFY, typeof(ZclIdentifyCluster)) },
                { 0x0004, new ZclClusterType(0x0004, ProfileType.ZIGBEE_HOME_AUTOMATION, "Groups", ClusterType.GROUPS, typeof(ZclGroupsCluster)) },
                { 0x0005, new ZclClusterType(0x0005, ProfileType.ZIGBEE_HOME_AUTOMATION, "Scenes", ClusterType.SCENES, typeof(ZclScenesCluster)) },
                { 0x0006, new ZclClusterType(0x0006, ProfileType.ZIGBEE_HOME_AUTOMATION, "On/Off", ClusterType.ON_OFF, typeof(ZclOnOffCluster)) },
                { 0x0007, new ZclClusterType(0x0007, ProfileType.ZIGBEE_HOME_AUTOMATION, "On/off Switch Configuration", ClusterType.ON_OFF_SWITCH_CONFIGURATION, typeof(ZclOnOffCluster)) },
                { 0x0008, new ZclClusterType(0x0008, ProfileType.ZIGBEE_HOME_AUTOMATION, "Level Control", ClusterType.LEVEL_CONTROL, typeof(ZclLevelControlCluster)) },
                { 0x0009, new ZclClusterType(0x0009, ProfileType.ZIGBEE_HOME_AUTOMATION, "Alarms", ClusterType.ALARMS, typeof(ZclAlarmsCluster)) },
                { 0x000A, new ZclClusterType(0x000A, ProfileType.ZIGBEE_HOME_AUTOMATION, "Time", ClusterType.TIME) },
                { 0x000B, new ZclClusterType(0x000B, ProfileType.ZIGBEE_HOME_AUTOMATION, "RSSI Location", ClusterType.RSSI_LOCATION) },
                { 0x000C, new ZclClusterType(0x000C, ProfileType.ZIGBEE_HOME_AUTOMATION, "Analog Input (Basic)", ClusterType.ANALOG_INPUT__BASIC) },
                { 0x000D, new ZclClusterType(0x000D, ProfileType.ZIGBEE_HOME_AUTOMATION, "Analog Output (Basic)", ClusterType.ANALOG_OUTPUT__BASIC) },
                { 0x000E, new ZclClusterType(0x000E, ProfileType.ZIGBEE_HOME_AUTOMATION, "Analog Value (Basic)", ClusterType.ANALOG_VALUE__BASIC) },
                { 0x000F, new ZclClusterType(0x000F, ProfileType.ZIGBEE_HOME_AUTOMATION, "Binary Input (Basic)", ClusterType.BINARY_INPUT__BASIC) },
                { 0x0010, new ZclClusterType(0x0010, ProfileType.ZIGBEE_HOME_AUTOMATION, "Binary Output (Basic)", ClusterType.BINARY_OUTPUT__BASIC) },
                { 0x0011, new ZclClusterType(0x0011, ProfileType.ZIGBEE_HOME_AUTOMATION, "Binary Value (Basic)", ClusterType.BINARY_VALUE__BASIC) },
                { 0x0012, new ZclClusterType(0x0012, ProfileType.ZIGBEE_HOME_AUTOMATION, "Multistate Input (Basic)", ClusterType.MULTISTATE_INPUT__BASIC) },
                { 0x0013, new ZclClusterType(0x0013, ProfileType.ZIGBEE_HOME_AUTOMATION, "Multistate Output (Basic)", ClusterType.MULTISTATE_OUTPUT__BASIC) },
                { 0x0014, new ZclClusterType(0x0014, ProfileType.ZIGBEE_HOME_AUTOMATION, "Multistate Value (Basic)", ClusterType.MULTISTATE_VALUE__BASIC) },
                { 0x0015, new ZclClusterType(0x0015, ProfileType.ZIGBEE_HOME_AUTOMATION, "Commissioning", ClusterType.COMMISSIONING) },
                { 0x0019, new ZclClusterType(0x0019, ProfileType.ZIGBEE_HOME_AUTOMATION, "OTA Upgrade", ClusterType.OTA_UPGRADE) },
                { 0x001B, new ZclClusterType(0x001B, ProfileType.ZIGBEE_HOME_AUTOMATION, "Appliance Control", ClusterType.APPLIANCE_CONTROL) },
                { 0x0020, new ZclClusterType(0x0020, ProfileType.ZIGBEE_HOME_AUTOMATION, "Poll Control", ClusterType.POLL_CONTROL) },
                { 0x0100, new ZclClusterType(0x0100, ProfileType.ZIGBEE_HOME_AUTOMATION, "Shade Configuration", ClusterType.SHADE_CONFIGURATION) },
                { 0x0101, new ZclClusterType(0x0101, ProfileType.ZIGBEE_HOME_AUTOMATION, "Door Lock", ClusterType.DOOR_LOCK) },
                { 0x0200, new ZclClusterType(0x0200, ProfileType.ZIGBEE_HOME_AUTOMATION, "Pump Configuration and Control", ClusterType.PUMP_CONFIGURATION_AND_CONTROL) },
                { 0x0201, new ZclClusterType(0x0201, ProfileType.ZIGBEE_HOME_AUTOMATION, "Thermostat", ClusterType.THERMOSTAT) },
                { 0x0202, new ZclClusterType(0x0202, ProfileType.ZIGBEE_HOME_AUTOMATION, "Fan Control", ClusterType.FAN_CONTROL) },
                { 0x0203, new ZclClusterType(0x0203, ProfileType.ZIGBEE_HOME_AUTOMATION, "Dehumidification Control", ClusterType.DEHUMIDIFICATION_CONTROL) },
                { 0x0204, new ZclClusterType(0x0204, ProfileType.ZIGBEE_HOME_AUTOMATION, "Thermostat User Interface Configuration", ClusterType.THERMOSTAT_USER_INTERFACE_CONFIGURATION) },
                { 0x0300, new ZclClusterType(0x0300, ProfileType.ZIGBEE_HOME_AUTOMATION, "Color Control", ClusterType.COLOR_CONTROL) },
                { 0x0301, new ZclClusterType(0x0301, ProfileType.ZIGBEE_HOME_AUTOMATION, "Ballast Configuration", ClusterType.BALLAST_CONFIGURATION) },
                { 0x0400, new ZclClusterType(0x0400, ProfileType.ZIGBEE_HOME_AUTOMATION, "Illuminance measurement", ClusterType.ILLUMINANCE_MEASUREMENT) },
                { 0x0401, new ZclClusterType(0x0401, ProfileType.ZIGBEE_HOME_AUTOMATION, "Illuminance level sensing", ClusterType.ILLUMINANCE_LEVEL_SENSING) },
                { 0x0402, new ZclClusterType(0x0402, ProfileType.ZIGBEE_HOME_AUTOMATION, "Temperature measurement", ClusterType.TEMPERATURE_MEASUREMENT) },
                { 0x0403, new ZclClusterType(0x0403, ProfileType.ZIGBEE_HOME_AUTOMATION, "Pressure measurement", ClusterType.PRESSURE_MEASUREMENT) },
                { 0x0404, new ZclClusterType(0x0404, ProfileType.ZIGBEE_HOME_AUTOMATION, "Flow measurement", ClusterType.FLOW_MEASUREMENT) },
                { 0x0405, new ZclClusterType(0x0405, ProfileType.ZIGBEE_HOME_AUTOMATION, "Relative humidity measurement", ClusterType.RELATIVE_HUMIDITY_MEASUREMENT) },
                { 0x0406, new ZclClusterType(0x0406, ProfileType.ZIGBEE_HOME_AUTOMATION, "Occupancy sensing", ClusterType.OCCUPANCY_SENSING) },
                { 0x0500, new ZclClusterType(0x0500, ProfileType.ZIGBEE_HOME_AUTOMATION, "IAS Zone", ClusterType.IAS_ZONE) },
                { 0x0501, new ZclClusterType(0x0501, ProfileType.ZIGBEE_HOME_AUTOMATION, "IAS ACE", ClusterType.IAS_ACE) },
                { 0x0502, new ZclClusterType(0x0502, ProfileType.ZIGBEE_HOME_AUTOMATION, "IAS WD", ClusterType.IAS_WD) },
                { 0x0600, new ZclClusterType(0x0600, ProfileType.ZIGBEE_HOME_AUTOMATION, "Generic Tunnel", ClusterType.GENERIC_TUNNEL) },
                { 0x0601, new ZclClusterType(0x0601, ProfileType.ZIGBEE_HOME_AUTOMATION, "BACnet Protocol Tunnel", ClusterType.BACNET_PROTOCOL_TUNNEL) },
                { 0x0602, new ZclClusterType(0x0602, ProfileType.ZIGBEE_HOME_AUTOMATION, "Analog Input (BACnet Regular)", ClusterType.ANALOG_INPUT__BACNET_REGULAR) },
                { 0x0603, new ZclClusterType(0x0603, ProfileType.ZIGBEE_HOME_AUTOMATION, "Analog Input (BACnet Extended)", ClusterType.ANALOG_INPUT__BACNET_EXTENDED) },
                { 0x0604, new ZclClusterType(0x0604, ProfileType.ZIGBEE_HOME_AUTOMATION, "Analog Output (BACnet Regular)", ClusterType.ANALOG_OUTPUT__BACNET_REGULAR) },
                { 0x0605, new ZclClusterType(0x0605, ProfileType.ZIGBEE_HOME_AUTOMATION, "Analog Output (BACnet Extended)", ClusterType.ANALOG_OUTPUT__BACNET_EXTENDED) },
                { 0x0606, new ZclClusterType(0x0606, ProfileType.ZIGBEE_HOME_AUTOMATION, "Analog Value (BACnet Regular)", ClusterType.ANALOG_VALUE__BACNET_REGULAR) },
                { 0x0607, new ZclClusterType(0x0607, ProfileType.ZIGBEE_HOME_AUTOMATION, "Analog Value (BACnet Extended)", ClusterType.ANALOG_VALUE__BACNET_EXTENDED) },
                { 0x0608, new ZclClusterType(0x0608, ProfileType.ZIGBEE_HOME_AUTOMATION, "Binary Input (BACnet Regular)", ClusterType.BINARY_INPUT__BACNET_REGULAR) },
                { 0x0609, new ZclClusterType(0x0609, ProfileType.ZIGBEE_HOME_AUTOMATION, "Binary Input (BACnet Extended)", ClusterType.BINARY_INPUT__BACNET_EXTENDED) },
                { 0x060A, new ZclClusterType(0x060A, ProfileType.ZIGBEE_HOME_AUTOMATION, "Binary Output (BACnet Regular)", ClusterType.BINARY_OUTPUT__BACNET_REGULAR) },
                { 0x060B, new ZclClusterType(0x060B, ProfileType.ZIGBEE_HOME_AUTOMATION, "Binary Output (BACnet Extended)", ClusterType.BINARY_OUTPUT__BACNET_EXTENDED) },
                { 0x060C, new ZclClusterType(0x060C, ProfileType.ZIGBEE_HOME_AUTOMATION, "Binary Value (BACnet Regular)", ClusterType.BINARY_VALUE__BACNET_REGULAR) },
                { 0x060D, new ZclClusterType(0x060D, ProfileType.ZIGBEE_HOME_AUTOMATION, "Binary Value (BACnet Extended)", ClusterType.BINARY_VALUE__BACNET_EXTENDED) },
                { 0x060E, new ZclClusterType(0x060E, ProfileType.ZIGBEE_HOME_AUTOMATION, "Multistate Input (BACnet Regular)", ClusterType.MULTISTATE_INPUT__BACNET_REGULAR) },
                { 0x060F, new ZclClusterType(0x060F, ProfileType.ZIGBEE_HOME_AUTOMATION, "Multistate Input (BACnet Extended)", ClusterType.MULTISTATE_INPUT__BACNET_EXTENDED) },
                { 0x0610, new ZclClusterType(0x0610, ProfileType.ZIGBEE_HOME_AUTOMATION, "Multistate Output (BACnet Regular)", ClusterType.MULTISTATE_OUTPUT__BACNET_REGULAR) },
                { 0x0611, new ZclClusterType(0x0611, ProfileType.ZIGBEE_HOME_AUTOMATION, "Multistate Output (BACnet Extended)", ClusterType.MULTISTATE_OUTPUT__BACNET_EXTENDED) },
                { 0x0612, new ZclClusterType(0x0612, ProfileType.ZIGBEE_HOME_AUTOMATION, "Multistate Value (BACnet Regular)", ClusterType.MULTISTATE_VALUE__BACNET_REGULAR) },
                { 0x0613, new ZclClusterType(0x0613, ProfileType.ZIGBEE_HOME_AUTOMATION, "Multistate Value (BACnet Extended)", ClusterType.MULTISTATE_VALUE__BACNET_EXTENDED) },
                { 0x0700, new ZclClusterType(0x0700, ProfileType.ZIGBEE_HOME_AUTOMATION, "Price", ClusterType.PRICE) },
                { 0x0701, new ZclClusterType(0x0701, ProfileType.ZIGBEE_HOME_AUTOMATION, "Demand Response and Load Control", ClusterType.DEMAND_RESPONSE_AND_LOAD_CONTROL) },
                { 0x0702, new ZclClusterType(0x0702, ProfileType.ZIGBEE_HOME_AUTOMATION, "Metering", ClusterType.METERING) },
                { 0x0703, new ZclClusterType(0x0703, ProfileType.ZIGBEE_HOME_AUTOMATION, "Messaging", ClusterType.MESSAGING) },
                { 0x0704, new ZclClusterType(0x0704, ProfileType.ZIGBEE_HOME_AUTOMATION, "Tunneling", ClusterType.TUNNELING) },
                { 0x0800, new ZclClusterType(0x0800, ProfileType.ZIGBEE_HOME_AUTOMATION, "Key Establishment", ClusterType.KEY_ESTABLISHMENT) },
                { 0x0B00, new ZclClusterType(0x0B00, ProfileType.ZIGBEE_HOME_AUTOMATION, "Appliance Identification", ClusterType.APPLIANCE_IDENTIFICATION) },
                { 0x0B02, new ZclClusterType(0x0B02, ProfileType.ZIGBEE_HOME_AUTOMATION, "Appliance Events and Alerts", ClusterType.APPLIANCE_EVENTS_AND_ALERTS) },
                { 0x0B03, new ZclClusterType(0x0B03, ProfileType.ZIGBEE_HOME_AUTOMATION, "Appliance Statistics", ClusterType.APPLIANCE_STATISTICS) },
                { 0x0B04, new ZclClusterType(0x0B04, ProfileType.ZIGBEE_HOME_AUTOMATION, "Electrical Measurement", ClusterType.ELECTRICAL_MEASUREMENT) },
                { 0x0B05, new ZclClusterType(0x0B05, ProfileType.ZIGBEE_HOME_AUTOMATION, "Diagnostics", ClusterType.DIAGNOSTICS) },
                { 0xFFFF, new ZclClusterType(0xFFFF, ProfileType.ZIGBEE_HOME_AUTOMATION, "General", ClusterType.GENERAL, typeof(ZclGeneralCluster)) },
                { 0x1000, new ZclClusterType(0x1000, ProfileType.ZIGBEE_LIGHT_LINK, "Touchlink", ClusterType.TOUCHLINK) },
            };
        }

        public static ZclClusterType GetValueById(ushort clusterId)
        {
            // Use index instead of Linq (Where())-> performance
            return _idValueMap[clusterId];
        }

        public static ZclClusterType GetValueById(ClusterType clusterId)
        {
            // Use index instead of Linq (Where())-> performance
            return _idValueMap[(ushort)clusterId];
        }        
    }
}



