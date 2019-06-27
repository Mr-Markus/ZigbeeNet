// License text here
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.ZCL.Protocol;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Clusters.OnOff;


namespace ZigBeeNet.ZCL.Clusters.OnOff
{
    /// <summary>
    /// Toggle Command value object class.
    /// <para>
    /// Cluster: On/Off. Command is sent TO the server.
    /// This command is a specific command used for the On/Off cluster.
    /// </para>
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class ToggleCommand : ZclCommand
    {

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ToggleCommand()
        {
            GenericCommand = false;
            ClusterId = 6;
            CommandId = 2;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("ToggleCommand [");
            builder.Append(base.ToString());
            builder.Append(']');

            return builder.ToString();
        }
    }
}
