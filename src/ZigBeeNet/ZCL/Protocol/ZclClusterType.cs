using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.Logging;
using ZigBeeNet.ZCL.Clusters;

namespace ZigBeeNet.ZCL.Protocol
{
    public enum ClusterType : ushort
    {
        BASIC = 0x0000,
        POWER_CONFIGURATION = 0x0001,
        DEVICE_TEMPERATURE_CONFIGURATION = 0x0002,
        IDENTIFY = 0x0003,
        GROUPS = 0x0004,
        SCENES = 0x0005,
        ON_OFF = 0x0006,
        ON_OFF_SWITCH_CONFIGURATION = 0x0007,
        LEVEL_CONTROL = 0x0008,
        ALARMS = 0x0009,
        TIME = 0x000A,
        RSSI_LOCATION = 0x000B,
        ANALOG_INPUT__BASIC = 0x000C,
        ANALOG_OUTPUT__BASIC = 0x000D,
        ANALOG_VALUE__BASIC = 0x000E,
        BINARY_INPUT__BASIC = 0x000F,
        BINARY_OUTPUT__BASIC = 0x0010,
        BINARY_VALUE__BASIC = 0x0011,
        MULTISTATE_INPUT__BASIC = 0x0012,
        MULTISTATE_OUTPUT__BASIC = 0x0013,
        MULTISTATE_VALUE__BASIC = 0x0014,
        COMMISSIONING = 0x0015,
        OTA_UPGRADE = 0x0019,
        APPLIANCE_CONTROL = 0x001B,
        POLL_CONTROL = 0x0020,
        SHADE_CONFIGURATION = 0x0100,
        DOOR_LOCK = 0x0101,
        PUMP_CONFIGURATION_AND_CONTROL = 0x0200,
        THERMOSTAT = 0x0201,
        FAN_CONTROL = 0x0202,
        DEHUMIDIFICATION_CONTROL = 0x0203,
        THERMOSTAT_USER_INTERFACE_CONFIGURATION = 0x0204,
        COLOR_CONTROL = 0x0300,
        BALLAST_CONFIGURATION = 0x0301,
        ILLUMINANCE_MEASUREMENT = 0x0400,
        ILLUMINANCE_LEVEL_SENSING = 0x0401,
        TEMPERATURE_MEASUREMENT = 0x0402,
        PRESSURE_MEASUREMENT = 0x0403,
        FLOW_MEASUREMENT = 0x0404,
        RELATIVE_HUMIDITY_MEASUREMENT = 0x0405,
        OCCUPANCY_SENSING = 0x0406,
        IAS_ZONE = 0x0500,
        IAS_ACE = 0x0501,
        IAS_WD = 0x0502,
        GENERIC_TUNNEL = 0x0600,
        BACNET_PROTOCOL_TUNNEL = 0x0601,
        ANALOG_INPUT__BACNET_REGULAR = 0x0602,
        ANALOG_INPUT__BACNET_EXTENDED = 0x0603,
        ANALOG_OUTPUT__BACNET_REGULAR = 0x0604,
        ANALOG_OUTPUT__BACNET_EXTENDED = 0x0605,
        ANALOG_VALUE__BACNET_REGULAR = 0x0606,
        ANALOG_VALUE__BACNET_EXTENDED = 0x0607,
        BINARY_INPUT__BACNET_REGULAR = 0x0608,
        BINARY_INPUT__BACNET_EXTENDED = 0x0609,
        BINARY_OUTPUT__BACNET_REGULAR = 0x060A,
        BINARY_OUTPUT__BACNET_EXTENDED = 0x060B,
        BINARY_VALUE__BACNET_REGULAR = 0x060C,
        BINARY_VALUE__BACNET_EXTENDED = 0x060D,
        MULTISTATE_INPUT__BACNET_REGULAR = 0x060E,
        MULTISTATE_INPUT__BACNET_EXTENDED = 0x060F,
        MULTISTATE_OUTPUT__BACNET_REGULAR = 0x0610,
        MULTISTATE_OUTPUT__BACNET_EXTENDED = 0x0611,
        MULTISTATE_VALUE__BACNET_REGULAR = 0x0612,
        MULTISTATE_VALUE__BACNET_EXTENDED = 0x0613,
        PRICE = 0x0700,
        DEMAND_RESPONSE_AND_LOAD_CONTROL = 0x0701,
        METERING = 0x0702,
        MESSAGING = 0x0703,
        TUNNELING = 0x0704,
        KEY_ESTABLISHMENT = 0x0800,
        APPLIANCE_IDENTIFICATION = 0x0B00,
        APPLIANCE_EVENTS_AND_ALERTS = 0x0B02,
        APPLIANCE_STATISTICS = 0x0B03,
        ELECTRICAL_MEASUREMENT = 0x0B04,
        DIAGNOSTICS = 0x0B05,
        GENERAL = 0xFFFF,
        TOUCHLINK = 0x1000
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

