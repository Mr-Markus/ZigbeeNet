using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.Security;
using ZigBeeNet.ZCL.Clusters.Metering;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Protocol;


namespace ZigBeeNet.ZCL.Clusters.Metering
{
    /// <summary>
    /// Remove Mirror value object class.
    ///
    /// Cluster: Metering. Command ID 0x02 is sent FROM the server.
    /// This command is a specific command used for the Metering cluster.
    ///
    /// This command is used to request the ESI to remove its mirror of Metering Device data.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class RemoveMirror : ZclCommand
    {
        /// <summary>
        /// The cluster ID to which this command belongs.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0702;

        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x02;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public RemoveMirror()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
            CommandDirection = ZclCommandDirection.SERVER_TO_CLIENT;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("RemoveMirror [");
            builder.Append(base.ToString());
            builder.Append(']');

            return builder.ToString();
        }
    }
}
