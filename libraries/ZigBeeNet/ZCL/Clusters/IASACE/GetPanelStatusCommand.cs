// License text here
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.ZCL.Protocol;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Clusters.IASACE;


namespace ZigBeeNet.ZCL.Clusters.IASACE
{
    /// <summary>
    /// Get Panel Status Command value object class.
    /// <para>
    /// Cluster: IAS ACE. Command is sent TO the server.
    /// This command is a specific command used for the IAS ACE cluster.
    ///
    /// This command is used by ACE clients to request an update to the status (e.g., security
    /// system arm state) of the ACE server (i.e., the IAS CIE). In particular, this command is
    /// useful for battery-powered ACE clients with polling rates longer than the ZigBee standard
    /// check-in rate.
    /// <br>
    /// On receipt of this command, the ACE server responds with the status of the security system.
    /// The IAS ACE server SHALL generate a Get Panel Status Response command.
    /// </para>
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class GetPanelStatusCommand : ZclCommand
    {

        /// <summary>
        /// Default constructor.
        /// </summary>
        public GetPanelStatusCommand()
        {
            GenericCommand = false;
            ClusterId = 1281;
            CommandId = 7;
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
