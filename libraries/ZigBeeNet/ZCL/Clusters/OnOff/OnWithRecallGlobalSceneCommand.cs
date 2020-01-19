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
    /// On With Recall Global Scene Command value object class.
    ///
    /// Cluster: On/Off. Command ID 0x41 is sent TO the server.
    /// This command is a specific command used for the On/Off cluster.
    ///
    /// The On With Recall Global Scene command allows the recall of the settings when the device
    /// was turned off.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class OnWithRecallGlobalSceneCommand : ZclCommand
    {
        /// <summary>
        /// The cluster ID to which this command belongs.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0006;

        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x41;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public OnWithRecallGlobalSceneCommand()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("OnWithRecallGlobalSceneCommand [");
            builder.Append(base.ToString());
            builder.Append(']');

            return builder.ToString();
        }
    }
}
