using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.Security;
using ZigBeeNet.ZCL.Clusters.PollControl;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Protocol;


namespace ZigBeeNet.ZCL.Clusters.PollControl
{
    /// <summary>
    /// Fast Poll Stop Command value object class.
    ///
    /// Cluster: Poll Control. Command ID 0x01 is sent TO the server.
    /// This command is a specific command used for the Poll Control cluster.
    ///
    /// The Fast Poll Stop command is used to stop the fast poll mode initiated by the Check-in
    /// response. The Fast Poll Stop command has no payload. <br> If the Poll Control Server
    /// receives a Fast Poll Stop from an unbound client it should send back a DefaultResponse
    /// with a value field indicating “ACTION_DENIED” . The Server shall respond with a
    /// DefaultResponse not equal to ZCL_SUCCESS. <br> If the Poll Control Server receives a
    /// Fast Poll Stop command from a bound client but it is unable to stop fast polling due to the
    /// fact that there is another bound client which has requested that polling continue it
    /// should respond with a Default Response with a status of “ACTION_DENIED” <br> If a Poll
    /// Control Server receives a Fast Poll Stop command from a bound client but it is not
    /// FastPolling it should respond with a Default Response with a status of ACTION_DENIED.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class FastPollStopCommand : ZclCommand
    {
        /// <summary>
        /// The cluster ID to which this command belongs.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0020;

        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x01;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public FastPollStopCommand()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("FastPollStopCommand [");
            builder.Append(base.ToString());
            builder.Append(']');

            return builder.ToString();
        }
    }
}
