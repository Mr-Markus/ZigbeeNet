
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ZigBeeNet.Security;
using ZigBeeNet.ZCL.Clusters.PollControl;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Protocol;


namespace ZigBeeNet.ZCL.Clusters
{
    /// <summary>
    /// Poll Control cluster implementation (Cluster ID 0x0020).
    ///
    /// This cluster provides a mechanism for the management of an end device’s MAC Data Request
    /// rate. For the purposes of this cluster, the term “poll” always refers to the sending of a
    /// MAC Data Request from the end device to the end device’s parent. This cluster can be used
    /// for instance by a configuration device to make an end device responsive for a certain
    /// period of time so that the device can be managed by the controller. This cluster is
    /// composed of a client and server. The end device implements the server side of this
    /// cluster. The server side contains several attributes related to the MAC Data Request
    /// rate for the device. The client side implements commands used to manage the poll rate for
    /// the device. The end device which implements the server side of this cluster sends a query
    /// to the client on a predetermined interval to see if the client would like to manage the
    /// poll period of the end device in question. When the client side of the cluster hears from
    /// the server it has the opportunity to respond with configuration data to either put the
    /// end device in a short poll mode or let the end device continue to function normally.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class ZclPollControlCluster : ZclCluster
    {
        /// <summary>
        /// The ZigBee Cluster Library Cluster ID
        /// </summary>
        public const ushort CLUSTER_ID = 0x0020;

        /// <summary>
        /// The ZigBee Cluster Library Cluster Name
        /// </summary>
        public const string CLUSTER_NAME = "Poll Control";

        // Attribute constants

        /// <summary>
        /// The Poll Control server is responsible for checking in with the poll control client
        /// periodically to see if the poll control client wants to modify the poll rate of the
        /// poll control server. This is due to the fact that the PollControl server is
        /// implemented on an end device that may have an unpredictable sleep-wake cycle. The
        /// CheckinInterval represents the default amount of time between check-ins by the
        /// poll control server with the poll control client. The CheckinInterval is measured
        /// in quarter-seconds. A value of 0 indicates that the Poll Control Server is turned
        /// off and the poll control server will not check-in with the poll control client. The
        /// Poll Control Server checks in with the Poll Control Client by sending a Checkin
        /// command to the Client. This value SHOULDbe longer than the LongPoll Interval
        /// attribute. If the Client writes an invalid attribute value (Example: Out of Range
        /// or a value smaller than the optional Check-inIntervalMinattribute value or a
        /// value smaller than the LongPollInterval attribute value), the Server should
        /// return Write Attributes Response with an error status not equal to ZCL_SUCCESS.
        /// The Poll Control Client will hold onto the actions or messages for the Poll Control
        /// Server at the application level until the Poll Control Server checks in with the
        /// Poll Control Client.
        /// </summary>
        public const ushort ATTR_CHECKININTERVAL = 0x0000;

        /// <summary>
        /// An end device that implements the Poll Control server may optionally expose a
        /// LongPollInterval attribute. The Long Poll Interval represents the maximum
        /// amount of time in quarter-seconds between MAC Data Requests from the end device to
        /// its parent.
        /// The LongPollInterval defines the frequency of polling that an end device does when
        /// it is NOT in fast poll mode. The LongPollInterval should be longer than the
        /// ShortPollInterval attribute but shorter than the CheckinInterval attribute.A
        /// value of 0xffffffff is reserved to indicate that the device does not have or does not
        /// know its long poll interval
        /// </summary>
        public const ushort ATTR_LONGPOLLINTERVAL = 0x0001;

        /// <summary>
        /// An end device that implements the Poll Control server may optionally expose the
        /// ShortPollInterval attribute. The ShortPollIntervalrepresents the number of
        /// quarterseconds that an end device waits between MAC Data Requests to its parent
        /// when it is expecting data (i.e.,in fast poll mode).
        /// </summary>
        public const ushort ATTR_SHORTPOLLINTERVAL = 0x0002;

        /// <summary>
        /// The FastPollTimeout attribute represents the number of quarterseconds that an
        /// end device will stay in fast poll mode by default. It is suggested that the
        /// FastPollTimeoutattribute value be greater than 7.68 seconds.The Poll Control
        /// Cluster Client MAYoverride this value by indicating a different value in the Fast
        /// Poll Duration argument in the Check-in Response command. If the Client writes a
        /// value out of range or greater than the optional FastPollTimeoutMax attribute
        /// value if supported, the Server should return a Write Attributes Response with a
        /// status of INVALID_VALUE30. An end device that implements the Poll Control server
        /// can be put into a fast poll mode during which it will send MAC Data Requests to its
        /// parent at the frequency of its configured ShortPollInterval attribute. During
        /// this period of time, fast polling is considered active. When the device goes into
        /// fast poll mode, it is required to send MAC DataRequests to its parent at an
        /// accelerated rate and is thus more responsive on the network and can receive data
        /// asynchronously from the device implementing the Poll Control Cluster Client.
        /// </summary>
        public const ushort ATTR_FASTPOLLTIMEOUT = 0x0003;

        /// <summary>
        /// The Poll Control Server may optionally provide its own minimum value for the
        /// Check-inInterval to protect against the Check-inInterval being set too low and
        /// draining the battery on the end device implementing the Poll Control Server.
        /// </summary>
        public const ushort ATTR_CHECKININTERVALMIN = 0x0004;

        /// <summary>
        /// The Poll Control Server MAYoptionally provide its own minimum value for the
        /// LongPollIntervalto protect against another device setting the value to too short
        /// a time resulting in an inadvertent power drain on the device.
        /// </summary>
        public const ushort ATTR_LONGPOLLINTERVALMIN = 0x0005;

        /// <summary>
        /// The Poll Control Server may optionally provide its own maximum value for the
        /// FastPollTimeout to avoid it being set to too high a value resulting in an
        /// inadvertent power drain on the device.
        /// </summary>
        public const ushort ATTR_FASTPOLLTIMEOUTMIN = 0x0006;

        protected override Dictionary<ushort, ZclAttribute> InitializeClientAttributes()
        {
            Dictionary<ushort, ZclAttribute> attributeMap = new Dictionary<ushort, ZclAttribute>(0);

            return attributeMap;
        }

        protected override Dictionary<ushort, ZclAttribute> InitializeServerAttributes()
        {
            Dictionary<ushort, ZclAttribute> attributeMap = new Dictionary<ushort, ZclAttribute>(7);

            attributeMap.Add(ATTR_CHECKININTERVAL, new ZclAttribute(this, ATTR_CHECKININTERVAL, "Checkin Interval", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), true, true, true, true));
            attributeMap.Add(ATTR_LONGPOLLINTERVAL, new ZclAttribute(this, ATTR_LONGPOLLINTERVAL, "Long Poll Interval", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), true, true, false, true));
            attributeMap.Add(ATTR_SHORTPOLLINTERVAL, new ZclAttribute(this, ATTR_SHORTPOLLINTERVAL, "Short Poll Interval", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, true));
            attributeMap.Add(ATTR_FASTPOLLTIMEOUT, new ZclAttribute(this, ATTR_FASTPOLLTIMEOUT, "Fast Poll Timeout", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, true));
            attributeMap.Add(ATTR_CHECKININTERVALMIN, new ZclAttribute(this, ATTR_CHECKININTERVALMIN, "Checkin Interval Min", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_LONGPOLLINTERVALMIN, new ZclAttribute(this, ATTR_LONGPOLLINTERVALMIN, "Long Poll Interval Min", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_FASTPOLLTIMEOUTMIN, new ZclAttribute(this, ATTR_FASTPOLLTIMEOUTMIN, "Fast Poll Timeout Min", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), true, true, false, false));

            return attributeMap;
        }

