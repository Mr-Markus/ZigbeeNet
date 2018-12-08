using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.ZCL.Clusters.General
{
    /**
     * Read Attributes Command value object class.
     * <p>
     * Cluster: <b>General</b>. Command is sent <b>TO</b> the server.
     * This command is a <b>generic</b> command used across the profile.
     * <p>
     * The read attributes command is generated when a device wishes to determine the
     * values of one or more attributes located on another device. Each attribute
     * identifier field shall contain the identifier of the attribute to be read.
     * <p>
     * Code is auto-generated. Modifications may be overwritten!
     */
    public class ReadAttributesCommand : ZclCommand
    {
        /**
         * Identifiers command message field.
         */
        public List<ushort> Identifiers { get; set; }

        /**
         * Default constructor.
         */
        public ReadAttributesCommand()
        {
            GenericCommand = true;
            CommandId = 0;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        public override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(Identifiers, ZclDataType.Get(DataType.N_X_ATTRIBUTE_IDENTIFIER));
        }

        public override void Deserialize(ZclFieldDeserializer deserializer)
        {
            Identifiers = (List<ushort>)deserializer.Deserialize(ZclDataType.Get(DataType.N_X_ATTRIBUTE_IDENTIFIER));
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder()
                .Append("ReadAttributesCommand [")
                .Append(base.ToString())
                .Append(", identifiers=")
                .Append(Identifiers)
                .Append(']');

            return builder.ToString();
        }
    }
}
