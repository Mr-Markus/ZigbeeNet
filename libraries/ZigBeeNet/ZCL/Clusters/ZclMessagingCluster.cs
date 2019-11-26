
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ZigBeeNet.DAO;
using ZigBeeNet.Security;
using ZigBeeNet.ZCL.Clusters.Messaging;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Protocol;


namespace ZigBeeNet.ZCL.Clusters
{
    /// <summary>
    /// Messaging cluster implementation (Cluster ID 0x0703).
    ///
    /// This cluster provides an interface for passing text messages between ZigBee devices.
    /// Messages are expected to be delivered via the ESI and then unicast to all individually
    /// registered devices implementing the Messaging Cluster on the ZigBee network, or just
    /// made available to all devices for later pickup. Nested and overlapping messages are not
    /// allowed. The current active message will be replaced if a new message is received by the
    /// ESI.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class ZclMessagingCluster : ZclCluster
    {
        /// <summary>
        /// The ZigBee Cluster Library Cluster ID
        /// </summary>
        public const ushort CLUSTER_ID = 0x0703;

        /// <summary>
        /// The ZigBee Cluster Library Cluster Name
        /// </summary>
        public const string CLUSTER_NAME = "Messaging";

        protected override Dictionary<ushort, ZclAttribute> InitializeClientAttributes()
        {
            Dictionary<ushort, ZclAttribute> attributeMap = new Dictionary<ushort, ZclAttribute>(0);

            return attributeMap;
        }

        protected override Dictionary<ushort, ZclAttribute> InitializeServerAttributes()
        {
            Dictionary<ushort, ZclAttribute> attributeMap = new Dictionary<ushort, ZclAttribute>(0);

            return attributeMap;
        }

        protected override Dictionary<ushort, Func<ZclCommand>> InitializeServerCommands()
        {
            Dictionary<ushort, Func<ZclCommand>> commandMap = new Dictionary<ushort, Func<ZclCommand>>(4);

            commandMap.Add(0x0000, () => new GetLastMessage());
            commandMap.Add(0x0001, () => new MessageConfirmation());
            commandMap.Add(0x0002, () => new GetMessageCancellation());
            commandMap.Add(0x0003, () => new CancelAllMessages());

            return commandMap;
        }

        protected override Dictionary<ushort, Func<ZclCommand>> InitializeClientCommands()
        {
            Dictionary<ushort, Func<ZclCommand>> commandMap = new Dictionary<ushort, Func<ZclCommand>>(4);

            commandMap.Add(0x0000, () => new DisplayMessageCommand());
            commandMap.Add(0x0001, () => new CancelMessageCommand());
            commandMap.Add(0x0002, () => new DisplayProtectedMessageCommand());
            commandMap.Add(0x0003, () => new CancelAllMessagesCommand());

            return commandMap;
        }

        /// <summary>
        /// Default constructor to create a Messaging cluster.
        ///
        /// <param name="zigbeeEndpoint"> the ZigBeeEndpoint this cluster is contained within </param>
        /// </summary>
        public ZclMessagingCluster(ZigBeeEndpoint zigbeeEndpoint)
            :base(zigbeeEndpoint, CLUSTER_ID, CLUSTER_NAME)
        {
        }

        /// <summary>
        /// The Display Message Command
        ///
        /// <param name="messageId" <see cref="uint"> Message ID</ param >
        /// <param name="messageControl" <see cref="byte"> Message Control</ param >
        /// <param name="startTime" <see cref="DateTime"> Start Time</ param >
        /// <param name="durationInMinutes" <see cref="ushort"> Duration In Minutes</ param >
        /// <param name="message" <see cref="string"> Message</ param >
        /// <param name="extendedMessageControl" <see cref="byte"> Extended Message Control</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> DisplayMessageCommand(uint messageId, byte messageControl, DateTime startTime, ushort durationInMinutes, string message, byte extendedMessageControl)
        {
            DisplayMessageCommand command = new DisplayMessageCommand();

            // Set the fields
            command.MessageId = messageId;
            command.MessageControl = messageControl;
            command.StartTime = startTime;
            command.DurationInMinutes = durationInMinutes;
            command.Message = message;
            command.ExtendedMessageControl = extendedMessageControl;

            return Send(command);
        }

        /// <summary>
        /// The Cancel Message Command
        ///
        /// The Cancel Message command provides the ability to cancel the sending or
        /// acceptance of previously sent messages. When this message is received the
        /// recipient device has the option of clearing any display or user interfaces it
        /// supports, or has the option of logging the message for future reference.
        ///
        /// <param name="messageId" <see cref="uint"> Message ID</ param >
        /// <param name="messageControl" <see cref="byte"> Message Control</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> CancelMessageCommand(uint messageId, byte messageControl)
        {
            CancelMessageCommand command = new CancelMessageCommand();

            // Set the fields
            command.MessageId = messageId;
            command.MessageControl = messageControl;

            return Send(command);
        }

        /// <summary>
        /// The Display Protected Message Command
        ///
        /// The Display Protected Message command is for use with messages that are protected
        /// by a password or PIN
        ///
        /// <param name="messageId" <see cref="uint"> Message ID</ param >
        /// <param name="messageControl" <see cref="byte"> Message Control</ param >
        /// <param name="startTime" <see cref="DateTime"> Start Time</ param >
        /// <param name="durationInMinutes" <see cref="ushort"> Duration In Minutes</ param >
        /// <param name="message" <see cref="string"> Message</ param >
        /// <param name="extendedMessageControl" <see cref="byte"> Extended Message Control</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> DisplayProtectedMessageCommand(uint messageId, byte messageControl, DateTime startTime, ushort durationInMinutes, string message, byte extendedMessageControl)
        {
            DisplayProtectedMessageCommand command = new DisplayProtectedMessageCommand();

            // Set the fields
            command.MessageId = messageId;
            command.MessageControl = messageControl;
            command.StartTime = startTime;
            command.DurationInMinutes = durationInMinutes;
            command.Message = message;
            command.ExtendedMessageControl = extendedMessageControl;

            return Send(command);
        }

        /// <summary>
        /// The Cancel All Messages Command
        ///
        /// The Cancel All Messages command indicates to a CLIENT | device that it should cancel
        /// all display messages currently held by it.
        ///
        /// <param name="implementationTime" <see cref="DateTime"> Implementation Time</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> CancelAllMessagesCommand(DateTime implementationTime)
        {
            CancelAllMessagesCommand command = new CancelAllMessagesCommand();

            // Set the fields
            command.ImplementationTime = implementationTime;

            return Send(command);
        }

        /// <summary>
        /// The Get Last Message
        ///
        /// On receipt of this command, the device shall send a Display Message or Display
        /// Protected Message command as appropriate. A ZCL Default Response with status
        /// NOT_FOUND shall be returned if no message is available.
        ///
        /// <param name="messageId" <see cref="uint"> Message ID</ param >
        /// <param name="messageControl" <see cref="byte"> Message Control</ param >
        /// <param name="startTime" <see cref="DateTime"> Start Time</ param >
        /// <param name="durationInMinutes" <see cref="ushort"> Duration In Minutes</ param >
        /// <param name="message" <see cref="string"> Message</ param >
        /// <param name="optionalExtendedMessageControl" <see cref="byte"> Optional Extended Message Control</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> GetLastMessage(uint messageId, byte messageControl, DateTime startTime, ushort durationInMinutes, string message, byte optionalExtendedMessageControl)
        {
            GetLastMessage command = new GetLastMessage();

            // Set the fields
            command.MessageId = messageId;
            command.MessageControl = messageControl;
            command.StartTime = startTime;
            command.DurationInMinutes = durationInMinutes;
            command.Message = message;
            command.OptionalExtendedMessageControl = optionalExtendedMessageControl;

            return Send(command);
        }

        /// <summary>
        /// The Message Confirmation
        ///
        /// The Message Confirmation command provides an indication that a Utility Customer
        /// has acknowledged and/or accepted the contents of a previously sent message.
        /// Enhanced Message Confirmation commands shall contain an answer of ‘NO’, ‘YES’
        /// and/or a message confirmation string.
        ///
        /// <param name="messageId" <see cref="uint"> Message ID</ param >
        /// <param name="confirmationTime" <see cref="DateTime"> Confirmation Time</ param >
        /// <param name="messageConfirmationControl" <see cref="byte"> Message Confirmation Control</ param >
        /// <param name="messageConfirmationResponse" <see cref="ByteArray"> Message Confirmation Response</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> MessageConfirmation(uint messageId, DateTime confirmationTime, byte messageConfirmationControl, ByteArray messageConfirmationResponse)
        {
            MessageConfirmation command = new MessageConfirmation();

            // Set the fields
            command.MessageId = messageId;
            command.ConfirmationTime = confirmationTime;
            command.MessageConfirmationControl = messageConfirmationControl;
            command.MessageConfirmationResponse = messageConfirmationResponse;

            return Send(command);
        }

        /// <summary>
        /// The Get Message Cancellation
        ///
        /// This command initiates the return of the first (and maybe only) Cancel All Messages
        /// command held on the associated server, and which has an implementation time equal
        /// to or later than the value indicated in the payload.
        ///
        /// <param name="earliestImplementationTime" <see cref="DateTime"> Earliest Implementation Time</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> GetMessageCancellation(DateTime earliestImplementationTime)
        {
            GetMessageCancellation command = new GetMessageCancellation();

            // Set the fields
            command.EarliestImplementationTime = earliestImplementationTime;

            return Send(command);
        }

        /// <summary>
        /// The Cancel All Messages
        ///
        /// The CancelAllMessages command indicates to a client device that it should cancel
        /// all display messages currently held by it.
        ///
        /// <param name="implementationDateTime" <see cref="DateTime"> Implementation Date Time</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> CancelAllMessages(DateTime implementationDateTime)
        {
            CancelAllMessages command = new CancelAllMessages();

            // Set the fields
            command.ImplementationDateTime = implementationDateTime;

            return Send(command);
        }
    }
}
