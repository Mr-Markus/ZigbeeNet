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
    /// Discover Attributes Extended Response value object class.
    /// <para>
    /// Cluster: General. Command is sent TO the server.
    /// This command is a generic command used across the profile.
    ///
    /// The Discover Attributes Extended Response command is generated in response to a Discover Attributes
    /// Extended command.
    /// </para>
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class DiscoverAttributesExtendedResponse : ZclCommand
    {
        /// <summary>
        /// Discovery complete command message field.
        /// </summary>
        public bool DiscoveryComplete { get; set; }

        /// <summary>
        /// Attribute Information command message field.
        /// </summary>
        public List<ExtendedAttributeInformation> AttributeInformation { get; set; }


        /// <summary>
        /// Default constructor.
        /// </summary>
        public DiscoverAttributesExtendedResponse()
        {
            GenericCommand = true;
            CommandId = 22;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(DiscoveryComplete, ZclDataType.Get(DataType.BOOLEAN));
            serializer.Serialize(AttributeInformation, ZclDataType.Get(DataType.N_X_EXTENDED_ATTRIBUTE_INFORMATION));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            DiscoveryComplete = deserializer.Deserialize<bool>(ZclDataType.Get(DataType.BOOLEAN));
            AttributeInformation = deserializer.Deserialize<List<ExtendedAttributeInformation>>(ZclDataType.Get(DataType.N_X_EXTENDED_ATTRIBUTE_INFORMATION));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("DiscoverAttributesExtendedResponse [");
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
