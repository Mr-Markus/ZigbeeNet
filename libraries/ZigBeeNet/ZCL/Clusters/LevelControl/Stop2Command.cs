using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.Security;
using ZigBeeNet.ZCL.Clusters.LevelControl;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Protocol;


namespace ZigBeeNet.ZCL.Clusters.LevelControl
{
    /// <summary>
    /// Stop 2 Command value object class.
    ///
    /// Cluster: Level Control. Command ID 0x07 is sent TO the server.
    /// This command is a specific command used for the Level Control cluster.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class Stop2Command : ZclCommand
    {
        /// <summary>
        /// The cluster ID to which this command belongs.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0008;

        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x07;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Stop2Command()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("Stop2Command [");
            builder.Append(base.ToString());
            builder.Append(']');

            return builder.ToString();
        }
    }
}
