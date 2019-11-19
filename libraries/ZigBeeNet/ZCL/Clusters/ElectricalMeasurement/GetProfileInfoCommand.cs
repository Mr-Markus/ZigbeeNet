using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.Security;
using ZigBeeNet.ZCL.Clusters.ElectricalMeasurement;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Protocol;


namespace ZigBeeNet.ZCL.Clusters.ElectricalMeasurement
{
    /// <summary>
    /// Get Profile Info Command value object class.
    ///
    /// Cluster: Electrical Measurement. Command ID 0x00 is sent TO the server.
    /// This command is a specific command used for the Electrical Measurement cluster.
    ///
    /// Retrieves the power profiling information from the electrical measurement server.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class GetProfileInfoCommand : ZclCommand
    {
        /// <summary>
        /// The cluster ID to which this command belongs.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0B04;

        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x00;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public GetProfileInfoCommand()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("GetProfileInfoCommand [");
            builder.Append(base.ToString());
            builder.Append(']');

            return builder.ToString();
        }
    }
}
