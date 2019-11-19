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
    /// Cancel All Messages Command value object class.
    ///
    /// Cluster: Messaging. Command ID 0x03 is sent TO the server.
    /// This command is a specific command used for the Messaging cluster.
    ///
    /// The Cancel All Messages command indicates to a CLIENT | device that it should cancel all
    /// display messages currently held by it.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class CancelAllMessagesCommand : ZclCommand
    {
        /// <summary>
        /// The cluster ID to which this command belongs.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0703;

        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x03;

        /// <summary>
        /// Implementation Time command message field.
        /// </summary>
        public DateTime ImplementationTime { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public CancelAllMessagesCommand()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(ImplementationTime, ZclDataType.Get(DataType.UTCTIME));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            ImplementationTime = deserializer.Deserialize<DateTime>(ZclDataType.Get(DataType.UTCTIME));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("CancelAllMessagesCommand [");
            builder.Append(base.ToString());
            builder.Append(", ImplementationTime=");
            builder.Append(ImplementationTime);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
