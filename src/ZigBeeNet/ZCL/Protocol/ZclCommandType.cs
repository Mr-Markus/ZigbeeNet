using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.ZCL.Clusters.OnOff;
using ZigBeeNet.ZCL.Clusters.General;
using ZigBeeNet.ZCL.Clusters.LevelControl;
using ZigBeeNet.ZCL.Clusters.Groups;
using ZigBeeNet.ZCL.Clusters.Scenes;
using ZigBeeNet.ZCL.Clusters.Alarms;
using ZigBeeNet.ZCL.Clusters.RSSILocation;
using ZigBeeNet.ZCL.Clusters.IASACE;
using ZigBeeNet.ZCL.Clusters.PollControl;
using ZigBeeNet.ZCL.Clusters.Thermostat;
using ZigBeeNet.ZCL.Clusters.ColorControl;
using ZigBeeNet.ZCL.Clusters.Identify;
using ZigBeeNet.ZCL.Clusters.OTAUpgrade;
using ZigBeeNet.ZCL.Clusters.IASZone;
using ZigBeeNet.ZCL.Clusters.DoorLock;
using ZigBeeNet.ZCL.Clusters.Commissioning;
using ZigBeeNet.ZCL.Clusters.Basic;
using ZigBeeNet.ZCL.Clusters.IASWD;

namespace ZigBeeNet.ZCL.Protocol
{
    public class ZclCommandType
    {
        private static readonly List<ZclCommandType> _commands;

        public int ClusterType { get; set; }
        public int CommandId { get; private set; }
        public ZclCommandDirection Direction { get; private set; }
        public CommandType Type { get; set; }

        private ZclCommandType(int clusterType, int commandId, ZclCommandDirection direction, CommandType commandType)
        {
            this.ClusterType = clusterType;
            this.CommandId = commandId;
            this.Type = commandType;
            this.Direction = direction;
        }

