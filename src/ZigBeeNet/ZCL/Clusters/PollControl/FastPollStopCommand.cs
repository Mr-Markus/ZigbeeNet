// License text here
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.ZCL.Protocol;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Clusters.PollControl;


namespace ZigBeeNet.ZCL.Clusters.PollControl
{
    /// <summary>
    /// Fast Poll Stop Command value object class.
    /// <para>
    /// Cluster: Poll Control. Command is sent TO the server.
    /// This command is a specific command used for the Poll Control cluster.
    ///
    /// The Fast Poll Stop command is used to stop the fast poll mode initiated by the Check-in response. The Fast Poll Stop command has no payload.
    /// <br>
    /// If the Poll Control Server receives a Fast Poll Stop from an unbound client it SHOULD send back a DefaultResponse with a value field
    /// indicating “ACTION_DENIED” . The Server SHALL respond with a DefaultResponse not equal to ZCL_SUCCESS.
    /// <br>
    /// If the Poll Control Server receives a Fast Poll Stop command from a bound client but it is unable to stop fast polling due to the fact that there
    /// is another bound client which has requested that polling continue it SHOULD respond with a Default Response with a status of
    /// “ACTION_DENIED”
    /// <br>
    /// If a Poll Control Server receives a Fast Poll Stop command from a bound client but it is not FastPolling it SHOULD respond with a Default
    /// Response with a status of ACTION_DENIED.
    /// </para>
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class FastPollStopCommand : ZclCommand
    {

        /// <summary>
        /// Default constructor.
        /// </summary>
        public FastPollStopCommand()
        {
            GenericCommand = false;
            ClusterId = 32;
            CommandId = 1;
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
