// License text here
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.ZCL.Protocol;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Clusters.LevelControl;


namespace ZigBeeNet.ZCL.Clusters.LevelControl
{
    /// <summary>
    /// Stop Command value object class.
    /// <para>
    /// Cluster: Level Control. Command is sent TO the server.
    /// This command is a specific command used for the Level Control cluster.
    /// </para>
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class StopCommand : ZclCommand
    {

        /// <summary>
        /// Default constructor.
        /// </summary>
        public StopCommand()
        {
            GenericCommand = false;
            ClusterId = 8;
            CommandId = 3;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("StopCommand [");
            builder.Append(base.ToString());
            builder.Append(']');

            return builder.ToString();
        }
    }
}
