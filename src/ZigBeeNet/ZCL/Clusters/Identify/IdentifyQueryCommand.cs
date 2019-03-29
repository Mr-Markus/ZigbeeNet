// License text here
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.ZCL.Protocol;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Clusters.Identify;


namespace ZigBeeNet.ZCL.Clusters.Identify
{
    /// <summary>
    /// Identify Query Command value object class.
    /// <para>
    /// Cluster: Identify. Command is sent TO the server.
    /// This command is a specific command used for the Identify cluster.
    /// </para>
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class IdentifyQueryCommand : ZclCommand
    {

        /// <summary>
        /// Default constructor.
        /// </summary>
        public IdentifyQueryCommand()
        {
            GenericCommand = false;
            ClusterId = 3;
            CommandId = 1;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("IdentifyQueryCommand [");
            builder.Append(base.ToString());
            builder.Append(']');

            return builder.ToString();
        }
    }
}
