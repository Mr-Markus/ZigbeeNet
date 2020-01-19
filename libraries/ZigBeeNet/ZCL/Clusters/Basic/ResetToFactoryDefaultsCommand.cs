using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.Security;
using ZigBeeNet.ZCL.Clusters.Basic;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Protocol;


namespace ZigBeeNet.ZCL.Clusters.Basic
{
    /// <summary>
    /// Reset To Factory Defaults Command value object class.
    ///
    /// Cluster: Basic. Command ID 0x00 is sent TO the server.
    /// This command is a specific command used for the Basic cluster.
    ///
    /// On receipt of this command, the device resets all the attributes of all its clusters to
    /// their factory defaults. Note that ZigBee networking functionality,bindings, groups
    /// or other persistent data are not affected by this command
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class ResetToFactoryDefaultsCommand : ZclCommand
    {
        /// <summary>
        /// The cluster ID to which this command belongs.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0000;

        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x00;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ResetToFactoryDefaultsCommand()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
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
