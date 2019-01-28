using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.ZCL.Clusters.OnOff;
using ZigBeeNet.ZCL.Clusters.General;
using ZigBeeNet.ZCL.Clusters.LevelControl;

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
                //case CommandType.ADD_GROUP_COMMAND:
                //    return new AddGroupCommand();
                //case CommandType.ADD_GROUP_IF_IDENTIFYING_COMMAND:
                //    return new AddGroupIfIdentifyingCommand();
                //case CommandType.ADD_GROUP_RESPONSE:
                //    return new AddGroupResponse();
                //case CommandType.ADD_SCENE_COMMAND:
                //    return new AddSceneCommand();
                //case CommandType.ADD_SCENE_RESPONSE:
                //    return new AddSceneResponse();
                //case CommandType.ALARM_COMMAND:
                //    return new AlarmCommand();
                //case CommandType.ANCHOR_NODE_ANNOUNCE_COMMAND:
                //    return new AnchorNodeAnnounceCommand();
                //case CommandType.ARM_COMMAND:
                //    return new ArmCommand();
                //case CommandType.ARM_RESPONSE:
                //    return new ArmResponse();
                //case CommandType.BYPASS_COMMAND:
                //    return new BypassCommand();
                //case CommandType.BYPASS_RESPONSE:
                //    return new BypassResponse();
                //case CommandType.CHECK_IN_COMMAND:
                //    return new CheckInCommand();
                //case CommandType.CHECK_IN_RESPONSE:
                //    return new CheckInResponse();
                //case CommandType.CLEAR_WEEKLY_SCHEDULE:
                //    return new ClearWeeklySchedule();
                //case CommandType.COLOR_LOOP_SET_COMMAND:
                //    return new ColorLoopSetCommand();
                //case CommandType.COMPACT_LOCATION_DATA_NOTIFICATION_COMMAND:
                //    return new CompactLocationDataNotificationCommand();
                case CommandType.CONFIGURE_REPORTING_COMMAND:
                    return new ConfigureReportingCommand();
                case CommandType.CONFIGURE_REPORTING_RESPONSE:
                    return new ConfigureReportingResponse();
                case CommandType.DEFAULT_RESPONSE:
                    return new DefaultResponse();
                //case CommandType.DEVICE_CONFIGURATION_RESPONSE:
                //    return new DeviceConfigurationResponse();
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
                //case CommandType.EMERGENCY_COMMAND:
                //    return new EmergencyCommand();
                //case CommandType.ENHANCED_MOVE_TO_HUE_AND_SATURATION_COMMAND:
                //    return new EnhancedMoveToHueAndSaturationCommand();
                //case CommandType.ENHANCED_MOVE_TO_HUE_COMMAND:
                //    return new EnhancedMoveToHueCommand();
                //case CommandType.ENHANCED_STEP_HUE_COMMAND:
                //    return new EnhancedStepHueCommand();
                //case CommandType.FAST_POLL_STOP_COMMAND:
                //    return new FastPollStopCommand();
                //case CommandType.FIRE_COMMAND:
                //    return new FireCommand();
                //case CommandType.GET_ALARM_COMMAND:
                //    return new GetAlarmCommand();
                //case CommandType.GET_ALARM_RESPONSE:
                //    return new GetAlarmResponse();
                //case CommandType.GET_BYPASSED_ZONE_LIST_COMMAND:
                //    return new GetBypassedZoneListCommand();
                //case CommandType.GET_DEVICE_CONFIGURATION_COMMAND:
                //    return new GetDeviceConfigurationCommand();
                //case CommandType.GET_GROUP_MEMBERSHIP_COMMAND:
                //    return new GetGroupMembershipCommand();
                //case CommandType.GET_GROUP_MEMBERSHIP_RESPONSE:
                //    return new GetGroupMembershipResponse();
                //case CommandType.GET_LOCATION_DATA_COMMAND:
                //    return new GetLocationDataCommand();
                //case CommandType.GET_PANEL_STATUS_COMMAND:
                //    return new GetPanelStatusCommand();
                //case CommandType.GET_PANEL_STATUS_RESPONSE:
                //    return new GetPanelStatusResponse();
                //case CommandType.GET_RELAY_STATUS_LOG:
                //    return new GetRelayStatusLog();
                //case CommandType.GET_RELAY_STATUS_LOG_RESPONSE:
                //    return new GetRelayStatusLogResponse();
                //case CommandType.GET_SCENE_MEMBERSHIP_COMMAND:
                //    return new GetSceneMembershipCommand();
                //case CommandType.GET_SCENE_MEMBERSHIP_RESPONSE:
                //    return new GetSceneMembershipResponse();
                //case CommandType.GET_WEEKLY_SCHEDULE:
                //    return new GetWeeklySchedule();
                //case CommandType.GET_WEEKLY_SCHEDULE_RESPONSE:
                //    return new GetWeeklyScheduleResponse();
                //case CommandType.GET_ZONE_ID_MAP_COMMAND:
                //    return new GetZoneIdMapCommand();
                //case CommandType.GET_ZONE_ID_MAP_RESPONSE:
                //    return new GetZoneIdMapResponse();
                //case CommandType.GET_ZONE_INFORMATION_COMMAND:
                //    return new GetZoneInformationCommand();
                //case CommandType.GET_ZONE_INFORMATION_RESPONSE:
                //    return new GetZoneInformationResponse();
                //case CommandType.GET_ZONE_STATUS_COMMAND:
                //    return new GetZoneStatusCommand();
                //case CommandType.GET_ZONE_STATUS_RESPONSE:
                //    return new GetZoneStatusResponse();
                //case CommandType.IDENTIFY_COMMAND:
                //    return new IdentifyCommand();
                //case CommandType.IDENTIFY_QUERY_COMMAND:
                //    return new IdentifyQueryCommand();
                //case CommandType.IDENTIFY_QUERY_RESPONSE:
                //    return new IdentifyQueryResponse();
                //case CommandType.IMAGE_BLOCK_COMMAND:
                //    return new ImageBlockCommand();
                //case CommandType.IMAGE_BLOCK_RESPONSE:
                //    return new ImageBlockResponse();
                //case CommandType.IMAGE_NOTIFY_COMMAND:
                //    return new ImageNotifyCommand();
                //case CommandType.IMAGE_PAGE_COMMAND:
                //    return new ImagePageCommand();
                //case CommandType.INITIATE_NORMAL_OPERATION_MODE_COMMAND:
                //    return new InitiateNormalOperationModeCommand();
                //case CommandType.INITIATE_TEST_MODE_COMMAND:
                //    return new InitiateTestModeCommand();
                //case CommandType.LOCATION_DATA_NOTIFICATION_COMMAND:
                //    return new LocationDataNotificationCommand();
                //case CommandType.LOCATION_DATA_RESPONSE:
                //    return new LocationDataResponse();
                //case CommandType.LOCK_DOOR_COMMAND:
                //    return new LockDoorCommand();
                //case CommandType.LOCK_DOOR_RESPONSE:
                //    return new LockDoorResponse();
                //case CommandType.MOVE_COLOR_COMMAND:
                //    return new MoveColorCommand();
                //case CommandType.MOVE_COMMAND:
                //    return new MoveCommand();
                //case CommandType.MOVE_HUE_COMMAND:
                //    return new MoveHueCommand();
                //case CommandType.MOVE_SATURATION_COMMAND:
                //    return new MoveSaturationCommand();
                //case CommandType.MOVE_TO_COLOR_COMMAND:
                //    return new MoveToColorCommand();
                //case CommandType.MOVE_TO_COLOR_TEMPERATURE_COMMAND:
                //    return new MoveToColorTemperatureCommand();
                //case CommandType.MOVE_TO_HUE_AND_SATURATION_COMMAND:
                //    return new MoveToHueAndSaturationCommand();
                //case CommandType.MOVE_TO_HUE_COMMAND:
                //    return new MoveToHueCommand();
                case CommandType.MOVE_TO_LEVEL_COMMAND:
                    return new MoveToLevelCommand();
                //case CommandType.MOVE_TO_LEVEL__WITH_ON_OFF__COMMAND:
                //    return new MoveToLevelWithOnOffCommand();
                //case CommandType.MOVE_TO_SATURATION_COMMAND:
                //    return new MoveToSaturationCommand();
                //case CommandType.MOVE__WITH_ON_OFF__COMMAND:
                //    return new MoveWithOnOffCommand();
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
                //case CommandType.PANEL_STATUS_CHANGED_COMMAND:
                //    return new PanelStatusChangedCommand();
                //case CommandType.PANIC_COMMAND:
                //    return new PanicCommand();
                //case CommandType.QUERY_NEXT_IMAGE_COMMAND:
                //    return new QueryNextImageCommand();
                //case CommandType.QUERY_NEXT_IMAGE_RESPONSE:
                //    return new QueryNextImageResponse();
                //case CommandType.QUERY_SPECIFIC_FILE_COMMAND:
                //    return new QuerySpecificFileCommand();
                //case CommandType.QUERY_SPECIFIC_FILE_RESPONSE:
                //    return new QuerySpecificFileResponse();
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
                //case CommandType.RECALL_SCENE_COMMAND:
                //    return new RecallSceneCommand();
                //case CommandType.REMOVE_ALL_GROUPS_COMMAND:
                //    return new RemoveAllGroupsCommand();
                //case CommandType.REMOVE_ALL_SCENES_COMMAND:
                //    return new RemoveAllScenesCommand();
                //case CommandType.REMOVE_ALL_SCENES_RESPONSE:
                //    return new RemoveAllScenesResponse();
                //case CommandType.REMOVE_GROUP_COMMAND:
                //    return new RemoveGroupCommand();
                //case CommandType.REMOVE_GROUP_RESPONSE:
                //    return new RemoveGroupResponse();
                //case CommandType.REMOVE_SCENE_COMMAND:
                //    return new RemoveSceneCommand();
                //case CommandType.REMOVE_SCENE_RESPONSE:
                //    return new RemoveSceneResponse();
                case CommandType.REPORT_ATTRIBUTES_COMMAND:
                    return new ReportAttributesCommand();
                //case CommandType.REPORT_RSSI_MEASUREMENTS_COMMAND:
                //    return new ReportRssiMeasurementsCommand();
                //case CommandType.REQUEST_OWN_LOCATION_COMMAND:
                //    return new RequestOwnLocationCommand();
                //case CommandType.RESET_ALARM_COMMAND:
                //    return new ResetAlarmCommand();
                //case CommandType.RESET_ALARM_LOG_COMMAND:
                //    return new ResetAlarmLogCommand();
                //case CommandType.RESET_ALL_ALARMS_COMMAND:
                //    return new ResetAllAlarmsCommand();
                //case CommandType.RESET_STARTUP_PARAMETERS_COMMAND:
                //    return new ResetStartupParametersCommand();
                //case CommandType.RESET_STARTUP_PARAMETERS_RESPONSE:
                //    return new ResetStartupParametersResponse();
                //case CommandType.RESET_TO_FACTORY_DEFAULTS_COMMAND:
                //    return new ResetToFactoryDefaultsCommand();
                //case CommandType.RESTART_DEVICE_COMMAND:
                //    return new RestartDeviceCommand();
                //case CommandType.RESTART_DEVICE_RESPONSE_RESPONSE:
                //    return new RestartDeviceResponseResponse();
                //case CommandType.RESTORE_STARTUP_PARAMETERS_COMMAND:
                //    return new RestoreStartupParametersCommand();
                //case CommandType.RESTORE_STARTUP_PARAMETERS_RESPONSE:
                //    return new RestoreStartupParametersResponse();
                //case CommandType.RSSI_PING_COMMAND:
                //    return new RssiPingCommand();
                //case CommandType.RSSI_REQUEST_COMMAND:
                //    return new RssiRequestCommand();
                //case CommandType.RSSI_RESPONSE:
                //    return new RssiResponse();
                //case CommandType.SAVE_STARTUP_PARAMETERS_COMMAND:
                //    return new SaveStartupParametersCommand();
                //case CommandType.SAVE_STARTUP_PARAMETERS_RESPONSE:
                //    return new SaveStartupParametersResponse();
                //case CommandType.SEND_PINGS_COMMAND:
                //    return new SendPingsCommand();
                //case CommandType.SETPOINT_RAISE_LOWER_COMMAND:
                //    return new SetpointRaiseLowerCommand();
                //case CommandType.SET_ABSOLUTE_LOCATION_COMMAND:
                //    return new SetAbsoluteLocationCommand();
                //case CommandType.SET_BYPASSED_ZONE_LIST_COMMAND:
                //    return new SetBypassedZoneListCommand();
                //case CommandType.SET_DEVICE_CONFIGURATION_COMMAND:
                //    return new SetDeviceConfigurationCommand();
                //case CommandType.SET_LONG_POLL_INTERVAL_COMMAND:
                //    return new SetLongPollIntervalCommand();
                //case CommandType.SET_SHORT_POLL_INTERVAL_COMMAND:
                //    return new SetShortPollIntervalCommand();
                //case CommandType.SET_WEEKLY_SCHEDULE:
                //    return new SetWeeklySchedule();
                //case CommandType.SQUAWK_COMMAND:
                //    return new SquawkCommand();
                //case CommandType.START_WARNING_COMMAND:
                //    return new StartWarningCommand();
                //case CommandType.STEP_COLOR_COMMAND:
                //    return new StepColorCommand();
                //case CommandType.STEP_COMMAND:
                //    return new StepCommand();
                //case CommandType.STEP_HUE_COMMAND:
                //    return new StepHueCommand();
                //case CommandType.STEP_SATURATION_COMMAND:
                //    return new StepSaturationCommand();
                //case CommandType.STEP__WITH_ON_OFF__COMMAND:
                //    return new StepWithOnOffCommand();
                //case CommandType.STOP_2_COMMAND:
                //    return new Stop2Command();
                //case CommandType.STOP_COMMAND:
                //    return new StopCommand();
                //case CommandType.STORE_SCENE_COMMAND:
                //    return new StoreSceneCommand();
                //case CommandType.STORE_SCENE_RESPONSE:
                //    return new StoreSceneResponse();
                case CommandType.TOGGLE_COMMAND:
                    return new ToggleCommand();
                //case CommandType.UNLOCK_DOOR_COMMAND:
                //    return new UnlockDoorCommand();
                //case CommandType.UNLOCK_DOOR_RESPONSE:
                //    return new UnlockDoorResponse();
                //case CommandType.UPGRADE_END_COMMAND:
                //    return new UpgradeEndCommand();
                //case CommandType.UPGRADE_END_RESPONSE:
                //    return new UpgradeEndResponse();
                //case CommandType.VIEW_GROUP_COMMAND:
                //    return new ViewGroupCommand();
                //case CommandType.VIEW_GROUP_RESPONSE:
                //    return new ViewGroupResponse();
                //case CommandType.VIEW_SCENE_COMMAND:
                //    return new ViewSceneCommand();
                //case CommandType.VIEW_SCENE_RESPONSE:
                //    return new ViewSceneResponse();
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
                //case CommandType.ZONE_ENROLL_REQUEST_COMMAND:
                //    return new ZoneEnrollRequestCommand();
                //case CommandType.ZONE_ENROLL_RESPONSE:
                //    return new ZoneEnrollResponse();
                //case CommandType.ZONE_STATUS_CHANGED_COMMAND:
                //    return new ZoneStatusChangedCommand();
                //case CommandType.ZONE_STATUS_CHANGE_NOTIFICATION_COMMAND:
                //    return new ZoneStatusChangeNotificationCommand();
                default:
                    throw new ArgumentException("Unknown ZclCommandType: " + Type.ToString());
            }
        }

        /**
         * Enumeration of ZigBee Cluster Library commands
         *
         */
        public enum CommandType
        {
            /**
             * ADD_GROUP_COMMAND: Add Group Command
             * 
             * See {@link AddGroupCommand}
             */
            ADD_GROUP_COMMAND,

            /**
            * ADD_GROUP_IF_IDENTIFYING_COMMAND: Add Group If Identifying Command
            * 
            * See {@link AddGroupIfIdentifyingCommand}
            */
            ADD_GROUP_IF_IDENTIFYING_COMMAND,

            /**
            * ADD_GROUP_RESPONSE: Add Group Response
            * 
            * See {@link AddGroupResponse}
            */
            ADD_GROUP_RESPONSE,

            /**
            * ADD_SCENE_COMMAND: Add Scene Command
            * 
            * See {@link AddSceneCommand}
            */
            ADD_SCENE_COMMAND,

            /**
            * ADD_SCENE_RESPONSE: Add Scene Response
            * 
            * See {@link AddSceneResponse}
            */
            ADD_SCENE_RESPONSE,

            /**
            * ALARM_COMMAND: Alarm Command
            * 
            * See {@link AlarmCommand}
            */
            ALARM_COMMAND,

            /**
            * ANCHOR_NODE_ANNOUNCE_COMMAND: Anchor Node Announce Command
            * 
            * See {@link AnchorNodeAnnounceCommand}
            */
            ANCHOR_NODE_ANNOUNCE_COMMAND,

            /**
            * ARM_COMMAND: Arm Command
            * 
            * See {@link ArmCommand}
            */
            ARM_COMMAND,

            /**
            * ARM_RESPONSE: Arm Response
            * 
            * See {@link ArmResponse}
            */
            ARM_RESPONSE,

            /**
            * BYPASS_COMMAND: Bypass Command
            * 
            * See {@link BypassCommand}
            */
            BYPASS_COMMAND,

            /**
            * BYPASS_RESPONSE: Bypass Response
            * 
            * See {@link BypassResponse}
            */
            BYPASS_RESPONSE,

            /**
            * CHECK_IN_COMMAND: Check In Command
            * 
            * See {@link CheckInCommand}
            */
            CHECK_IN_COMMAND,

            /**
            * CHECK_IN_RESPONSE: Check In Response
            * 
            * See {@link CheckInResponse}
            */
            CHECK_IN_RESPONSE,

            /**
            * CLEAR_WEEKLY_SCHEDULE: Clear Weekly Schedule
            * 
            * See {@link ClearWeeklySchedule}
            */
            CLEAR_WEEKLY_SCHEDULE,

            /**
            * COLOR_LOOP_SET_COMMAND: Color Loop Set Command
            * 
            * See {@link ColorLoopSetCommand}
            */
            COLOR_LOOP_SET_COMMAND,

            /**
            * COMPACT_LOCATION_DATA_NOTIFICATION_COMMAND: Compact Location Data Notification Command
            * 
            * See {@link CompactLocationDataNotificationCommand}
            */
            COMPACT_LOCATION_DATA_NOTIFICATION_COMMAND,

            /**
            * CONFIGURE_REPORTING_COMMAND: Configure Reporting Command
            * 
            * See {@link ConfigureReportingCommand}
            */
            CONFIGURE_REPORTING_COMMAND,

            /**
            * CONFIGURE_REPORTING_RESPONSE: Configure Reporting Response
            * 
            * See {@link ConfigureReportingResponse}
            */
            CONFIGURE_REPORTING_RESPONSE,

            /**
            * DEFAULT_RESPONSE: Default Response
            * 
            * See {@link DefaultResponse}
            */
            DEFAULT_RESPONSE,

            /**
            * DEVICE_CONFIGURATION_RESPONSE: Device Configuration Response
            * 
            * See {@link DeviceConfigurationResponse}
            */
            DEVICE_CONFIGURATION_RESPONSE,

            /**
            * DISCOVER_ATTRIBUTES_COMMAND: Discover Attributes Command
            * 
            * See {@link DiscoverAttributesCommand}
            */
            DISCOVER_ATTRIBUTES_COMMAND,

            /**
            * DISCOVER_ATTRIBUTES_EXTENDED: Discover Attributes Extended
            * 
            * See {@link DiscoverAttributesExtended}
            */
            DISCOVER_ATTRIBUTES_EXTENDED,

            /**
            * DISCOVER_ATTRIBUTES_EXTENDED_RESPONSE: Discover Attributes Extended Response
            * 
            * See {@link DiscoverAttributesExtendedResponse}
            */
            DISCOVER_ATTRIBUTES_EXTENDED_RESPONSE,

            /**
            * DISCOVER_ATTRIBUTES_RESPONSE: Discover Attributes Response
            * 
            * See {@link DiscoverAttributesResponse}
            */
            DISCOVER_ATTRIBUTES_RESPONSE,

            /**
            * DISCOVER_COMMANDS_GENERATED: Discover Commands Generated
            * 
            * See {@link DiscoverCommandsGenerated}
            */
            DISCOVER_COMMANDS_GENERATED,

            /**
            * DISCOVER_COMMANDS_GENERATED_RESPONSE: Discover Commands Generated Response
            * 
            * See {@link DiscoverCommandsGeneratedResponse}
            */
            DISCOVER_COMMANDS_GENERATED_RESPONSE,

            /**
            * DISCOVER_COMMANDS_RECEIVED: Discover Commands Received
            * 
            * See {@link DiscoverCommandsReceived}
            */
            DISCOVER_COMMANDS_RECEIVED,

            /**
            * DISCOVER_COMMANDS_RECEIVED_RESPONSE: Discover Commands Received Response
            * 
            * See {@link DiscoverCommandsReceivedResponse}
            */
            DISCOVER_COMMANDS_RECEIVED_RESPONSE,

            /**
            * EMERGENCY_COMMAND: Emergency Command
            * 
            * See {@link EmergencyCommand}
            */
            EMERGENCY_COMMAND,

            /**
            * ENHANCED_MOVE_TO_HUE_AND_SATURATION_COMMAND: Enhanced Move To Hue and Saturation Command
            * 
            * See {@link EnhancedMoveToHueAndSaturationCommand}
            */
            ENHANCED_MOVE_TO_HUE_AND_SATURATION_COMMAND,

            /**
            * ENHANCED_MOVE_TO_HUE_COMMAND: Enhanced Move To Hue Command
            * 
            * See {@link EnhancedMoveToHueCommand}
            */
            ENHANCED_MOVE_TO_HUE_COMMAND,

            /**
            * ENHANCED_STEP_HUE_COMMAND: Enhanced Step Hue Command
            * 
            * See {@link EnhancedStepHueCommand}
            */
            ENHANCED_STEP_HUE_COMMAND,

            /**
            * FAST_POLL_STOP_COMMAND: Fast Poll Stop Command
            * 
            * See {@link FastPollStopCommand}
            */
            FAST_POLL_STOP_COMMAND,

            /**
            * FIRE_COMMAND: Fire Command
            * 
            * See {@link FireCommand}
            */
            FIRE_COMMAND,

            /**
            * GET_ALARM_COMMAND: Get Alarm Command
            * 
            * See {@link GetAlarmCommand}
            */
            GET_ALARM_COMMAND,

            /**
            * GET_ALARM_RESPONSE: Get Alarm Response
            * 
            * See {@link GetAlarmResponse}
            */
            GET_ALARM_RESPONSE,

            /**
            * GET_BYPASSED_ZONE_LIST_COMMAND: Get Bypassed Zone List Command
            * 
            * See {@link GetBypassedZoneListCommand}
            */
            GET_BYPASSED_ZONE_LIST_COMMAND,

            /**
            * GET_DEVICE_CONFIGURATION_COMMAND: Get Device Configuration Command
            * 
            * See {@link GetDeviceConfigurationCommand}
            */
            GET_DEVICE_CONFIGURATION_COMMAND,

            /**
            * GET_GROUP_MEMBERSHIP_COMMAND: Get Group Membership Command
            * 
            * See {@link GetGroupMembershipCommand}
            */  
            GET_GROUP_MEMBERSHIP_COMMAND,

            /**
            * GET_GROUP_MEMBERSHIP_RESPONSE: Get Group Membership Response
            * 
            * See {@link GetGroupMembershipResponse}
            */
            GET_GROUP_MEMBERSHIP_RESPONSE,

            /**
            * GET_LOCATION_DATA_COMMAND: Get Location Data Command
            * 
            * See {@link GetLocationDataCommand}
            */
            GET_LOCATION_DATA_COMMAND,

            /**
            * GET_PANEL_STATUS_COMMAND: Get Panel Status Command
            * 
            * See {@link GetPanelStatusCommand}
            */
            GET_PANEL_STATUS_COMMAND,

            /**
            * GET_PANEL_STATUS_RESPONSE: Get Panel Status Response
            * 
            * See {@link GetPanelStatusResponse}
            */      
            GET_PANEL_STATUS_RESPONSE,

            /**
            * GET_RELAY_STATUS_LOG: Get Relay Status Log
            * 
            * See {@link GetRelayStatusLog}
            */
            GET_RELAY_STATUS_LOG,

            /**
            * GET_RELAY_STATUS_LOG_RESPONSE: Get Relay Status Log Response
            * 
            * See {@link GetRelayStatusLogResponse}
            */
            GET_RELAY_STATUS_LOG_RESPONSE,

            /**
            * GET_SCENE_MEMBERSHIP_COMMAND: Get Scene Membership Command
            * 
            * See {@link GetSceneMembershipCommand}
            */
            GET_SCENE_MEMBERSHIP_COMMAND,

            /**
            * GET_SCENE_MEMBERSHIP_RESPONSE: Get Scene Membership Response
            * 
            * See {@link GetSceneMembershipResponse}
            */
            GET_SCENE_MEMBERSHIP_RESPONSE,

            /**
            * GET_WEEKLY_SCHEDULE: Get Weekly Schedule
            * 
            * See {@link GetWeeklySchedule}
            */
            GET_WEEKLY_SCHEDULE,

            /**
            * GET_WEEKLY_SCHEDULE_RESPONSE: Get Weekly Schedule Response
            * 
            * See {@link GetWeeklyScheduleResponse}
            */
            GET_WEEKLY_SCHEDULE_RESPONSE,

            /**
            * GET_ZONE_ID_MAP_COMMAND: Get Zone ID Map Command
            * 
            * See {@link GetZoneIdMapCommand}
            */
            GET_ZONE_ID_MAP_COMMAND,

            /**
            * GET_ZONE_ID_MAP_RESPONSE: Get Zone ID Map Response
            * 
            * See {@link GetZoneIdMapResponse}
            */
            GET_ZONE_ID_MAP_RESPONSE,

            /**
            * GET_ZONE_INFORMATION_COMMAND: Get Zone Information Command
            * 
            * See {@link GetZoneInformationCommand}
            */
            GET_ZONE_INFORMATION_COMMAND,

            /**
            * GET_ZONE_INFORMATION_RESPONSE: Get Zone Information Response
            * 
            * See {@link GetZoneInformationResponse}
            */
            GET_ZONE_INFORMATION_RESPONSE,

            /**
            * GET_ZONE_STATUS_COMMAND: Get Zone Status Command
            * 
            * See {@link GetZoneStatusCommand}
            */
            GET_ZONE_STATUS_COMMAND,

            /**
            * GET_ZONE_STATUS_RESPONSE: Get Zone Status Response
            * 
            * See {@link GetZoneStatusResponse}
            */
            GET_ZONE_STATUS_RESPONSE,

            /**
            * IDENTIFY_COMMAND: Identify Command
            * 
            * See {@link IdentifyCommand}
            */
            IDENTIFY_COMMAND,

            /**
            * IDENTIFY_QUERY_COMMAND: Identify Query Command
            * 
            * See {@link IdentifyQueryCommand}
            */
            IDENTIFY_QUERY_COMMAND,

            /**
            * IDENTIFY_QUERY_RESPONSE: Identify Query Response
            * 
            * See {@link IdentifyQueryResponse}
            */
            IDENTIFY_QUERY_RESPONSE,

            /**
            * IMAGE_BLOCK_COMMAND: Image Block Command
            * 
            * See {@link ImageBlockCommand}
            */
            IMAGE_BLOCK_COMMAND,

            /**
            * IMAGE_BLOCK_RESPONSE: Image Block Response
            * 
            * See {@link ImageBlockResponse}
            */
            IMAGE_BLOCK_RESPONSE,

            /**
            * IMAGE_NOTIFY_COMMAND: Image Notify Command
            * 
            * See {@link ImageNotifyCommand}
            */
            IMAGE_NOTIFY_COMMAND,

            /**
            * IMAGE_PAGE_COMMAND: Image Page Command
            * 
            * See {@link ImagePageCommand}
            */
            IMAGE_PAGE_COMMAND,

            /**
            * INITIATE_NORMAL_OPERATION_MODE_COMMAND: Initiate Normal Operation Mode Command
            * 
            * See {@link InitiateNormalOperationModeCommand}
            */
            INITIATE_NORMAL_OPERATION_MODE_COMMAND,

            /**
            * INITIATE_TEST_MODE_COMMAND: Initiate Test Mode Command
            * 
            * See {@link InitiateTestModeCommand}
            */
            INITIATE_TEST_MODE_COMMAND,

            /**
            * LOCATION_DATA_NOTIFICATION_COMMAND: Location Data Notification Command
            * 
            * See {@link LocationDataNotificationCommand}
            */
            LOCATION_DATA_NOTIFICATION_COMMAND,

            /**
            * LOCATION_DATA_RESPONSE: Location Data Response
            * 
            * See {@link LocationDataResponse}
            */
            LOCATION_DATA_RESPONSE,

            /**
            * LOCK_DOOR_COMMAND: Lock Door Command
            * 
            * See {@link LockDoorCommand}
            */
            LOCK_DOOR_COMMAND,

            /**
            * LOCK_DOOR_RESPONSE: Lock Door Response
            * 
            * See {@link LockDoorResponse}
            */
            LOCK_DOOR_RESPONSE,

            /**
            * MOVE_COLOR_COMMAND: Move Color Command
            * 
            * See {@link MoveColorCommand}
            */
            MOVE_COLOR_COMMAND,

            /**
            * MOVE_COMMAND: Move Command
            * 
            * See {@link MoveCommand}
            */
            MOVE_COMMAND,

            /**
            * MOVE_HUE_COMMAND: Move Hue Command
            * 
            * See {@link MoveHueCommand}
            */
            MOVE_HUE_COMMAND,

            /**
            * MOVE_SATURATION_COMMAND: Move Saturation Command
            * 
            * See {@link MoveSaturationCommand}
            */
            MOVE_SATURATION_COMMAND,

            /**
            * MOVE_TO_COLOR_COMMAND: Move to Color Command
            * 
            * See {@link MoveToColorCommand}
            */
            MOVE_TO_COLOR_COMMAND,

            /**
            * MOVE_TO_COLOR_TEMPERATURE_COMMAND: Move to Color Temperature Command
            * 
            * See {@link MoveToColorTemperatureCommand}
            */
            MOVE_TO_COLOR_TEMPERATURE_COMMAND,

            /**
            * MOVE_TO_HUE_AND_SATURATION_COMMAND: Move to Hue and Saturation Command
            * 
            * See {@link MoveToHueAndSaturationCommand}
            */
            MOVE_TO_HUE_AND_SATURATION_COMMAND,

            /**
            * MOVE_TO_HUE_COMMAND: Move to Hue Command
            * 
            * See {@link MoveToHueCommand}
            */
            MOVE_TO_HUE_COMMAND,

            /**
            * MOVE_TO_LEVEL_COMMAND: Move to Level Command
            * 
            * See {@link MoveToLevelCommand}
            */
            MOVE_TO_LEVEL_COMMAND,

            /**
            * MOVE_TO_LEVEL__WITH_ON_OFF__COMMAND: Move to Level (with On/Off) Command
            * 
            * See {@link MoveToLevelWithOnOffCommand}
            */
            MOVE_TO_LEVEL__WITH_ON_OFF__COMMAND,

            /**
            * MOVE_TO_SATURATION_COMMAND: Move to Saturation Command
            * 
            * See {@link MoveToSaturationCommand}
            */
            MOVE_TO_SATURATION_COMMAND,

            /**
            * MOVE__WITH_ON_OFF__COMMAND: Move (with On/Off) Command
            * 
            * See {@link MoveWithOnOffCommand}
            */
            MOVE__WITH_ON_OFF__COMMAND,

            /**
            * OFF_COMMAND: Off Command
            * 
            * See {@link OffCommand}
            */
            OFF_COMMAND,

            /**
            * OFF_WITH_EFFECT_COMMAND: Off With Effect Command
            * 
            * See {@link OffWithEffectCommand}
            */
            OFF_WITH_EFFECT_COMMAND,

            /**
            * ON_COMMAND: On Command
            * 
            * See {@link OnCommand}
            */
            ON_COMMAND,

            /**
            * ON_WITH_RECALL_GLOBAL_SCENE_COMMAND: On With Recall Global Scene Command
            * 
            * See {@link OnWithRecallGlobalSceneCommand}
            */
            ON_WITH_RECALL_GLOBAL_SCENE_COMMAND,

            /**
            * ON_WITH_TIMED_OFF_COMMAND: On With Timed Off Command
            * 
            * See {@link OnWithTimedOffCommand}
            */
            ON_WITH_TIMED_OFF_COMMAND,

            /**
            * PANEL_STATUS_CHANGED_COMMAND: Panel Status Changed Command
            * 
            * See {@link PanelStatusChangedCommand}
            */
            PANEL_STATUS_CHANGED_COMMAND,

            /**
            * PANIC_COMMAND: Panic Command
            * 
            * See {@link PanicCommand}
            */
            PANIC_COMMAND,

            /**
            * QUERY_NEXT_IMAGE_COMMAND: Query Next Image Command
            * 
            * See {@link QueryNextImageCommand}
            */
            QUERY_NEXT_IMAGE_COMMAND,

            /**
            * QUERY_NEXT_IMAGE_RESPONSE: Query Next Image Response
            * 
            * See {@link QueryNextImageResponse}
            */
            QUERY_NEXT_IMAGE_RESPONSE,

            /**
            * QUERY_SPECIFIC_FILE_COMMAND: Query Specific File Command
            * 
            * See {@link QuerySpecificFileCommand}
            */
            QUERY_SPECIFIC_FILE_COMMAND,

            /**
            * QUERY_SPECIFIC_FILE_RESPONSE: Query Specific File Response
            * 
            * See {@link QuerySpecificFileResponse}
            */
            QUERY_SPECIFIC_FILE_RESPONSE,

            /**
            * READ_ATTRIBUTES_COMMAND: Read Attributes Command
            * 
            * See {@link ReadAttributesCommand}
            */
            READ_ATTRIBUTES_COMMAND,

            /**
            * READ_ATTRIBUTES_RESPONSE: Read Attributes Response
            * 
            * See {@link ReadAttributesResponse}
            */
            READ_ATTRIBUTES_RESPONSE,

            /**
            * READ_ATTRIBUTES_STRUCTURED_COMMAND: Read Attributes Structured Command
            * 
            * See {@link ReadAttributesStructuredCommand}
            */
            READ_ATTRIBUTES_STRUCTURED_COMMAND,

            /**
            * READ_REPORTING_CONFIGURATION_COMMAND: Read Reporting Configuration Command
            * 
            * See {@link ReadReportingConfigurationCommand}
            */
            READ_REPORTING_CONFIGURATION_COMMAND,

            /**
            * READ_REPORTING_CONFIGURATION_RESPONSE: Read Reporting Configuration Response
            * 
            * See {@link ReadReportingConfigurationResponse}
            */
            READ_REPORTING_CONFIGURATION_RESPONSE,

            /**
            * RECALL_SCENE_COMMAND: Recall Scene Command
            * 
            * See {@link RecallSceneCommand}
            */
            RECALL_SCENE_COMMAND,

            /**
            * REMOVE_ALL_GROUPS_COMMAND: Remove All Groups Command
            * 
            * See {@link RemoveAllGroupsCommand}
            */
            REMOVE_ALL_GROUPS_COMMAND,

            /**
            * REMOVE_ALL_SCENES_COMMAND: Remove All Scenes Command
            * 
            * See {@link RemoveAllScenesCommand}
            */
            REMOVE_ALL_SCENES_COMMAND,

            /**
            * REMOVE_ALL_SCENES_RESPONSE: Remove All Scenes Response
            * 
            * See {@link RemoveAllScenesResponse}
            */
            REMOVE_ALL_SCENES_RESPONSE,

            /**
            * REMOVE_GROUP_COMMAND: Remove Group Command
            * 
            * See {@link RemoveGroupCommand}
            */
            REMOVE_GROUP_COMMAND,

            /**
            * REMOVE_GROUP_RESPONSE: Remove Group Response
            * 
            * See {@link RemoveGroupResponse}
            */
            REMOVE_GROUP_RESPONSE,

            /**
            * REMOVE_SCENE_COMMAND: Remove Scene Command
            * 
            * See {@link RemoveSceneCommand}
            */
            REMOVE_SCENE_COMMAND,

            /**
            * REMOVE_SCENE_RESPONSE: Remove Scene Response
            * 
            * See {@link RemoveSceneResponse}
            */
            REMOVE_SCENE_RESPONSE,

            /**
            * REPORT_ATTRIBUTES_COMMAND: Report Attributes Command
            * 
            * See {@link ReportAttributesCommand}
            */
            REPORT_ATTRIBUTES_COMMAND,

            /**
            * REPORT_RSSI_MEASUREMENTS_COMMAND: Report RSSI Measurements Command
            * 
            * See {@link ReportRssiMeasurementsCommand}
            */
            REPORT_RSSI_MEASUREMENTS_COMMAND,

            /**
            * REQUEST_OWN_LOCATION_COMMAND: Request Own Location Command
            * 
            * See {@link RequestOwnLocationCommand}
            */
            REQUEST_OWN_LOCATION_COMMAND,

            /**
            * RESET_ALARM_COMMAND: Reset Alarm Command
            * 
            * See {@link ResetAlarmCommand}
            */
            RESET_ALARM_COMMAND,

            /**
            * RESET_ALARM_LOG_COMMAND: Reset Alarm Log Command
            * 
            * See {@link ResetAlarmLogCommand}
            */
            RESET_ALARM_LOG_COMMAND,

            /**
            * RESET_ALL_ALARMS_COMMAND: Reset All Alarms Command
            * 
            * See {@link ResetAllAlarmsCommand}
            */
            RESET_ALL_ALARMS_COMMAND,

            /**
            * RESET_STARTUP_PARAMETERS_COMMAND: Reset Startup Parameters Command
            * 
            * See {@link ResetStartupParametersCommand}
            */
            RESET_STARTUP_PARAMETERS_COMMAND,

            /**
            * RESET_STARTUP_PARAMETERS_RESPONSE: Reset Startup Parameters Response
            * 
            * See {@link ResetStartupParametersResponse}
            */
            RESET_STARTUP_PARAMETERS_RESPONSE,

            /**
            * RESET_TO_FACTORY_DEFAULTS_COMMAND: Reset to Factory Defaults Command
            * 
            * See {@link ResetToFactoryDefaultsCommand}
            */
            RESET_TO_FACTORY_DEFAULTS_COMMAND,

            /**
            * RESTART_DEVICE_COMMAND: Restart Device Command
            * 
            * See {@link RestartDeviceCommand}
            */
            RESTART_DEVICE_COMMAND,

            /**
            * RESTART_DEVICE_RESPONSE_RESPONSE: Restart Device Response Response
            * 
            * See {@link RestartDeviceResponseResponse}
            */
            RESTART_DEVICE_RESPONSE_RESPONSE,

            /**
            * RESTORE_STARTUP_PARAMETERS_COMMAND: Restore Startup Parameters Command
            * 
            * See {@link RestoreStartupParametersCommand}
            */
            RESTORE_STARTUP_PARAMETERS_COMMAND,

            /**
            * RESTORE_STARTUP_PARAMETERS_RESPONSE: Restore Startup Parameters Response
            * 
            * See {@link RestoreStartupParametersResponse}
            */
            RESTORE_STARTUP_PARAMETERS_RESPONSE,

            /**
            * RSSI_PING_COMMAND: RSSI Ping Command
            * 
            * See {@link RssiPingCommand}
            */
            RSSI_PING_COMMAND,

            /**
            * RSSI_REQUEST_COMMAND: RSSI Request Command
            * 
            * See {@link RssiRequestCommand}
            */
            RSSI_REQUEST_COMMAND,

            /**
            * RSSI_RESPONSE: RSSI Response
            * 
            * See {@link RssiResponse}
            */
            RSSI_RESPONSE,

            /**
            * SAVE_STARTUP_PARAMETERS_COMMAND: Save Startup Parameters Command
            * 
            * See {@link SaveStartupParametersCommand}
            */
            SAVE_STARTUP_PARAMETERS_COMMAND,

            /**
            * SAVE_STARTUP_PARAMETERS_RESPONSE: Save Startup Parameters Response
            * 
            * See {@link SaveStartupParametersResponse}
            */
            SAVE_STARTUP_PARAMETERS_RESPONSE,

            /**
            * SEND_PINGS_COMMAND: Send Pings Command
            * 
            * See {@link SendPingsCommand}
            */
            SEND_PINGS_COMMAND,

            /**
            * SETPOINT_RAISE_LOWER_COMMAND: Setpoint Raise/Lower Command
            * 
            * See {@link SetpointRaiseLowerCommand}
            */
            SETPOINT_RAISE_LOWER_COMMAND,

            /**
            * SET_ABSOLUTE_LOCATION_COMMAND: Set Absolute Location Command
            * 
            * See {@link SetAbsoluteLocationCommand}
            */
            SET_ABSOLUTE_LOCATION_COMMAND,

            /**
            * SET_BYPASSED_ZONE_LIST_COMMAND: Set Bypassed Zone List Command
            * 
            * See {@link SetBypassedZoneListCommand}
            */
            SET_BYPASSED_ZONE_LIST_COMMAND,

            /**
            * SET_DEVICE_CONFIGURATION_COMMAND: Set Device Configuration Command
            * 
            * See {@link SetDeviceConfigurationCommand}
            */
            SET_DEVICE_CONFIGURATION_COMMAND,

            /**
            * SET_LONG_POLL_INTERVAL_COMMAND: Set Long Poll Interval Command
            * 
            * See {@link SetLongPollIntervalCommand}
            */
            SET_LONG_POLL_INTERVAL_COMMAND,

            /**
            * SET_SHORT_POLL_INTERVAL_COMMAND: Set Short Poll Interval Command
            * 
            * See {@link SetShortPollIntervalCommand}
            */
            SET_SHORT_POLL_INTERVAL_COMMAND,

            /**
            * SET_WEEKLY_SCHEDULE: Set Weekly Schedule
            * 
            * See {@link SetWeeklySchedule}
            */
            SET_WEEKLY_SCHEDULE,

            /**
            * SQUAWK_COMMAND: Squawk Command
            * 
            * See {@link SquawkCommand}
            */
            SQUAWK_COMMAND,

            /**
            * START_WARNING_COMMAND: Start Warning Command
            * 
            * See {@link StartWarningCommand}
            */
            START_WARNING_COMMAND,

            /**
            * STEP_COLOR_COMMAND: Step Color Command
            * 
            * See {@link StepColorCommand}
            */
            STEP_COLOR_COMMAND,

            /**
            * STEP_COMMAND: Step Command
            * 
            * See {@link StepCommand}
            */
            STEP_COMMAND,

            /**
            * STEP_HUE_COMMAND: Step Hue Command
            * 
            * See {@link StepHueCommand}
            */
            STEP_HUE_COMMAND,

            /**
            * STEP_SATURATION_COMMAND: Step Saturation Command
            * 
            * See {@link StepSaturationCommand}
            */
            STEP_SATURATION_COMMAND,

            /**
            * STEP__WITH_ON_OFF__COMMAND: Step (with On/Off) Command
            * 
            * See {@link StepWithOnOffCommand}
            */
            STEP__WITH_ON_OFF__COMMAND,

            /**
            * STOP_2_COMMAND: Stop 2 Command
            * 
            * See {@link Stop2Command}
            */
            STOP_2_COMMAND,

            /**
            * STOP_COMMAND: Stop Command
            * 
            * See {@link StopCommand}
            */
            STOP_COMMAND,

            /**
            * STORE_SCENE_COMMAND: Store Scene Command
            * 
            * See {@link StoreSceneCommand}
            */
            STORE_SCENE_COMMAND,

            /**
            * STORE_SCENE_RESPONSE: Store Scene Response
            * 
            * See {@link StoreSceneResponse}
            */
            STORE_SCENE_RESPONSE,

            /**
            * TOGGLE_COMMAND: Toggle Command
            * 
            * See {@link ToggleCommand}
            */
            TOGGLE_COMMAND,

            /**
            * UNLOCK_DOOR_COMMAND: Unlock Door Command
            * 
            * See {@link UnlockDoorCommand}
            */
            UNLOCK_DOOR_COMMAND,

            /**
            * UNLOCK_DOOR_RESPONSE: Unlock Door Response
            * 
            * See {@link UnlockDoorResponse}
            */
            UNLOCK_DOOR_RESPONSE,

            /**
            * UPGRADE_END_COMMAND: Upgrade End Command
            * 
            * See {@link UpgradeEndCommand}
            */
            UPGRADE_END_COMMAND,

            /**
            * UPGRADE_END_RESPONSE: Upgrade End Response
            * 
            * See {@link UpgradeEndResponse}
            */
            UPGRADE_END_RESPONSE,

            /**
            * VIEW_GROUP_COMMAND: View Group Command
            * 
            * See {@link ViewGroupCommand}
            */
            VIEW_GROUP_COMMAND,

            /**
            * VIEW_GROUP_RESPONSE: View Group Response
            * 
            * See {@link ViewGroupResponse}
            */
            VIEW_GROUP_RESPONSE,

            /**
            * VIEW_SCENE_COMMAND: View Scene Command
            * 
            * See {@link ViewSceneCommand}
            */
            VIEW_SCENE_COMMAND,

            /**
            * VIEW_SCENE_RESPONSE: View Scene Response
            * 
            * See {@link ViewSceneResponse}
            */
            VIEW_SCENE_RESPONSE,

            /**
            * WRITE_ATTRIBUTES_COMMAND: Write Attributes Command
            * 
            * See {@link WriteAttributesCommand}
            */
            WRITE_ATTRIBUTES_COMMAND,

            /**
            * WRITE_ATTRIBUTES_NO_RESPONSE: Write Attributes No Response
            * 
            * See {@link WriteAttributesNoResponse}
            */
            WRITE_ATTRIBUTES_NO_RESPONSE,

            /**
            * WRITE_ATTRIBUTES_RESPONSE: Write Attributes Response
            * 
            * See {@link WriteAttributesResponse}
            */
            WRITE_ATTRIBUTES_RESPONSE,

            /**
            * WRITE_ATTRIBUTES_STRUCTURED_COMMAND: Write Attributes Structured Command
            * 
            * See {@link WriteAttributesStructuredCommand}
            */
            WRITE_ATTRIBUTES_STRUCTURED_COMMAND,

            /**
            * WRITE_ATTRIBUTES_STRUCTURED_RESPONSE: Write Attributes Structured Response
            * 
            * See {@link WriteAttributesStructuredResponse}
            */
            WRITE_ATTRIBUTES_STRUCTURED_RESPONSE,

            /**
            * WRITE_ATTRIBUTES_UNDIVIDED_COMMAND: Write Attributes Undivided Command
            * 
            * See {@link WriteAttributesUndividedCommand}
            */
            WRITE_ATTRIBUTES_UNDIVIDED_COMMAND,

            /**
            * ZONE_ENROLL_REQUEST_COMMAND: Zone Enroll Request Command
            * 
            * See {@link ZoneEnrollRequestCommand}
            */
            ZONE_ENROLL_REQUEST_COMMAND,

            /**
            * ZONE_ENROLL_RESPONSE: Zone Enroll Response
            * 
            * See {@link ZoneEnrollResponse}
            */
            ZONE_ENROLL_RESPONSE,

            /**
            * ZONE_STATUS_CHANGED_COMMAND: Zone Status Changed Command
            * 
            * See {@link ZoneStatusChangedCommand}
            */
            ZONE_STATUS_CHANGED_COMMAND,

            /**
            * ZONE_STATUS_CHANGE_NOTIFICATION_COMMAND: Zone Status Change Notification Command
            * 
            * See {@link ZoneStatusChangeNotificationCommand}
            */
            ZONE_STATUS_CHANGE_NOTIFICATION_COMMAND,


        }
    }
}
