using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.ZCL.Clusters.General
{
    /**
     * Discover Attributes Command value object class.
     * <p>
     * Cluster: <b>General</b>. Command is sent <b>TO</b> the server.
     * This command is a <b>generic</b> command used across the profile.
     * <p>
     * The discover attributes command is generated when a remote device wishes to
     * discover the identifiers and types of the attributes on a device which are supported
     * within the cluster to which this command is directed.
     * <p>
     * Code is auto-generated. Modifications may be overwritten!
     */
    public class DiscoverAttributesCommand : ZclCommand
    {
        /**
         * Start attribute identifier command message field.
         * <p>
         * The start attribute identifier field is 16 bits in length and specifies the value
         * of the identifier at which to begin the attribute discovery.
         */
        public ushort StartAttributeIdentifier { get; set; }

        /**
         * Maximum attribute identifiers command message field.
         * <p>
         * The  maximum attribute identifiers field is 8 bits in length and specifies the
         * maximum number of attribute identifiers that are to be returned in the resulting
         * Discover Attributes Response command.
         */
        public byte MaximumAttributeIdentifiers { get; set; }

        /**
         * Default constructor.
         */
        public DiscoverAttributesCommand()
        {
            GenericCommand = true;
            CommandId = 12;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        public override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(StartAttributeIdentifier, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            serializer.Serialize(MaximumAttributeIdentifiers, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
        }

        public override void Deserialize(ZclFieldDeserializer deserializer)
        {
            StartAttributeIdentifier = (ushort)deserializer.Deserialize(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            MaximumAttributeIdentifiers = (byte)deserializer.Deserialize(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder()
                .Append("DiscoverAttributesCommand [")
                .Append(base.ToString())
                .Append(", startAttributeIdentifier=")
                .Append(StartAttributeIdentifier)
                .Append(", maximumAttributeIdentifiers=")
                .Append(MaximumAttributeIdentifiers)
                .Append(']');

            return builder.ToString();
        }
    }
}
