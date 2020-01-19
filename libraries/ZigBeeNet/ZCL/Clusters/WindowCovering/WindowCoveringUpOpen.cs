using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.Security;
using ZigBeeNet.ZCL.Clusters.WindowCovering;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Protocol;


namespace ZigBeeNet.ZCL.Clusters.WindowCovering
{
    /// <summary>
    /// Window Covering Up Open value object class.
    ///
    /// Cluster: Window Covering. Command ID 0x00 is sent TO the server.
    /// This command is a specific command used for the Window Covering cluster.
    ///
    /// Moves window covering to InstalledOpenLimit
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class WindowCoveringUpOpen : ZclCommand
    {
        /// <summary>
        /// The cluster ID to which this command belongs.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0102;

        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x00;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public WindowCoveringUpOpen()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("WindowCoveringUpOpen [");
            builder.Append(base.ToString());
            builder.Append(']');

            return builder.ToString();
        }
    }
}
