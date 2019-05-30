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
    /// Discover Commands Received value object class.
    /// <para>
    /// Cluster: General. Command is sent TO the server.
    /// This command is a generic command used across the profile.
    ///
    /// The Discover Commands Received command is generated when a remote device wishes to discover the
    /// optional and mandatory commands the cluster to which this command is sent can process.
    /// </para>
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class DiscoverCommandsReceived : ZclCommand
    {
        /// <summary>
        /// Start command identifier command message field.
        /// </summary>
        public byte StartCommandIdentifier { get; set; }

        /// <summary>
        /// Maximum command identifiers command message field.
        /// </summary>
        public byte MaximumCommandIdentifiers { get; set; }


        /// <summary>
        /// Default constructor.
        /// </summary>
        public DiscoverCommandsReceived()
        {
            GenericCommand = true;
            CommandId = 17;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(StartCommandIdentifier, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            serializer.Serialize(MaximumCommandIdentifiers, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            StartCommandIdentifier = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            MaximumCommandIdentifiers = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("DiscoverCommandsReceived [");
            builder.Append(base.ToString());
            builder.Append(", StartCommandIdentifier=");
            builder.Append(StartCommandIdentifier);
            builder.Append(", MaximumCommandIdentifiers=");
            builder.Append(MaximumCommandIdentifiers);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
