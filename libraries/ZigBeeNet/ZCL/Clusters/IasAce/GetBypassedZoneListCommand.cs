using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.Security;
using ZigBeeNet.ZCL.Clusters.IASACE;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Protocol;


namespace ZigBeeNet.ZCL.Clusters.IASACE
{
    /// <summary>
    /// Get Bypassed Zone List Command value object class.
    ///
    /// Cluster: IAS ACE. Command ID 0x08 is sent TO the server.
    /// This command is a specific command used for the IAS ACE cluster.
    ///
    /// Provides IAS ACE clients with a way to retrieve the list of zones to be bypassed. This
    /// provides them with the ability to provide greater local functionality (i.e., at the IAS
    /// ACE client) for users to modify the Bypassed Zone List and reduce communications to the
    /// IAS ACE server when trying to arm the CIE security system.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class GetBypassedZoneListCommand : ZclCommand
    {
        /// <summary>
        /// The cluster ID to which this command belongs.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0501;

        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x08;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public GetBypassedZoneListCommand()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("GetBypassedZoneListCommand [");
            builder.Append(base.ToString());
            builder.Append(']');

            return builder.ToString();
        }
    }
}
