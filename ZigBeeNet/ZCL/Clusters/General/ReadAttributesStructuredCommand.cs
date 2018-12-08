using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.ZCL.Clusters.General
{
    /**
     * Read Attributes Structured Command value object class.
     * <p>
     * Cluster: <b>General</b>. Command is sent <b>TO</b> the server.
     * This command is a <b>generic</b> command used across the profile.
     * <p>
     * The read attributes command is generated when a device wishes to determine the
     * values of one or more attributes, or elements of attributes, located on another
     * device. Each attribute identifier field shall contain the identifier of the attribute to
     * be read.
     * <p>
     * Code is auto-generated. Modifications may be overwritten!
     */
    public class ReadAttributesStructuredCommand : ZclCommand
    {
        /**
         * Attribute selectors command message field.
         */
        public object AttributeSelectors { get; set; }

        /**
         * Default constructor.
         */
        public ReadAttributesStructuredCommand()
        {
            GenericCommand = true;
            CommandId = 14;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        public override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(AttributeSelectors, ZclDataType.Get(DataType.N_X_ATTRIBUTE_SELECTOR));
        }

        public override void Deserialize(ZclFieldDeserializer deserializer)
        {
            AttributeSelectors = deserializer.Deserialize(ZclDataType.Get(DataType.N_X_ATTRIBUTE_SELECTOR));
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder()
                .Append("ReadAttributesStructuredCommand [")
                .Append(base.ToString())
                .Append(", attributeSelectors=")
                .Append(AttributeSelectors)
                .Append(']');

            return builder.ToString();
        }
    }
}
