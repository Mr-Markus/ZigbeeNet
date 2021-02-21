using System;
using System.Collections.Generic;
using ZigBeeNet.ZCL.Clusters;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.ZCL.Protocol
{
    /// <summary>
    /// Enumeration of ZigBee Clusters.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public enum ClusterType : ushort
    {
        BASIC = 0x0000,
        POWER_CONFIGURATION = 0x0001,
        IDENTIFY = 0x0003,
        GROUPS = 0x0004,
        SCENES = 0x0005,
        ON_OFF = 0x0006,
        ON_OFF_SWITCH_CONFIGURATION = 0x0007,
        LEVEL_CONTROL = 0x0008,
        ALARMS = 0x0009,
        TIME = 0x000A,
        RSSI_LOCATION = 0x000B,
        ANALOG_INPUT_BASIC = 0x000C,
        BINARY_INPUT_BASIC = 0x000F,
        MULTISTATE_INPUT_BASIC = 0x0012,
        MULTISTATE_OUTPUT_BASIC = 0x0013,
        MULTISTATE_VALUE_BASIC = 0x0014,
        COMMISSIONING = 0x0015,
        OTA_UPGRADE = 0x0019,
        POLL_CONTROL = 0x0020,
        GREEN_POWER = 0x0021,
        DOOR_LOCK = 0x0101,
        WINDOW_COVERING = 0x0102,
        THERMOSTAT = 0x0201,
        FAN_CONTROL = 0x0202,
        DEHUMIDIFICATION_CONTROL = 0x0203,
        THERMOSTAT_USER_INTERFACE_CONFIGURATION = 0x0204,
        COLOR_CONTROL = 0x0300,
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
        PRICE = 0x0700,
        DEMAND_RESPONSE_AND_LOAD_CONTROL = 0x0701,
        METERING = 0x0702,
        MESSAGING = 0x0703,
        PREPAYMENT = 0x0705,
        KEY_ESTABLISHMENT = 0x0800,
        METER_IDENTIFICATION = 0x0B01,
        ELECTRICAL_MEASUREMENT = 0x0B04,
        DIAGNOSTICS = 0x0B05,
    }

    public class ZclClusterType
    {
        private static readonly Dictionary<ushort, ZclClusterType> _idValueMap;

        public ClusterType Type { get; private set; }

        public int ClusterId { get; private set; }

        public string Label { get; private set; }

        public Func<ZigBeeEndpoint, ZclCluster> ClusterFactory { get; private set; }

        private ZclClusterType(int clusterId, string label, ClusterType clusterType, Func<ZigBeeEndpoint, ZclCluster> clusterFactory)
        {
            this.ClusterId = clusterId;
            this.Label = label;
            this.Type = clusterType;
            this.ClusterFactory = clusterFactory;
        }

        public override string ToString()
        {
            return $"{Label} ({ClusterId})";
        }

        static ZclClusterType()
        {
            _idValueMap = new Dictionary<ushort, ZclClusterType>
            {
                { 0x0000, new ZclClusterType(0x0000, "Basic", ClusterType.BASIC, (endpoint) => new ZclBasicCluster(endpoint)) },
                { 0x0001, new ZclClusterType(0x0001, "Power Configuration", ClusterType.POWER_CONFIGURATION, (endpoint) => new ZclPowerConfigurationCluster(endpoint)) },
                { 0x0003, new ZclClusterType(0x0003, "Identify", ClusterType.IDENTIFY, (endpoint) => new ZclIdentifyCluster(endpoint)) },
                { 0x0004, new ZclClusterType(0x0004, "Groups", ClusterType.GROUPS, (endpoint) => new ZclGroupsCluster(endpoint)) },
                { 0x0005, new ZclClusterType(0x0005, "Scenes", ClusterType.SCENES, (endpoint) => new ZclScenesCluster(endpoint)) },
                { 0x0006, new ZclClusterType(0x0006, "On/Off", ClusterType.ON_OFF, (endpoint) => new ZclOnOffCluster(endpoint)) },
                { 0x0007, new ZclClusterType(0x0007, "On / Off Switch Configuration", ClusterType.ON_OFF_SWITCH_CONFIGURATION, (endpoint) => new ZclOnOffSwitchConfigurationCluster(endpoint)) },
                { 0x0008, new ZclClusterType(0x0008, "Level Control", ClusterType.LEVEL_CONTROL, (endpoint) => new ZclLevelControlCluster(endpoint)) },
                { 0x0009, new ZclClusterType(0x0009, "Alarms", ClusterType.ALARMS, (endpoint) => new ZclAlarmsCluster(endpoint)) },
                { 0x000A, new ZclClusterType(0x000A, "Time", ClusterType.TIME, (endpoint) => new ZclTimeCluster(endpoint)) },
                { 0x000B, new ZclClusterType(0x000B, "RSSI Location", ClusterType.RSSI_LOCATION, (endpoint) => new ZclRssiLocationCluster(endpoint)) },
                { 0x000C, new ZclClusterType(0x000C, "Analog Input (Basic)", ClusterType.ANALOG_INPUT_BASIC, (endpoint) => new ZclAnalogInputBasicCluster(endpoint)) },
                { 0x000F, new ZclClusterType(0x000F, "Binary Input (Basic)", ClusterType.BINARY_INPUT_BASIC, (endpoint) => new ZclBinaryInputBasicCluster(endpoint)) },
                { 0x0012, new ZclClusterType(0x0012, "Multistate Input (Basic)", ClusterType.MULTISTATE_INPUT_BASIC, (endpoint) => new ZclMultistateInputBasicCluster(endpoint)) },
                { 0x0013, new ZclClusterType(0x0013, "Multistate Output (Basic)", ClusterType.MULTISTATE_OUTPUT_BASIC, (endpoint) => new ZclMultistateOutputBasicCluster(endpoint)) },
                { 0x0014, new ZclClusterType(0x0014, "Multistate Value (Basic)", ClusterType.MULTISTATE_VALUE_BASIC, (endpoint) => new ZclMultistateValueBasicCluster(endpoint)) },
                { 0x0015, new ZclClusterType(0x0015, "Commissioning", ClusterType.COMMISSIONING, (endpoint) => new ZclCommissioningCluster(endpoint)) },
                { 0x0019, new ZclClusterType(0x0019, "Ota Upgrade", ClusterType.OTA_UPGRADE, (endpoint) => new ZclOtaUpgradeCluster(endpoint)) },
                { 0x0020, new ZclClusterType(0x0020, "Poll Control", ClusterType.POLL_CONTROL, (endpoint) => new ZclPollControlCluster(endpoint)) },
                { 0x0021, new ZclClusterType(0x0021, "Green Power", ClusterType.GREEN_POWER, (endpoint) => new ZclGreenPowerCluster(endpoint)) },
                { 0x0101, new ZclClusterType(0x0101, "Door Lock", ClusterType.DOOR_LOCK, (endpoint) => new ZclDoorLockCluster(endpoint)) },
                { 0x0102, new ZclClusterType(0x0102, "Window Covering", ClusterType.WINDOW_COVERING, (endpoint) => new ZclWindowCoveringCluster(endpoint)) },
                { 0x0201, new ZclClusterType(0x0201, "Thermostat", ClusterType.THERMOSTAT, (endpoint) => new ZclThermostatCluster(endpoint)) },
                { 0x0202, new ZclClusterType(0x0202, "Fan Control", ClusterType.FAN_CONTROL, (endpoint) => new ZclFanControlCluster(endpoint)) },
                { 0x0203, new ZclClusterType(0x0203, "Dehumidification Control", ClusterType.DEHUMIDIFICATION_CONTROL, (endpoint) => new ZclDehumidificationControlCluster(endpoint)) },
                { 0x0204, new ZclClusterType(0x0204, "Thermostat User Interface Configuration", ClusterType.THERMOSTAT_USER_INTERFACE_CONFIGURATION, (endpoint) => new ZclThermostatUserInterfaceConfigurationCluster(endpoint)) },
                { 0x0300, new ZclClusterType(0x0300, "Color Control", ClusterType.COLOR_CONTROL, (endpoint) => new ZclColorControlCluster(endpoint)) },
                { 0x0400, new ZclClusterType(0x0400, "Illuminance Measurement", ClusterType.ILLUMINANCE_MEASUREMENT, (endpoint) => new ZclIlluminanceMeasurementCluster(endpoint)) },
                { 0x0401, new ZclClusterType(0x0401, "Illuminance Level Sensing", ClusterType.ILLUMINANCE_LEVEL_SENSING, (endpoint) => new ZclIlluminanceLevelSensingCluster(endpoint)) },
                { 0x0402, new ZclClusterType(0x0402, "Temperature Measurement", ClusterType.TEMPERATURE_MEASUREMENT, (endpoint) => new ZclTemperatureMeasurementCluster(endpoint)) },
                { 0x0403, new ZclClusterType(0x0403, "Pressure Measurement", ClusterType.PRESSURE_MEASUREMENT, (endpoint) => new ZclPressureMeasurementCluster(endpoint)) },
                { 0x0404, new ZclClusterType(0x0404, "Flow Measurement", ClusterType.FLOW_MEASUREMENT, (endpoint) => new ZclFlowMeasurementCluster(endpoint)) },
                { 0x0405, new ZclClusterType(0x0405, "Relative Humidity Measurement", ClusterType.RELATIVE_HUMIDITY_MEASUREMENT, (endpoint) => new ZclRelativeHumidityMeasurementCluster(endpoint)) },
                { 0x0406, new ZclClusterType(0x0406, "Occupancy Sensing", ClusterType.OCCUPANCY_SENSING, (endpoint) => new ZclOccupancySensingCluster(endpoint)) },
                { 0x0500, new ZclClusterType(0x0500, "IAS Zone", ClusterType.IAS_ZONE, (endpoint) => new ZclIasZoneCluster(endpoint)) },
                { 0x0501, new ZclClusterType(0x0501, "IAS ACE", ClusterType.IAS_ACE, (endpoint) => new ZclIasAceCluster(endpoint)) },
                { 0x0502, new ZclClusterType(0x0502, "IAS WD", ClusterType.IAS_WD, (endpoint) => new ZclIasWdCluster(endpoint)) },
                { 0x0700, new ZclClusterType(0x0700, "Price", ClusterType.PRICE, (endpoint) => new ZclPriceCluster(endpoint)) },
                { 0x0701, new ZclClusterType(0x0701, "Demand Response And Load Control", ClusterType.DEMAND_RESPONSE_AND_LOAD_CONTROL, (endpoint) => new ZclDemandResponseAndLoadControlCluster(endpoint)) },
                { 0x0702, new ZclClusterType(0x0702, "Metering", ClusterType.METERING, (endpoint) => new ZclMeteringCluster(endpoint)) },
                { 0x0703, new ZclClusterType(0x0703, "Messaging", ClusterType.MESSAGING, (endpoint) => new ZclMessagingCluster(endpoint)) },
                { 0x0705, new ZclClusterType(0x0705, "Prepayment", ClusterType.PREPAYMENT, (endpoint) => new ZclPrepaymentCluster(endpoint)) },
                { 0x0800, new ZclClusterType(0x0800, "Key Establishment", ClusterType.KEY_ESTABLISHMENT, (endpoint) => new ZclKeyEstablishmentCluster(endpoint)) },
                { 0x0B01, new ZclClusterType(0x0B01, "Meter Identification", ClusterType.METER_IDENTIFICATION, (endpoint) => new ZclMeterIdentificationCluster(endpoint)) },
                { 0x0B04, new ZclClusterType(0x0B04, "Electrical Measurement", ClusterType.ELECTRICAL_MEASUREMENT, (endpoint) => new ZclElectricalMeasurementCluster(endpoint)) },
                { 0x0B05, new ZclClusterType(0x0B05, "Diagnostics", ClusterType.DIAGNOSTICS, (endpoint) => new ZclDiagnosticsCluster(endpoint)) },
            };
        }

        public static ZclClusterType GetValueById(ushort clusterId)
        {
            if (_idValueMap.TryGetValue(clusterId, out ZclClusterType result))
                return result;
            else
                return null;
        }

        public static ZclClusterType GetValueById(ClusterType clusterId)
        {
            return _idValueMap[(ushort)clusterId];
        }
    }
}
