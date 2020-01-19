using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.Security;
using ZigBeeNet.ZCL.Clusters.LevelControl;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Protocol;


namespace ZigBeeNet.ZCL.Clusters.LevelControl
{
    /// <summary>
    /// Stop Command value object class.
    ///
    /// Cluster: Level Control. Command ID 0x03 is sent TO the server.
    /// This command is a specific command used for the Level Control cluster.
    ///
    /// Upon receipt of this command, any Move to Level, Move or Step command (and their 'with
    /// On/Off' variants) currently in process shall be terminated. The value of CurrentLevel
    /// shall be left at its value upon receipt of the Stop command, and RemainingTime shall be
    /// set to zero.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class StopCommand : ZclCommand
    {
        /// <summary>
        /// The cluster ID to which this command belongs.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0008;

        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x03;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public StopCommand()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("StopCommand [");
            builder.Append(base.ToString());
            builder.Append(']');

            return builder.ToString();
        }
    }
}
