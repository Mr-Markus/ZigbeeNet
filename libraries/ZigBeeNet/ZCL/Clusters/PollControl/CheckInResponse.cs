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
    /// Check In Response value object class.
    /// <para>
    /// Cluster: Poll Control. Command is sent TO the server.
    /// This command is a specific command used for the Poll Control cluster.
    ///
    /// The Check-in Response is sent in response to the receipt of a Check-in command. The Check-in Response is used by the Poll Control Client to
    /// indicate whether it would like the device implementing the Poll Control Cluster Server to go into a fast poll mode and for how long. If the Poll
    /// Control Cluster Client indicates that it would like the device to go into a fast poll mode, it is responsible for telling the device to stop
    /// fast polling when it is done sending messages to the fast polling device.
    /// <br>
    /// If the Poll Control Server receives a Check-In Response from a client for which there is no binding (unbound), it SHOULD respond with a
    /// Default Response with a status value indicating ACTION_DENIED.
    /// <br>
    /// If the Poll Control Server receives a Check-In Response from a client for which there is a binding (bound) with an invalid fast poll interval
    /// it SHOULD respond with a Default Response with status INVALID_VALUE.
    /// <br>
    /// If the Poll Control Server receives a Check-In Response from a bound client after temporary fast poll mode is completed it SHOULD respond
    /// with a Default Response with a status value indicating TIMEOUT.
    /// <br>
    /// In all of the above cases, the Server SHALL respond with a Default Response not equal to ZCL_SUCCESS.
    /// </para>
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class CheckInResponse : ZclCommand
    {
        /// <summary>
        /// Start Fast Polling command message field.
        /// </summary>
        public bool StartFastPolling { get; set; }

        /// <summary>
        /// Fast Poll Timeout command message field.
        /// </summary>
        public ushort FastPollTimeout { get; set; }


        /// <summary>
        /// Default constructor.
        /// </summary>
        public CheckInResponse()
        {
            GenericCommand = false;
            ClusterId = 32;
            CommandId = 0;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(StartFastPolling, ZclDataType.Get(DataType.BOOLEAN));
            serializer.Serialize(FastPollTimeout, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            StartFastPolling = deserializer.Deserialize<bool>(ZclDataType.Get(DataType.BOOLEAN));
            FastPollTimeout = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("CheckInResponse [");
            builder.Append(base.ToString());
            builder.Append(", StartFastPolling=");
            builder.Append(StartFastPolling);
            builder.Append(", FastPollTimeout=");
            builder.Append(FastPollTimeout);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
