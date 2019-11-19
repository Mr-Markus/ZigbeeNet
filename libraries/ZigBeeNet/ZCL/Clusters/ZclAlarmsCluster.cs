
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ZigBeeNet.DAO;
using ZigBeeNet.Security;
using ZigBeeNet.ZCL.Clusters.Alarms;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Protocol;


namespace ZigBeeNet.ZCL.Clusters
{
    /// <summary>
    /// Alarms cluster implementation (Cluster ID 0x0009.
    ///
    /// Attributes and commands for sending alarm notifications and configuring alarm
    /// functionality.
    /// Alarm conditions and their respective alarm codes are described in individual
    /// clusters, along with an alarm mask field. Where not masked, alarm notifications are
    /// reported to subscribed targets using binding.
    /// Where an alarm table is implemented, all alarms, masked or otherwise, are recorded and
    /// may be retrieved on demand.
    /// Alarms may either reset automatically when the conditions that cause are no longer
    /// active, or may need to be explicitly reset.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class ZclAlarmsCluster : ZclCluster
    {
        /// <summary>
        /// The ZigBee Cluster Library Cluster ID
        /// </summary>
        public const ushort CLUSTER_ID = 0x0009;

        /// <summary>
        /// The ZigBee Cluster Library Cluster Name
        /// </summary>
        public const string CLUSTER_NAME = "Alarms";

        // Attribute constants

        /// <summary>
        /// The AlarmCount attribute is 16-bits in length and specifies the number of entries
        /// currently in the alarm table. This attribute shall be specified in the range 0x00 to
        /// the maximum defined in the profile using this cluster.
        /// If alarm logging is not implemented this attribute shall always take the value
        /// 0x00.
        /// </summary>
        public const ushort ATTR_ALARMCOUNT = 0x0000;

        protected override Dictionary<ushort, ZclAttribute> InitializeClientAttributes()
        {
            Dictionary<ushort, ZclAttribute> attributeMap = new Dictionary<ushort, ZclAttribute>(0);

            return attributeMap;
        }

        protected override Dictionary<ushort, ZclAttribute> InitializeServerAttributes()
        {
            Dictionary<ushort, ZclAttribute> attributeMap = new Dictionary<ushort, ZclAttribute>(1);

            attributeMap.Add(ATTR_ALARMCOUNT, new ZclAttribute(this, ATTR_ALARMCOUNT, "Alarm Count", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), false, true, false, false));

            return attributeMap;
        }

        protected override Dictionary<ushort, Func<ZclCommand>> InitializeServerCommands()
        {
            Dictionary<ushort, Func<ZclCommand>> commandMap = new Dictionary<ushort, Func<ZclCommand>>(2);

            commandMap.Add(0x0000, () => new AlarmCommand());
            commandMap.Add(0x0001, () => new GetAlarmResponse());

            return commandMap;
        }

        protected override Dictionary<ushort, Func<ZclCommand>> InitializeClientCommands()
        {
            Dictionary<ushort, Func<ZclCommand>> commandMap = new Dictionary<ushort, Func<ZclCommand>>(4);

            commandMap.Add(0x0000, () => new ResetAlarmCommand());
            commandMap.Add(0x0001, () => new ResetAllAlarmsCommand());
            commandMap.Add(0x0002, () => new GetAlarmCommand());
            commandMap.Add(0x0003, () => new ResetAlarmLogCommand());

            return commandMap;
        }

        /// <summary>
        /// Default constructor to create a Alarms cluster.
        ///
        /// <param name="zigbeeEndpoint"> the ZigBeeEndpoint this cluster is contained within </param>
        /// </summary>
        public ZclAlarmsCluster(ZigBeeEndpoint zigbeeEndpoint)
            :base(zigbeeEndpoint, CLUSTER_ID, CLUSTER_NAME)
        {
        }

        /// <summary>
        /// The Reset Alarm Command
        ///
        /// This command resets a specific alarm. This is needed for some alarms that do not
        /// reset automatically. If the alarm condition being reset was in fact still active
        /// then a new notification will be generated and, where implemented, a new record
        /// added to the alarm log.
        ///
        /// <param name="alarmCode" <see cref="byte"> Alarm Code</ param >
        /// <param name="clusterIdentifier" <see cref="ushort"> Cluster Identifier</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> ResetAlarmCommand(byte alarmCode, ushort clusterIdentifier)
        {
            ResetAlarmCommand command = new ResetAlarmCommand();

            // Set the fields
            command.AlarmCode = alarmCode;
            command.ClusterIdentifier = clusterIdentifier;

            return Send(command);
        }

        /// <summary>
        /// The Reset All Alarms Command
        ///
        /// This command resets all alarms. Any alarm conditions that were in fact still active
        /// will cause a new notification to be generated and, where implemented, a new record
        /// added to the alarm log.
        ///
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> ResetAllAlarmsCommand()
        {
            return Send(new ResetAllAlarmsCommand());
        }

        /// <summary>
        /// The Get Alarm Command
        ///
        /// This command causes the alarm with the earliest generated alarm entry in the alarm
        /// table to be reported in a get alarm response command. This command enables the
        /// reading of logged alarm conditions from the alarm table. Once an alarm condition
        /// has been reported the corresponding entry in the table is removed.
        ///
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> GetAlarmCommand()
        {
            return Send(new GetAlarmCommand());
        }

        /// <summary>
        /// The Reset Alarm Log Command
        ///
        /// This command causes the alarm table to be cleared.
        ///
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> ResetAlarmLogCommand()
        {
            return Send(new ResetAlarmLogCommand());
        }

        /// <summary>
        /// The Alarm Command
        ///
        /// The alarm command signals an alarm situation on the sending device. <br> An alarm
        /// command is generated when a cluster which has alarm functionality detects an alarm
        /// condition, e.g., an attribute has taken on a value that is outside a ‘safe’ range.
        /// The details are given by individual cluster specifications.
        ///
        /// <param name="alarmCode" <see cref="byte"> Alarm Code</ param >
        /// <param name="clusterIdentifier" <see cref="ushort"> Cluster Identifier</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> AlarmCommand(byte alarmCode, ushort clusterIdentifier)
        {
            AlarmCommand command = new AlarmCommand();

            // Set the fields
            command.AlarmCode = alarmCode;
            command.ClusterIdentifier = clusterIdentifier;

            return Send(command);
        }

        /// <summary>
        /// The Get Alarm Response
        ///
        /// If there is at least one alarm record in the alarm table then the status field is set to
        /// SUCCESS. The alarm code, cluster identifier and time stamp fields shall all be
        /// present and shall take their values from the item in the alarm table that they are
        /// reporting.If there are no more alarms logged in the alarm table then the status
        /// field is set to NOT_FOUND and the alarm code, cluster identifier and time stamp
        /// fields shall be omitted.
        ///
        /// <param name="status" <see cref="byte"> Status</ param >
        /// <param name="alarmCode" <see cref="byte"> Alarm Code</ param >
        /// <param name="clusterIdentifier" <see cref="ushort"> Cluster Identifier</ param >
        /// <param name="timestamp" <see cref="uint"> Timestamp</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> GetAlarmResponse(byte status, byte alarmCode, ushort clusterIdentifier, uint timestamp)
        {
            GetAlarmResponse command = new GetAlarmResponse();

            // Set the fields
            command.Status = status;
            command.AlarmCode = alarmCode;
            command.ClusterIdentifier = clusterIdentifier;
            command.Timestamp = timestamp;

            return Send(command);
        }
    }
}
