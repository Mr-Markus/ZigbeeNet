// License text here
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.ZCL.Protocol;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Clusters.Alarms;


namespace ZigBeeNet.ZCL.Clusters.Alarms
{
    /// <summary>
    /// Reset Alarm Log Command value object class.
    /// <para>
    /// Cluster: Alarms. Command is sent TO the server.
    /// This command is a specific command used for the Alarms cluster.
    /// </para>
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class ResetAlarmLogCommand : ZclCommand
    {

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ResetAlarmLogCommand()
        {
            GenericCommand = false;
            ClusterId = 9;
            CommandId = 3;
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
