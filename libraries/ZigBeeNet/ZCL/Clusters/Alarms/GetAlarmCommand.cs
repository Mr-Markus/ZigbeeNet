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
    /// Get Alarm Command value object class.
    /// <para>
    /// Cluster: Alarms. Command is sent TO the server.
    /// This command is a specific command used for the Alarms cluster.
    /// </para>
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class GetAlarmCommand : ZclCommand
    {

        /// <summary>
        /// Default constructor.
        /// </summary>
        public GetAlarmCommand()
        {
            GenericCommand = false;
            ClusterId = 9;
            CommandId = 2;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("GetAlarmCommand [");
            builder.Append(base.ToString());
            builder.Append(']');

            return builder.ToString();
        }
    }
}
