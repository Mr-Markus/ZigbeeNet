using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.ZCL.Clusters.General
{
    /**
     * Discover Attributes Response value object class.
     * <p>
     * Cluster: <b>General</b>. Command is sent <b>TO</b> the server.
     * This command is a <b>generic</b> command used across the profile.
     * <p>
     * The discover attributes response command is generated in response to a discover
     * attributes command.
     * <p>
     * Code is auto-generated. Modifications may be overwritten!
     */
    public class DiscoverAttributesResponse : ZclCommand
    {
        /**
         * Discovery Complete command message field.
         * <p>
         * The discovery complete field is a Boolean field. A value of 0 indicates that there
         * are more attributes to be discovered that have an attribute identifier value greater
         * than the last attribute identifier in the last attribute information field. A value
         * of 1 indicates that there are no more attributes to be discovered.
         * The attribute identifier field SHALL contain the identifier of a discovered attribute.
         * Attributes SHALL be included in ascending order, starting with the lowest attribute
         * identifier that is greater than or equal to the start attribute identifier field of the
         * received Discover Attributes command.
         */
        public bool DiscoveryComplete { get; set; }

        /**
         * Attribute Information command message field.
         */
        public List<AttributeInformation> AttributeInformation { get; set; }

        /**
         * Default constructor.
         */
        public DiscoverAttributesResponse()
        {
            GenericCommand = true;
            CommandId = 13;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        public override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(DiscoveryComplete, ZclDataType.Get(DataType.BOOLEAN));
            serializer.Serialize(AttributeInformation, ZclDataType.Get(DataType.N_X_ATTRIBUTE_INFORMATION));
        }

        public override void Deserialize(ZclFieldDeserializer deserializer)
        {
            DiscoveryComplete = (bool)deserializer.Deserialize(ZclDataType.Get(DataType.BOOLEAN));
            AttributeInformation = (List<AttributeInformation>)deserializer.Deserialize(ZclDataType.Get(DataType.N_X_ATTRIBUTE_INFORMATION));
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder()
                .Append("DiscoverAttributesResponse [")
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
