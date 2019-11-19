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
    /// Discover Attributes Extended value object class.
    ///
    /// Cluster: General. Command ID 0x15 is sent TO the server.
    /// This command is a generic command used across the profile.
    ///
    /// The Discover Attributes Extended command is generated when a remote device wishes to
    /// discover the identifiers and types of the attributes on a device which are supported
    /// within the cluster to which this command is directed, including whether the attribute
    /// is readable, writeable or reportable.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class DiscoverAttributesExtended : ZclCommand
    {
        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x15;

        /// <summary>
        /// Start Attribute Identifier command message field.
        /// </summary>
        public ushort StartAttributeIdentifier { get; set; }

        /// <summary>
        /// Maximum Attribute Identifiers command message field.
        /// </summary>
        public byte MaximumAttributeIdentifiers { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public DiscoverAttributesExtended()
        {
            CommandId = COMMAND_ID;
            GenericCommand = true;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(StartAttributeIdentifier, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            serializer.Serialize(MaximumAttributeIdentifiers, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            StartAttributeIdentifier = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            MaximumAttributeIdentifiers = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("DiscoverAttributesExtended [");
            builder.Append(base.ToString());
            builder.Append(", StartAttributeIdentifier=");
            builder.Append(StartAttributeIdentifier);
            builder.Append(", MaximumAttributeIdentifiers=");
            builder.Append(MaximumAttributeIdentifiers);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