        protected override Dictionary<ushort, Func<ZclCommand>> InitializeServerCommands()
        {
            Dictionary<ushort, Func<ZclCommand>> commandMap = new Dictionary<ushort, Func<ZclCommand>>(1);

            commandMap.Add(0x0000, () => new CheckInCommand());

            return commandMap;
        }

        protected override Dictionary<ushort, Func<ZclCommand>> InitializeClientCommands()
        {
            Dictionary<ushort, Func<ZclCommand>> commandMap = new Dictionary<ushort, Func<ZclCommand>>(4);

            commandMap.Add(0x0000, () => new CheckInResponse());
            commandMap.Add(0x0001, () => new FastPollStopCommand());
            commandMap.Add(0x0002, () => new SetLongPollIntervalCommand());
            commandMap.Add(0x0003, () => new SetShortPollIntervalCommand());

            return commandMap;
        }

        /// <summary>
        /// Default constructor to create a Poll Control cluster.
        ///
        /// <param name="zigbeeEndpoint"> the ZigBeeEndpoint this cluster is contained within </param>
        /// </summary>
        public ZclPollControlCluster(ZigBeeEndpoint zigbeeEndpoint)
            :base(zigbeeEndpoint, CLUSTER_ID, CLUSTER_NAME)
        {
        }

