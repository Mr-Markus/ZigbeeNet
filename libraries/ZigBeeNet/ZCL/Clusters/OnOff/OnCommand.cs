using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.Security;
using ZigBeeNet.ZCL.Clusters.OnOff;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Protocol;


namespace ZigBeeNet.ZCL.Clusters.OnOff
{
    /// <summary>
    /// On Command value object class.
    ///
    /// Cluster: On/Off. Command ID 0x01 is sent TO the server.
    /// This command is a specific command used for the On/Off cluster.
    ///
    /// On receipt of this command, a device shall enter its ‘On’ state. This state is device
    /// dependent, but it is recommended that it is used for power on or similar functions. On
    /// receipt of the On command, if the value of the OnTime attribute is equal to 0x0000, the
    /// device shall set the OffWaitTime attribute to 0x0000.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class OnCommand : ZclCommand
    {
        /// <summary>
        /// The cluster ID to which this command belongs.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0006;

        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x01;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public OnCommand()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("OnCommand [");
            builder.Append(base.ToString());
            builder.Append(']');

            return builder.ToString();
        }
    }
}
