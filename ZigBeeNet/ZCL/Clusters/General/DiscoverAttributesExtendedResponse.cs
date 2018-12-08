using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.ZCL.Clusters.General
{
    /**
     * Discover Attributes Extended Response value object class.
     * <p>
     * Cluster: <b>General</b>. Command is sent <b>TO</b> the server.
     * This command is a <b>generic</b> command used across the profile.
     * <p>
     * The Discover Attributes Extended Response command is generated in response to a Discover Attributes
     * Extended command.
     * <p>
     * Code is auto-generated. Modifications may be overwritten!
     */
    public class DiscoverAttributesExtendedResponse : ZclCommand
    {
        /**
         * Discovery complete command message field.
         */
        public bool DiscoveryComplete { get; set; }

        /**
         * Attribute Information command message field.
         */
        public List<ExtendedAttributeInformation> AttributeInformation { get; set; }

        /**
         * Default constructor.
         */
        public DiscoverAttributesExtendedResponse()
        {
            GenericCommand = true;
            CommandId = 22;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        public override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(DiscoveryComplete, ZclDataType.Get(DataType.BOOLEAN));
            serializer.Serialize(AttributeInformation, ZclDataType.Get(DataType.N_X_EXTENDED_ATTRIBUTE_INFORMATION));
        }

        public override void Deserialize(ZclFieldDeserializer deserializer)
        {
            DiscoveryComplete = (bool)deserializer.Deserialize(ZclDataType.Get(DataType.BOOLEAN));
            AttributeInformation = (List<ExtendedAttributeInformation>)deserializer.Deserialize(ZclDataType.Get(DataType.N_X_EXTENDED_ATTRIBUTE_INFORMATION));
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder()
                .Append("DiscoverAttributesExtendedResponse [")
                .Append(base.ToString())
                .Append(", discoveryComplete=")
                .Append(DiscoveryComplete)
                .Append(", attributeInformation=")
                .Append(AttributeInformation)
                .Append(']');

            return builder.ToString();
        }
    }
}