        /// <summary>
        /// The Check In Response
        ///
        /// The Check-in Response is sent in response to the receipt of a Check-in command. The
        /// Check-in Response is used by the Poll Control Client to indicate whether it would
        /// like the device implementing the Poll Control Cluster Server to go into a fast poll
        /// mode and for how long. If the Poll Control Cluster Client indicates that it would
        /// like the device to go into a fast poll mode, it is responsible for telling the device
        /// to stop fast polling when it is done sending messages to the fast polling device.
        /// <br> If the Poll Control Server receives a Check-In Response from a client for which
        /// there is no binding (unbound), it should respond with a Default Response with a
        /// status value indicating ACTION_DENIED. <br> If the Poll Control Server receives a
        /// Check-In Response from a client for which there is a binding (bound) with an invalid
        /// fast poll interval it should respond with a Default Response with status
        /// INVALID_VALUE. <br> If the Poll Control Server receives a Check-In Response from a
        /// bound client after temporary fast poll mode is completed it should respond with a
        /// Default Response with a status value indicating TIMEOUT. <br> In all of the above
        /// cases, the Server shall respond with a Default Response not equal to ZCL_SUCCESS.
        ///
        /// <param name="startFastPolling" <see cref="bool"> Start Fast Polling</ param >
        /// <param name="fastPollTimeout" <see cref="ushort"> Fast Poll Timeout</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> CheckInResponse(bool startFastPolling, ushort fastPollTimeout)
        {
            CheckInResponse command = new CheckInResponse();

            // Set the fields
            command.StartFastPolling = startFastPolling;
            command.FastPollTimeout = fastPollTimeout;

            return Send(command);
        }

        /// <summary>
        /// The Fast Poll Stop Command
        ///
        /// The Fast Poll Stop command is used to stop the fast poll mode initiated by the
        /// Check-in response. The Fast Poll Stop command has no payload. <br> If the Poll
        /// Control Server receives a Fast Poll Stop from an unbound client it should send back a
        /// DefaultResponse with a value field indicating “ACTION_DENIED” . The Server shall
        /// respond with a DefaultResponse not equal to ZCL_SUCCESS. <br> If the Poll Control
        /// Server receives a Fast Poll Stop command from a bound client but it is unable to stop
        /// fast polling due to the fact that there is another bound client which has requested
        /// that polling continue it should respond with a Default Response with a status of
        /// “ACTION_DENIED” <br> If a Poll Control Server receives a Fast Poll Stop command
        /// from a bound client but it is not FastPolling it should respond with a Default
        /// Response with a status of ACTION_DENIED.
        ///
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> FastPollStopCommand()
        {
            return Send(new FastPollStopCommand());
        }

        /// <summary>
        /// The Set Long Poll Interval Command
        ///
        /// The Set Long Poll Interval command is used to set the Read Only LongPollInterval
        /// attribute. <br> When the Poll Control Server receives the Set Long Poll Interval
        /// Command, it should check its internal minimal limit and the attributes
        /// relationship if the new Long Poll Interval is acceptable. If the new value is
        /// acceptable, the new value shall be saved to the LongPollInterval attribute. If the
        /// new value is not acceptable, the Poll Control Server shall send a default response
        /// of INVALID_VALUE and the LongPollInterval attribute value is not updated.
        ///
        /// <param name="newLongPollInterval" <see cref="ushort"> New Long Poll Interval</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> SetLongPollIntervalCommand(ushort newLongPollInterval)
        {
            SetLongPollIntervalCommand command = new SetLongPollIntervalCommand();

            // Set the fields
            command.NewLongPollInterval = newLongPollInterval;

            return Send(command);
        }

        /// <summary>
        /// The Set Short Poll Interval Command
        ///
        /// The Set Short Poll Interval command is used to set the Read Only ShortPollInterval
        /// attribute. <br> When the Poll Control Server receives the Set Short Poll Interval
        /// Command, it should check its internal minimal limit and the attributes
        /// relationship if the new Short Poll Interval is acceptable. If the new value is
        /// acceptable, the new value shall be saved to the ShortPollInterval attribute. If
        /// the new value is not acceptable, the Poll Control Server shall send a default
        /// response of INVALID_VALUE and the ShortPollInterval attribute value is not
        /// updated.
        ///
        /// <param name="newShortPollInterval" <see cref="ushort"> New Short Poll Interval</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> SetShortPollIntervalCommand(ushort newShortPollInterval)
        {
            SetShortPollIntervalCommand command = new SetShortPollIntervalCommand();

            // Set the fields
            command.NewShortPollInterval = newShortPollInterval;

            return Send(command);
        }

        /// <summary>
        /// The Check In Command
        ///
        /// The Poll Control Cluster server sends out a Check-in command to the devices to which
        /// it is paired based on the server’s Check-inInterval attribute. It does this to find
        /// out if any of the Poll Control Cluster Clients with which it is paired are interested
        /// in having it enter fast poll mode so that it can be managed. This request is sent out
        /// based on either the Check-inInterval, or the next Check-in value in the Fast Poll
        /// Stop Request generated by the Poll Control Cluster Client. <br> The Check-in
        /// command expects a Check-in Response command to be sent back from the Poll Control
        /// Client. If the Poll Control Server does not receive a Check-in response back from
        /// the Poll Control Client up to 7.68 seconds it is free to return to polling according
        /// to the LongPollInterval.
        ///
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> CheckInCommand()
        {
            return Send(new CheckInCommand());
        }
    }
}
