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
    /// Cancel All Messages value object class.
    ///
    /// Cluster: Messaging. Command ID 0x03 is sent FROM the server.
    /// This command is a specific command used for the Messaging cluster.
    ///
    /// The CancelAllMessages command indicates to a client device that it should cancel all
    /// display messages currently held by it.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class CancelAllMessages : ZclCommand
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
        /// Implementation Date Time command message field.
        /// 
        /// A UTC Time field to indicate the date/time at which all existing display messages
        /// should be cleared.
        /// </summary>
        public DateTime ImplementationDateTime { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public CancelAllMessages()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
            CommandDirection = ZclCommandDirection.SERVER_TO_CLIENT;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(ImplementationDateTime, ZclDataType.Get(DataType.UTCTIME));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            ImplementationDateTime = deserializer.Deserialize<DateTime>(ZclDataType.Get(DataType.UTCTIME));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("CancelAllMessages [");
            builder.Append(base.ToString());
            builder.Append(", ImplementationDateTime=");
            builder.Append(ImplementationDateTime);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
