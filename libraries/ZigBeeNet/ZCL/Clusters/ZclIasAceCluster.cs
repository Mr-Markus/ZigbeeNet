
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ZigBeeNet.DAO;
using ZigBeeNet.Security;
using ZigBeeNet.ZCL.Clusters.IASACE;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Protocol;


namespace ZigBeeNet.ZCL.Clusters
{
    /// <summary>
    /// IAS ACE cluster implementation (Cluster ID 0x0501.
    ///
    /// The IAS ACE cluster defines an interface to the functionality of any Ancillary Control
    /// Equipment of the IAS system. Using this cluster, a ZigBee enabled ACE device can access a
    /// IAS CIE device and manipulate the IAS system, on behalf of a level-2 user.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class ZclIasAceCluster : ZclCluster
    {
        /// <summary>
        /// The ZigBee Cluster Library Cluster ID
        /// </summary>
        public const ushort CLUSTER_ID = 0x0501;

        /// <summary>
        /// The ZigBee Cluster Library Cluster Name
        /// </summary>
        public const string CLUSTER_NAME = "IAS ACE";

        protected override Dictionary<ushort, ZclAttribute> InitializeClientAttributes()
        {
            Dictionary<ushort, ZclAttribute> attributeMap = new Dictionary<ushort, ZclAttribute>(0);

            return attributeMap;
        }

        protected override Dictionary<ushort, ZclAttribute> InitializeServerAttributes()
        {
            Dictionary<ushort, ZclAttribute> attributeMap = new Dictionary<ushort, ZclAttribute>(0);

            return attributeMap;
        }

        protected override Dictionary<ushort, Func<ZclCommand>> InitializeServerCommands()
        {
            Dictionary<ushort, Func<ZclCommand>> commandMap = new Dictionary<ushort, Func<ZclCommand>>(9);

            commandMap.Add(0x0000, () => new ArmResponse());
            commandMap.Add(0x0001, () => new GetZoneIdMapResponse());
            commandMap.Add(0x0002, () => new GetZoneInformationResponse());
            commandMap.Add(0x0003, () => new ZoneStatusChangedCommand());
            commandMap.Add(0x0004, () => new PanelStatusChangedCommand());
            commandMap.Add(0x0005, () => new GetPanelStatusResponse());
            commandMap.Add(0x0006, () => new SetBypassedZoneListCommand());
            commandMap.Add(0x0007, () => new BypassResponse());
            commandMap.Add(0x0008, () => new GetZoneStatusResponse());

            return commandMap;
        }

        protected override Dictionary<ushort, Func<ZclCommand>> InitializeClientCommands()
        {
            Dictionary<ushort, Func<ZclCommand>> commandMap = new Dictionary<ushort, Func<ZclCommand>>(10);

            commandMap.Add(0x0000, () => new ArmCommand());
            commandMap.Add(0x0001, () => new BypassCommand());
            commandMap.Add(0x0002, () => new EmergencyCommand());
            commandMap.Add(0x0003, () => new FireCommand());
            commandMap.Add(0x0004, () => new PanicCommand());
            commandMap.Add(0x0005, () => new GetZoneIdMapCommand());
            commandMap.Add(0x0006, () => new GetZoneInformationCommand());
            commandMap.Add(0x0007, () => new GetPanelStatusCommand());
            commandMap.Add(0x0008, () => new GetBypassedZoneListCommand());
            commandMap.Add(0x0009, () => new GetZoneStatusCommand());

            return commandMap;
        }

        /// <summary>
        /// Default constructor to create a IAS ACE cluster.
        ///
        /// <param name="zigbeeEndpoint"> the ZigBeeEndpoint this cluster is contained within </param>
        /// </summary>
        public ZclIasAceCluster(ZigBeeEndpoint zigbeeEndpoint)
            :base(zigbeeEndpoint, CLUSTER_ID, CLUSTER_NAME)
        {
        }

        /// <summary>
        /// The Arm Command
        ///
        /// On receipt of this command, the receiving device sets its arm mode according to the
        /// value of the Arm Mode field. It is not guaranteed that an Arm command will succeed.
        /// Based on the current state of the IAS CIE, and its related devices, the command can be
        /// rejected. The device shall generate an Arm Response command to indicate the
        /// resulting armed state
        ///
        /// <param name="armMode" <see cref="byte"> Arm Mode</ param >
        /// <param name="armDisarmCode" <see cref="string"> Arm/Disarm Code</ param >
        /// <param name="zoneId" <see cref="byte"> Zone ID</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> ArmCommand(byte armMode, string armDisarmCode, byte zoneId)
        {
            ArmCommand command = new ArmCommand();

            // Set the fields
            command.ArmMode = armMode;
            command.ArmDisarmCode = armDisarmCode;
            command.ZoneId = zoneId;

            return Send(command);
        }

        /// <summary>
        /// The Bypass Command
        ///
        /// Provides IAS ACE clients with a method to send zone bypass requests to the IAS ACE
        /// server. Bypassed zones may be faulted or in alarm but will not trigger the security
        /// system to go into alarm. For example, a user MAYwish to allow certain windows in his
        /// premises protected by an IAS Zone server to be left open while the user leaves the
        /// premises. The user could bypass the IAS Zone server protecting the window on his IAS
        /// ACE client (e.g., security keypad), and if the IAS ACE server indicates that zone is
        /// successfully by-passed, arm his security system while he is away.
        ///
        /// <param name="numberOfZones" <see cref="byte"> Number Of Zones</ param >
        /// <param name="zoneIds" <see cref="List<byte>"> Zone IDs</ param >
        /// <param name="armDisarmCode" <see cref="string"> Arm/Disarm Code</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> BypassCommand(byte numberOfZones, List<byte> zoneIds, string armDisarmCode)
        {
            BypassCommand command = new BypassCommand();

            // Set the fields
            command.NumberOfZones = numberOfZones;
            command.ZoneIds = zoneIds;
            command.ArmDisarmCode = armDisarmCode;

            return Send(command);
        }

        /// <summary>
        /// The Emergency Command
        ///
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> EmergencyCommand()
        {
            return Send(new EmergencyCommand());
        }

        /// <summary>
        /// The Fire Command
        ///
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> FireCommand()
        {
            return Send(new FireCommand());
        }

        /// <summary>
        /// The Panic Command
        ///
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> PanicCommand()
        {
            return Send(new PanicCommand());
        }

        /// <summary>
        /// The Get Zone ID Map Command
        ///
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> GetZoneIdMapCommand()
        {
            return Send(new GetZoneIdMapCommand());
        }

        /// <summary>
        /// The Get Zone Information Command
        ///
        /// <param name="zoneId" <see cref="byte"> Zone ID</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> GetZoneInformationCommand(byte zoneId)
        {
            GetZoneInformationCommand command = new GetZoneInformationCommand();

            // Set the fields
            command.ZoneId = zoneId;

            return Send(command);
        }

        /// <summary>
        /// The Get Panel Status Command
        ///
        /// This command is used by ACE clients to request an update to the status (e.g.,
        /// security system arm state) of the ACE server (i.e., the IAS CIE). In particular,
        /// this command is useful for battery-powered ACE clients with polling rates longer
        /// than the ZigBee standard check-in rate. <br> On receipt of this command, the ACE
        /// server responds with the status of the security system. The IAS ACE server shall
        /// generate a Get Panel Status Response command.
        ///
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> GetPanelStatusCommand()
        {
            return Send(new GetPanelStatusCommand());
        }

        /// <summary>
        /// The Get Bypassed Zone List Command
        ///
        /// Provides IAS ACE clients with a way to retrieve the list of zones to be bypassed. This
        /// provides them with the ability to provide greater local functionality (i.e., at
        /// the IAS ACE client) for users to modify the Bypassed Zone List and reduce
        /// communications to the IAS ACE server when trying to arm the CIE security system.
        ///
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> GetBypassedZoneListCommand()
        {
            return Send(new GetBypassedZoneListCommand());
        }

        /// <summary>
        /// The Get Zone Status Command
        ///
        /// This command is used by ACE clients to request an update of the status of the IAS Zone
        /// devices managed by the ACE server (i.e., the IAS CIE). In particular, this command
        /// is useful for battery-powered ACE clients with polling rates longer than the
        /// ZigBee standard check-in rate. The command is similar to the Get Attributes
        /// Supported command in that it specifies a starting Zone ID and a number of Zone IDs for
        /// which information is requested. Depending on the number of IAS Zone devices
        /// managed by the IAS ACE server, sending the Zone Status of all zones may not fit into a
        /// single Get ZoneStatus Response command. IAS ACE clients may need to send multiple
        /// Get Zone Status commands in order to get the information they seek.
        ///
        /// <param name="startingZoneId" <see cref="byte"> Starting Zone ID</ param >
        /// <param name="maxZoneIDs" <see cref="byte"> Max Zone I Ds</ param >
        /// <param name="zoneStatusMaskFlag" <see cref="bool"> Zone Status Mask Flag</ param >
        /// <param name="zoneStatusMask" <see cref="ushort"> Zone Status Mask</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> GetZoneStatusCommand(byte startingZoneId, byte maxZoneIDs, bool zoneStatusMaskFlag, ushort zoneStatusMask)
        {
            GetZoneStatusCommand command = new GetZoneStatusCommand();

            // Set the fields
            command.StartingZoneId = startingZoneId;
            command.MaxZoneIDs = maxZoneIDs;
            command.ZoneStatusMaskFlag = zoneStatusMaskFlag;
            command.ZoneStatusMask = zoneStatusMask;

            return Send(command);
        }

        /// <summary>
        /// The Arm Response
        ///
        /// <param name="armNotification" <see cref="byte"> Arm Notification</ param >
        /// <returns> the command result Task </returns>
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
        /// The 16 fields of the payload indicate whether each of the Zone IDs from 0x00 to 0xff is
        /// allocated or not. If bit n of Zone ID Map section N is set to 1, then Zone ID (16 x N + n ) is
        /// allocated, else it is not allocated.
        ///
        /// <param name="zoneIdMapSection0" <see cref="ushort"> Zone ID Map Section 0</ param >
        /// <param name="zoneIdMapSection1" <see cref="ushort"> Zone ID Map Section 1</ param >
        /// <param name="zoneIdMapSection2" <see cref="ushort"> Zone ID Map Section 2</ param >
        /// <param name="zoneIdMapSection3" <see cref="ushort"> Zone ID Map Section 3</ param >
        /// <param name="zoneIdMapSection4" <see cref="ushort"> Zone ID Map Section 4</ param >
        /// <param name="zoneIdMapSection5" <see cref="ushort"> Zone ID Map Section 5</ param >
        /// <param name="zoneIdMapSection6" <see cref="ushort"> Zone ID Map Section 6</ param >
        /// <param name="zoneIdMapSection7" <see cref="ushort"> Zone ID Map Section 7</ param >
        /// <param name="zoneIdMapSection8" <see cref="ushort"> Zone ID Map Section 8</ param >
        /// <param name="zoneIdMapSection9" <see cref="ushort"> Zone ID Map Section 9</ param >
        /// <param name="zoneIdMapSection10" <see cref="ushort"> Zone ID Map Section 10</ param >
        /// <param name="zoneIdMapSection11" <see cref="ushort"> Zone ID Map Section 11</ param >
        /// <param name="zoneIdMapSection12" <see cref="ushort"> Zone ID Map Section 12</ param >
        /// <param name="zoneIdMapSection13" <see cref="ushort"> Zone ID Map Section 13</ param >
        /// <param name="zoneIdMapSection14" <see cref="ushort"> Zone ID Map Section 14</ param >
        /// <param name="zoneIdMapSection15" <see cref="ushort"> Zone ID Map Section 15</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> GetZoneIdMapResponse(ushort zoneIdMapSection0, ushort zoneIdMapSection1, ushort zoneIdMapSection2, ushort zoneIdMapSection3, ushort zoneIdMapSection4, ushort zoneIdMapSection5, ushort zoneIdMapSection6, ushort zoneIdMapSection7, ushort zoneIdMapSection8, ushort zoneIdMapSection9, ushort zoneIdMapSection10, ushort zoneIdMapSection11, ushort zoneIdMapSection12, ushort zoneIdMapSection13, ushort zoneIdMapSection14, ushort zoneIdMapSection15)
        {
            GetZoneIdMapResponse command = new GetZoneIdMapResponse();

            // Set the fields
            command.ZoneIdMapSection0 = zoneIdMapSection0;
            command.ZoneIdMapSection1 = zoneIdMapSection1;
            command.ZoneIdMapSection2 = zoneIdMapSection2;
            command.ZoneIdMapSection3 = zoneIdMapSection3;
            command.ZoneIdMapSection4 = zoneIdMapSection4;
            command.ZoneIdMapSection5 = zoneIdMapSection5;
            command.ZoneIdMapSection6 = zoneIdMapSection6;
            command.ZoneIdMapSection7 = zoneIdMapSection7;
            command.ZoneIdMapSection8 = zoneIdMapSection8;
            command.ZoneIdMapSection9 = zoneIdMapSection9;
            command.ZoneIdMapSection10 = zoneIdMapSection10;
            command.ZoneIdMapSection11 = zoneIdMapSection11;
            command.ZoneIdMapSection12 = zoneIdMapSection12;
            command.ZoneIdMapSection13 = zoneIdMapSection13;
            command.ZoneIdMapSection14 = zoneIdMapSection14;
            command.ZoneIdMapSection15 = zoneIdMapSection15;

            return Send(command);
        }

        /// <summary>
        /// The Get Zone Information Response
        ///
        /// <param name="zoneId" <see cref="byte"> Zone ID</ param >
        /// <param name="zoneType" <see cref="ushort"> Zone Type</ param >
        /// <param name="ieeeAddress" <see cref="IeeeAddress"> IEEE Address</ param >
        /// <param name="zoneLabel" <see cref="string"> Zone Label</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> GetZoneInformationResponse(byte zoneId, ushort zoneType, IeeeAddress ieeeAddress, string zoneLabel)
        {
            GetZoneInformationResponse command = new GetZoneInformationResponse();

            // Set the fields
            command.ZoneId = zoneId;
            command.ZoneType = zoneType;
            command.IeeeAddress = ieeeAddress;
            command.ZoneLabel = zoneLabel;

            return Send(command);
        }

        /// <summary>
        /// The Zone Status Changed Command
        ///
        /// This command updates ACE clients in the system of changes to zone status recorded by
        /// the ACE server (e.g., IAS CIE device). An IAS ACE server should send a Zone Status
        /// Changed command upon a change to an IAS Zone device’s ZoneStatus that it manages
        /// (i.e., IAS ACE server should send a Zone Status Changed command upon receipt of a
        /// Zone Status Change Notification command).
        ///
        /// <param name="zoneId" <see cref="byte"> Zone ID</ param >
        /// <param name="zoneStatus" <see cref="ushort"> Zone Status</ param >
        /// <param name="audibleNotification" <see cref="byte"> Audible Notification</ param >
        /// <param name="zoneLabel" <see cref="string"> Zone Label</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> ZoneStatusChangedCommand(byte zoneId, ushort zoneStatus, byte audibleNotification, string zoneLabel)
        {
            ZoneStatusChangedCommand command = new ZoneStatusChangedCommand();

            // Set the fields
            command.ZoneId = zoneId;
            command.ZoneStatus = zoneStatus;
            command.AudibleNotification = audibleNotification;
            command.ZoneLabel = zoneLabel;

            return Send(command);
        }

        /// <summary>
        /// The Panel Status Changed Command
        ///
        /// This command updates ACE clients in the system of changes to panel status recorded
        /// by the ACE server (e.g., IAS CIE device).Sending the Panel Status Changed command
        /// (vs.the Get Panel Status and Get Panel Status Response method) is generally useful
        /// only when there are IAS ACE clients that data poll within the retry timeout of the
        /// network (e.g., less than 7.68 seconds). <br> An IAS ACE server shall send a Panel
        /// Status Changed command upon a change to the IAS CIE’s panel status (e.g., Disarmed
        /// to Arming Away/Stay/Night, Arming Away/Stay/Night to Armed, Armed to Disarmed)
        /// as defined in the Panel Status field. <br> When Panel Status is Arming
        /// Away/Stay/Night, an IAS ACE server should send Panel Status Changed commands
        /// every second in order to update the Seconds Remaining. In some markets (e.g., North
        /// America), the final 10 seconds of the Arming Away/Stay/Night sequence requires a
        /// separate audible notification (e.g., a double tone).
        ///
        /// <param name="panelStatus" <see cref="byte"> Panel Status</ param >
        /// <param name="secondsRemaining" <see cref="byte"> Seconds Remaining</ param >
        /// <param name="audibleNotification" <see cref="byte"> Audible Notification</ param >
        /// <param name="alarmStatus" <see cref="byte"> Alarm Status</ param >
        /// <returns> the command result Task </returns>
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
        /// This command updates requesting IAS ACE clients in the system of changes to the
        /// security panel status recorded by the ACE server (e.g., IAS CIE device).
        ///
        /// <param name="panelStatus" <see cref="byte"> Panel Status</ param >
        /// <param name="secondsRemaining" <see cref="byte"> Seconds Remaining</ param >
        /// <param name="audibleNotification" <see cref="byte"> Audible Notification</ param >
        /// <param name="alarmStatus" <see cref="byte"> Alarm Status</ param >
        /// <returns> the command result Task </returns>
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
        /// Sets the list of bypassed zones on the IAS ACE client. This command can be sent either
        /// as a response to the GetBypassedZoneList command or unsolicited when the list of
        /// bypassed zones changes on the ACE server.
        ///
        /// <param name="zoneId" <see cref="List<byte>"> Zone ID</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> SetBypassedZoneListCommand(List<byte> zoneId)
        {
            SetBypassedZoneListCommand command = new SetBypassedZoneListCommand();

            // Set the fields
            command.ZoneId = zoneId;

            return Send(command);
        }

        /// <summary>
        /// The Bypass Response
        ///
        /// Provides the response of the security panel to the request from the IAS ACE client to
        /// bypass zones via a Bypass command.
        ///
        /// <param name="bypassResult" <see cref="List<byte>"> Bypass Result</ param >
        /// <returns> the command result Task </returns>
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
        /// This command updates requesting IAS ACE clients in the system of changes to the IAS
        /// Zone server statuses recorded by the ACE server (e.g., IAS CIE device).
        ///
        /// <param name="zoneStatusComplete" <see cref="bool"> Zone Status Complete</ param >
        /// <param name="numberOfZones" <see cref="byte"> Number Of Zones</ param >
        /// <param name="iasAceZoneStatus" <see cref="byte"> IAS ACE Zone Status</ param >
        /// <param name="zoneId" <see cref="byte"> Zone ID</ param >
        /// <param name="zoneStatus" <see cref="ushort"> Zone Status</ param >
        /// <returns> the command result Task </returns>
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
    }
}
