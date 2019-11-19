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
    /// Toggle Command value object class.
    ///
    /// Cluster: On/Off. Command ID 0x02 is sent TO the server.
    /// This command is a specific command used for the On/Off cluster.
    ///
    /// On receipt of this command, if a device is in its ‘Off’ state it shall enter its ‘On’ state.
    /// Otherwise, if it is in its ‘On’ state it shall enter its ‘Off’ state. On receipt of the
    /// Toggle command, if the value of the OnOff attribute is equal to 0x00 and if the value of the
    /// OnTime attribute is equal to 0x0000, the device shall set the OffWaitTime attribute to
    /// 0x0000. If the value of the OnOff attribute is equal to 0x01, the OnTime attribute shall
    /// be set to 0x0000.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class ToggleCommand : ZclCommand
    {
        /// <summary>
        /// The cluster ID to which this command belongs.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0006;

        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x02;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ToggleCommand()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("ToggleCommand [");
            builder.Append(base.ToString());
            builder.Append(']');

            return builder.ToString();
        }
    }
}
