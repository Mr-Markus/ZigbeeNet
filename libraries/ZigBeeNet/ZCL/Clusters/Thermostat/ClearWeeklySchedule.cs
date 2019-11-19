using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.Security;
using ZigBeeNet.ZCL.Clusters.Thermostat;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Protocol;


namespace ZigBeeNet.ZCL.Clusters.Thermostat
{
    /// <summary>
    /// Clear Weekly Schedule value object class.
    ///
    /// Cluster: Thermostat. Command ID 0x03 is sent TO the server.
    /// This command is a specific command used for the Thermostat cluster.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class ClearWeeklySchedule : ZclCommand
    {
        /// <summary>
        /// The cluster ID to which this command belongs.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0201;

        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x03;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ClearWeeklySchedule()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
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
