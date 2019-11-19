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
    /// Check In Response value object class.
    ///
    /// Cluster: Poll Control. Command ID 0x00 is sent TO the server.
    /// This command is a specific command used for the Poll Control cluster.
    ///
    /// The Check-in Response is sent in response to the receipt of a Check-in command. The
    /// Check-in Response is used by the Poll Control Client to indicate whether it would like
    /// the device implementing the Poll Control Cluster Server to go into a fast poll mode and
    /// for how long. If the Poll Control Cluster Client indicates that it would like the device
    /// to go into a fast poll mode, it is responsible for telling the device to stop fast polling
    /// when it is done sending messages to the fast polling device. <br> If the Poll Control
    /// Server receives a Check-In Response from a client for which there is no binding
    /// (unbound), it should respond with a Default Response with a status value indicating
    /// ACTION_DENIED. <br> If the Poll Control Server receives a Check-In Response from a
    /// client for which there is a binding (bound) with an invalid fast poll interval it should
    /// respond with a Default Response with status INVALID_VALUE. <br> If the Poll Control
    /// Server receives a Check-In Response from a bound client after temporary fast poll mode
    /// is completed it should respond with a Default Response with a status value indicating
    /// TIMEOUT. <br> In all of the above cases, the Server shall respond with a Default Response
    /// not equal to ZCL_SUCCESS.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class CheckInResponse : ZclCommand
    {
        /// <summary>
        /// The cluster ID to which this command belongs.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0020;

        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x00;

        /// <summary>
        /// Start Fast Polling command message field.
        /// 
        /// This Boolean value indicates whether or not the Poll Control Server device should
        /// begin fast polling or not. If the Start Fast Polling value is true, the server device
        /// is EXPECTED to begin fast polling until the Fast Poll Timeout has expired. If the
        /// Start Fast Polling argument is false, the Poll Control Server may continue in
        /// normal operation and is not required to go into fast poll mode.
        /// </summary>
        public bool StartFastPolling { get; set; }

        /// <summary>
        /// Fast Poll Timeout command message field.
        /// 
        /// The Fast Poll Timeout value indicates the number of quarterseconds during which
        /// the device should continue fast polling. If the Fast Poll Timeout value is 0, the
        /// device is EXPECTED to continue fast polling until the amount of time indicated it
        /// the FastPollTimeout attribute has elapsed or it receives a Fast Poll Stop command.
        /// If the Start Fast Polling argument is false, the Poll Control Server may ignore the
        /// Fast Poll Timeout argument.
        /// The Fast Poll Timeout argument temporarily overrides the FastPollTimeout
        /// attribute on the Poll Control Cluster Server for the fast poll mode induced by the
        /// Check-in Response command. This value is not EXPECTED to overwrite the stored
        /// value in the FastPollTimeout attribute.
        /// If the FastPollTimeout parameter in the CheckInResponse command is greater than
        /// the FastPollTimeoutMax attribute value, the Server Device shall respond with a
        /// default response of error status not equal to ZCL_SUCCESS. It is suggested to use
        /// the Error Status of ZCL_INVALID_FIELD.
        /// </summary>
        public ushort FastPollTimeout { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public CheckInResponse()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
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
