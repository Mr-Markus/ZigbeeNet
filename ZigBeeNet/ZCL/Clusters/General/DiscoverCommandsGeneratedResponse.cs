using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.ZCL.Clusters.General
{
    /**
     * Discover Commands Generated Response value object class.
     * <p>
     * Cluster: <b>General</b>. Command is sent <b>TO</b> the server.
     * This command is a <b>generic</b> command used across the profile.
     * <p>
     * The Discover Commands Generated Response is generated in response to a Discover Commands Generated
     * command.
     * <p>
     * Code is auto-generated. Modifications may be overwritten!
     */
    public class DiscoverCommandsGeneratedResponse : ZclCommand
    {
        /**
         * Discovery complete command message field.
         */
        public bool DiscoveryComplete { get; set; }

        /**
         * Command identifiers command message field.
         */
        public List<byte> CommandIdentifiers { get; set; }

        /**
         * Default constructor.
         */
        public DiscoverCommandsGeneratedResponse()
        {
            GenericCommand = true;
            CommandId = 20;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        public override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(DiscoveryComplete, ZclDataType.Get(DataType.BOOLEAN));
            serializer.Serialize(CommandIdentifiers, ZclDataType.Get(DataType.X_UNSIGNED_8_BIT_INTEGER));
        }

        public override void Deserialize(ZclFieldDeserializer deserializer)
        {
            DiscoveryComplete = (bool)deserializer.Deserialize(ZclDataType.Get(DataType.BOOLEAN));
            CommandIdentifiers = (List<byte>)deserializer.Deserialize(ZclDataType.Get(DataType.X_UNSIGNED_8_BIT_INTEGER));
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder()
                .Append("DiscoverCommandsGeneratedResponse [")
                .Append(base.ToString())
                .Append(", discoveryComplete=")
                .Append(DiscoveryComplete)
                .Append(", commandIdentifiers=")
                .Append(CommandIdentifiers)
                .Append(']');

            return builder.ToString();
        }
    }
}
