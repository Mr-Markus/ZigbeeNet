using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.Security;
using ZigBeeNet.ZCL.Clusters.General;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Protocol;


namespace ZigBeeNet.ZCL.Clusters.General
{
    /// <summary>
    /// Discover Commands Generated Response value object class.
    ///
    /// Cluster: General. Command ID 0x14 is sent TO the server.
    /// This command is a generic command used across the profile.
    ///
    /// The Discover Commands Generated Response is generated in response to a Discover
    /// Commands Generated command.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class DiscoverCommandsGeneratedResponse : ZclCommand
    {
        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x14;

        /// <summary>
        /// Discovery Complete command message field.
        /// </summary>
        public bool DiscoveryComplete { get; set; }

        /// <summary>
        /// Command Identifiers command message field.
        /// </summary>
        public List<byte> CommandIdentifiers { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public DiscoverCommandsGeneratedResponse()
        {
            CommandId = COMMAND_ID;
            GenericCommand = true;
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

            builder.Append("DiscoverCommandsGeneratedResponse [");
            builder.Append(base.ToString());
            builder.Append(", DiscoveryComplete=");
            builder.Append(DiscoveryComplete);
            builder.Append(", CommandIdentifiers=");
            builder.Append(string.Join(", ", CommandIdentifiers));
            builder.Append(']');

            return builder.ToString();
        }
    }
}
