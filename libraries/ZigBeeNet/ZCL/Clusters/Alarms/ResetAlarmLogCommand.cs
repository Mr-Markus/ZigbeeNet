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
    /// Reset Alarm Log Command value object class.
    ///
    /// Cluster: Alarms. Command ID 0x03 is sent TO the server.
    /// This command is a specific command used for the Alarms cluster.
    ///
    /// This command causes the alarm table to be cleared.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class ResetAlarmLogCommand : ZclCommand
    {
        /// <summary>
        /// The cluster ID to which this command belongs.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0009;

        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x03;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ResetAlarmLogCommand()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("ResetAlarmLogCommand [");
            builder.Append(base.ToString());
            builder.Append(']');

            return builder.ToString();
        }
    }
}
