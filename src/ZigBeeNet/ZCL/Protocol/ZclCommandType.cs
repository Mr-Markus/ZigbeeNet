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

        /// <summary>
         /// Enumeration of ZigBee Cluster Library commands
         ///
         /// </summary>
        public enum CommandType
        {
            /// <summary>
             /// ADD_GROUP_COMMAND: Add Group Command
             /// 
             /// See {@link AddGroupCommand}
             /// </summary>
            ADD_GROUP_COMMAND,

            /// <summary>
            /// ADD_GROUP_IF_IDENTIFYING_COMMAND: Add Group If Identifying Command
            /// 
            /// See {@link AddGroupIfIdentifyingCommand}
            /// </summary>
            ADD_GROUP_IF_IDENTIFYING_COMMAND,

            /// <summary>
            /// ADD_GROUP_RESPONSE: Add Group Response
            /// 
            /// See {@link AddGroupResponse}
            /// </summary>
            ADD_GROUP_RESPONSE,

            /// <summary>
            /// ADD_SCENE_COMMAND: Add Scene Command
            /// 
            /// See {@link AddSceneCommand}
            /// </summary>
            ADD_SCENE_COMMAND,

            /// <summary>
            /// ADD_SCENE_RESPONSE: Add Scene Response
            /// 
            /// See {@link AddSceneResponse}
            /// </summary>
            ADD_SCENE_RESPONSE,

            /// <summary>
            /// ALARM_COMMAND: Alarm Command
            /// 
            /// See {@link AlarmCommand}
            /// </summary>
            ALARM_COMMAND,

            /// <summary>
            /// ANCHOR_NODE_ANNOUNCE_COMMAND: Anchor Node Announce Command
            /// 
            /// See {@link AnchorNodeAnnounceCommand}
            /// </summary>
            ANCHOR_NODE_ANNOUNCE_COMMAND,

            /// <summary>
            /// ARM_COMMAND: Arm Command
            /// 
            /// See {@link ArmCommand}
            /// </summary>
            ARM_COMMAND,

            /// <summary>
            /// ARM_RESPONSE: Arm Response
            /// 
            /// See {@link ArmResponse}
            /// </summary>
            ARM_RESPONSE,

            /// <summary>
            /// BYPASS_COMMAND: Bypass Command
            /// 
            /// See {@link BypassCommand}
            /// </summary>
            BYPASS_COMMAND,

            /// <summary>
            /// BYPASS_RESPONSE: Bypass Response
            /// 
            /// See {@link BypassResponse}
            /// </summary>
            BYPASS_RESPONSE,

            /// <summary>
            /// CHECK_IN_COMMAND: Check In Command
            /// 
            /// See {@link CheckInCommand}
            /// </summary>
            CHECK_IN_COMMAND,

            /// <summary>
            /// CHECK_IN_RESPONSE: Check In Response
            /// 
            /// See {@link CheckInResponse}
            /// </summary>
            CHECK_IN_RESPONSE,

            /// <summary>
            /// CLEAR_WEEKLY_SCHEDULE: Clear Weekly Schedule
            /// 
            /// See {@link ClearWeeklySchedule}
            /// </summary>
            CLEAR_WEEKLY_SCHEDULE,

            /// <summary>
            /// COLOR_LOOP_SET_COMMAND: Color Loop Set Command
            /// 
            /// See {@link ColorLoopSetCommand}
            /// </summary>
            COLOR_LOOP_SET_COMMAND,

            /// <summary>
            /// COMPACT_LOCATION_DATA_NOTIFICATION_COMMAND: Compact Location Data Notification Command
            /// 
            /// See {@link CompactLocationDataNotificationCommand}
            /// </summary>
            COMPACT_LOCATION_DATA_NOTIFICATION_COMMAND,

            /// <summary>
            /// CONFIGURE_REPORTING_COMMAND: Configure Reporting Command
            /// 
            /// See {@link ConfigureReportingCommand}
            /// </summary>
            CONFIGURE_REPORTING_COMMAND,

            /// <summary>
            /// CONFIGURE_REPORTING_RESPONSE: Configure Reporting Response
            /// 
            /// See {@link ConfigureReportingResponse}
            /// </summary>
            CONFIGURE_REPORTING_RESPONSE,

            /// <summary>
            /// DEFAULT_RESPONSE: Default Response
            /// 
            /// See {@link DefaultResponse}
            /// </summary>
            DEFAULT_RESPONSE,

            /// <summary>
            /// DEVICE_CONFIGURATION_RESPONSE: Device Configuration Response
            /// 
            /// See {@link DeviceConfigurationResponse}
            /// </summary>
            DEVICE_CONFIGURATION_RESPONSE,

            /// <summary>
            /// DISCOVER_ATTRIBUTES_COMMAND: Discover Attributes Command
            /// 
            /// See {@link DiscoverAttributesCommand}
            /// </summary>
            DISCOVER_ATTRIBUTES_COMMAND,

            /// <summary>
            /// DISCOVER_ATTRIBUTES_EXTENDED: Discover Attributes Extended
            /// 
            /// See {@link DiscoverAttributesExtended}
            /// </summary>
            DISCOVER_ATTRIBUTES_EXTENDED,

            /// <summary>
            /// DISCOVER_ATTRIBUTES_EXTENDED_RESPONSE: Discover Attributes Extended Response
            /// 
            /// See {@link DiscoverAttributesExtendedResponse}
            /// </summary>
            DISCOVER_ATTRIBUTES_EXTENDED_RESPONSE,

            /// <summary>
            /// DISCOVER_ATTRIBUTES_RESPONSE: Discover Attributes Response
            /// 
            /// See {@link DiscoverAttributesResponse}
            /// </summary>
            DISCOVER_ATTRIBUTES_RESPONSE,

            /// <summary>
            /// DISCOVER_COMMANDS_GENERATED: Discover Commands Generated
            /// 
            /// See {@link DiscoverCommandsGenerated}
            /// </summary>
            DISCOVER_COMMANDS_GENERATED,

            /// <summary>
            /// DISCOVER_COMMANDS_GENERATED_RESPONSE: Discover Commands Generated Response
            /// 
            /// See {@link DiscoverCommandsGeneratedResponse}
            /// </summary>
            DISCOVER_COMMANDS_GENERATED_RESPONSE,

            /// <summary>
            /// DISCOVER_COMMANDS_RECEIVED: Discover Commands Received
            /// 
            /// See {@link DiscoverCommandsReceived}
            /// </summary>
            DISCOVER_COMMANDS_RECEIVED,

            /// <summary>
            /// DISCOVER_COMMANDS_RECEIVED_RESPONSE: Discover Commands Received Response
            /// 
            /// See {@link DiscoverCommandsReceivedResponse}
            /// </summary>
            DISCOVER_COMMANDS_RECEIVED_RESPONSE,

            /// <summary>
            /// EMERGENCY_COMMAND: Emergency Command
            /// 
            /// See {@link EmergencyCommand}
            /// </summary>
            EMERGENCY_COMMAND,

            /// <summary>
            /// ENHANCED_MOVE_TO_HUE_AND_SATURATION_COMMAND: Enhanced Move To Hue and Saturation Command
            /// 
            /// See {@link EnhancedMoveToHueAndSaturationCommand}
            /// </summary>
            ENHANCED_MOVE_TO_HUE_AND_SATURATION_COMMAND,

            /// <summary>
            /// ENHANCED_MOVE_TO_HUE_COMMAND: Enhanced Move To Hue Command
            /// 
            /// See {@link EnhancedMoveToHueCommand}
            /// </summary>
            ENHANCED_MOVE_TO_HUE_COMMAND,

            /// <summary>
            /// ENHANCED_STEP_HUE_COMMAND: Enhanced Step Hue Command
            /// 
            /// See {@link EnhancedStepHueCommand}
            /// </summary>
            ENHANCED_STEP_HUE_COMMAND,

            /// <summary>
            /// FAST_POLL_STOP_COMMAND: Fast Poll Stop Command
            /// 
            /// See {@link FastPollStopCommand}
            /// </summary>
            FAST_POLL_STOP_COMMAND,

            /// <summary>
            /// FIRE_COMMAND: Fire Command
            /// 
            /// See {@link FireCommand}
            /// </summary>
            FIRE_COMMAND,

            /// <summary>
            /// GET_ALARM_COMMAND: Get Alarm Command
            /// 
            /// See {@link GetAlarmCommand}
            /// </summary>
            GET_ALARM_COMMAND,

            /// <summary>
            /// GET_ALARM_RESPONSE: Get Alarm Response
            /// 
            /// See {@link GetAlarmResponse}
            /// </summary>
            GET_ALARM_RESPONSE,

            /// <summary>
            /// GET_BYPASSED_ZONE_LIST_COMMAND: Get Bypassed Zone List Command
            /// 
            /// See {@link GetBypassedZoneListCommand}
            /// </summary>
            GET_BYPASSED_ZONE_LIST_COMMAND,

            /// <summary>
            /// GET_DEVICE_CONFIGURATION_COMMAND: Get Device Configuration Command
            /// 
            /// See {@link GetDeviceConfigurationCommand}
            /// </summary>
            GET_DEVICE_CONFIGURATION_COMMAND,

            /// <summary>
            /// GET_GROUP_MEMBERSHIP_COMMAND: Get Group Membership Command
            /// 
            /// See {@link GetGroupMembershipCommand}
            /// </summary>  
            GET_GROUP_MEMBERSHIP_COMMAND,

            /// <summary>
            /// GET_GROUP_MEMBERSHIP_RESPONSE: Get Group Membership Response
            /// 
            /// See {@link GetGroupMembershipResponse}
            /// </summary>
            GET_GROUP_MEMBERSHIP_RESPONSE,

            /// <summary>
            /// GET_LOCATION_DATA_COMMAND: Get Location Data Command
            /// 
            /// See {@link GetLocationDataCommand}
            /// </summary>
            GET_LOCATION_DATA_COMMAND,

            /// <summary>
            /// GET_PANEL_STATUS_COMMAND: Get Panel Status Command
            /// 
            /// See {@link GetPanelStatusCommand}
            /// </summary>
            GET_PANEL_STATUS_COMMAND,

            /// <summary>
            /// GET_PANEL_STATUS_RESPONSE: Get Panel Status Response
            /// 
            /// See {@link GetPanelStatusResponse}
            /// </summary>      
            GET_PANEL_STATUS_RESPONSE,

            /// <summary>
            /// GET_RELAY_STATUS_LOG: Get Relay Status Log
            /// 
            /// See {@link GetRelayStatusLog}
            /// </summary>
            GET_RELAY_STATUS_LOG,

            /// <summary>
            /// GET_RELAY_STATUS_LOG_RESPONSE: Get Relay Status Log Response
            /// 
            /// See {@link GetRelayStatusLogResponse}
            /// </summary>
            GET_RELAY_STATUS_LOG_RESPONSE,

            /// <summary>
            /// GET_SCENE_MEMBERSHIP_COMMAND: Get Scene Membership Command
            /// 
            /// See {@link GetSceneMembershipCommand}
            /// </summary>
            GET_SCENE_MEMBERSHIP_COMMAND,

            /// <summary>
            /// GET_SCENE_MEMBERSHIP_RESPONSE: Get Scene Membership Response
            /// 
            /// See {@link GetSceneMembershipResponse}
            /// </summary>
            GET_SCENE_MEMBERSHIP_RESPONSE,

            /// <summary>
            /// GET_WEEKLY_SCHEDULE: Get Weekly Schedule
            /// 
            /// See {@link GetWeeklySchedule}
            /// </summary>
            GET_WEEKLY_SCHEDULE,

            /// <summary>
            /// GET_WEEKLY_SCHEDULE_RESPONSE: Get Weekly Schedule Response
            /// 
            /// See {@link GetWeeklyScheduleResponse}
            /// </summary>
            GET_WEEKLY_SCHEDULE_RESPONSE,

            /// <summary>
            /// GET_ZONE_ID_MAP_COMMAND: Get Zone ID Map Command
            /// 
            /// See {@link GetZoneIdMapCommand}
            /// </summary>
            GET_ZONE_ID_MAP_COMMAND,

            /// <summary>
            /// GET_ZONE_ID_MAP_RESPONSE: Get Zone ID Map Response
            /// 
            /// See {@link GetZoneIdMapResponse}
            /// </summary>
            GET_ZONE_ID_MAP_RESPONSE,

            /// <summary>
            /// GET_ZONE_INFORMATION_COMMAND: Get Zone Information Command
            /// 
            /// See {@link GetZoneInformationCommand}
            /// </summary>
            GET_ZONE_INFORMATION_COMMAND,

            /// <summary>
            /// GET_ZONE_INFORMATION_RESPONSE: Get Zone Information Response
            /// 
            /// See {@link GetZoneInformationResponse}
            /// </summary>
            GET_ZONE_INFORMATION_RESPONSE,

            /// <summary>
            /// GET_ZONE_STATUS_COMMAND: Get Zone Status Command
            /// 
            /// See {@link GetZoneStatusCommand}
            /// </summary>
            GET_ZONE_STATUS_COMMAND,

            /// <summary>
            /// GET_ZONE_STATUS_RESPONSE: Get Zone Status Response
            /// 
            /// See {@link GetZoneStatusResponse}
            /// </summary>
            GET_ZONE_STATUS_RESPONSE,

            /// <summary>
            /// IDENTIFY_COMMAND: Identify Command
            /// 
            /// See {@link IdentifyCommand}
            /// </summary>
            IDENTIFY_COMMAND,

            /// <summary>
            /// IDENTIFY_QUERY_COMMAND: Identify Query Command
            /// 
            /// See {@link IdentifyQueryCommand}
            /// </summary>
            IDENTIFY_QUERY_COMMAND,

            /// <summary>
            /// IDENTIFY_QUERY_RESPONSE: Identify Query Response
            /// 
            /// See {@link IdentifyQueryResponse}
            /// </summary>
            IDENTIFY_QUERY_RESPONSE,

            /// <summary>
            /// IMAGE_BLOCK_COMMAND: Image Block Command
            /// 
            /// See {@link ImageBlockCommand}
            /// </summary>
            IMAGE_BLOCK_COMMAND,

            /// <summary>
            /// IMAGE_BLOCK_RESPONSE: Image Block Response
            /// 
            /// See {@link ImageBlockResponse}
            /// </summary>
            IMAGE_BLOCK_RESPONSE,

            /// <summary>
            /// IMAGE_NOTIFY_COMMAND: Image Notify Command
            /// 
            /// See {@link ImageNotifyCommand}
            /// </summary>
            IMAGE_NOTIFY_COMMAND,

            /// <summary>
            /// IMAGE_PAGE_COMMAND: Image Page Command
            /// 
            /// See {@link ImagePageCommand}
            /// </summary>
            IMAGE_PAGE_COMMAND,

            /// <summary>
            /// INITIATE_NORMAL_OPERATION_MODE_COMMAND: Initiate Normal Operation Mode Command
            /// 
            /// See {@link InitiateNormalOperationModeCommand}
            /// </summary>
            INITIATE_NORMAL_OPERATION_MODE_COMMAND,

            /// <summary>
            /// INITIATE_TEST_MODE_COMMAND: Initiate Test Mode Command
            /// 
            /// See {@link InitiateTestModeCommand}
            /// </summary>
            INITIATE_TEST_MODE_COMMAND,

            /// <summary>
            /// LOCATION_DATA_NOTIFICATION_COMMAND: Location Data Notification Command
            /// 
            /// See {@link LocationDataNotificationCommand}
            /// </summary>
            LOCATION_DATA_NOTIFICATION_COMMAND,

            /// <summary>
            /// LOCATION_DATA_RESPONSE: Location Data Response
            /// 
            /// See {@link LocationDataResponse}
            /// </summary>
            LOCATION_DATA_RESPONSE,

            /// <summary>
            /// LOCK_DOOR_COMMAND: Lock Door Command
            /// 
            /// See {@link LockDoorCommand}
            /// </summary>
            LOCK_DOOR_COMMAND,

            /// <summary>
            /// LOCK_DOOR_RESPONSE: Lock Door Response
            /// 
            /// See {@link LockDoorResponse}
            /// </summary>
            LOCK_DOOR_RESPONSE,

            /// <summary>
            /// MOVE_COLOR_COMMAND: Move Color Command
            /// 
            /// See {@link MoveColorCommand}
            /// </summary>
            MOVE_COLOR_COMMAND,

            /// <summary>
            /// MOVE_COMMAND: Move Command
            /// 
            /// See {@link MoveCommand}
            /// </summary>
            MOVE_COMMAND,

            /// <summary>
            /// MOVE_HUE_COMMAND: Move Hue Command
            /// 
            /// See {@link MoveHueCommand}
            /// </summary>
            MOVE_HUE_COMMAND,

            /// <summary>
            /// MOVE_SATURATION_COMMAND: Move Saturation Command
            /// 
            /// See {@link MoveSaturationCommand}
            /// </summary>
            MOVE_SATURATION_COMMAND,

            /// <summary>
            /// MOVE_TO_COLOR_COMMAND: Move to Color Command
            /// 
            /// See {@link MoveToColorCommand}
            /// </summary>
            MOVE_TO_COLOR_COMMAND,

            /// <summary>
            /// MOVE_TO_COLOR_TEMPERATURE_COMMAND: Move to Color Temperature Command
            /// 
            /// See {@link MoveToColorTemperatureCommand}
            /// </summary>
            MOVE_TO_COLOR_TEMPERATURE_COMMAND,

            /// <summary>
            /// MOVE_TO_HUE_AND_SATURATION_COMMAND: Move to Hue and Saturation Command
            /// 
            /// See {@link MoveToHueAndSaturationCommand}
            /// </summary>
            MOVE_TO_HUE_AND_SATURATION_COMMAND,

            /// <summary>
            /// MOVE_TO_HUE_COMMAND: Move to Hue Command
            /// 
            /// See {@link MoveToHueCommand}
            /// </summary>
            MOVE_TO_HUE_COMMAND,

            /// <summary>
            /// MOVE_TO_LEVEL_COMMAND: Move to Level Command
            /// 
            /// See {@link MoveToLevelCommand}
            /// </summary>
            MOVE_TO_LEVEL_COMMAND,

            /// <summary>
            /// MOVE_TO_LEVEL__WITH_ON_OFF__COMMAND: Move to Level (with On/Off) Command
            /// 
            /// See {@link MoveToLevelWithOnOffCommand}
            /// </summary>
            MOVE_TO_LEVEL__WITH_ON_OFF__COMMAND,

            /// <summary>
            /// MOVE_TO_SATURATION_COMMAND: Move to Saturation Command
            /// 
            /// See {@link MoveToSaturationCommand}
            /// </summary>
            MOVE_TO_SATURATION_COMMAND,

            /// <summary>
            /// MOVE__WITH_ON_OFF__COMMAND: Move (with On/Off) Command
            /// 
            /// See {@link MoveWithOnOffCommand}
            /// </summary>
            MOVE__WITH_ON_OFF__COMMAND,

            /// <summary>
            /// OFF_COMMAND: Off Command
            /// 
            /// See {@link OffCommand}
            /// </summary>
            OFF_COMMAND,

            /// <summary>
            /// OFF_WITH_EFFECT_COMMAND: Off With Effect Command
            /// 
            /// See {@link OffWithEffectCommand}
            /// </summary>
            OFF_WITH_EFFECT_COMMAND,

            /// <summary>
            /// ON_COMMAND: On Command
            /// 
            /// See {@link OnCommand}
            /// </summary>
            ON_COMMAND,

            /// <summary>
            /// ON_WITH_RECALL_GLOBAL_SCENE_COMMAND: On With Recall Global Scene Command
            /// 
            /// See {@link OnWithRecallGlobalSceneCommand}
            /// </summary>
            ON_WITH_RECALL_GLOBAL_SCENE_COMMAND,

            /// <summary>
            /// ON_WITH_TIMED_OFF_COMMAND: On With Timed Off Command
            /// 
            /// See {@link OnWithTimedOffCommand}
            /// </summary>
            ON_WITH_TIMED_OFF_COMMAND,

            /// <summary>
            /// PANEL_STATUS_CHANGED_COMMAND: Panel Status Changed Command
            /// 
            /// See {@link PanelStatusChangedCommand}
            /// </summary>
            PANEL_STATUS_CHANGED_COMMAND,

            /// <summary>
            /// PANIC_COMMAND: Panic Command
            /// 
            /// See {@link PanicCommand}
            /// </summary>
            PANIC_COMMAND,

            /// <summary>
            /// QUERY_NEXT_IMAGE_COMMAND: Query Next Image Command
            /// 
            /// See {@link QueryNextImageCommand}
            /// </summary>
            QUERY_NEXT_IMAGE_COMMAND,

            /// <summary>
            /// QUERY_NEXT_IMAGE_RESPONSE: Query Next Image Response
            /// 
            /// See {@link QueryNextImageResponse}
            /// </summary>
            QUERY_NEXT_IMAGE_RESPONSE,

            /// <summary>
            /// QUERY_SPECIFIC_FILE_COMMAND: Query Specific File Command
            /// 
            /// See {@link QuerySpecificFileCommand}
            /// </summary>
            QUERY_SPECIFIC_FILE_COMMAND,

            /// <summary>
            /// QUERY_SPECIFIC_FILE_RESPONSE: Query Specific File Response
            /// 
            /// See {@link QuerySpecificFileResponse}
            /// </summary>
            QUERY_SPECIFIC_FILE_RESPONSE,

            /// <summary>
            /// READ_ATTRIBUTES_COMMAND: Read Attributes Command
            /// 
            /// See {@link ReadAttributesCommand}
            /// </summary>
            READ_ATTRIBUTES_COMMAND,

            /// <summary>
            /// READ_ATTRIBUTES_RESPONSE: Read Attributes Response
            /// 
            /// See {@link ReadAttributesResponse}
            /// </summary>
            READ_ATTRIBUTES_RESPONSE,

            /// <summary>
            /// READ_ATTRIBUTES_STRUCTURED_COMMAND: Read Attributes Structured Command
            /// 
            /// See {@link ReadAttributesStructuredCommand}
            /// </summary>
            READ_ATTRIBUTES_STRUCTURED_COMMAND,

            /// <summary>
            /// READ_REPORTING_CONFIGURATION_COMMAND: Read Reporting Configuration Command
            /// 
            /// See {@link ReadReportingConfigurationCommand}
            /// </summary>
            READ_REPORTING_CONFIGURATION_COMMAND,

            /// <summary>
            /// READ_REPORTING_CONFIGURATION_RESPONSE: Read Reporting Configuration Response
            /// 
            /// See {@link ReadReportingConfigurationResponse}
            /// </summary>
            READ_REPORTING_CONFIGURATION_RESPONSE,

            /// <summary>
            /// RECALL_SCENE_COMMAND: Recall Scene Command
            /// 
            /// See {@link RecallSceneCommand}
            /// </summary>
            RECALL_SCENE_COMMAND,

            /// <summary>
            /// REMOVE_ALL_GROUPS_COMMAND: Remove All Groups Command
            /// 
            /// See {@link RemoveAllGroupsCommand}
            /// </summary>
            REMOVE_ALL_GROUPS_COMMAND,

            /// <summary>
            /// REMOVE_ALL_SCENES_COMMAND: Remove All Scenes Command
            /// 
            /// See {@link RemoveAllScenesCommand}
            /// </summary>
            REMOVE_ALL_SCENES_COMMAND,

            /// <summary>
            /// REMOVE_ALL_SCENES_RESPONSE: Remove All Scenes Response
            /// 
            /// See {@link RemoveAllScenesResponse}
            /// </summary>
            REMOVE_ALL_SCENES_RESPONSE,

            /// <summary>
            /// REMOVE_GROUP_COMMAND: Remove Group Command
            /// 
            /// See {@link RemoveGroupCommand}
            /// </summary>
            REMOVE_GROUP_COMMAND,

            /// <summary>
            /// REMOVE_GROUP_RESPONSE: Remove Group Response
            /// 
            /// See {@link RemoveGroupResponse}
            /// </summary>
            REMOVE_GROUP_RESPONSE,

            /// <summary>
            /// REMOVE_SCENE_COMMAND: Remove Scene Command
            /// 
            /// See {@link RemoveSceneCommand}
            /// </summary>
            REMOVE_SCENE_COMMAND,

            /// <summary>
            /// REMOVE_SCENE_RESPONSE: Remove Scene Response
            /// 
            /// See {@link RemoveSceneResponse}
            /// </summary>
            REMOVE_SCENE_RESPONSE,

            /// <summary>
            /// REPORT_ATTRIBUTES_COMMAND: Report Attributes Command
            /// 
            /// See {@link ReportAttributesCommand}
            /// </summary>
            REPORT_ATTRIBUTES_COMMAND,

            /// <summary>
            /// REPORT_RSSI_MEASUREMENTS_COMMAND: Report RSSI Measurements Command
            /// 
            /// See {@link ReportRssiMeasurementsCommand}
            /// </summary>
            REPORT_RSSI_MEASUREMENTS_COMMAND,

            /// <summary>
            /// REQUEST_OWN_LOCATION_COMMAND: Request Own Location Command
            /// 
            /// See {@link RequestOwnLocationCommand}
            /// </summary>
            REQUEST_OWN_LOCATION_COMMAND,

            /// <summary>
            /// RESET_ALARM_COMMAND: Reset Alarm Command
            /// 
            /// See {@link ResetAlarmCommand}
            /// </summary>
            RESET_ALARM_COMMAND,

            /// <summary>
            /// RESET_ALARM_LOG_COMMAND: Reset Alarm Log Command
            /// 
            /// See {@link ResetAlarmLogCommand}
            /// </summary>
            RESET_ALARM_LOG_COMMAND,

            /// <summary>
            /// RESET_ALL_ALARMS_COMMAND: Reset All Alarms Command
            /// 
            /// See {@link ResetAllAlarmsCommand}
            /// </summary>
            RESET_ALL_ALARMS_COMMAND,

            /// <summary>
            /// RESET_STARTUP_PARAMETERS_COMMAND: Reset Startup Parameters Command
            /// 
            /// See {@link ResetStartupParametersCommand}
            /// </summary>
            RESET_STARTUP_PARAMETERS_COMMAND,

            /// <summary>
            /// RESET_STARTUP_PARAMETERS_RESPONSE: Reset Startup Parameters Response
            /// 
            /// See {@link ResetStartupParametersResponse}
            /// </summary>
            RESET_STARTUP_PARAMETERS_RESPONSE,

            /// <summary>
            /// RESET_TO_FACTORY_DEFAULTS_COMMAND: Reset to Factory Defaults Command
            /// 
            /// See {@link ResetToFactoryDefaultsCommand}
            /// </summary>
            RESET_TO_FACTORY_DEFAULTS_COMMAND,

            /// <summary>
            /// RESTART_DEVICE_COMMAND: Restart Device Command
            /// 
            /// See {@link RestartDeviceCommand}
            /// </summary>
            RESTART_DEVICE_COMMAND,

            /// <summary>
            /// RESTART_DEVICE_RESPONSE_RESPONSE: Restart Device Response Response
            /// 
            /// See {@link RestartDeviceResponseResponse}
            /// </summary>
            RESTART_DEVICE_RESPONSE_RESPONSE,

            /// <summary>
            /// RESTORE_STARTUP_PARAMETERS_COMMAND: Restore Startup Parameters Command
            /// 
            /// See {@link RestoreStartupParametersCommand}
            /// </summary>
            RESTORE_STARTUP_PARAMETERS_COMMAND,

            /// <summary>
            /// RESTORE_STARTUP_PARAMETERS_RESPONSE: Restore Startup Parameters Response
            /// 
            /// See {@link RestoreStartupParametersResponse}
            /// </summary>
            RESTORE_STARTUP_PARAMETERS_RESPONSE,

            /// <summary>
            /// RSSI_PING_COMMAND: RSSI Ping Command
            /// 
            /// See {@link RssiPingCommand}
            /// </summary>
            RSSI_PING_COMMAND,

            /// <summary>
            /// RSSI_REQUEST_COMMAND: RSSI Request Command
            /// 
            /// See {@link RssiRequestCommand}
            /// </summary>
            RSSI_REQUEST_COMMAND,

            /// <summary>
            /// RSSI_RESPONSE: RSSI Response
            /// 
            /// See {@link RssiResponse}
            /// </summary>
            RSSI_RESPONSE,

            /// <summary>
            /// SAVE_STARTUP_PARAMETERS_COMMAND: Save Startup Parameters Command
            /// 
            /// See {@link SaveStartupParametersCommand}
            /// </summary>
            SAVE_STARTUP_PARAMETERS_COMMAND,

            /// <summary>
            /// SAVE_STARTUP_PARAMETERS_RESPONSE: Save Startup Parameters Response
            /// 
            /// See {@link SaveStartupParametersResponse}
            /// </summary>
            SAVE_STARTUP_PARAMETERS_RESPONSE,

            /// <summary>
            /// SEND_PINGS_COMMAND: Send Pings Command
            /// 
            /// See {@link SendPingsCommand}
            /// </summary>
            SEND_PINGS_COMMAND,

            /// <summary>
            /// SETPOINT_RAISE_LOWER_COMMAND: Setpoint Raise/Lower Command
            /// 
            /// See {@link SetpointRaiseLowerCommand}
            /// </summary>
            SETPOINT_RAISE_LOWER_COMMAND,

            /// <summary>
            /// SET_ABSOLUTE_LOCATION_COMMAND: Set Absolute Location Command
            /// 
            /// See {@link SetAbsoluteLocationCommand}
            /// </summary>
            SET_ABSOLUTE_LOCATION_COMMAND,

            /// <summary>
            /// SET_BYPASSED_ZONE_LIST_COMMAND: Set Bypassed Zone List Command
            /// 
            /// See {@link SetBypassedZoneListCommand}
            /// </summary>
            SET_BYPASSED_ZONE_LIST_COMMAND,

            /// <summary>
            /// SET_DEVICE_CONFIGURATION_COMMAND: Set Device Configuration Command
            /// 
            /// See {@link SetDeviceConfigurationCommand}
            /// </summary>
            SET_DEVICE_CONFIGURATION_COMMAND,

            /// <summary>
            /// SET_LONG_POLL_INTERVAL_COMMAND: Set Long Poll Interval Command
            /// 
            /// See {@link SetLongPollIntervalCommand}
            /// </summary>
            SET_LONG_POLL_INTERVAL_COMMAND,

            /// <summary>
            /// SET_SHORT_POLL_INTERVAL_COMMAND: Set Short Poll Interval Command
            /// 
            /// See {@link SetShortPollIntervalCommand}
            /// </summary>
            SET_SHORT_POLL_INTERVAL_COMMAND,

            /// <summary>
            /// SET_WEEKLY_SCHEDULE: Set Weekly Schedule
            /// 
            /// See {@link SetWeeklySchedule}
            /// </summary>
            SET_WEEKLY_SCHEDULE,

            /// <summary>
            /// SQUAWK_COMMAND: Squawk Command
            /// 
            /// See {@link SquawkCommand}
            /// </summary>
            SQUAWK_COMMAND,

            /// <summary>
            /// START_WARNING_COMMAND: Start Warning Command
            /// 
            /// See {@link StartWarningCommand}
            /// </summary>
            START_WARNING_COMMAND,

            /// <summary>
            /// STEP_COLOR_COMMAND: Step Color Command
            /// 
            /// See {@link StepColorCommand}
            /// </summary>
            STEP_COLOR_COMMAND,

            /// <summary>
            /// STEP_COMMAND: Step Command
            /// 
            /// See {@link StepCommand}
            /// </summary>
            STEP_COMMAND,

            /// <summary>
            /// STEP_HUE_COMMAND: Step Hue Command
            /// 
            /// See {@link StepHueCommand}
            /// </summary>
            STEP_HUE_COMMAND,

            /// <summary>
            /// STEP_SATURATION_COMMAND: Step Saturation Command
            /// 
            /// See {@link StepSaturationCommand}
            /// </summary>
            STEP_SATURATION_COMMAND,

            /// <summary>
            /// STEP__WITH_ON_OFF__COMMAND: Step (with On/Off) Command
            /// 
            /// See {@link StepWithOnOffCommand}
            /// </summary>
            STEP__WITH_ON_OFF__COMMAND,

            /// <summary>
            /// STOP_2_COMMAND: Stop 2 Command
            /// 
            /// See {@link Stop2Command}
            /// </summary>
            STOP_2_COMMAND,

            /// <summary>
            /// STOP_COMMAND: Stop Command
            /// 
            /// See {@link StopCommand}
            /// </summary>
            STOP_COMMAND,

            /// <summary>
            /// STORE_SCENE_COMMAND: Store Scene Command
            /// 
            /// See {@link StoreSceneCommand}
            /// </summary>
            STORE_SCENE_COMMAND,

            /// <summary>
            /// STORE_SCENE_RESPONSE: Store Scene Response
            /// 
            /// See {@link StoreSceneResponse}
            /// </summary>
            STORE_SCENE_RESPONSE,

            /// <summary>
            /// TOGGLE_COMMAND: Toggle Command
            /// 
            /// See {@link ToggleCommand}
            /// </summary>
            TOGGLE_COMMAND,

            /// <summary>
            /// UNLOCK_DOOR_COMMAND: Unlock Door Command
            /// 
            /// See {@link UnlockDoorCommand}
            /// </summary>
            UNLOCK_DOOR_COMMAND,

            /// <summary>
            /// UNLOCK_DOOR_RESPONSE: Unlock Door Response
            /// 
            /// See {@link UnlockDoorResponse}
            /// </summary>
            UNLOCK_DOOR_RESPONSE,

            /// <summary>
            /// UPGRADE_END_COMMAND: Upgrade End Command
            /// 
            /// See {@link UpgradeEndCommand}
            /// </summary>
            UPGRADE_END_COMMAND,

            /// <summary>
            /// UPGRADE_END_RESPONSE: Upgrade End Response
            /// 
            /// See {@link UpgradeEndResponse}
            /// </summary>
            UPGRADE_END_RESPONSE,

            /// <summary>
            /// VIEW_GROUP_COMMAND: View Group Command
            /// 
            /// See {@link ViewGroupCommand}
            /// </summary>
            VIEW_GROUP_COMMAND,

            /// <summary>
            /// VIEW_GROUP_RESPONSE: View Group Response
            /// 
            /// See {@link ViewGroupResponse}
            /// </summary>
            VIEW_GROUP_RESPONSE,

            /// <summary>
            /// VIEW_SCENE_COMMAND: View Scene Command
            /// 
            /// See {@link ViewSceneCommand}
            /// </summary>
            VIEW_SCENE_COMMAND,

            /// <summary>
            /// VIEW_SCENE_RESPONSE: View Scene Response
            /// 
            /// See {@link ViewSceneResponse}
            /// </summary>
            VIEW_SCENE_RESPONSE,

            /// <summary>
            /// WRITE_ATTRIBUTES_COMMAND: Write Attributes Command
            /// 
            /// See {@link WriteAttributesCommand}
            /// </summary>
            WRITE_ATTRIBUTES_COMMAND,

            /// <summary>
            /// WRITE_ATTRIBUTES_NO_RESPONSE: Write Attributes No Response
            /// 
            /// See {@link WriteAttributesNoResponse}
            /// </summary>
            WRITE_ATTRIBUTES_NO_RESPONSE,

            /// <summary>
            /// WRITE_ATTRIBUTES_RESPONSE: Write Attributes Response
            /// 
            /// See {@link WriteAttributesResponse}
            /// </summary>
            WRITE_ATTRIBUTES_RESPONSE,

            /// <summary>
            /// WRITE_ATTRIBUTES_STRUCTURED_COMMAND: Write Attributes Structured Command
            /// 
            /// See {@link WriteAttributesStructuredCommand}
            /// </summary>
            WRITE_ATTRIBUTES_STRUCTURED_COMMAND,

            /// <summary>
            /// WRITE_ATTRIBUTES_STRUCTURED_RESPONSE: Write Attributes Structured Response
            /// 
            /// See {@link WriteAttributesStructuredResponse}
            /// </summary>
            WRITE_ATTRIBUTES_STRUCTURED_RESPONSE,

            /// <summary>
            /// WRITE_ATTRIBUTES_UNDIVIDED_COMMAND: Write Attributes Undivided Command
            /// 
            /// See {@link WriteAttributesUndividedCommand}
            /// </summary>
            WRITE_ATTRIBUTES_UNDIVIDED_COMMAND,

            /// <summary>
            /// ZONE_ENROLL_REQUEST_COMMAND: Zone Enroll Request Command
            /// 
            /// See {@link ZoneEnrollRequestCommand}
            /// </summary>
            ZONE_ENROLL_REQUEST_COMMAND,

            /// <summary>
            /// ZONE_ENROLL_RESPONSE: Zone Enroll Response
            /// 
            /// See {@link ZoneEnrollResponse}
            /// </summary>
            ZONE_ENROLL_RESPONSE,

            /// <summary>
            /// ZONE_STATUS_CHANGED_COMMAND: Zone Status Changed Command
            /// 
            /// See {@link ZoneStatusChangedCommand}
            /// </summary>
            ZONE_STATUS_CHANGED_COMMAND,

            /// <summary>
            /// ZONE_STATUS_CHANGE_NOTIFICATION_COMMAND: Zone Status Change Notification Command
            /// 
            /// See {@link ZoneStatusChangeNotificationCommand}
            /// </summary>
            ZONE_STATUS_CHANGE_NOTIFICATION_COMMAND,


        }
    }
}
