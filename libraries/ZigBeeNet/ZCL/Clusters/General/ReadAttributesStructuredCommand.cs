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
    /// Read Attributes Structured Command value object class.
    /// <para>
    /// Cluster: General. Command is sent TO the server.
    /// This command is a generic command used across the profile.
    ///
    /// The read attributes command is generated when a device wishes to determine the
    /// values of one or more attributes, or elements of attributes, located on another
    /// device. Each attribute identifier field shall contain the identifier of the attribute to
    /// be read.
    /// </para>
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class ReadAttributesStructuredCommand : ZclCommand
    {
        /// <summary>
        /// Attribute selectors command message field.
        /// </summary>
        public object AttributeSelectors { get; set; }


        /// <summary>
        /// Default constructor.
        /// </summary>
        public ReadAttributesStructuredCommand()
        {
            GenericCommand = true;
            CommandId = 14;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(AttributeSelectors, ZclDataType.Get(DataType.N_X_ATTRIBUTE_SELECTOR));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            AttributeSelectors = deserializer.Deserialize<object>(ZclDataType.Get(DataType.N_X_ATTRIBUTE_SELECTOR));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("ReadAttributesStructuredCommand [");
            builder.Append(base.ToString());
            builder.Append(", AttributeSelectors=");
            builder.Append(AttributeSelectors);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
