
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ZigBeeNet.DAO;
using ZigBeeNet.Security;
using ZigBeeNet.ZCL.Clusters.DemandResponseAndLoadControl;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Protocol;


namespace ZigBeeNet.ZCL.Clusters
{
    /// <summary>
    /// Demand Response And Load Control cluster implementation (Cluster ID 0x0701).
    ///
    /// This cluster provides an interface to the functionality of Smart Energy Demand
    /// Response and Load Control. Devices targeted by this cluster include thermostats and
    /// devices that support load control.
    /// The ESI is defined as the Server due to its role in acting as the proxy for upstream demand
    /// response/load control management systems and subsequent data stores.
    /// A server device shall be capable of storing at least two load control events.
    /// Events carried using this cluster include a timestamp with the assumption that target
    /// devices maintain a real-time clock. If a device does not support a real-time clock, it is
    /// assumed the device will ignore all values within the Time field except the “Start Now”
    /// value.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class ZclDemandResponseAndLoadControlCluster : ZclCluster
    {
        /// <summary>
        /// The ZigBee Cluster Library Cluster ID
        /// </summary>
        public const ushort CLUSTER_ID = 0x0701;

        /// <summary>
        /// The ZigBee Cluster Library Cluster Name
        /// </summary>
        public const string CLUSTER_NAME = "Demand Response And Load Control";

        // Attribute constants

        /// <summary>
        /// The UtilityEnrollmentGroup provides a method for utilities to assign devices to
        /// groups. In other words, Utility defined groups provide a mechanism to arbitrarily
        /// group together different sets of load control or demand response devices for use as
        /// part of a larger utility program. The definition of the groups, implied usage, and
        /// their assigned values are dictated by the Utilities and subsequently used at their
        /// discretion, therefore outside the scope of this specification. The valid range
        /// for this attribute is 0x00 to 0xFF, where 0x00 (the default value) indicates the
        /// device is a member of all groups and values 0x01 to 0xFF indicates that the device is
        /// member of that specified group.
        /// </summary>
        public const ushort ATTR_UTILITYENROLLMENTGROUP = 0x0000;

        /// <summary>
        /// The StartRandomizedMinutes represents the maximum number of minutes to be used
        /// when randomizing the start of an event. As an example, if StartRandomizedMinutes
        /// is set for 3 minutes, the device could randomly select 2 minutes (but never greater
        /// than the 3 minutes) for this event, causing the start of the event to be delayed by two
        /// minutes. The valid range for this attribute is 0x00 to 0x3C where 0x00 indicates
        /// start event randomization is not performed.
        /// </summary>
        public const ushort ATTR_STARTRANDOMIZATIONMINUTES = 0x0001;

        /// <summary>
        /// The EndRandomizedMinutes represents the maximum number of minutes to be used when
        /// randomizing the end of an event. As an example, if EndRandomizedMinutes is set for 3
        /// minutes, the device could randomly select one minute (but never greater than 3
        /// minutes) for this event, causing the end of the event to be delayed by one minute. The
        /// valid range for this attribute is 0x00 to 0x3C where 0x00 indicates end event
        /// randomization is not performed.
        /// </summary>
        public const ushort ATTR_ENDRANDOMIZATIONMINUTES = 0x0002;

        /// <summary>
        /// The DeviceClassValue attribute identifies which bits the device will match in the
        /// Device Class fields. Please refer to Table D-2, “Device Class Field BitMap/
        /// Encoding” for further details. Although the attribute has a read/write access
        /// property, the device is permitted to refuse to change the DeviceClass by setting
        /// the status field of the corresponding write attribute status record to
        /// NOT_AUTHORIZED.
        /// Although, for backwards compatibility, the Type cannot be changed, this 16-bit
        /// Integer should be treated as if it were a 16-bit BitMap.
        /// Device Class and/or Utility Enrollment Group fields are to be used as filters for
        /// deciding to accept or ignore a Load Control Event or a Cancel Load Control Event
        /// command. There is no requirement for a device to store or remember the Device Class
        /// and/or Utility Enrollment Group once the decision to accept the event has been
        /// made. A consequence of this is that devices that accept multiple device classes may
        /// have an event created for one device class superseded by an event created for
        /// another device class.
        /// </summary>
        public const ushort ATTR_DEVICECLASSVALUE = 0x0003;

        protected override Dictionary<ushort, ZclAttribute> InitializeClientAttributes()
        {
            Dictionary<ushort, ZclAttribute> attributeMap = new Dictionary<ushort, ZclAttribute>(4);

            attributeMap.Add(ATTR_UTILITYENROLLMENTGROUP, new ZclAttribute(this, ATTR_UTILITYENROLLMENTGROUP, "Utility Enrollment Group", ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER), false, true, true, true));
            attributeMap.Add(ATTR_STARTRANDOMIZATIONMINUTES, new ZclAttribute(this, ATTR_STARTRANDOMIZATIONMINUTES, "Start Randomization Minutes", ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER), false, true, true, true));
            attributeMap.Add(ATTR_ENDRANDOMIZATIONMINUTES, new ZclAttribute(this, ATTR_ENDRANDOMIZATIONMINUTES, "End Randomization Minutes", ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER), false, true, true, true));
            attributeMap.Add(ATTR_DEVICECLASSVALUE, new ZclAttribute(this, ATTR_DEVICECLASSVALUE, "Device Class Value", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), false, true, true, true));

            return attributeMap;
        }

        protected override Dictionary<ushort, ZclAttribute> InitializeServerAttributes()
        {
            Dictionary<ushort, ZclAttribute> attributeMap = new Dictionary<ushort, ZclAttribute>(0);

            return attributeMap;
        }

        protected override Dictionary<ushort, Func<ZclCommand>> InitializeServerCommands()
        {
            Dictionary<ushort, Func<ZclCommand>> commandMap = new Dictionary<ushort, Func<ZclCommand>>(3);

            commandMap.Add(0x0000, () => new LoadControlEventCommand());
            commandMap.Add(0x0001, () => new CancelLoadControlEvent());
            commandMap.Add(0x0002, () => new CancelAllLoadControlEvents());

            return commandMap;
        }

        protected override Dictionary<ushort, Func<ZclCommand>> InitializeClientCommands()
        {
            Dictionary<ushort, Func<ZclCommand>> commandMap = new Dictionary<ushort, Func<ZclCommand>>(2);

            commandMap.Add(0x0000, () => new ReportEventStatus());
            commandMap.Add(0x0001, () => new GetScheduledEvents());

            return commandMap;
        }

        /// <summary>
        /// Default constructor to create a Demand Response And Load Control cluster.
        ///
        /// <param name="zigbeeEndpoint"> the ZigBeeEndpoint this cluster is contained within </param>
        /// </summary>
        public ZclDemandResponseAndLoadControlCluster(ZigBeeEndpoint zigbeeEndpoint)
            :base(zigbeeEndpoint, CLUSTER_ID, CLUSTER_NAME)
        {
        }

        /// <summary>
        /// The Report Event Status
        ///
        /// <param name="issuerEventId" <see cref="uint"> Issuer Event ID</ param >
        /// <param name="eventStatus" <see cref="byte"> Event Status</ param >
        /// <param name="eventStatusTime" <see cref="DateTime"> Event Status Time</ param >
        /// <param name="criticalityLevelApplied" <see cref="byte"> Criticality Level Applied</ param >
        /// <param name="coolingTemperatureSetPointApplied" <see cref="ushort"> Cooling Temperature Set Point Applied</ param >
        /// <param name="heatingTemperatureSetPointApplied" <see cref="ushort"> Heating Temperature Set Point Applied</ param >
        /// <param name="averageLoadAdjustmentPercentageApplied" <see cref="sbyte"> Average Load Adjustment Percentage Applied</ param >
        /// <param name="dutyCycleApplied" <see cref="byte"> Duty Cycle Applied</ param >
        /// <param name="eventControl" <see cref="byte"> Event Control</ param >
        /// <param name="signatureType" <see cref="byte"> Signature Type</ param >
        /// <param name="signature" <see cref="ByteArray"> Signature</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> ReportEventStatus(uint issuerEventId, byte eventStatus, DateTime eventStatusTime, byte criticalityLevelApplied, ushort coolingTemperatureSetPointApplied, ushort heatingTemperatureSetPointApplied, sbyte averageLoadAdjustmentPercentageApplied, byte dutyCycleApplied, byte eventControl, byte signatureType, ByteArray signature)
        {
            ReportEventStatus command = new ReportEventStatus();

            // Set the fields
            command.IssuerEventId = issuerEventId;
            command.EventStatus = eventStatus;
            command.EventStatusTime = eventStatusTime;
            command.CriticalityLevelApplied = criticalityLevelApplied;
            command.CoolingTemperatureSetPointApplied = coolingTemperatureSetPointApplied;
            command.HeatingTemperatureSetPointApplied = heatingTemperatureSetPointApplied;
            command.AverageLoadAdjustmentPercentageApplied = averageLoadAdjustmentPercentageApplied;
            command.DutyCycleApplied = dutyCycleApplied;
            command.EventControl = eventControl;
            command.SignatureType = signatureType;
            command.Signature = signature;

            return Send(command);
        }

        /// <summary>
        /// The Get Scheduled Events
        ///
        /// This command is used to request that all scheduled Load Control Events, starting at
        /// or after the supplied Start Time, are re-issued to the requesting device. When
        /// received by the Server, one or more Load Control Event commands will be sent
        /// covering both active and scheduled Load Control Events.
        ///
        /// <param name="startTime" <see cref="DateTime"> Start Time</ param >
        /// <param name="numberOfEvents" <see cref="byte"> Number Of Events</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> GetScheduledEvents(DateTime startTime, byte numberOfEvents)
        {
            GetScheduledEvents command = new GetScheduledEvents();

            // Set the fields
            command.StartTime = startTime;
            command.NumberOfEvents = numberOfEvents;

            return Send(command);
        }

        /// <summary>
        /// The Load Control Event Command
        ///
        /// <param name="issuerEventId" <see cref="uint"> Issuer Event ID</ param >
        /// <param name="deviceClass" <see cref="ushort"> Device Class</ param >
        /// <param name="utilityEnrollmentGroup" <see cref="byte"> Utility Enrollment Group</ param >
        /// <param name="startTime" <see cref="DateTime"> Start Time</ param >
        /// <param name="durationInMinutes" <see cref="ushort"> Duration In Minutes</ param >
        /// <param name="criticalityLevel" <see cref="byte"> Criticality Level</ param >
        /// <param name="coolingTemperatureOffset" <see cref="byte"> Cooling Temperature Offset</ param >
        /// <param name="heatingTemperatureOffset" <see cref="byte"> Heating Temperature Offset</ param >
        /// <param name="coolingTemperatureSetPoint" <see cref="short"> Cooling Temperature Set Point</ param >
        /// <param name="heatingTemperatureSetPoint" <see cref="short"> Heating Temperature Set Point</ param >
        /// <param name="averageLoadAdjustmentPercentage" <see cref="sbyte"> Average Load Adjustment Percentage</ param >
        /// <param name="dutyCycle" <see cref="byte"> Duty Cycle</ param >
        /// <param name="eventControl" <see cref="byte"> Event Control</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> LoadControlEventCommand(uint issuerEventId, ushort deviceClass, byte utilityEnrollmentGroup, DateTime startTime, ushort durationInMinutes, byte criticalityLevel, byte coolingTemperatureOffset, byte heatingTemperatureOffset, short coolingTemperatureSetPoint, short heatingTemperatureSetPoint, sbyte averageLoadAdjustmentPercentage, byte dutyCycle, byte eventControl)
        {
            LoadControlEventCommand command = new LoadControlEventCommand();

            // Set the fields
            command.IssuerEventId = issuerEventId;
            command.DeviceClass = deviceClass;
            command.UtilityEnrollmentGroup = utilityEnrollmentGroup;
            command.StartTime = startTime;
            command.DurationInMinutes = durationInMinutes;
            command.CriticalityLevel = criticalityLevel;
            command.CoolingTemperatureOffset = coolingTemperatureOffset;
            command.HeatingTemperatureOffset = heatingTemperatureOffset;
            command.CoolingTemperatureSetPoint = coolingTemperatureSetPoint;
            command.HeatingTemperatureSetPoint = heatingTemperatureSetPoint;
            command.AverageLoadAdjustmentPercentage = averageLoadAdjustmentPercentage;
            command.DutyCycle = dutyCycle;
            command.EventControl = eventControl;

            return Send(command);
        }

        /// <summary>
        /// The Cancel Load Control Event
        ///
        /// <param name="issuerEventId" <see cref="uint"> Issuer Event ID</ param >
        /// <param name="deviceClass" <see cref="ushort"> Device Class</ param >
        /// <param name="utilityEnrollmentGroup" <see cref="byte"> Utility Enrollment Group</ param >
        /// <param name="cancelControl" <see cref="byte"> Cancel Control</ param >
        /// <param name="effectiveTime" <see cref="DateTime"> Effective Time</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> CancelLoadControlEvent(uint issuerEventId, ushort deviceClass, byte utilityEnrollmentGroup, byte cancelControl, DateTime effectiveTime)
        {
            CancelLoadControlEvent command = new CancelLoadControlEvent();

            // Set the fields
            command.IssuerEventId = issuerEventId;
            command.DeviceClass = deviceClass;
            command.UtilityEnrollmentGroup = utilityEnrollmentGroup;
            command.CancelControl = cancelControl;
            command.EffectiveTime = effectiveTime;

            return Send(command);
        }

        /// <summary>
        /// The Cancel All Load Control Events
        ///
        /// <param name="cancelControl" <see cref="byte"> Cancel Control</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> CancelAllLoadControlEvents(byte cancelControl)
        {
            CancelAllLoadControlEvents command = new CancelAllLoadControlEvents();

            // Set the fields
            command.CancelControl = cancelControl;

            return Send(command);
        }
    }
}
