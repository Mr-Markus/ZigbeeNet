using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.Security;
using ZigBeeNet.ZCL.Clusters.RssiLocation;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Protocol;


namespace ZigBeeNet.ZCL.Clusters.RssiLocation
{
    /// <summary>
    /// RSSI Request Command value object class.
    ///
    /// Cluster: RSSI Location. Command ID 0x05 is sent FROM the server.
    /// This command is a specific command used for the RSSI Location cluster.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class RssiRequestCommand : ZclCommand
    {
        /// <summary>
        /// The cluster ID to which this command belongs.
        /// </summary>
        public const ushort CLUSTER_ID = 0x000B;

        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x05;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public RssiRequestCommand()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
            CommandDirection = ZclCommandDirection.SERVER_TO_CLIENT;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("RssiRequestCommand [");
            builder.Append(base.ToString());
            builder.Append(']');

            return builder.ToString();
        }
    }
}
