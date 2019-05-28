// License text here
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.ZCL.Protocol;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Clusters.Thermostat;


namespace ZigBeeNet.ZCL.Clusters.Thermostat
{
    /// <summary>
    /// Clear Weekly Schedule value object class.
    /// <para>
    /// Cluster: Thermostat. Command is sent TO the server.
    /// This command is a specific command used for the Thermostat cluster.
    /// </para>
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class ClearWeeklySchedule : ZclCommand
    {

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ClearWeeklySchedule()
        {
            GenericCommand = false;
            ClusterId = 513;
            CommandId = 3;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("ClearWeeklySchedule [");
            builder.Append(base.ToString());
            builder.Append(']');

            return builder.ToString();
        }
    }
}