        public override string ToString()
        {
            return $"{Label} ({ClusterId})";
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
                { 0x000A, new ZclClusterType(0x000A, ProfileType.ZIGBEE_HOME_AUTOMATION, "Time", ClusterType.TIME, typeof(ZclTimeCluster)) },
                { 0x000B, new ZclClusterType(0x000B, ProfileType.ZIGBEE_HOME_AUTOMATION, "RSSI Location", ClusterType.RSSI_LOCATION, typeof(ZclRSSILocationCluster)) },
                { 0x000C, new ZclClusterType(0x000C, ProfileType.ZIGBEE_HOME_AUTOMATION, "Analog Input (Basic)", ClusterType.ANALOG_INPUT__BASIC, typeof(ZclAnalogInputBasicCluster)) },
                { 0x000D, new ZclClusterType(0x000D, ProfileType.ZIGBEE_HOME_AUTOMATION, "Analog Output (Basic)", ClusterType.ANALOG_OUTPUT__BASIC, typeof(ZclAnalogOutputBasicCluster)) },
                { 0x000E, new ZclClusterType(0x000E, ProfileType.ZIGBEE_HOME_AUTOMATION, "Analog Value (Basic)", ClusterType.ANALOG_VALUE__BASIC, typeof(ZclAnalogValueBasicCluster)) },
                { 0x000F, new ZclClusterType(0x000F, ProfileType.ZIGBEE_HOME_AUTOMATION, "Binary Input (Basic)", ClusterType.BINARY_INPUT__BASIC, typeof(ZclBinaryInputBasicCluster)) },
                { 0x0010, new ZclClusterType(0x0010, ProfileType.ZIGBEE_HOME_AUTOMATION, "Binary Output (Basic)", ClusterType.BINARY_OUTPUT__BASIC, typeof(ZclBinaryOutputBasicCluster)) },
                { 0x0011, new ZclClusterType(0x0011, ProfileType.ZIGBEE_HOME_AUTOMATION, "Binary Value (Basic)", ClusterType.BINARY_VALUE__BASIC, typeof(ZclBinaryValueBasicCluster)) },
                { 0x0012, new ZclClusterType(0x0012, ProfileType.ZIGBEE_HOME_AUTOMATION, "Multistate Input (Basic)", ClusterType.MULTISTATE_INPUT__BASIC, typeof(ZclMultistateInputBasicCluster)) },
                { 0x0013, new ZclClusterType(0x0013, ProfileType.ZIGBEE_HOME_AUTOMATION, "Multistate Output (Basic)", ClusterType.MULTISTATE_OUTPUT__BASIC, typeof(ZclMultistateOutputBasicCluster)) },
                { 0x0014, new ZclClusterType(0x0014, ProfileType.ZIGBEE_HOME_AUTOMATION, "Multistate Value (Basic)", ClusterType.MULTISTATE_VALUE__BASIC, typeof(ZclMultistateValueBasicCluster)) },
                { 0x0015, new ZclClusterType(0x0015, ProfileType.ZIGBEE_HOME_AUTOMATION, "Commissioning", ClusterType.COMMISSIONING, typeof(ZclCommissioningCluster)) },
                { 0x0019, new ZclClusterType(0x0019, ProfileType.ZIGBEE_HOME_AUTOMATION, "OTA Upgrade", ClusterType.OTA_UPGRADE, typeof(ZclOTAUpgradeCluster)) },
                { 0x001B, new ZclClusterType(0x001B, ProfileType.ZIGBEE_HOME_AUTOMATION, "Appliance Control", ClusterType.APPLIANCE_CONTROL, typeof(ZclApplianceControlCluster)) },
                { 0x0020, new ZclClusterType(0x0020, ProfileType.ZIGBEE_HOME_AUTOMATION, "Poll Control", ClusterType.POLL_CONTROL, typeof(ZclPollControlCluster)) },
                { 0x0100, new ZclClusterType(0x0100, ProfileType.ZIGBEE_HOME_AUTOMATION, "Shade Configuration", ClusterType.SHADE_CONFIGURATION, typeof(ZclShadeConfigurationCluster)) },
                { 0x0101, new ZclClusterType(0x0101, ProfileType.ZIGBEE_HOME_AUTOMATION, "Door Lock", ClusterType.DOOR_LOCK, typeof(ZclDoorLockCluster)) },
                { 0x0200, new ZclClusterType(0x0200, ProfileType.ZIGBEE_HOME_AUTOMATION, "Pump Configuration and Control", ClusterType.PUMP_CONFIGURATION_AND_CONTROL, typeof(ZclPumpConfigurationAndControlCluster)) },
                { 0x0201, new ZclClusterType(0x0201, ProfileType.ZIGBEE_HOME_AUTOMATION, "Thermostat", ClusterType.THERMOSTAT, typeof(ZclThermostatCluster)) },
                { 0x0202, new ZclClusterType(0x0202, ProfileType.ZIGBEE_HOME_AUTOMATION, "Fan Control", ClusterType.FAN_CONTROL, typeof(ZclFanControlCluster)) },
                { 0x0203, new ZclClusterType(0x0203, ProfileType.ZIGBEE_HOME_AUTOMATION, "Dehumidification Control", ClusterType.DEHUMIDIFICATION_CONTROL, typeof(ZclDehumidificationControlCluster)) },
                { 0x0204, new ZclClusterType(0x0204, ProfileType.ZIGBEE_HOME_AUTOMATION, "Thermostat User Interface Configuration", ClusterType.THERMOSTAT_USER_INTERFACE_CONFIGURATION, typeof(ZclThermostatUserInterfaceConfigurationCluster)) },
                { 0x0300, new ZclClusterType(0x0300, ProfileType.ZIGBEE_HOME_AUTOMATION, "Color Control", ClusterType.COLOR_CONTROL, typeof(ZclColorControlCluster)) },
                { 0x0301, new ZclClusterType(0x0301, ProfileType.ZIGBEE_HOME_AUTOMATION, "Ballast Configuration", ClusterType.BALLAST_CONFIGURATION, typeof(ZclBallastConfigurationCluster)) },
                { 0x0400, new ZclClusterType(0x0400, ProfileType.ZIGBEE_HOME_AUTOMATION, "Illuminance measurement", ClusterType.ILLUMINANCE_MEASUREMENT, typeof(ZclIlluminanceMeasurementCluster)) },
                { 0x0401, new ZclClusterType(0x0401, ProfileType.ZIGBEE_HOME_AUTOMATION, "Illuminance level sensing", ClusterType.ILLUMINANCE_LEVEL_SENSING, typeof(ZclIlluminanceLevelSensingCluster)) },
                { 0x0402, new ZclClusterType(0x0402, ProfileType.ZIGBEE_HOME_AUTOMATION, "Temperature measurement", ClusterType.TEMPERATURE_MEASUREMENT, typeof(ZclTemperatureMeasurementCluster)) },
                { 0x0403, new ZclClusterType(0x0403, ProfileType.ZIGBEE_HOME_AUTOMATION, "Pressure measurement", ClusterType.PRESSURE_MEASUREMENT, typeof(ZclPressureMeasurementCluster)) },
                { 0x0404, new ZclClusterType(0x0404, ProfileType.ZIGBEE_HOME_AUTOMATION, "Flow measurement", ClusterType.FLOW_MEASUREMENT, typeof(ZclFlowMeasurementCluster)) },
                { 0x0405, new ZclClusterType(0x0405, ProfileType.ZIGBEE_HOME_AUTOMATION, "Relative humidity measurement", ClusterType.RELATIVE_HUMIDITY_MEASUREMENT, typeof(ZclRelativeHumidityMeasurementCluster)) },
                { 0x0406, new ZclClusterType(0x0406, ProfileType.ZIGBEE_HOME_AUTOMATION, "Occupancy sensing", ClusterType.OCCUPANCY_SENSING, typeof(ZclOccupancySensingCluster)) },
                { 0x0500, new ZclClusterType(0x0500, ProfileType.ZIGBEE_HOME_AUTOMATION, "IAS Zone", ClusterType.IAS_ZONE, typeof(ZclIASZoneCluster)) },
                { 0x0501, new ZclClusterType(0x0501, ProfileType.ZIGBEE_HOME_AUTOMATION, "IAS ACE", ClusterType.IAS_ACE, typeof(ZclIASACECluster)) },
                { 0x0502, new ZclClusterType(0x0502, ProfileType.ZIGBEE_HOME_AUTOMATION, "IAS WD", ClusterType.IAS_WD, typeof(ZclIASWDCluster)) },
                { 0x0600, new ZclClusterType(0x0600, ProfileType.ZIGBEE_HOME_AUTOMATION, "Generic Tunnel", ClusterType.GENERIC_TUNNEL, typeof(ZclGenericTunnelCluster)) },
                { 0x0601, new ZclClusterType(0x0601, ProfileType.ZIGBEE_HOME_AUTOMATION, "BACnet Protocol Tunnel", ClusterType.BACNET_PROTOCOL_TUNNEL, typeof(ZclBACnetProtocolTunnelCluster)) },
                { 0x0602, new ZclClusterType(0x0602, ProfileType.ZIGBEE_HOME_AUTOMATION, "Analog Input (BACnet Regular)", ClusterType.ANALOG_INPUT__BACNET_REGULAR, typeof(ZclAnalogInputBACnetRegularCluster)) },
                { 0x0603, new ZclClusterType(0x0603, ProfileType.ZIGBEE_HOME_AUTOMATION, "Analog Input (BACnet Extended)", ClusterType.ANALOG_INPUT__BACNET_EXTENDED, typeof(ZclAnalogInputBACnetExtendedCluster)) },
                { 0x0604, new ZclClusterType(0x0604, ProfileType.ZIGBEE_HOME_AUTOMATION, "Analog Output (BACnet Regular)", ClusterType.ANALOG_OUTPUT__BACNET_REGULAR, typeof(ZclAnalogValueBACnetRegularCluster)) },
                { 0x0605, new ZclClusterType(0x0605, ProfileType.ZIGBEE_HOME_AUTOMATION, "Analog Output (BACnet Extended)", ClusterType.ANALOG_OUTPUT__BACNET_EXTENDED, typeof(ZclAnalogValueBACnetExtendedCluster)) },
                { 0x0606, new ZclClusterType(0x0606, ProfileType.ZIGBEE_HOME_AUTOMATION, "Analog Value (BACnet Regular)", ClusterType.ANALOG_VALUE__BACNET_REGULAR, typeof(ZclAnalogValueBACnetRegularCluster)) },
                { 0x0607, new ZclClusterType(0x0607, ProfileType.ZIGBEE_HOME_AUTOMATION, "Analog Value (BACnet Extended)", ClusterType.ANALOG_VALUE__BACNET_EXTENDED, typeof(ZclAnalogValueBACnetExtendedCluster)) },
                { 0x0608, new ZclClusterType(0x0608, ProfileType.ZIGBEE_HOME_AUTOMATION, "Binary Input (BACnet Regular)", ClusterType.BINARY_INPUT__BACNET_REGULAR, typeof(ZclBinaryInputBACnetRegularCluster)) },
                { 0x0609, new ZclClusterType(0x0609, ProfileType.ZIGBEE_HOME_AUTOMATION, "Binary Input (BACnet Extended)", ClusterType.BINARY_INPUT__BACNET_EXTENDED, typeof(ZclBinaryInputBACnetExtendedCluster)) },
                { 0x060A, new ZclClusterType(0x060A, ProfileType.ZIGBEE_HOME_AUTOMATION, "Binary Output (BACnet Regular)", ClusterType.BINARY_OUTPUT__BACNET_REGULAR, typeof(ZclBinaryOutputBACnetRegularCluster)) },
                { 0x060B, new ZclClusterType(0x060B, ProfileType.ZIGBEE_HOME_AUTOMATION, "Binary Output (BACnet Extended)", ClusterType.BINARY_OUTPUT__BACNET_EXTENDED, typeof(ZclBinaryOutputBACnetExtendedCluster)) },
                { 0x060C, new ZclClusterType(0x060C, ProfileType.ZIGBEE_HOME_AUTOMATION, "Binary Value (BACnet Regular)", ClusterType.BINARY_VALUE__BACNET_REGULAR, typeof(ZclBinaryValueBACnetRegularCluster)) },
                { 0x060D, new ZclClusterType(0x060D, ProfileType.ZIGBEE_HOME_AUTOMATION, "Binary Value (BACnet Extended)", ClusterType.BINARY_VALUE__BACNET_EXTENDED, typeof(ZclBinaryValueBACnetExtendedCluster)) },
                { 0x060E, new ZclClusterType(0x060E, ProfileType.ZIGBEE_HOME_AUTOMATION, "Multistate Input (BACnet Regular)", ClusterType.MULTISTATE_INPUT__BACNET_REGULAR, typeof(ZclMultistateInputBACnetRegularCluster)) },
                { 0x060F, new ZclClusterType(0x060F, ProfileType.ZIGBEE_HOME_AUTOMATION, "Multistate Input (BACnet Extended)", ClusterType.MULTISTATE_INPUT__BACNET_EXTENDED, typeof(ZclMultistateInputBACnetExtendedCluster)) },
                { 0x0610, new ZclClusterType(0x0610, ProfileType.ZIGBEE_HOME_AUTOMATION, "Multistate Output (BACnet Regular)", ClusterType.MULTISTATE_OUTPUT__BACNET_REGULAR, typeof(ZclMultistateValueBACnetRegularCluster)) },
                { 0x0611, new ZclClusterType(0x0611, ProfileType.ZIGBEE_HOME_AUTOMATION, "Multistate Output (BACnet Extended)", ClusterType.MULTISTATE_OUTPUT__BACNET_EXTENDED, typeof(ZclMultistateOutputBACnetExtendedCluster)) },
                { 0x0612, new ZclClusterType(0x0612, ProfileType.ZIGBEE_HOME_AUTOMATION, "Multistate Value (BACnet Regular)", ClusterType.MULTISTATE_VALUE__BACNET_REGULAR, typeof(ZclMultistateValueBACnetRegularCluster)) },
                { 0x0613, new ZclClusterType(0x0613, ProfileType.ZIGBEE_HOME_AUTOMATION, "Multistate Value (BACnet Extended)", ClusterType.MULTISTATE_VALUE__BACNET_EXTENDED, typeof(ZclMultistateValueBACnetExtendedCluster)) },
                { 0x0700, new ZclClusterType(0x0700, ProfileType.ZIGBEE_HOME_AUTOMATION, "Price", ClusterType.PRICE, typeof(ZclPriceCluster)) },
                { 0x0701, new ZclClusterType(0x0701, ProfileType.ZIGBEE_HOME_AUTOMATION, "Demand Response and Load Control", ClusterType.DEMAND_RESPONSE_AND_LOAD_CONTROL, typeof(ZclDemandResponseAndLoadControlCluster)) },
                { 0x0702, new ZclClusterType(0x0702, ProfileType.ZIGBEE_HOME_AUTOMATION, "Metering", ClusterType.METERING, typeof(ZclMeteringCluster)) },
                { 0x0703, new ZclClusterType(0x0703, ProfileType.ZIGBEE_HOME_AUTOMATION, "Messaging", ClusterType.MESSAGING, typeof(ZclMessagingCluster)) },
                { 0x0704, new ZclClusterType(0x0704, ProfileType.ZIGBEE_HOME_AUTOMATION, "Tunneling", ClusterType.TUNNELING, typeof(ZclTunnelingCluster)) },
                { 0x0800, new ZclClusterType(0x0800, ProfileType.ZIGBEE_HOME_AUTOMATION, "Key Establishment", ClusterType.KEY_ESTABLISHMENT, typeof(ZclKeyEstablishmentCluster)) },
                { 0x0B00, new ZclClusterType(0x0B00, ProfileType.ZIGBEE_HOME_AUTOMATION, "Appliance Identification", ClusterType.APPLIANCE_IDENTIFICATION, typeof(ZclApplianceIdentificationCluster)) },
                { 0x0B02, new ZclClusterType(0x0B02, ProfileType.ZIGBEE_HOME_AUTOMATION, "Appliance Events and Alerts", ClusterType.APPLIANCE_EVENTS_AND_ALERTS, typeof(ZclApplianceEventsAndAlertsCluster)) },
                { 0x0B03, new ZclClusterType(0x0B03, ProfileType.ZIGBEE_HOME_AUTOMATION, "Appliance Statistics", ClusterType.APPLIANCE_STATISTICS, typeof(ZclApplianceStatisticsCluster)) },
                { 0x0B04, new ZclClusterType(0x0B04, ProfileType.ZIGBEE_HOME_AUTOMATION, "Electrical Measurement", ClusterType.ELECTRICAL_MEASUREMENT, typeof(ZclElectricalMeasurementCluster)) },
                { 0x0B05, new ZclClusterType(0x0B05, ProfileType.ZIGBEE_HOME_AUTOMATION, "Diagnostics", ClusterType.DIAGNOSTICS, typeof(ZclDiagnosticsCluster)) },
                { 0xFFFF, new ZclClusterType(0xFFFF, ProfileType.ZIGBEE_HOME_AUTOMATION, "General", ClusterType.GENERAL, typeof(ZclGeneralCluster)) },
                { 0x1000, new ZclClusterType(0x1000, ProfileType.ZIGBEE_LIGHT_LINK, "Touchlink", ClusterType.TOUCHLINK) },
            };
        }

        public static ZclClusterType GetValueById(ushort clusterId)
        {
            // Use index instead of Linq (Where())-> performance
            if (_idValueMap.TryGetValue(clusterId, out ZclClusterType result))
                return result;
            else
                return null;
        }

        public static ZclClusterType GetValueById(ClusterType clusterId)
        {
            // Use index instead of Linq (Where())-> performance
            return _idValueMap[(ushort)clusterId];
        }
    }
}