        static ZclCommandType()
        {
            _commands = new List<ZclCommandType>
            {
                new ZclCommandType(0x0004, 0, ZclCommandDirection.CLIENT_TO_SERVER, CommandType.ADD_GROUP_COMMAND),
                new ZclCommandType(0x0004, 5, ZclCommandDirection.CLIENT_TO_SERVER, CommandType.ADD_GROUP_IF_IDENTIFYING_COMMAND),
                new ZclCommandType(0x0004, 0, ZclCommandDirection.SERVER_TO_CLIENT, CommandType.ADD_GROUP_RESPONSE),
                new ZclCommandType(0x0005, 0, ZclCommandDirection.CLIENT_TO_SERVER, CommandType.ADD_SCENE_COMMAND),
                new ZclCommandType(0x0005, 0, ZclCommandDirection.SERVER_TO_CLIENT, CommandType.ADD_SCENE_RESPONSE),
                new ZclCommandType(0x0009, 0, ZclCommandDirection.SERVER_TO_CLIENT, CommandType.ALARM_COMMAND),
                new ZclCommandType(0x000B, 6, ZclCommandDirection.CLIENT_TO_SERVER, CommandType.ANCHOR_NODE_ANNOUNCE_COMMAND),
                new ZclCommandType(0x0501, 0, ZclCommandDirection.CLIENT_TO_SERVER, CommandType.ARM_COMMAND),
                new ZclCommandType(0x0501, 0, ZclCommandDirection.SERVER_TO_CLIENT, CommandType.ARM_RESPONSE),
                new ZclCommandType(0x0501, 1, ZclCommandDirection.CLIENT_TO_SERVER, CommandType.BYPASS_COMMAND),
                new ZclCommandType(0x0501, 7, ZclCommandDirection.SERVER_TO_CLIENT, CommandType.BYPASS_RESPONSE),
                new ZclCommandType(0x0020, 0, ZclCommandDirection.SERVER_TO_CLIENT, CommandType.CHECK_IN_COMMAND),
                new ZclCommandType(0x0020, 0, ZclCommandDirection.CLIENT_TO_SERVER, CommandType.CHECK_IN_RESPONSE),
                new ZclCommandType(0x0201, 3, ZclCommandDirection.CLIENT_TO_SERVER, CommandType.CLEAR_WEEKLY_SCHEDULE),
                new ZclCommandType(0x0300, 67, ZclCommandDirection.CLIENT_TO_SERVER, CommandType.COLOR_LOOP_SET_COMMAND),
                new ZclCommandType(0x000B, 3, ZclCommandDirection.SERVER_TO_CLIENT, CommandType.COMPACT_LOCATION_DATA_NOTIFICATION_COMMAND),
                new ZclCommandType(0xFFFF, 6, ZclCommandDirection.CLIENT_TO_SERVER, CommandType.CONFIGURE_REPORTING_COMMAND),
                new ZclCommandType(0xFFFF, 7, ZclCommandDirection.CLIENT_TO_SERVER, CommandType.CONFIGURE_REPORTING_RESPONSE),
                new ZclCommandType(0xFFFF, 11, ZclCommandDirection.CLIENT_TO_SERVER, CommandType.DEFAULT_RESPONSE),
                new ZclCommandType(0x000B, 0, ZclCommandDirection.SERVER_TO_CLIENT, CommandType.DEVICE_CONFIGURATION_RESPONSE),
                new ZclCommandType(0xFFFF, 12, ZclCommandDirection.CLIENT_TO_SERVER, CommandType.DISCOVER_ATTRIBUTES_COMMAND),
                new ZclCommandType(0xFFFF, 21, ZclCommandDirection.CLIENT_TO_SERVER, CommandType.DISCOVER_ATTRIBUTES_EXTENDED),
                new ZclCommandType(0xFFFF, 22, ZclCommandDirection.CLIENT_TO_SERVER, CommandType.DISCOVER_ATTRIBUTES_EXTENDED_RESPONSE),
                new ZclCommandType(0xFFFF, 13, ZclCommandDirection.CLIENT_TO_SERVER, CommandType.DISCOVER_ATTRIBUTES_RESPONSE),
                new ZclCommandType(0xFFFF, 19, ZclCommandDirection.CLIENT_TO_SERVER, CommandType.DISCOVER_COMMANDS_GENERATED),
                new ZclCommandType(0xFFFF, 20, ZclCommandDirection.CLIENT_TO_SERVER, CommandType.DISCOVER_COMMANDS_GENERATED_RESPONSE),
                new ZclCommandType(0xFFFF, 17, ZclCommandDirection.CLIENT_TO_SERVER, CommandType.DISCOVER_COMMANDS_RECEIVED),
                new ZclCommandType(0xFFFF, 18, ZclCommandDirection.CLIENT_TO_SERVER, CommandType.DISCOVER_COMMANDS_RECEIVED_RESPONSE),
                new ZclCommandType(0x0501, 2, ZclCommandDirection.CLIENT_TO_SERVER, CommandType.EMERGENCY_COMMAND),
                new ZclCommandType(0x0300, 66, ZclCommandDirection.CLIENT_TO_SERVER, CommandType.ENHANCED_MOVE_TO_HUE_AND_SATURATION_COMMAND),
                new ZclCommandType(0x0300, 64, ZclCommandDirection.CLIENT_TO_SERVER, CommandType.ENHANCED_MOVE_TO_HUE_COMMAND),
                new ZclCommandType(0x0300, 65, ZclCommandDirection.CLIENT_TO_SERVER, CommandType.ENHANCED_STEP_HUE_COMMAND),
                new ZclCommandType(0x0020, 1, ZclCommandDirection.CLIENT_TO_SERVER, CommandType.FAST_POLL_STOP_COMMAND),
                new ZclCommandType(0x0501, 3, ZclCommandDirection.CLIENT_TO_SERVER, CommandType.FIRE_COMMAND),
                new ZclCommandType(0x0009, 2, ZclCommandDirection.CLIENT_TO_SERVER, CommandType.GET_ALARM_COMMAND),
                new ZclCommandType(0x0009, 1, ZclCommandDirection.SERVER_TO_CLIENT, CommandType.GET_ALARM_RESPONSE),
                new ZclCommandType(0x0501, 8, ZclCommandDirection.CLIENT_TO_SERVER, CommandType.GET_BYPASSED_ZONE_LIST_COMMAND),
                new ZclCommandType(0x000B, 2, ZclCommandDirection.CLIENT_TO_SERVER, CommandType.GET_DEVICE_CONFIGURATION_COMMAND),
                new ZclCommandType(0x0004, 2, ZclCommandDirection.CLIENT_TO_SERVER, CommandType.GET_GROUP_MEMBERSHIP_COMMAND),
                new ZclCommandType(0x0004, 2, ZclCommandDirection.SERVER_TO_CLIENT, CommandType.GET_GROUP_MEMBERSHIP_RESPONSE),
                new ZclCommandType(0x000B, 3, ZclCommandDirection.CLIENT_TO_SERVER, CommandType.GET_LOCATION_DATA_COMMAND),
                new ZclCommandType(0x0501, 7, ZclCommandDirection.CLIENT_TO_SERVER, CommandType.GET_PANEL_STATUS_COMMAND),
                new ZclCommandType(0x0501, 5, ZclCommandDirection.SERVER_TO_CLIENT, CommandType.GET_PANEL_STATUS_RESPONSE),
                new ZclCommandType(0x0201, 4, ZclCommandDirection.CLIENT_TO_SERVER, CommandType.GET_RELAY_STATUS_LOG),
                new ZclCommandType(0x0201, 1, ZclCommandDirection.SERVER_TO_CLIENT, CommandType.GET_RELAY_STATUS_LOG_RESPONSE),
                new ZclCommandType(0x0005, 6, ZclCommandDirection.CLIENT_TO_SERVER, CommandType.GET_SCENE_MEMBERSHIP_COMMAND),
                new ZclCommandType(0x0005, 5, ZclCommandDirection.SERVER_TO_CLIENT, CommandType.GET_SCENE_MEMBERSHIP_RESPONSE),
                new ZclCommandType(0x0201, 2, ZclCommandDirection.CLIENT_TO_SERVER, CommandType.GET_WEEKLY_SCHEDULE),
                new ZclCommandType(0x0201, 0, ZclCommandDirection.SERVER_TO_CLIENT, CommandType.GET_WEEKLY_SCHEDULE_RESPONSE),
                new ZclCommandType(0x0501, 5, ZclCommandDirection.CLIENT_TO_SERVER, CommandType.GET_ZONE_ID_MAP_COMMAND),
                new ZclCommandType(0x0501, 1, ZclCommandDirection.SERVER_TO_CLIENT, CommandType.GET_ZONE_ID_MAP_RESPONSE),
                new ZclCommandType(0x0501, 6, ZclCommandDirection.CLIENT_TO_SERVER, CommandType.GET_ZONE_INFORMATION_COMMAND),
                new ZclCommandType(0x0501, 2, ZclCommandDirection.SERVER_TO_CLIENT, CommandType.GET_ZONE_INFORMATION_RESPONSE),
                new ZclCommandType(0x0501, 9, ZclCommandDirection.CLIENT_TO_SERVER, CommandType.GET_ZONE_STATUS_COMMAND),
                new ZclCommandType(0x0501, 8, ZclCommandDirection.SERVER_TO_CLIENT, CommandType.GET_ZONE_STATUS_RESPONSE),
                new ZclCommandType(0x0003, 0, ZclCommandDirection.CLIENT_TO_SERVER, CommandType.IDENTIFY_COMMAND),
                new ZclCommandType(0x0003, 1, ZclCommandDirection.CLIENT_TO_SERVER, CommandType.IDENTIFY_QUERY_COMMAND),
                new ZclCommandType(0x0003, 0, ZclCommandDirection.SERVER_TO_CLIENT, CommandType.IDENTIFY_QUERY_RESPONSE),
                new ZclCommandType(0x0019, 3, ZclCommandDirection.CLIENT_TO_SERVER, CommandType.IMAGE_BLOCK_COMMAND),
                new ZclCommandType(0x0019, 5, ZclCommandDirection.SERVER_TO_CLIENT, CommandType.IMAGE_BLOCK_RESPONSE),
                new ZclCommandType(0x0019, 0, ZclCommandDirection.CLIENT_TO_SERVER, CommandType.IMAGE_NOTIFY_COMMAND),
                new ZclCommandType(0x0019, 4, ZclCommandDirection.CLIENT_TO_SERVER, CommandType.IMAGE_PAGE_COMMAND),
                new ZclCommandType(0x0500, 1, ZclCommandDirection.CLIENT_TO_SERVER, CommandType.INITIATE_NORMAL_OPERATION_MODE_COMMAND),
                new ZclCommandType(0x0500, 2, ZclCommandDirection.CLIENT_TO_SERVER, CommandType.INITIATE_TEST_MODE_COMMAND),
                new ZclCommandType(0x000B, 2, ZclCommandDirection.SERVER_TO_CLIENT, CommandType.LOCATION_DATA_NOTIFICATION_COMMAND),
                new ZclCommandType(0x000B, 1, ZclCommandDirection.SERVER_TO_CLIENT, CommandType.LOCATION_DATA_RESPONSE),
                new ZclCommandType(0x0101, 0, ZclCommandDirection.CLIENT_TO_SERVER, CommandType.LOCK_DOOR_COMMAND),
                new ZclCommandType(0x0101, 0, ZclCommandDirection.SERVER_TO_CLIENT, CommandType.LOCK_DOOR_RESPONSE),
                new ZclCommandType(0x0300, 8, ZclCommandDirection.CLIENT_TO_SERVER, CommandType.MOVE_COLOR_COMMAND),
                new ZclCommandType(0x0008, 1, ZclCommandDirection.CLIENT_TO_SERVER, CommandType.MOVE_COMMAND),
                new ZclCommandType(0x0300, 1, ZclCommandDirection.CLIENT_TO_SERVER, CommandType.MOVE_HUE_COMMAND),
                new ZclCommandType(0x0300, 4, ZclCommandDirection.CLIENT_TO_SERVER, CommandType.MOVE_SATURATION_COMMAND),
                new ZclCommandType(0x0300, 7, ZclCommandDirection.CLIENT_TO_SERVER, CommandType.MOVE_TO_COLOR_COMMAND),
                new ZclCommandType(0x0300, 10, ZclCommandDirection.CLIENT_TO_SERVER, CommandType.MOVE_TO_COLOR_TEMPERATURE_COMMAND),
                new ZclCommandType(0x0300, 6, ZclCommandDirection.CLIENT_TO_SERVER, CommandType.MOVE_TO_HUE_AND_SATURATION_COMMAND),
                new ZclCommandType(0x0300, 0, ZclCommandDirection.CLIENT_TO_SERVER, CommandType.MOVE_TO_HUE_COMMAND),
                new ZclCommandType(0x0008, 0, ZclCommandDirection.CLIENT_TO_SERVER, CommandType.MOVE_TO_LEVEL_COMMAND),
                new ZclCommandType(0x0008, 4, ZclCommandDirection.CLIENT_TO_SERVER, CommandType.MOVE_TO_LEVEL__WITH_ON_OFF__COMMAND),
                new ZclCommandType(0x0300, 3, ZclCommandDirection.CLIENT_TO_SERVER, CommandType.MOVE_TO_SATURATION_COMMAND),
                new ZclCommandType(0x0008, 5, ZclCommandDirection.CLIENT_TO_SERVER, CommandType.MOVE__WITH_ON_OFF__COMMAND),
                new ZclCommandType(0x0006, 0, ZclCommandDirection.CLIENT_TO_SERVER, CommandType.OFF_COMMAND),
                new ZclCommandType(0x0006, 64, ZclCommandDirection.CLIENT_TO_SERVER, CommandType.OFF_WITH_EFFECT_COMMAND),
                new ZclCommandType(0x0006, 1, ZclCommandDirection.CLIENT_TO_SERVER, CommandType.ON_COMMAND),
                new ZclCommandType(0x0006, 65, ZclCommandDirection.CLIENT_TO_SERVER, CommandType.ON_WITH_RECALL_GLOBAL_SCENE_COMMAND),
                new ZclCommandType(0x0006, 66, ZclCommandDirection.CLIENT_TO_SERVER, CommandType.ON_WITH_TIMED_OFF_COMMAND),
                new ZclCommandType(0x0501, 4, ZclCommandDirection.SERVER_TO_CLIENT, CommandType.PANEL_STATUS_CHANGED_COMMAND),
                new ZclCommandType(0x0501, 4, ZclCommandDirection.CLIENT_TO_SERVER, CommandType.PANIC_COMMAND),
                new ZclCommandType(0x0019, 1, ZclCommandDirection.CLIENT_TO_SERVER, CommandType.QUERY_NEXT_IMAGE_COMMAND),
                new ZclCommandType(0x0019, 2, ZclCommandDirection.SERVER_TO_CLIENT, CommandType.QUERY_NEXT_IMAGE_RESPONSE),
                new ZclCommandType(0x0019, 8, ZclCommandDirection.CLIENT_TO_SERVER, CommandType.QUERY_SPECIFIC_FILE_COMMAND),
                new ZclCommandType(0x0019, 9, ZclCommandDirection.SERVER_TO_CLIENT, CommandType.QUERY_SPECIFIC_FILE_RESPONSE),
                new ZclCommandType(0xFFFF, 0, ZclCommandDirection.CLIENT_TO_SERVER, CommandType.READ_ATTRIBUTES_COMMAND),
                new ZclCommandType(0xFFFF, 1, ZclCommandDirection.CLIENT_TO_SERVER, CommandType.READ_ATTRIBUTES_RESPONSE),
                new ZclCommandType(0xFFFF, 14, ZclCommandDirection.CLIENT_TO_SERVER, CommandType.READ_ATTRIBUTES_STRUCTURED_COMMAND),
                new ZclCommandType(0xFFFF, 8, ZclCommandDirection.CLIENT_TO_SERVER, CommandType.READ_REPORTING_CONFIGURATION_COMMAND),
                new ZclCommandType(0xFFFF, 9, ZclCommandDirection.CLIENT_TO_SERVER, CommandType.READ_REPORTING_CONFIGURATION_RESPONSE),
                new ZclCommandType(0x0005, 5, ZclCommandDirection.CLIENT_TO_SERVER, CommandType.RECALL_SCENE_COMMAND),
                new ZclCommandType(0x0004, 4, ZclCommandDirection.CLIENT_TO_SERVER, CommandType.REMOVE_ALL_GROUPS_COMMAND),
                new ZclCommandType(0x0005, 3, ZclCommandDirection.CLIENT_TO_SERVER, CommandType.REMOVE_ALL_SCENES_COMMAND),
                new ZclCommandType(0x0005, 3, ZclCommandDirection.SERVER_TO_CLIENT, CommandType.REMOVE_ALL_SCENES_RESPONSE),
                new ZclCommandType(0x0004, 3, ZclCommandDirection.CLIENT_TO_SERVER, CommandType.REMOVE_GROUP_COMMAND),
                new ZclCommandType(0x0004, 3, ZclCommandDirection.SERVER_TO_CLIENT, CommandType.REMOVE_GROUP_RESPONSE),
                new ZclCommandType(0x0005, 2, ZclCommandDirection.CLIENT_TO_SERVER, CommandType.REMOVE_SCENE_COMMAND),
                new ZclCommandType(0x0005, 2, ZclCommandDirection.SERVER_TO_CLIENT, CommandType.REMOVE_SCENE_RESPONSE),
                new ZclCommandType(0xFFFF, 10, ZclCommandDirection.CLIENT_TO_SERVER, CommandType.REPORT_ATTRIBUTES_COMMAND),
                new ZclCommandType(0x000B, 6, ZclCommandDirection.SERVER_TO_CLIENT, CommandType.REPORT_RSSI_MEASUREMENTS_COMMAND),
                new ZclCommandType(0x000B, 7, ZclCommandDirection.SERVER_TO_CLIENT, CommandType.REQUEST_OWN_LOCATION_COMMAND),
                new ZclCommandType(0x0009, 0, ZclCommandDirection.CLIENT_TO_SERVER, CommandType.RESET_ALARM_COMMAND),
                new ZclCommandType(0x0009, 3, ZclCommandDirection.CLIENT_TO_SERVER, CommandType.RESET_ALARM_LOG_COMMAND),
                new ZclCommandType(0x0009, 1, ZclCommandDirection.CLIENT_TO_SERVER, CommandType.RESET_ALL_ALARMS_COMMAND),
                new ZclCommandType(0x0015, 3, ZclCommandDirection.CLIENT_TO_SERVER, CommandType.RESET_STARTUP_PARAMETERS_COMMAND),
                new ZclCommandType(0x0015, 3, ZclCommandDirection.SERVER_TO_CLIENT, CommandType.RESET_STARTUP_PARAMETERS_RESPONSE),
                new ZclCommandType(0x0000, 0, ZclCommandDirection.CLIENT_TO_SERVER, CommandType.RESET_TO_FACTORY_DEFAULTS_COMMAND),
                new ZclCommandType(0x0015, 0, ZclCommandDirection.CLIENT_TO_SERVER, CommandType.RESTART_DEVICE_COMMAND),
                new ZclCommandType(0x0015, 0, ZclCommandDirection.SERVER_TO_CLIENT, CommandType.RESTART_DEVICE_RESPONSE_RESPONSE),
                new ZclCommandType(0x0015, 2, ZclCommandDirection.CLIENT_TO_SERVER, CommandType.RESTORE_STARTUP_PARAMETERS_COMMAND),
                new ZclCommandType(0x0015, 2, ZclCommandDirection.SERVER_TO_CLIENT, CommandType.RESTORE_STARTUP_PARAMETERS_RESPONSE),
                new ZclCommandType(0x000B, 4, ZclCommandDirection.SERVER_TO_CLIENT, CommandType.RSSI_PING_COMMAND),
                new ZclCommandType(0x000B, 5, ZclCommandDirection.SERVER_TO_CLIENT, CommandType.RSSI_REQUEST_COMMAND),
                new ZclCommandType(0x000B, 4, ZclCommandDirection.CLIENT_TO_SERVER, CommandType.RSSI_RESPONSE),
                new ZclCommandType(0x0015, 1, ZclCommandDirection.CLIENT_TO_SERVER, CommandType.SAVE_STARTUP_PARAMETERS_COMMAND),
                new ZclCommandType(0x0015, 1, ZclCommandDirection.SERVER_TO_CLIENT, CommandType.SAVE_STARTUP_PARAMETERS_RESPONSE),
                new ZclCommandType(0x000B, 5, ZclCommandDirection.CLIENT_TO_SERVER, CommandType.SEND_PINGS_COMMAND),
                new ZclCommandType(0x0201, 0, ZclCommandDirection.CLIENT_TO_SERVER, CommandType.SETPOINT_RAISE_LOWER_COMMAND),
                new ZclCommandType(0x000B, 0, ZclCommandDirection.CLIENT_TO_SERVER, CommandType.SET_ABSOLUTE_LOCATION_COMMAND),
                new ZclCommandType(0x0501, 6, ZclCommandDirection.SERVER_TO_CLIENT, CommandType.SET_BYPASSED_ZONE_LIST_COMMAND),
                new ZclCommandType(0x000B, 1, ZclCommandDirection.CLIENT_TO_SERVER, CommandType.SET_DEVICE_CONFIGURATION_COMMAND),
                new ZclCommandType(0x0020, 2, ZclCommandDirection.CLIENT_TO_SERVER, CommandType.SET_LONG_POLL_INTERVAL_COMMAND),
                new ZclCommandType(0x0020, 3, ZclCommandDirection.CLIENT_TO_SERVER, CommandType.SET_SHORT_POLL_INTERVAL_COMMAND),
                new ZclCommandType(0x0201, 1, ZclCommandDirection.CLIENT_TO_SERVER, CommandType.SET_WEEKLY_SCHEDULE),
                new ZclCommandType(0x0502, 2, ZclCommandDirection.CLIENT_TO_SERVER, CommandType.SQUAWK_COMMAND),
                new ZclCommandType(0x0502, 0, ZclCommandDirection.CLIENT_TO_SERVER, CommandType.START_WARNING_COMMAND),
                new ZclCommandType(0x0300, 9, ZclCommandDirection.CLIENT_TO_SERVER, CommandType.STEP_COLOR_COMMAND),
                new ZclCommandType(0x0008, 2, ZclCommandDirection.CLIENT_TO_SERVER, CommandType.STEP_COMMAND),
                new ZclCommandType(0x0300, 2, ZclCommandDirection.CLIENT_TO_SERVER, CommandType.STEP_HUE_COMMAND),
                new ZclCommandType(0x0300, 5, ZclCommandDirection.CLIENT_TO_SERVER, CommandType.STEP_SATURATION_COMMAND),
                new ZclCommandType(0x0008, 6, ZclCommandDirection.CLIENT_TO_SERVER, CommandType.STEP__WITH_ON_OFF__COMMAND),
                new ZclCommandType(0x0008, 7, ZclCommandDirection.CLIENT_TO_SERVER, CommandType.STOP_2_COMMAND),
                new ZclCommandType(0x0008, 3, ZclCommandDirection.CLIENT_TO_SERVER, CommandType.STOP_COMMAND),
                new ZclCommandType(0x0005, 4, ZclCommandDirection.CLIENT_TO_SERVER, CommandType.STORE_SCENE_COMMAND),
                new ZclCommandType(0x0005, 4, ZclCommandDirection.SERVER_TO_CLIENT, CommandType.STORE_SCENE_RESPONSE),
                new ZclCommandType(0x0006, 2, ZclCommandDirection.CLIENT_TO_SERVER, CommandType.TOGGLE_COMMAND),
                new ZclCommandType(0x0101, 1, ZclCommandDirection.CLIENT_TO_SERVER, CommandType.UNLOCK_DOOR_COMMAND),
                new ZclCommandType(0x0101, 1, ZclCommandDirection.SERVER_TO_CLIENT, CommandType.UNLOCK_DOOR_RESPONSE),
                new ZclCommandType(0x0019, 6, ZclCommandDirection.CLIENT_TO_SERVER, CommandType.UPGRADE_END_COMMAND),
                new ZclCommandType(0x0019, 7, ZclCommandDirection.SERVER_TO_CLIENT, CommandType.UPGRADE_END_RESPONSE),
                new ZclCommandType(0x0004, 1, ZclCommandDirection.CLIENT_TO_SERVER, CommandType.VIEW_GROUP_COMMAND),
                new ZclCommandType(0x0004, 1, ZclCommandDirection.SERVER_TO_CLIENT, CommandType.VIEW_GROUP_RESPONSE),
                new ZclCommandType(0x0005, 1, ZclCommandDirection.CLIENT_TO_SERVER, CommandType.VIEW_SCENE_COMMAND),
                new ZclCommandType(0x0005, 1, ZclCommandDirection.SERVER_TO_CLIENT, CommandType.VIEW_SCENE_RESPONSE),
                new ZclCommandType(0xFFFF, 2, ZclCommandDirection.CLIENT_TO_SERVER, CommandType.WRITE_ATTRIBUTES_COMMAND),
                new ZclCommandType(0xFFFF, 5, ZclCommandDirection.CLIENT_TO_SERVER, CommandType.WRITE_ATTRIBUTES_NO_RESPONSE),
                new ZclCommandType(0xFFFF, 4, ZclCommandDirection.CLIENT_TO_SERVER, CommandType.WRITE_ATTRIBUTES_RESPONSE),
                new ZclCommandType(0xFFFF, 15, ZclCommandDirection.CLIENT_TO_SERVER, CommandType.WRITE_ATTRIBUTES_STRUCTURED_COMMAND),
                new ZclCommandType(0xFFFF, 16, ZclCommandDirection.CLIENT_TO_SERVER, CommandType.WRITE_ATTRIBUTES_STRUCTURED_RESPONSE),
                new ZclCommandType(0xFFFF, 3, ZclCommandDirection.CLIENT_TO_SERVER, CommandType.WRITE_ATTRIBUTES_UNDIVIDED_COMMAND),
                new ZclCommandType(0x0500, 1, ZclCommandDirection.SERVER_TO_CLIENT, CommandType.ZONE_ENROLL_REQUEST_COMMAND),
                new ZclCommandType(0x0500, 0, ZclCommandDirection.CLIENT_TO_SERVER, CommandType.ZONE_ENROLL_RESPONSE),
                new ZclCommandType(0x0501, 3, ZclCommandDirection.SERVER_TO_CLIENT, CommandType.ZONE_STATUS_CHANGED_COMMAND),
                new ZclCommandType(0x0500, 0, ZclCommandDirection.SERVER_TO_CLIENT, CommandType.ZONE_STATUS_CHANGE_NOTIFICATION_COMMAND),
            };
        }

