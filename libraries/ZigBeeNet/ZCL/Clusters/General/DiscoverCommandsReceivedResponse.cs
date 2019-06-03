// License text here
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.ZCL.Protocol;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Clusters.General;


namespace ZigBeeNet.ZCL.Clusters.General
{
    /// <summary>
    /// Discover Commands Received Response value object class.
    /// <para>
    /// Cluster: General. Command is sent TO the server.
    /// This command is a generic command used across the profile.
    ///
    /// The Discover Commands Received Response is generated in response to a Discover Commands Received
    /// command.
    /// </para>
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class DiscoverCommandsReceivedResponse : ZclCommand
    {
        /// <summary>
        /// Discovery complete command message field.
        /// </summary>
        public bool DiscoveryComplete { get; set; }

        /// <summary>
        /// Command identifiers command message field.
        /// </summary>
        public List<byte> CommandIdentifiers { get; set; }


        /// <summary>
        /// Default constructor.
        /// </summary>
        public DiscoverCommandsReceivedResponse()
        {
            GenericCommand = true;
            CommandId = 18;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(DiscoveryComplete, ZclDataType.Get(DataType.BOOLEAN));
            serializer.Serialize(CommandIdentifiers, ZclDataType.Get(DataType.X_UNSIGNED_8_BIT_INTEGER));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            DiscoveryComplete = deserializer.Deserialize<bool>(ZclDataType.Get(DataType.BOOLEAN));
            CommandIdentifiers = deserializer.Deserialize<List<byte>>(ZclDataType.Get(DataType.X_UNSIGNED_8_BIT_INTEGER));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("DiscoverCommandsReceivedResponse [");
            builder.Append(base.ToString());
            builder.Append(", DiscoveryComplete=");
            builder.Append(DiscoveryComplete);
            builder.Append(", CommandIdentifiers=");
            builder.Append(CommandIdentifiers);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
