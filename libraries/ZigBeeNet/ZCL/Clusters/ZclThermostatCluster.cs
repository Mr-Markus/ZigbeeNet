
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ZigBeeNet.DAO;
using ZigBeeNet.Security;
using ZigBeeNet.ZCL.Clusters.Thermostat;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Protocol;


namespace ZigBeeNet.ZCL.Clusters
{
    /// <summary>
    /// Thermostat cluster implementation (Cluster ID 0x0201.
    ///
    /// This cluster provides an interface to the functionality of a thermostat.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class ZclThermostatCluster : ZclCluster
    {
        /// <summary>
        /// The ZigBee Cluster Library Cluster ID
        /// </summary>
        public const ushort CLUSTER_ID = 0x0201;

        /// <summary>
        /// The ZigBee Cluster Library Cluster Name
        /// </summary>
        public const string CLUSTER_NAME = "Thermostat";

        // Attribute constants

        /// <summary>
        /// LocalTemperature represents the temperature in degrees Celsius, as measured
        /// locally.
        /// </summary>
        public const ushort ATTR_LOCALTEMPERATURE = 0x0000;

        /// <summary>
        /// OutdoorTemperature represents the temperature in degrees Celsius, as measured
        /// locally.
        /// </summary>
        public const ushort ATTR_OUTDOORTEMPERATURE = 0x0001;

        /// <summary>
        /// Occupancy specifies whether the heated/cooled space is occupied or not
        /// </summary>
        public const ushort ATTR_OCCUPANCY = 0x0002;

        /// <summary>
        /// The MinHeatSetpointLimit attribute specifies the absolute minimum level that
        /// the heating setpoint may be set to. This is a limitation imposed by the
        /// manufacturer.
        /// </summary>
        public const ushort ATTR_ABSMINHEATSETPOINTLIMIT = 0x0003;

        /// <summary>
        /// The MaxHeatSetpointLimit attribute specifies the absolute maximum level that
        /// the heating setpoint may be set to. This is a limitation imposed by the
        /// manufacturer.
        /// </summary>
        public const ushort ATTR_ABSMAXHEATSETPOINTLIMIT = 0x0004;

        /// <summary>
        /// The MinCoolSetpointLimit attribute specifies the absolute minimum level that
        /// the cooling setpoint may be set to. This is a limitation imposed by the
        /// manufacturer.
        /// </summary>
        public const ushort ATTR_ABSMINCOOLSETPOINTLIMIT = 0x0005;

        /// <summary>
        /// The MaxCoolSetpointLimit attribute specifies the absolute maximum level that
        /// the cooling setpoint may be set to. This is a limitation imposed by the
        /// manufacturer.
        /// </summary>
        public const ushort ATTR_ABSMAXCOOLSETPOINTLIMIT = 0x0006;

        /// <summary>
        /// The PICoolingDemandattribute is 8 bits in length and specifies the level of
        /// cooling demanded by the PI (proportional integral) control loop in use by the
        /// thermostat (if any), in percent. This value is 0 when the thermostat is in “off” or
        /// “heating” mode.
        /// </summary>
        public const ushort ATTR_PICOOLINGDEMAND = 0x0007;

        /// <summary>
        /// The PIHeatingDemand attribute is 8 bits in length and specifies the level of
        /// heating demanded by the PI (proportional integral) control loop in use by the
        /// thermostat (if any), in percent. This value is 0 when the thermostat is in “off” or
        /// “cooling” mode.
        /// </summary>
        public const ushort ATTR_PIHEATINGDEMAND = 0x0008;
        public const ushort ATTR_HVACSYSTEMTYPECONFIGURATION = 0x0009;
        public const ushort ATTR_LOCALTEMPERATURECALIBRATION = 0x0010;
        public const ushort ATTR_OCCUPIEDCOOLINGSETPOINT = 0x0011;
        public const ushort ATTR_OCCUPIEDHEATINGSETPOINT = 0x0012;
        public const ushort ATTR_UNOCCUPIEDCOOLINGSETPOINT = 0x0013;
        public const ushort ATTR_UNOCCUPIEDHEATINGSETPOINT = 0x0014;
        public const ushort ATTR_MINHEATSETPOINTLIMIT = 0x0015;
        public const ushort ATTR_MAXHEATSETPOINTLIMIT = 0x0016;
        public const ushort ATTR_MINCOOLSETPOINTLIMIT = 0x0017;
        public const ushort ATTR_MAXCOOLSETPOINTLIMIT = 0x0018;
        public const ushort ATTR_MINSETPOINTDEADBAND = 0x0019;
        public const ushort ATTR_REMOTESENSING = 0x001A;
        public const ushort ATTR_CONTROLSEQUENCEOFOPERATION = 0x001B;
        public const ushort ATTR_SYSTEMMODE = 0x001C;
        public const ushort ATTR_ALARMMASK = 0x001D;
        public const ushort ATTR_THERMOSTATRUNNINGMODE = 0x001E;

        protected override Dictionary<ushort, ZclAttribute> InitializeClientAttributes()
        {
            Dictionary<ushort, ZclAttribute> attributeMap = new Dictionary<ushort, ZclAttribute>(0);

            return attributeMap;
        }

        protected override Dictionary<ushort, ZclAttribute> InitializeServerAttributes()
        {
            Dictionary<ushort, ZclAttribute> attributeMap = new Dictionary<ushort, ZclAttribute>(25);

            attributeMap.Add(ATTR_LOCALTEMPERATURE, new ZclAttribute(this, ATTR_LOCALTEMPERATURE, "Local Temperature", ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER), true, true, false, true));
            attributeMap.Add(ATTR_OUTDOORTEMPERATURE, new ZclAttribute(this, ATTR_OUTDOORTEMPERATURE, "Outdoor Temperature", ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_OCCUPANCY, new ZclAttribute(this, ATTR_OCCUPANCY, "Occupancy", ZclDataType.Get(DataType.BITMAP_8_BIT), false, true, false, false));
            attributeMap.Add(ATTR_ABSMINHEATSETPOINTLIMIT, new ZclAttribute(this, ATTR_ABSMINHEATSETPOINTLIMIT, "Abs Min Heat Setpoint Limit", ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_ABSMAXHEATSETPOINTLIMIT, new ZclAttribute(this, ATTR_ABSMAXHEATSETPOINTLIMIT, "Abs Max Heat Setpoint Limit", ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_ABSMINCOOLSETPOINTLIMIT, new ZclAttribute(this, ATTR_ABSMINCOOLSETPOINTLIMIT, "Abs Min Cool Setpoint Limit", ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_ABSMAXCOOLSETPOINTLIMIT, new ZclAttribute(this, ATTR_ABSMAXCOOLSETPOINTLIMIT, "Abs Max Cool Setpoint Limit", ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_PICOOLINGDEMAND, new ZclAttribute(this, ATTR_PICOOLINGDEMAND, "Pi Cooling Demand", ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER), false, true, false, true));
            attributeMap.Add(ATTR_PIHEATINGDEMAND, new ZclAttribute(this, ATTR_PIHEATINGDEMAND, "Pi Heating Demand", ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER), false, true, false, true));
            attributeMap.Add(ATTR_HVACSYSTEMTYPECONFIGURATION, new ZclAttribute(this, ATTR_HVACSYSTEMTYPECONFIGURATION, "Hvac System Type Configuration", ZclDataType.Get(DataType.BITMAP_8_BIT), false, true, false, false));
            attributeMap.Add(ATTR_LOCALTEMPERATURECALIBRATION, new ZclAttribute(this, ATTR_LOCALTEMPERATURECALIBRATION, "Local Temperature Calibration", ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_OCCUPIEDCOOLINGSETPOINT, new ZclAttribute(this, ATTR_OCCUPIEDCOOLINGSETPOINT, "Occupied Cooling Setpoint", ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_OCCUPIEDHEATINGSETPOINT, new ZclAttribute(this, ATTR_OCCUPIEDHEATINGSETPOINT, "Occupied Heating Setpoint", ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_UNOCCUPIEDCOOLINGSETPOINT, new ZclAttribute(this, ATTR_UNOCCUPIEDCOOLINGSETPOINT, "Unoccupied Cooling Setpoint", ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_UNOCCUPIEDHEATINGSETPOINT, new ZclAttribute(this, ATTR_UNOCCUPIEDHEATINGSETPOINT, "Unoccupied Heating Setpoint", ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_MINHEATSETPOINTLIMIT, new ZclAttribute(this, ATTR_MINHEATSETPOINTLIMIT, "Min Heat Setpoint Limit", ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_MAXHEATSETPOINTLIMIT, new ZclAttribute(this, ATTR_MAXHEATSETPOINTLIMIT, "Max Heat Setpoint Limit", ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_MINCOOLSETPOINTLIMIT, new ZclAttribute(this, ATTR_MINCOOLSETPOINTLIMIT, "Min Cool Setpoint Limit", ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_MAXCOOLSETPOINTLIMIT, new ZclAttribute(this, ATTR_MAXCOOLSETPOINTLIMIT, "Max Cool Setpoint Limit", ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_MINSETPOINTDEADBAND, new ZclAttribute(this, ATTR_MINSETPOINTDEADBAND, "Min Setpoint Dead Band", ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_REMOTESENSING, new ZclAttribute(this, ATTR_REMOTESENSING, "Remote Sensing", ZclDataType.Get(DataType.BITMAP_8_BIT), false, true, false, false));
            attributeMap.Add(ATTR_CONTROLSEQUENCEOFOPERATION, new ZclAttribute(this, ATTR_CONTROLSEQUENCEOFOPERATION, "Control Sequence Of Operation", ZclDataType.Get(DataType.ENUMERATION_8_BIT), true, true, false, false));
            attributeMap.Add(ATTR_SYSTEMMODE, new ZclAttribute(this, ATTR_SYSTEMMODE, "System Mode", ZclDataType.Get(DataType.ENUMERATION_8_BIT), true, true, false, false));
            attributeMap.Add(ATTR_ALARMMASK, new ZclAttribute(this, ATTR_ALARMMASK, "Alarm Mask", ZclDataType.Get(DataType.ENUMERATION_8_BIT), false, true, false, false));
            attributeMap.Add(ATTR_THERMOSTATRUNNINGMODE, new ZclAttribute(this, ATTR_THERMOSTATRUNNINGMODE, "Thermostat Running Mode", ZclDataType.Get(DataType.ENUMERATION_8_BIT), false, true, false, false));

            return attributeMap;
        }

        protected override Dictionary<ushort, Func<ZclCommand>> InitializeServerCommands()
        {
            Dictionary<ushort, Func<ZclCommand>> commandMap = new Dictionary<ushort, Func<ZclCommand>>(2);

            commandMap.Add(0x0000, () => new GetWeeklyScheduleResponse());
            commandMap.Add(0x0001, () => new GetRelayStatusLogResponse());

            return commandMap;
        }

        protected override Dictionary<ushort, Func<ZclCommand>> InitializeClientCommands()
        {
            Dictionary<ushort, Func<ZclCommand>> commandMap = new Dictionary<ushort, Func<ZclCommand>>(5);

            commandMap.Add(0x0000, () => new SetpointRaiseLowerCommand());
            commandMap.Add(0x0001, () => new SetWeeklySchedule());
            commandMap.Add(0x0002, () => new GetWeeklySchedule());
            commandMap.Add(0x0003, () => new ClearWeeklySchedule());
            commandMap.Add(0x0004, () => new GetRelayStatusLog());

            return commandMap;
        }

        /// <summary>
        /// Default constructor to create a Thermostat cluster.
        ///
        /// <param name="zigbeeEndpoint"> the ZigBeeEndpoint this cluster is contained within </param>
        /// </summary>
        public ZclThermostatCluster(ZigBeeEndpoint zigbeeEndpoint)
            :base(zigbeeEndpoint, CLUSTER_ID, CLUSTER_NAME)
        {
        }

        /// <summary>
        /// The Setpoint Raise/Lower Command
        ///
        /// <param name="mode" <see cref="byte"> Mode</ param >
        /// <param name="amount" <see cref="sbyte"> Amount</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> SetpointRaiseLowerCommand(byte mode, sbyte amount)
        {
            SetpointRaiseLowerCommand command = new SetpointRaiseLowerCommand();

            // Set the fields
            command.Mode = mode;
            command.Amount = amount;

            return Send(command);
        }

        /// <summary>
        /// The Set Weekly Schedule
        ///
        /// The set weekly schedule command is used to update the thermostat weekly set point
        /// schedule from a management system. If the thermostat already has a weekly set point
        /// schedule programmed then it should replace each daily set point set as it receives
        /// the updates from the management system. For example if the thermostat has 4 set
        /// points for every day of the week and is sent a Set Weekly Schedule command with one set
        /// point for Saturday then the thermostat should remove all 4 set points for Saturday
        /// and replace those with the updated set point but leave all other days unchanged.
        /// <br> If the schedule is larger than what fits in one ZigBee frame or contains more
        /// than 10 transitions, the schedule shall then be sent using multipleSet Weekly
        /// Schedule Commands.
        ///
        /// <param name="numberOfTransitions" <see cref="byte"> Number Of Transitions</ param >
        /// <param name="dayOfWeek" <see cref="byte"> Day Of Week</ param >
        /// <param name="mode" <see cref="byte"> Mode</ param >
        /// <param name="transition" <see cref="ushort"> Transition</ param >
        /// <param name="heatSet" <see cref="short"> Heat Set</ param >
        /// <param name="coolSet" <see cref="short"> Cool Set</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> SetWeeklySchedule(byte numberOfTransitions, byte dayOfWeek, byte mode, ushort transition, short heatSet, short coolSet)
        {
            SetWeeklySchedule command = new SetWeeklySchedule();

            // Set the fields
            command.NumberOfTransitions = numberOfTransitions;
            command.DayOfWeek = dayOfWeek;
            command.Mode = mode;
            command.Transition = transition;
            command.HeatSet = heatSet;
            command.CoolSet = coolSet;

            return Send(command);
        }

        /// <summary>
        /// The Get Weekly Schedule
        ///
        /// <param name="daysToReturn" <see cref="byte"> Days To Return</ param >
        /// <param name="modeToReturn" <see cref="byte"> Mode To Return</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> GetWeeklySchedule(byte daysToReturn, byte modeToReturn)
        {
            GetWeeklySchedule command = new GetWeeklySchedule();

            // Set the fields
            command.DaysToReturn = daysToReturn;
            command.ModeToReturn = modeToReturn;

            return Send(command);
        }

        /// <summary>
        /// The Clear Weekly Schedule
        ///
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> ClearWeeklySchedule()
        {
            return Send(new ClearWeeklySchedule());
        }

        /// <summary>
        /// The Get Relay Status Log
        ///
        /// The Get Relay Status Log command is used to query the thermostat internal relay
        /// status log. This command has no payload. <br> The log storing order is First in First
        /// Out (FIFO) when the log is generated and stored into the Queue. <br> The first record
        /// in the log (i.e., the oldest) one, is the first to be replaced when there is a new
        /// record and there is no more space in the log. Thus, the newest record will overwrite
        /// the oldest one if there is no space left. <br> The log storing order is Last In First
        /// Out (LIFO) when the log is being retrieved from the Queue by a client device. Once the
        /// "Get Relay Status Log Response" frame is sent by the Server, the "Unread Entries"
        /// attribute should be decremented to indicate the number of unread records that
        /// remain in the queue. <br> If the "Unread Entries"attribute reaches zero and the
        /// Client sends a new "Get Relay Status Log Request", the Server may send one of the
        /// following items as a response: <br> i) resend the last Get Relay Status Log Response
        /// or ii) generate new log record at the time of request and send Get Relay Status Log
        /// Response with the new data
        ///
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> GetRelayStatusLog()
        {
            return Send(new GetRelayStatusLog());
        }

        /// <summary>
        /// The Get Weekly Schedule Response
        ///
        /// <param name="numberOfTransitions" <see cref="byte"> Number Of Transitions</ param >
        /// <param name="dayOfWeek" <see cref="byte"> Day Of Week</ param >
        /// <param name="mode" <see cref="byte"> Mode</ param >
        /// <param name="transition" <see cref="ushort"> Transition</ param >
        /// <param name="heatSet" <see cref="short"> Heat Set</ param >
        /// <param name="coolSet" <see cref="short"> Cool Set</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> GetWeeklyScheduleResponse(byte numberOfTransitions, byte dayOfWeek, byte mode, ushort transition, short heatSet, short coolSet)
        {
            GetWeeklyScheduleResponse command = new GetWeeklyScheduleResponse();

            // Set the fields
            command.NumberOfTransitions = numberOfTransitions;
            command.DayOfWeek = dayOfWeek;
            command.Mode = mode;
            command.Transition = transition;
            command.HeatSet = heatSet;
            command.CoolSet = coolSet;

            return Send(command);
        }

        /// <summary>
        /// The Get Relay Status Log Response
        ///
        /// <param name="timeOfDay" <see cref="ushort"> Time Of Day</ param >
        /// <param name="relayStatus" <see cref="byte"> Relay Status</ param >
        /// <param name="localTemperature" <see cref="short"> Local Temperature</ param >
        /// <param name="humidity" <see cref="byte"> Humidity</ param >
        /// <param name="setpoint" <see cref="short"> Setpoint</ param >
        /// <param name="unreadEntries" <see cref="ushort"> Unread Entries</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> GetRelayStatusLogResponse(ushort timeOfDay, byte relayStatus, short localTemperature, byte humidity, short setpoint, ushort unreadEntries)
        {
            GetRelayStatusLogResponse command = new GetRelayStatusLogResponse();

            // Set the fields
            command.TimeOfDay = timeOfDay;
            command.RelayStatus = relayStatus;
            command.LocalTemperature = localTemperature;
            command.Humidity = humidity;
            command.Setpoint = setpoint;
            command.UnreadEntries = unreadEntries;

            return Send(command);
        }
    }
}
