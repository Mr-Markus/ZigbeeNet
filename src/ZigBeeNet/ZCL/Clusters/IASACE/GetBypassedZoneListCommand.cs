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
    /// Get Bypassed Zone List Command value object class.
    /// <para>
    /// Cluster: IAS ACE. Command is sent TO the server.
    /// This command is a specific command used for the IAS ACE cluster.
    ///
    /// Provides IAS ACE clients with a way to retrieve the list of zones to be bypassed. This provides them with the ability
    /// to provide greater local functionality (i.e., at the IAS ACE client) for users to modify the Bypassed Zone List and reduce
    /// communications to the IAS ACE server when trying to arm the CIE security system.
    /// </para>
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class GetBypassedZoneListCommand : ZclCommand
    {

        /// <summary>
        /// Default constructor.
        /// </summary>
        public GetBypassedZoneListCommand()
        {
            GenericCommand = false;
            ClusterId = 1281;
            CommandId = 8;
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
