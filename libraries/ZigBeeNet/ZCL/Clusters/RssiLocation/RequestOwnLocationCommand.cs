using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.Security;
using ZigBeeNet.ZCL.Clusters.RssiLocation;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Protocol;


namespace ZigBeeNet.ZCL.Clusters.RssiLocation
{
    /// <summary>
    /// Request Own Location Command value object class.
    ///
    /// Cluster: RSSI Location. Command ID 0x07 is sent FROM the server.
    /// This command is a specific command used for the RSSI Location cluster.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class RequestOwnLocationCommand : ZclCommand
    {
        /// <summary>
        /// The cluster ID to which this command belongs.
        /// </summary>
        public const ushort CLUSTER_ID = 0x000B;

        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x07;

        /// <summary>
        /// Requesting Address command message field.
        /// </summary>
        public IeeeAddress RequestingAddress { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public RequestOwnLocationCommand()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
            CommandDirection = ZclCommandDirection.SERVER_TO_CLIENT;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(RequestingAddress, ZclDataType.Get(DataType.IEEE_ADDRESS));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            RequestingAddress = deserializer.Deserialize<IeeeAddress>(ZclDataType.Get(DataType.IEEE_ADDRESS));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("RequestOwnLocationCommand [");
            builder.Append(base.ToString());
            builder.Append(", RequestingAddress=");
            builder.Append(RequestingAddress);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
