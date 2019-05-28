// License text here
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.ZCL.Protocol;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Clusters.Thermostat;


namespace ZigBeeNet.ZCL.Clusters.Thermostat
{
    /// <summary>
    /// Get Relay Status Log value object class.
    /// <para>
    /// Cluster: Thermostat. Command is sent TO the server.
    /// This command is a specific command used for the Thermostat cluster.
    ///
    /// The Get Relay Status Log command is used to query the thermostat internal relay status log. This command has no payload.
    /// <br>
    /// The log storing order is First in First Out (FIFO) when the log is generated and stored into the Queue.
    /// <br>
    /// The first record in the log (i.e., the oldest) one, is the first to be replaced when there is a new record and there is
    /// no more space in the log. Thus, the newest record will overwrite the oldest one if there is no space left.
    /// <br>
    /// The log storing order is Last In First Out (LIFO) when the log is being retrieved from the Queue by a client device.
    /// Once the "Get Relay Status Log Response" frame is sent by the Server, the "Unread Entries" attribute
    /// SHOULD be decremented to indicate the number of unread records that remain in the queue.
    /// <br>
    /// If the "Unread Entries"attribute reaches zero and the Client sends a new "Get Relay Status Log Request", the Server
    /// MAY send one of the following items as a response:
    /// <br>
    /// i) resend the last Get Relay Status Log Response
    /// or
    /// ii) generate new log record at the time of request and send Get Relay Status Log Response with the new data
    /// </para>
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class GetRelayStatusLog : ZclCommand
    {

        /// <summary>
        /// Default constructor.
        /// </summary>
        public GetRelayStatusLog()
        {
            GenericCommand = false;
            ClusterId = 513;
            CommandId = 4;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("GetRelayStatusLog [");
            builder.Append(base.ToString());
            builder.Append(']');

            return builder.ToString();
        }
    }
}
