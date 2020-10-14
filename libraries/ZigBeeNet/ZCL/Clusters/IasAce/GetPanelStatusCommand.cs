using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.Security;
using ZigBeeNet.ZCL.Clusters.IasAce;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Protocol;


namespace ZigBeeNet.ZCL.Clusters.IasAce
{
    /// <summary>
    /// Get Panel Status Command value object class.
    ///
    /// Cluster: IAS ACE. Command ID 0x07 is sent TO the server.
    /// This command is a specific command used for the IAS ACE cluster.
    ///
    /// This command is used by ACE clients to request an update to the status (e.g., security
    /// system arm state) of the ACE server (i.e., the IAS CIE). In particular, this command is
    /// useful for battery-powered ACE clients with polling rates longer than the ZigBee
    /// standard check-in rate. <br> On receipt of this command, the ACE server responds with
    /// the status of the security system. The IAS ACE server shall generate a Get Panel Status
    /// Response command.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class GetPanelStatusCommand : ZclCommand
    {
        /// <summary>
        /// The cluster ID to which this command belongs.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0501;

        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x07;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public GetPanelStatusCommand()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("GetPanelStatusCommand [");
            builder.Append(base.ToString());
            builder.Append(']');

            return builder.ToString();
        }
    }
}
