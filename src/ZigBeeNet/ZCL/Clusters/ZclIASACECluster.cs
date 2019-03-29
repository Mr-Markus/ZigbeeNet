// License text here

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ZigBeeNet.DAO;
using ZigBeeNet.ZCL.Protocol;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Clusters.IASACE;

namespace ZigBeeNet.ZCL.Clusters
{
    /// <summary>
    /// IAS ACEcluster implementation (Cluster ID 0x0501).
    ///
    /// The IAS ACE cluster defines an interface to the functionality of any Ancillary
    /// Control Equipment of the IAS system. Using this cluster, a ZigBee enabled ACE
    /// device can access a IAS CIE device and manipulate the IAS system, on behalf of a
    /// level-2 user.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class ZclIASACECluster : ZclCluster
    {
        /// <summary>
        /// The ZigBee Cluster Library Cluster ID
        /// </summary>
        public const ushort CLUSTER_ID = 0x0501;

        /// <summary>
        /// The ZigBee Cluster Library Cluster Name
        /// </summary>
        public const string CLUSTER_NAME = "IAS ACE";

        // Attribute initialisation
        protected override Dictionary<ushort, ZclAttribute> InitializeAttributes()
        {
            Dictionary<ushort, ZclAttribute> attributeMap = new Dictionary<ushort, ZclAttribute>(0);

            return attributeMap;
        }

        /// <summary>
        /// Default constructor to create a IAS ACE cluster.
        ///
        /// <param name ="zigbeeEndpoint">The ZigBeeEndpoint</param>
        /// </summary>
        public ZclIASACECluster(ZigBeeEndpoint zigbeeEndpoint)
            : base(zigbeeEndpoint, CLUSTER_ID, CLUSTER_NAME)
        {
        }


        /// <summary>
        /// The Arm Command
        ///
        /// On receipt of this command, the receiving device sets its arm mode according to the value of the Arm Mode field. It
        /// is not guaranteed that an Arm command will succeed. Based on the current state of
        /// the IAS CIE, and its related devices, the command can be rejected. The device SHALL generate an Arm Response command
        /// to indicate the resulting armed state
        ///
        /// <param name="armMode"><see cref="byte"/> Arm Mode</param>
        /// <param name="armDisarmCode"><see cref="string"/> Arm/Disarm Code</param>
        /// <param name="zoneID"><see cref="byte"/> Zone ID</param>
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> ArmCommand(byte armMode, string armDisarmCode, byte zoneID)
        {
            ArmCommand command = new ArmCommand();

            // Set the fields
            command.ArmMode = armMode;
            command.ArmDisarmCode = armDisarmCode;
            command.ZoneID = zoneID;

            return Send(command);
        }

        /// <summary>
        /// The Bypass Command
        ///
        /// Provides IAS ACE clients with a method to send zone bypass requests to the IAS ACE server.
        /// Bypassed zones MAYbe faulted or in alarm but will not trigger the security system to go into alarm.
        /// For example, a user MAYwish to allow certain windows in his premises protected by an IAS Zone server to
        /// be left open while the user leaves the premises. The user could bypass the IAS Zone server protecting
        /// the window on his IAS ACE client (e.g., security keypad), and if the IAS ACE server indicates that zone is
        /// successfully by-passed, arm his security system while he is away.
        ///
        /// <param name="numberOfZones"><see cref="byte"/> Number of Zones</param>
        /// <param name="zoneIDs"><see cref="List<byte>"/> Zone IDs</param>
        /// <param name="armDisarmCode"><see cref="string"/> Arm/Disarm Code</param>
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> BypassCommand(byte numberOfZones, List<byte> zoneIDs, string armDisarmCode)
        {
            BypassCommand command = new BypassCommand();

            // Set the fields
            command.NumberOfZones = numberOfZones;
            command.ZoneIDs = zoneIDs;
            command.ArmDisarmCode = armDisarmCode;

            return Send(command);
        }

        /// <summary>
        /// The Emergency Command
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> EmergencyCommand()
        {
            EmergencyCommand command = new EmergencyCommand();

            return Send(command);
        }

        /// <summary>
        /// The Fire Command
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> FireCommand()
        {
            FireCommand command = new FireCommand();

            return Send(command);
        }

        /// <summary>
        /// The Panic Command
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> PanicCommand()
        {
            PanicCommand command = new PanicCommand();

            return Send(command);
        }

        /// <summary>
        /// The Get Zone ID Map Command
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> GetZoneIDMapCommand()
        {
            GetZoneIDMapCommand command = new GetZoneIDMapCommand();

            return Send(command);
        }

        /// <summary>
        /// The Get Zone Information Command
        ///
        /// <param name="zoneID"><see cref="byte"/> Zone ID</param>
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> GetZoneInformationCommand(byte zoneID)
        {
            GetZoneInformationCommand command = new GetZoneInformationCommand();

            // Set the fields
            command.ZoneID = zoneID;

            return Send(command);
        }

        /// <summary>
        /// The Get Panel Status Command
        ///
        /// This command is used by ACE clients to request an update to the status (e.g., security
        /// system arm state) of the ACE server (i.e., the IAS CIE). In particular, this command is
        /// useful for battery-powered ACE clients with polling rates longer than the ZigBee standard
        /// check-in rate.
        /// <br>
        /// On receipt of this command, the ACE server responds with the status of the security system.
        /// The IAS ACE server SHALL generate a Get Panel Status Response command.
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> GetPanelStatusCommand()
        {
            GetPanelStatusCommand command = new GetPanelStatusCommand();

            return Send(command);
        }

        /// <summary>
        /// The Get Bypassed Zone List Command
        ///
        /// Provides IAS ACE clients with a way to retrieve the list of zones to be bypassed. This provides them with the ability
        /// to provide greater local functionality (i.e., at the IAS ACE client) for users to modify the Bypassed Zone List and reduce
        /// communications to the IAS ACE server when trying to arm the CIE security system.
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> GetBypassedZoneListCommand()
        {
            GetBypassedZoneListCommand command = new GetBypassedZoneListCommand();

            return Send(command);
        }

        /// <summary>
        /// The Get Zone Status Command
        ///
        /// This command is used by ACE clients to request an update of the status of the IAS Zone devices managed by the ACE server
        /// (i.e., the IAS CIE). In particular, this command is useful for battery-powered ACE clients with polling rates longer than
        /// the ZigBee standard check-in rate. The command is similar to the Get Attributes Supported command in that it specifies a
        /// starting Zone ID and a number of Zone IDs for which information is requested. Depending on the number of IAS Zone devices
        /// managed by the IAS ACE server, sending the Zone Status of all zones MAY not fit into a single Get ZoneStatus Response command.
        /// IAS ACE clients MAY need to send multiple Get Zone Status commands in order to get the information they seek.
        ///
        /// <param name="startingZoneID"><see cref="byte"/> Starting Zone ID</param>
        /// <param name="maxZoneIDs"><see cref="byte"/> Max Zone IDs</param>
        /// <param name="zoneStatusMaskFlag"><see cref="bool"/> Zone Status Mask Flag</param>
        /// <param name="zoneStatusMask"><see cref="ushort"/> Zone Status Mask</param>
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> GetZoneStatusCommand(byte startingZoneID, byte maxZoneIDs, bool zoneStatusMaskFlag, ushort zoneStatusMask)
        {
            GetZoneStatusCommand command = new GetZoneStatusCommand();

            // Set the fields
            command.StartingZoneID = startingZoneID;
            command.MaxZoneIDs = maxZoneIDs;
            command.ZoneStatusMaskFlag = zoneStatusMaskFlag;
            command.ZoneStatusMask = zoneStatusMask;

            return Send(command);
        }

        /// <summary>
        /// The Arm Response
        ///
        /// <param name="armNotification"><see cref="byte"/> Arm Notification</param>
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> ArmResponse(byte armNotification)
        {
            ArmResponse command = new ArmResponse();

            // Set the fields
            command.ArmNotification = armNotification;

            return Send(command);
        }

        /// <summary>
        /// The Get Zone ID Map Response
        ///
        /// The 16 fields of the payload indicate whether each of the Zone IDs from 0 to 0xff is allocated or not. If bit n
        /// of Zone ID Map section N is set to 1, then Zone ID (16 x N + n ) is allocated, else it is not allocated
        ///
        /// <param name="zoneIDMapSection0"><see cref="ushort"/> Zone ID Map section 0</param>
        /// <param name="zoneIDMapSection1"><see cref="ushort"/> Zone ID Map section 1</param>
        /// <param name="zoneIDMapSection2"><see cref="ushort"/> Zone ID Map section 2</param>
        /// <param name="zoneIDMapSection3"><see cref="ushort"/> Zone ID Map section 3</param>
        /// <param name="zoneIDMapSection4"><see cref="ushort"/> Zone ID Map section 4</param>
        /// <param name="zoneIDMapSection5"><see cref="ushort"/> Zone ID Map section 5</param>
        /// <param name="zoneIDMapSection6"><see cref="ushort"/> Zone ID Map section 6</param>
        /// <param name="zoneIDMapSection7"><see cref="ushort"/> Zone ID Map section 7</param>
        /// <param name="zoneIDMapSection8"><see cref="ushort"/> Zone ID Map section 8</param>
        /// <param name="zoneIDMapSection9"><see cref="ushort"/> Zone ID Map section 9</param>
        /// <param name="zoneIDMapSection10"><see cref="ushort"/> Zone ID Map section 10</param>
        /// <param name="zoneIDMapSection11"><see cref="ushort"/> Zone ID Map section 11</param>
        /// <param name="zoneIDMapSection12"><see cref="ushort"/> Zone ID Map section 12</param>
        /// <param name="zoneIDMapSection13"><see cref="ushort"/> Zone ID Map section 13</param>
        /// <param name="zoneIDMapSection14"><see cref="ushort"/> Zone ID Map section 14</param>
        /// <param name="zoneIDMapSection15"><see cref="ushort"/> Zone ID Map section 15</param>
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> GetZoneIDMapResponse(ushort zoneIDMapSection0, ushort zoneIDMapSection1, ushort zoneIDMapSection2, ushort zoneIDMapSection3, ushort zoneIDMapSection4, ushort zoneIDMapSection5, ushort zoneIDMapSection6, ushort zoneIDMapSection7, ushort zoneIDMapSection8, ushort zoneIDMapSection9, ushort zoneIDMapSection10, ushort zoneIDMapSection11, ushort zoneIDMapSection12, ushort zoneIDMapSection13, ushort zoneIDMapSection14, ushort zoneIDMapSection15)
        {
            GetZoneIDMapResponse command = new GetZoneIDMapResponse();

            // Set the fields
            command.ZoneIDMapSection0 = zoneIDMapSection0;
            command.ZoneIDMapSection1 = zoneIDMapSection1;
            command.ZoneIDMapSection2 = zoneIDMapSection2;
            command.ZoneIDMapSection3 = zoneIDMapSection3;
            command.ZoneIDMapSection4 = zoneIDMapSection4;
            command.ZoneIDMapSection5 = zoneIDMapSection5;
            command.ZoneIDMapSection6 = zoneIDMapSection6;
            command.ZoneIDMapSection7 = zoneIDMapSection7;
            command.ZoneIDMapSection8 = zoneIDMapSection8;
            command.ZoneIDMapSection9 = zoneIDMapSection9;
            command.ZoneIDMapSection10 = zoneIDMapSection10;
            command.ZoneIDMapSection11 = zoneIDMapSection11;
            command.ZoneIDMapSection12 = zoneIDMapSection12;
            command.ZoneIDMapSection13 = zoneIDMapSection13;
            command.ZoneIDMapSection14 = zoneIDMapSection14;
            command.ZoneIDMapSection15 = zoneIDMapSection15;

            return Send(command);
        }

        /// <summary>
        /// The Get Zone Information Response
        ///
        /// <param name="zoneID"><see cref="byte"/> Zone ID</param>
        /// <param name="zoneType"><see cref="ushort"/> Zone Type</param>
        /// <param name="iEEEAddress"><see cref="IeeeAddress"/> IEEE address</param>
        /// <param name="zoneLabel"><see cref="string"/> Zone Label</param>
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> GetZoneInformationResponse(byte zoneID, ushort zoneType, IeeeAddress iEEEAddress, string zoneLabel)
        {
            GetZoneInformationResponse command = new GetZoneInformationResponse();

            // Set the fields
            command.ZoneID = zoneID;
            command.ZoneType = zoneType;
            command.IEEEAddress = iEEEAddress;
            command.ZoneLabel = zoneLabel;

            return Send(command);
        }

        /// <summary>
        /// The Zone Status Changed Command
        ///
        /// This command updates ACE clients in the system of changes to zone status recorded by the ACE server (e.g., IAS CIE device).
        /// An IAS ACE server SHOULD send a Zone Status Changed command upon a change to an IAS Zone device’s ZoneStatus that it manages (i.e.,
        /// IAS ACE server SHOULD send a Zone Status Changed command upon receipt of a Zone Status Change Notification command).
        ///
        /// <param name="zoneID"><see cref="byte"/> Zone ID</param>
        /// <param name="zoneStatus"><see cref="ushort"/> Zone Status</param>
        /// <param name="audibleNotification"><see cref="byte"/> Audible Notification</param>
        /// <param name="zoneLabel"><see cref="string"/> Zone Label</param>
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> ZoneStatusChangedCommand(byte zoneID, ushort zoneStatus, byte audibleNotification, string zoneLabel)
        {
            ZoneStatusChangedCommand command = new ZoneStatusChangedCommand();

            // Set the fields
            command.ZoneID = zoneID;
            command.ZoneStatus = zoneStatus;
            command.AudibleNotification = audibleNotification;
            command.ZoneLabel = zoneLabel;

            return Send(command);
        }

        /// <summary>
        /// The Panel Status Changed Command
        ///
        /// This command updates ACE clients in the system of changes to panel status recorded by the ACE server (e.g., IAS CIE
        /// device).Sending the Panel Status Changed command (vs.the Get Panel Status and Get Panel Status Response method) is
        /// generally useful only when there are IAS ACE clients that data poll within the retry timeout of the network (e.g., less than
        /// 7.68 seconds).
        /// <br>
        /// An IAS ACE server SHALL send a Panel Status Changed command upon a change to the IAS CIE’s panel status (e.g.,
        /// Disarmed to Arming Away/Stay/Night, Arming Away/Stay/Night to Armed, Armed to Disarmed) as defined in the Panel Status field.
        /// <br>
        /// When Panel Status is Arming Away/Stay/Night, an IAS ACE server SHOULD send Panel Status Changed commands every second in order to
        /// update the Seconds Remaining. In some markets (e.g., North America), the final 10 seconds of the Arming Away/Stay/Night sequence
        /// requires a separate audible notification (e.g., a double tone).
        ///
        /// <param name="panelStatus"><see cref="byte"/> Panel Status</param>
        /// <param name="secondsRemaining"><see cref="byte"/> Seconds Remaining</param>
        /// <param name="audibleNotification"><see cref="byte"/> Audible Notification</param>
        /// <param name="alarmStatus"><see cref="byte"/> Alarm Status</param>
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> PanelStatusChangedCommand(byte panelStatus, byte secondsRemaining, byte audibleNotification, byte alarmStatus)
        {
            PanelStatusChangedCommand command = new PanelStatusChangedCommand();

            // Set the fields
            command.PanelStatus = panelStatus;
            command.SecondsRemaining = secondsRemaining;
            command.AudibleNotification = audibleNotification;
            command.AlarmStatus = alarmStatus;

            return Send(command);
        }

        /// <summary>
        /// The Get Panel Status Response
        ///
        /// This command updates requesting IAS ACE clients in the system of changes to the security panel status recorded by
        /// the ACE server (e.g., IAS CIE device).
        ///
        /// <param name="panelStatus"><see cref="byte"/> Panel Status</param>
        /// <param name="secondsRemaining"><see cref="byte"/> Seconds Remaining</param>
        /// <param name="audibleNotification"><see cref="byte"/> Audible Notification</param>
        /// <param name="alarmStatus"><see cref="byte"/> Alarm Status</param>
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> GetPanelStatusResponse(byte panelStatus, byte secondsRemaining, byte audibleNotification, byte alarmStatus)
        {
            GetPanelStatusResponse command = new GetPanelStatusResponse();

            // Set the fields
            command.PanelStatus = panelStatus;
            command.SecondsRemaining = secondsRemaining;
            command.AudibleNotification = audibleNotification;
            command.AlarmStatus = alarmStatus;

            return Send(command);
        }

        /// <summary>
        /// The Set Bypassed Zone List Command
        ///
        /// Sets the list of bypassed zones on the IAS ACE client. This command can be sent either as a response to the
        /// GetBypassedZoneList command or unsolicited when the list of bypassed zones changes on the ACE server.
        ///
        /// <param name="zoneID"><see cref="List<byte>"/> Zone ID</param>
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> SetBypassedZoneListCommand(List<byte> zoneID)
        {
            SetBypassedZoneListCommand command = new SetBypassedZoneListCommand();

            // Set the fields
            command.ZoneID = zoneID;

            return Send(command);
        }

        /// <summary>
        /// The Bypass Response
        ///
        /// Provides the response of the security panel to the request from the IAS ACE client to bypass zones via a Bypass command.
        ///
        /// <param name="bypassResult"><see cref="List<byte>"/> Bypass Result</param>
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> BypassResponse(List<byte> bypassResult)
        {
            BypassResponse command = new BypassResponse();

            // Set the fields
            command.BypassResult = bypassResult;

            return Send(command);
        }

        /// <summary>
        /// The Get Zone Status Response
        ///
        /// This command updates requesting IAS ACE clients in the system of changes to the IAS Zone server statuses recorded
        /// by the ACE server (e.g., IAS CIE device).
        ///
        /// <param name="zoneStatusComplete"><see cref="bool"/> Zone Status Complete</param>
        /// <param name="numberOfZones"><see cref="byte"/> Number of zones</param>
        /// <param name="iasAceZoneStatus"><see cref="byte"/> Ias Ace Zone Status</param>
        /// <param name="zoneId"><see cref="byte"/> Zone Id</param>
        /// <param name="zoneStatus"><see cref="ushort"/> Zone Status</param>
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> GetZoneStatusResponse(bool zoneStatusComplete, byte numberOfZones, byte iasAceZoneStatus, byte zoneId, ushort zoneStatus)
        {
            GetZoneStatusResponse command = new GetZoneStatusResponse();

            // Set the fields
            command.ZoneStatusComplete = zoneStatusComplete;
            command.NumberOfZones = numberOfZones;
            command.IasAceZoneStatus = iasAceZoneStatus;
            command.ZoneId = zoneId;
            command.ZoneStatus = zoneStatus;

            return Send(command);
        }

        public override ZclCommand GetCommandFromId(int commandId)
        {
            switch (commandId)
            {
                case 0: // ARM_COMMAND
                    return new ArmCommand();
                case 1: // BYPASS_COMMAND
                    return new BypassCommand();
                case 2: // EMERGENCY_COMMAND
                    return new EmergencyCommand();
                case 3: // FIRE_COMMAND
                    return new FireCommand();
                case 4: // PANIC_COMMAND
                    return new PanicCommand();
                case 5: // GET_ZONE_ID_MAP_COMMAND
                    return new GetZoneIDMapCommand();
                case 6: // GET_ZONE_INFORMATION_COMMAND
                    return new GetZoneInformationCommand();
                case 7: // GET_PANEL_STATUS_COMMAND
                    return new GetPanelStatusCommand();
                case 8: // GET_BYPASSED_ZONE_LIST_COMMAND
                    return new GetBypassedZoneListCommand();
                case 9: // GET_ZONE_STATUS_COMMAND
                    return new GetZoneStatusCommand();
                    default:
                        return null;
            }
        }

        public ZclCommand getResponseFromId(int commandId)
        {
            switch (commandId)
            {
                case 0: // ARM_RESPONSE
                    return new ArmResponse();
                case 1: // GET_ZONE_ID_MAP_RESPONSE
                    return new GetZoneIDMapResponse();
                case 2: // GET_ZONE_INFORMATION_RESPONSE
                    return new GetZoneInformationResponse();
                case 3: // ZONE_STATUS_CHANGED_COMMAND
                    return new ZoneStatusChangedCommand();
                case 4: // PANEL_STATUS_CHANGED_COMMAND
                    return new PanelStatusChangedCommand();
                case 5: // GET_PANEL_STATUS_RESPONSE
                    return new GetPanelStatusResponse();
                case 6: // SET_BYPASSED_ZONE_LIST_COMMAND
                    return new SetBypassedZoneListCommand();
                case 7: // BYPASS_RESPONSE
                    return new BypassResponse();
                case 8: // GET_ZONE_STATUS_RESPONSE
                    return new GetZoneStatusResponse();
                    default:
                        return null;
            }
        }
    }
}
