using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.ZCL.Clusters.General
{
    /**
     * Discover Commands Generated value object class.
     * <p>
     * Cluster: <b>General</b>. Command is sent <b>TO</b> the server.
     * This command is a <b>generic</b> command used across the profile.
     * <p>
     * The Discover Commands Generated command is generated when a remote device wishes to discover the
     * commands that a cluster may generate on the device to which this command is directed.
     * <p>
     * Code is auto-generated. Modifications may be overwritten!
     */
    public class DiscoverCommandsGenerated : ZclCommand
    {
        /**
         * Start command identifier command message field.
         */
        public byte StartCommandIdentifier { get; set; }

        /**
         * Maximum command identifiers command message field.
         */
        public byte MaximumCommandIdentifiers { get; set; }

        /**
         * Default constructor.
         */
        public DiscoverCommandsGenerated()
        {
            GenericCommand = true;
            CommandId = 19;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        public override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(StartCommandIdentifier, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            serializer.Serialize(MaximumCommandIdentifiers, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
        }

        public override void Deserialize(ZclFieldDeserializer deserializer)
        {
            StartCommandIdentifier = (byte)deserializer.Deserialize(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            MaximumCommandIdentifiers = (byte)deserializer.Deserialize(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder()
                .Append("DiscoverCommandsGenerated [")
                .Append(base.ToString())
                .Append(", startCommandIdentifier=")
                .Append(StartCommandIdentifier)
                .Append(", maximumCommandIdentifiers=")
                .Append(MaximumCommandIdentifiers)
                .Append(']');

            return builder.ToString();
        }
    }
}
