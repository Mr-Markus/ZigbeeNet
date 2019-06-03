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
    /// Discover Attributes Response value object class.
    /// <para>
    /// Cluster: General. Command is sent TO the server.
    /// This command is a generic command used across the profile.
    ///
    /// The discover attributes response command is generated in response to a discover
    /// attributes command.
    /// </para>
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class DiscoverAttributesResponse : ZclCommand
    {
        /// <summary>
        /// Discovery Complete command message field.
        ///
        /// The discovery complete field is a Boolean field. A value of 0 indicates that there
        /// are more attributes to be discovered that have an attribute identifier value greater
        /// than the last attribute identifier in the last attribute information field. A value
        /// of 1 indicates that there are no more attributes to be discovered.
        /// The attribute identifier field SHALL contain the identifier of a discovered attribute.
        /// Attributes SHALL be included in ascending order, starting with the lowest attribute
        /// identifier that is greater than or equal to the start attribute identifier field of the
        /// received Discover Attributes command.
        /// </summary>
        public bool DiscoveryComplete { get; set; }

        /// <summary>
        /// Attribute Information command message field.
        /// </summary>
        public List<AttributeInformation> AttributeInformation { get; set; }


        /// <summary>
        /// Default constructor.
        /// </summary>
        public DiscoverAttributesResponse()
        {
            GenericCommand = true;
            CommandId = 13;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(DiscoveryComplete, ZclDataType.Get(DataType.BOOLEAN));
            serializer.Serialize(AttributeInformation, ZclDataType.Get(DataType.N_X_ATTRIBUTE_INFORMATION));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            DiscoveryComplete = deserializer.Deserialize<bool>(ZclDataType.Get(DataType.BOOLEAN));
            AttributeInformation = deserializer.Deserialize<List<AttributeInformation>>(ZclDataType.Get(DataType.N_X_ATTRIBUTE_INFORMATION));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("DiscoverAttributesResponse [");
            builder.Append(base.ToString());
            builder.Append(", DiscoveryComplete=");
            builder.Append(DiscoveryComplete);
            builder.Append(", AttributeInformation=");
            builder.Append(AttributeInformation);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