        public static ZclCommandType GetCommandType(ushort clusterType, int commandId, ZclCommandDirection direction)
        {
            foreach (ZclCommandType value in _commands)
            {
                if (value.Direction == direction && value.ClusterType == clusterType && value.CommandId == commandId)
                {
                    return value;
                }
            }

            return null;
        }

        public static ZclCommandType GetGeneric(int commandId)
        {
            foreach (ZclCommandType value in _commands)
            {
                if (value.ClusterType == 0xFFFF && value.CommandId == commandId)
                {
                    return value;
                }
            }
            return null;
        }

        public ZclCommand GetCommand()
        {
            switch (Type)
            {
                case CommandType.ADD_GROUP_COMMAND:
                    return new AddGroupCommand();
                case CommandType.ADD_GROUP_IF_IDENTIFYING_COMMAND:
                    return new AddGroupIfIdentifyingCommand();
                case CommandType.ADD_GROUP_RESPONSE:
                    return new AddGroupResponse();
                case CommandType.ADD_SCENE_COMMAND:
                    return new AddSceneCommand();
                case CommandType.ADD_SCENE_RESPONSE:
                    return new AddSceneResponse();
                case CommandType.ALARM_COMMAND:
                    return new AlarmCommand();
                case CommandType.ANCHOR_NODE_ANNOUNCE_COMMAND:
                    return new AnchorNodeAnnounceCommand();
                case CommandType.ARM_COMMAND:
                    return new ArmCommand();
                case CommandType.ARM_RESPONSE:
                    return new ArmResponse();
                case CommandType.BYPASS_COMMAND:
                    return new BypassCommand();
                case CommandType.BYPASS_RESPONSE:
                    return new BypassResponse();
                case CommandType.CHECK_IN_COMMAND:
                    return new CheckInCommand();
                case CommandType.CHECK_IN_RESPONSE:
                    return new CheckInResponse();
                case CommandType.CLEAR_WEEKLY_SCHEDULE:
                    return new ClearWeeklySchedule();
                case CommandType.COLOR_LOOP_SET_COMMAND:
                    return new ColorLoopSetCommand();
                case CommandType.COMPACT_LOCATION_DATA_NOTIFICATION_COMMAND:
                    return new CompactLocationDataNotificationCommand();
                case CommandType.CONFIGURE_REPORTING_COMMAND:
                    return new ConfigureReportingCommand();
                case CommandType.CONFIGURE_REPORTING_RESPONSE:
                    return new ConfigureReportingResponse();
                case CommandType.DEFAULT_RESPONSE:
                    return new DefaultResponse();
                case CommandType.DEVICE_CONFIGURATION_RESPONSE:
                    return new DeviceConfigurationResponse();
                case CommandType.DISCOVER_ATTRIBUTES_COMMAND:
                    return new DiscoverAttributesCommand();
                case CommandType.DISCOVER_ATTRIBUTES_EXTENDED:
                    return new DiscoverAttributesExtended();
                case CommandType.DISCOVER_ATTRIBUTES_EXTENDED_RESPONSE:
                    return new DiscoverAttributesExtendedResponse();
                case CommandType.DISCOVER_ATTRIBUTES_RESPONSE:
                    return new DiscoverAttributesResponse();
                case CommandType.DISCOVER_COMMANDS_GENERATED:
                    return new DiscoverCommandsGenerated();
                case CommandType.DISCOVER_COMMANDS_GENERATED_RESPONSE:
                    return new DiscoverCommandsGeneratedResponse();
                case CommandType.DISCOVER_COMMANDS_RECEIVED:
                    return new DiscoverCommandsReceived();
                case CommandType.DISCOVER_COMMANDS_RECEIVED_RESPONSE:
                    return new DiscoverCommandsReceivedResponse();
                case CommandType.EMERGENCY_COMMAND:
                    return new EmergencyCommand();
                case CommandType.ENHANCED_MOVE_TO_HUE_AND_SATURATION_COMMAND:
                    return new EnhancedMoveToHueAndSaturationCommand();
                case CommandType.ENHANCED_MOVE_TO_HUE_COMMAND:
                    return new EnhancedMoveToHueCommand();
                case CommandType.ENHANCED_STEP_HUE_COMMAND:
                    return new EnhancedStepHueCommand();
                case CommandType.FAST_POLL_STOP_COMMAND:
                    return new FastPollStopCommand();
                case CommandType.FIRE_COMMAND:
                    return new FireCommand();
                case CommandType.GET_ALARM_COMMAND:
                    return new GetAlarmCommand();
                case CommandType.GET_ALARM_RESPONSE:
                    return new GetAlarmResponse();
                case CommandType.GET_BYPASSED_ZONE_LIST_COMMAND:
                    return new GetBypassedZoneListCommand();
                case CommandType.GET_DEVICE_CONFIGURATION_COMMAND:
                    return new GetDeviceConfigurationCommand();
                case CommandType.GET_GROUP_MEMBERSHIP_COMMAND:
                    return new GetGroupMembershipCommand();
                case CommandType.GET_GROUP_MEMBERSHIP_RESPONSE:
                    return new GetGroupMembershipResponse();
                case CommandType.GET_LOCATION_DATA_COMMAND:
                    return new GetLocationDataCommand();
                case CommandType.GET_PANEL_STATUS_COMMAND:
                    return new GetPanelStatusCommand();
                case CommandType.GET_PANEL_STATUS_RESPONSE:
                    return new GetPanelStatusResponse();
                case CommandType.GET_RELAY_STATUS_LOG:
                    return new GetRelayStatusLog();
                case CommandType.GET_RELAY_STATUS_LOG_RESPONSE:
                    return new GetRelayStatusLogResponse();
                case CommandType.GET_SCENE_MEMBERSHIP_COMMAND:
                    return new GetSceneMembershipCommand();
                case CommandType.GET_SCENE_MEMBERSHIP_RESPONSE:
                    return new GetSceneMembershipResponse();
                case CommandType.GET_WEEKLY_SCHEDULE:
                    return new GetWeeklySchedule();
                case CommandType.GET_WEEKLY_SCHEDULE_RESPONSE:
                    return new GetWeeklyScheduleResponse();
                case CommandType.GET_ZONE_ID_MAP_COMMAND:
                    return new GetZoneIDMapCommand();
                case CommandType.GET_ZONE_ID_MAP_RESPONSE:
                    return new GetZoneIDMapResponse();
                case CommandType.GET_ZONE_INFORMATION_COMMAND:
                    return new GetZoneInformationCommand();
                case CommandType.GET_ZONE_INFORMATION_RESPONSE:
                    return new GetZoneInformationResponse();
                case CommandType.GET_ZONE_STATUS_COMMAND:
                    return new GetZoneStatusCommand();
                case CommandType.GET_ZONE_STATUS_RESPONSE:
                    return new GetZoneStatusResponse();
                case CommandType.IDENTIFY_COMMAND:
                    return new IdentifyCommand();
                case CommandType.IDENTIFY_QUERY_COMMAND:
                    return new IdentifyQueryCommand();
                case CommandType.IDENTIFY_QUERY_RESPONSE:
                    return new IdentifyQueryResponse();
                case CommandType.IMAGE_BLOCK_COMMAND:
                    return new ImageBlockCommand();
                case CommandType.IMAGE_BLOCK_RESPONSE:
                    return new ImageBlockResponse();
                case CommandType.IMAGE_NOTIFY_COMMAND:
                    return new ImageNotifyCommand();
                case CommandType.IMAGE_PAGE_COMMAND:
                    return new ImagePageCommand();
                case CommandType.INITIATE_NORMAL_OPERATION_MODE_COMMAND:
                    return new InitiateNormalOperationModeCommand();
                case CommandType.INITIATE_TEST_MODE_COMMAND:
                    return new InitiateTestModeCommand();
                case CommandType.LOCATION_DATA_NOTIFICATION_COMMAND:
                    return new LocationDataNotificationCommand();
                case CommandType.LOCATION_DATA_RESPONSE:
                    return new LocationDataResponse();
                case CommandType.LOCK_DOOR_COMMAND:
                    return new LockDoorCommand();
                case CommandType.LOCK_DOOR_RESPONSE:
                    return new LockDoorResponse();
                case CommandType.MOVE_COLOR_COMMAND:
                    return new MoveColorCommand();
                case CommandType.MOVE_COMMAND:
                    return new MoveCommand();
                case CommandType.MOVE_HUE_COMMAND:
                    return new MoveHueCommand();
                case CommandType.MOVE_SATURATION_COMMAND:
                    return new MoveSaturationCommand();
                case CommandType.MOVE_TO_COLOR_COMMAND:
                    return new MoveToColorCommand();
                case CommandType.MOVE_TO_COLOR_TEMPERATURE_COMMAND:
                    return new MoveToColorTemperatureCommand();
                case CommandType.MOVE_TO_HUE_AND_SATURATION_COMMAND:
                    return new MoveToHueAndSaturationCommand();
                case CommandType.MOVE_TO_HUE_COMMAND:
                    return new MoveToHueCommand();
                case CommandType.MOVE_TO_LEVEL_COMMAND:
                    return new MoveToLevelCommand();
                case CommandType.MOVE_TO_LEVEL__WITH_ON_OFF__COMMAND:
                    return new MoveToLevelWithOnOffCommand();
                case CommandType.MOVE_TO_SATURATION_COMMAND:
                    return new MoveToSaturationCommand();
                case CommandType.MOVE__WITH_ON_OFF__COMMAND:
                    return new MoveWithOnOffCommand();
                case CommandType.OFF_COMMAND:
                    return new OffCommand();
                case CommandType.OFF_WITH_EFFECT_COMMAND:
                    return new OffWithEffectCommand();
                case CommandType.ON_COMMAND:
                    return new OnCommand();
                case CommandType.ON_WITH_RECALL_GLOBAL_SCENE_COMMAND:
                    return new OnWithRecallGlobalSceneCommand();
                case CommandType.ON_WITH_TIMED_OFF_COMMAND:
                    return new OnWithTimedOffCommand();
                case CommandType.PANEL_STATUS_CHANGED_COMMAND:
                    return new PanelStatusChangedCommand();
                case CommandType.PANIC_COMMAND:
                    return new PanicCommand();
                case CommandType.QUERY_NEXT_IMAGE_COMMAND:
                    return new QueryNextImageCommand();
                case CommandType.QUERY_NEXT_IMAGE_RESPONSE:
                    return new QueryNextImageResponse();
                case CommandType.QUERY_SPECIFIC_FILE_COMMAND:
                    return new QuerySpecificFileCommand();
                case CommandType.QUERY_SPECIFIC_FILE_RESPONSE:
                    return new QuerySpecificFileResponse();
                case CommandType.READ_ATTRIBUTES_COMMAND:
                    return new ReadAttributesCommand();
                case CommandType.READ_ATTRIBUTES_RESPONSE:
                    return new ReadAttributesResponse();
                case CommandType.READ_ATTRIBUTES_STRUCTURED_COMMAND:
                    return new ReadAttributesStructuredCommand();
                case CommandType.READ_REPORTING_CONFIGURATION_COMMAND:
                    return new ReadReportingConfigurationCommand();
                case CommandType.READ_REPORTING_CONFIGURATION_RESPONSE:
                    return new ReadReportingConfigurationResponse();
                case CommandType.RECALL_SCENE_COMMAND:
                    return new RecallSceneCommand();
                case CommandType.REMOVE_ALL_GROUPS_COMMAND:
                    return new RemoveAllGroupsCommand();
                case CommandType.REMOVE_ALL_SCENES_COMMAND:
                    return new RemoveAllScenesCommand();
                case CommandType.REMOVE_ALL_SCENES_RESPONSE:
                    return new RemoveAllScenesResponse();
                case CommandType.REMOVE_GROUP_COMMAND:
                    return new RemoveGroupCommand();
                case CommandType.REMOVE_GROUP_RESPONSE:
                    return new RemoveGroupResponse();
                case CommandType.REMOVE_SCENE_COMMAND:
                    return new RemoveSceneCommand();
                case CommandType.REMOVE_SCENE_RESPONSE:
                    return new RemoveSceneResponse();
                case CommandType.REPORT_ATTRIBUTES_COMMAND:
                    return new ReportAttributesCommand();
                case CommandType.REPORT_RSSI_MEASUREMENTS_COMMAND:
                    return new ReportRSSIMeasurementsCommand();
                case CommandType.REQUEST_OWN_LOCATION_COMMAND:
                    return new RequestOwnLocationCommand();
                case CommandType.RESET_ALARM_COMMAND:
                    return new ResetAlarmCommand();
                case CommandType.RESET_ALARM_LOG_COMMAND:
                    return new ResetAlarmLogCommand();
                case CommandType.RESET_ALL_ALARMS_COMMAND:
                    return new ResetAllAlarmsCommand();
                case CommandType.RESET_STARTUP_PARAMETERS_COMMAND:
                    return new ResetStartupParametersCommand();
                case CommandType.RESET_STARTUP_PARAMETERS_RESPONSE:
                    return new ResetStartupParametersResponse();
                case CommandType.RESET_TO_FACTORY_DEFAULTS_COMMAND:
                    return new ResetToFactoryDefaultsCommand();
                case CommandType.RESTART_DEVICE_COMMAND:
                    return new RestartDeviceCommand();
                case CommandType.RESTART_DEVICE_RESPONSE_RESPONSE:
                    return new RestartDeviceResponseResponse();
                case CommandType.RESTORE_STARTUP_PARAMETERS_COMMAND:
                    return new RestoreStartupParametersCommand();
                case CommandType.RESTORE_STARTUP_PARAMETERS_RESPONSE:
                    return new RestoreStartupParametersResponse();
                case CommandType.RSSI_PING_COMMAND:
                    return new RSSIPingCommand();
                case CommandType.RSSI_REQUEST_COMMAND:
                    return new RSSIRequestCommand();
                case CommandType.RSSI_RESPONSE:
                    return new RSSIResponse();
                case CommandType.SAVE_STARTUP_PARAMETERS_COMMAND:
                    return new SaveStartupParametersCommand();
                case CommandType.SAVE_STARTUP_PARAMETERS_RESPONSE:
                    return new SaveStartupParametersResponse();
                case CommandType.SEND_PINGS_COMMAND:
                    return new SendPingsCommand();
                case CommandType.SETPOINT_RAISE_LOWER_COMMAND:
                    return new SetpointRaiseLowerCommand();
                case CommandType.SET_ABSOLUTE_LOCATION_COMMAND:
                    return new SetAbsoluteLocationCommand();
                case CommandType.SET_BYPASSED_ZONE_LIST_COMMAND:
                    return new SetBypassedZoneListCommand();
                case CommandType.SET_DEVICE_CONFIGURATION_COMMAND:
                    return new SetDeviceConfigurationCommand();
                case CommandType.SET_LONG_POLL_INTERVAL_COMMAND:
                    return new SetLongPollIntervalCommand();
                case CommandType.SET_SHORT_POLL_INTERVAL_COMMAND:
                    return new SetShortPollIntervalCommand();
                case CommandType.SET_WEEKLY_SCHEDULE:
                    return new SetWeeklySchedule();
                case CommandType.SQUAWK_COMMAND:
                    return new SquawkCommand();
                case CommandType.START_WARNING_COMMAND:
                    return new StartWarningCommand();
                case CommandType.STEP_COLOR_COMMAND:
                    return new StepColorCommand();
                case CommandType.STEP_COMMAND:
                    return new StepCommand();
                case CommandType.STEP_HUE_COMMAND:
                    return new StepHueCommand();
                case CommandType.STEP_SATURATION_COMMAND:
                    return new StepSaturationCommand();
                case CommandType.STEP__WITH_ON_OFF__COMMAND:
                    return new StepWithOnOffCommand();
                case CommandType.STOP_2_COMMAND:
                    return new Stop2Command();
                case CommandType.STOP_COMMAND:
                    return new StopCommand();
                case CommandType.STORE_SCENE_COMMAND:
                    return new StoreSceneCommand();
                case CommandType.STORE_SCENE_RESPONSE:
                    return new StoreSceneResponse();
                case CommandType.TOGGLE_COMMAND:
                    return new ToggleCommand();
                case CommandType.UNLOCK_DOOR_COMMAND:
                    return new UnlockDoorCommand();
                case CommandType.UNLOCK_DOOR_RESPONSE:
                    return new UnlockDoorResponse();
                case CommandType.UPGRADE_END_COMMAND:
                    return new UpgradeEndCommand();
                case CommandType.UPGRADE_END_RESPONSE:
                    return new UpgradeEndResponse();
                case CommandType.VIEW_GROUP_COMMAND:
                    return new ViewGroupCommand();
                case CommandType.VIEW_GROUP_RESPONSE:
                    return new ViewGroupResponse();
                case CommandType.VIEW_SCENE_COMMAND:
                    return new ViewSceneCommand();
                case CommandType.VIEW_SCENE_RESPONSE:
                    return new ViewSceneResponse();
                case CommandType.WRITE_ATTRIBUTES_COMMAND:
                    return new WriteAttributesCommand();
                case CommandType.WRITE_ATTRIBUTES_NO_RESPONSE:
                    return new WriteAttributesNoResponse();
                case CommandType.WRITE_ATTRIBUTES_RESPONSE:
                    return new WriteAttributesResponse();
                case CommandType.WRITE_ATTRIBUTES_STRUCTURED_COMMAND:
                    return new WriteAttributesStructuredCommand();
                case CommandType.WRITE_ATTRIBUTES_STRUCTURED_RESPONSE:
                    return new WriteAttributesStructuredResponse();
                case CommandType.WRITE_ATTRIBUTES_UNDIVIDED_COMMAND:
                    return new WriteAttributesUndividedCommand();
                case CommandType.ZONE_ENROLL_REQUEST_COMMAND:
                    return new ZoneEnrollRequestCommand();
                case CommandType.ZONE_ENROLL_RESPONSE:
                    return new ZoneEnrollResponse();
                case CommandType.ZONE_STATUS_CHANGED_COMMAND:
                    return new ZoneStatusChangedCommand();
                case CommandType.ZONE_STATUS_CHANGE_NOTIFICATION_COMMAND:
                    return new ZoneStatusChangeNotificationCommand();
                default:
                    throw new ArgumentException("Unknown ZclCommandType: " + Type.ToString());
            }
        }

         
    }
}
