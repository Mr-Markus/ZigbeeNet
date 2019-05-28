// License text here
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.ZCL.Protocol;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Clusters.Basic;


namespace ZigBeeNet.ZCL.Clusters.Basic
{
    /// <summary>
    /// Reset to Factory Defaults Command value object class.
    /// <para>
    /// Cluster: Basic. Command is sent TO the server.
    /// This command is a specific command used for the Basic cluster.
    ///
    /// On receipt of this command, the device resets all the attributes of all its clusters
    /// to their factory defaults. Note that ZigBee networking functionality,bindings, groups
    /// or other persistent data are not affected by this command
    /// </para>
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class ResetToFactoryDefaultsCommand : ZclCommand
    {

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ResetToFactoryDefaultsCommand()
        {
            GenericCommand = false;
            ClusterId = 0;
            CommandId = 0;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("ResetToFactoryDefaultsCommand [");
            builder.Append(base.ToString());
            builder.Append(']');

            return builder.ToString();
        }
    }
}
