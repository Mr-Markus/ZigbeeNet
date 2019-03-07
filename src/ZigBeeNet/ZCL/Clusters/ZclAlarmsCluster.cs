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
using ZigBeeNet.ZCL.Clusters.Alarms;

<summary>
Alarmscluster implementation (Cluster ID 0x0009).
 
 * Attributes and commands for sending alarm notifications and configuring alarm * functionality. * <p> * Alarm conditions and their respective alarm codes are described in individual * clusters, along with an alarm mask field. Where not masked, alarm notifications * are reported to subscribed targets using binding. * <p> * Where an alarm table is implemented, all alarms, masked or otherwise, are * recorded and may be retrieved on demand. * <p> * Alarms may either reset automatically when the conditions that cause are no * longer active, or may need to be explicitly reset. 
  Code is auto-generated. Modifications may be overwritten!
 </summary>
namespace ZigBeeNet.ZCL.Clusters
{
   public class ZclAlarmsCluster : ZclCluster
   {
       <summary>
        The ZigBee Cluster Library Cluster ID
       </summary>
       public const ushort CLUSTER_ID = 0x0009;

       <summary>
        The ZigBee Cluster Library Cluster Name
       </summary>
       public const string CLUSTER_NAME = "Alarms";

       /* Attribute constants */
       <summary>
        * The AlarmCount attribute is 16-bits in length and specifies the number of entries        * currently in the alarm table. This attribute shall be specified in the range 0x00 to        * the maximum defined in the profile using this cluster.        * <p>        * If alarm logging is not implemented this attribute shall always take the value        * 0x00.       </summary>
       public const ushort ATTR_ALARMCOUNT = 0x0000;


       // Attribute initialisation
       protected override Dictionary<ushort, ZclAttribute> InitializeAttributes()
       {
           Dictionary<ushort, ZclAttribute> attributeMap = new Dictionary<ushort, ZclAttribute>(1);

           ZclClusterType alarms = ZclClusterType.GetValueById(ClusterType.ALARMS);

           attributeMap.Add(ATTR_ALARMCOUNT, new ZclAttribute(alarms, ATTR_ALARMCOUNT, "AlarmCount", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), false, true, false, false));

           return attributeMap;
       }

        Default constructor to create a Alarms cluster.
       
       <param name= zigbeeEndpoint the {@link ZigBeeEndpoint}
       </param>
       public ZclAlarmsCluster(ZigBeeEndpoint zigbeeEndpoint)
           : base(zigbeeEndpoint, CLUSTER_ID, CLUSTER_NAME)
       {
       }


       <summary>
       * Get the AlarmCount attribute [attribute ID0].
       
       * The AlarmCount attribute is 16-bits in length and specifies the number of entries       * currently in the alarm table. This attribute shall be specified in the range 0x00 to       * the maximum defined in the profile using this cluster.       * <p>       * If alarm logging is not implemented this attribute shall always take the value       * 0x00.       
        The attribute is of type ushort.
       
        The implementation of this attribute by a device is OPTIONAL
      </summary>
       <returns> the Task<CommandResult> command result Task</returns>
       
       public Task<CommandResult> GetAlarmCountAsync()
       {
           return Read(_attributes[ATTR_ALARMCOUNT]);
       }

       <summary>
       * Synchronously Get the AlarmCount attribute [attribute ID0].
       
       * The AlarmCount attribute is 16-bits in length and specifies the number of entries       * currently in the alarm table. This attribute shall be specified in the range 0x00 to       * the maximum defined in the profile using this cluster.       * <p>       * If alarm logging is not implemented this attribute shall always take the value       * 0x00.       
        The attribute is of type ushort.
       
        The implementation of this attribute by a device is OPTIONAL
      </summary>
       <returns> the Task<CommandResult> command result Task</returns>
       
       public ushort GetAlarmCount(long refreshPeriod)
       {
           if (_attributes[ATTR_ALARMCOUNT].IsLastValueCurrent(refreshPeriod))
           {
               return (ushort)_attributes[ATTR_ALARMCOUNT].LastValue;
           }

           return (ushort)ReadSync(_attributes[ATTR_ALARMCOUNT]);
       }


