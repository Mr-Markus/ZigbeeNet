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
    /// Get Alarm Command value object class.
    ///
    /// Cluster: Alarms. Command ID 0x02 is sent TO the server.
    /// This command is a specific command used for the Alarms cluster.
    ///
    /// This command causes the alarm with the earliest generated alarm entry in the alarm table
    /// to be reported in a get alarm response command. This command enables the reading of
    /// logged alarm conditions from the alarm table. Once an alarm condition has been reported
    /// the corresponding entry in the table is removed.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class GetAlarmCommand : ZclCommand
    {
        /// <summary>
        /// The cluster ID to which this command belongs.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0009;

        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x02;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public GetAlarmCommand()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
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
