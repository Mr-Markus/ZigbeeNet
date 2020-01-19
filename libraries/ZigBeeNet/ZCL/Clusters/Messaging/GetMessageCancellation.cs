using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.Security;
using ZigBeeNet.ZCL.Clusters.Messaging;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Protocol;


namespace ZigBeeNet.ZCL.Clusters.Messaging
{
    /// <summary>
    /// Get Message Cancellation value object class.
    ///
    /// Cluster: Messaging. Command ID 0x02 is sent FROM the server.
    /// This command is a specific command used for the Messaging cluster.
    ///
    /// This command initiates the return of the first (and maybe only) Cancel All Messages
    /// command held on the associated server, and which has an implementation time equal to or
    /// later than the value indicated in the payload.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class GetMessageCancellation : ZclCommand
    {
        /// <summary>
        /// The cluster ID to which this command belongs.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0703;

        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x02;

        /// <summary>
        /// Earliest Implementation Time command message field.
        /// 
        /// UTC Timestamp indicating the earliest implementation time of a Cancel All
        /// Messages command to be returned.
        /// </summary>
        public DateTime EarliestImplementationTime { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public GetMessageCancellation()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
            CommandDirection = ZclCommandDirection.SERVER_TO_CLIENT;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(EarliestImplementationTime, ZclDataType.Get(DataType.UTCTIME));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            EarliestImplementationTime = deserializer.Deserialize<DateTime>(ZclDataType.Get(DataType.UTCTIME));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("GetMessageCancellation [");
            builder.Append(base.ToString());
            builder.Append(", EarliestImplementationTime=");
            builder.Append(EarliestImplementationTime);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