       <summary>
        The Reset Alarm Command
       </summary>
       <param name= alarmCode {@link byte} Alarm code</param>
       <param name= clusterIdentifier {@link ushort} Cluster identifier</param>
       <returns the Task<CommandResult> command result Task
       </returns>
       public Task<CommandResult> ResetAlarmCommand(byte alarmCode, ushort clusterIdentifier)
       {
           ResetAlarmCommand command = new ResetAlarmCommand();

           // Set the fields
           command.AlarmCode = alarmCode;
           command.ClusterIdentifier = clusterIdentifier;

           return Send(command);
       }

       <summary>
        The Reset All Alarms Command
       </summary>
       <returns the Task<CommandResult> command result Task
       </returns>
       public Task<CommandResult> ResetAllAlarmsCommand()
       {
           ResetAllAlarmsCommand command = new ResetAllAlarmsCommand();

           return Send(command);
       }

       <summary>
        The Get Alarm Command
       </summary>
       <returns the Task<CommandResult> command result Task
       </returns>
       public Task<CommandResult> GetAlarmCommand()
       {
           GetAlarmCommand command = new GetAlarmCommand();

           return Send(command);
       }

       <summary>
        The Reset Alarm Log Command
       </summary>
       <returns the Task<CommandResult> command result Task
       </returns>
       public Task<CommandResult> ResetAlarmLogCommand()
       {
           ResetAlarmLogCommand command = new ResetAlarmLogCommand();

           return Send(command);
       }

       <summary>
        The Alarm Command
       
       * The alarm command signals an alarm situation on the sending device.       * <br>       * An alarm command is generated when a  cluster  which has alarm functionality detects an alarm       * condition, e.g., an attribute has taken on a value that is outside a ‘safe’ range. The details       * are given by individual cluster specifications.       </summary>
       <param name= alarmCode {@link byte} Alarm code</param>
       <param name= clusterIdentifier {@link ushort} Cluster identifier</param>
       <returns the Task<CommandResult> command result Task
       </returns>
       public Task<CommandResult> AlarmCommand(byte alarmCode, ushort clusterIdentifier)
       {
           AlarmCommand command = new AlarmCommand();

           // Set the fields
           command.AlarmCode = alarmCode;
           command.ClusterIdentifier = clusterIdentifier;

           return Send(command);
       }

       <summary>
        The Get Alarm Response
       
       * If there is at least one alarm record in the alarm table then the status field is set to SUCCESS.       * The alarm code, cluster identifier and time stamp fields SHALL all be present and SHALL take their       * values from the item in the alarm table that they are reporting.If there  are  no more  alarms logged       * in the  alarm table  then the  status field is set  to NOT_FOUND  and the alarm code, cluster       * identifier and time stamp fields SHALL be omitted.       </summary>
       <param name= status {@link byte} Status</param>
       <param name= alarmCode {@link byte} Alarm code</param>
       <param name= clusterIdentifier {@link ushort} Cluster identifier</param>
       <param name= timestamp {@link uint} Timestamp</param>
       <returns the Task<CommandResult> command result Task
       </returns>
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

       public override ZclCommand GetCommandFromId(int commandId)
       {
           switch (commandId)
           {
               case 0: // RESET_ALARM_COMMAND
                   return new ResetAlarmCommand();
               case 1: // RESET_ALL_ALARMS_COMMAND
                   return new ResetAllAlarmsCommand();
               case 2: // GET_ALARM_COMMAND
                   return new GetAlarmCommand();
               case 3: // RESET_ALARM_LOG_COMMAND
                   return new ResetAlarmLogCommand();
                   default:
                       return null;
           }
       }

       public ZclCommand getResponseFromId(int commandId)
       {
           switch (commandId)
           {
               case 0: // ALARM_COMMAND
                   return new AlarmCommand();
               case 1: // GET_ALARM_RESPONSE
                   return new GetAlarmResponse();
                   default:
                       return null;
           }
       }
   }
}
