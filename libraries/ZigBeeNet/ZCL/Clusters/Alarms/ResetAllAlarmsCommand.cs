using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.Security;
using ZigBeeNet.ZCL.Clusters.Alarms;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Protocol;


namespace ZigBeeNet.ZCL.Clusters.Alarms
{
    /// <summary>
    /// Reset All Alarms Command value object class.
    ///
    /// Cluster: Alarms. Command ID 0x01 is sent TO the server.
    /// This command is a specific command used for the Alarms cluster.
    ///
    /// This command resets all alarms. Any alarm conditions that were in fact still active will
    /// cause a new notification to be generated and, where implemented, a new record added to
    /// the alarm log.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class ResetAllAlarmsCommand : ZclCommand
    {
        /// <summary>
        /// The cluster ID to which this command belongs.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0009;

        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x01;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ResetAllAlarmsCommand()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("ResetAllAlarmsCommand [");
            builder.Append(base.ToString());
            builder.Append(']');

            return builder.ToString();
        }
    }
}
