using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.Security;
using ZigBeeNet.ZCL.Clusters.ColorControl;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Protocol;


namespace ZigBeeNet.ZCL.Clusters.ColorControl
{
    /// <summary>
    /// Stop Move Step Command value object class.
    ///
    /// Cluster: Color Control. Command ID 0x47 is sent TO the server.
    /// This command is a specific command used for the Color Control cluster.
    ///
    /// The Stop Move Step command is provided to allow Move to and Step commands to be stopped.
    /// (Note this automatically provides symmetry to the Level Control cluster.)
    /// Upon receipt of this command, any Move to, Move or Step command currently in process
    /// shall be ter- minated. The values of the CurrentHue, EnhancedCurrentHue and
    /// CurrentSaturation attributes shall be left at their present value upon receipt of the
    /// Stop Move Step command, and the RemainingTime attribute shall be set to zero.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class StopMoveStepCommand : ZclCommand
    {
        /// <summary>
        /// The cluster ID to which this command belongs.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0300;

        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x47;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public StopMoveStepCommand()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("StopMoveStepCommand [");
            builder.Append(base.ToString());
            builder.Append(']');

            return builder.ToString();
        }
    }
}
