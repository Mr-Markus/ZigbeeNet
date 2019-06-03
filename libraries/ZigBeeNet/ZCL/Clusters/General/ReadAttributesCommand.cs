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
    /// Read Attributes Command value object class.
    /// <para>
    /// Cluster: General. Command is sent TO the server.
    /// This command is a generic command used across the profile.
    ///
    /// The read attributes command is generated when a device wishes to determine the
    /// values of one or more attributes located on another device. Each attribute
    /// identifier field shall contain the identifier of the attribute to be read.
    /// </para>
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class ReadAttributesCommand : ZclCommand
    {
        /// <summary>
        /// Identifiers command message field.
        /// </summary>
        public List<ushort> Identifiers { get; set; }


        /// <summary>
        /// Default constructor.
        /// </summary>
        public ReadAttributesCommand()
        {
            GenericCommand = true;
            CommandId = 0;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(Identifiers, ZclDataType.Get(DataType.N_X_ATTRIBUTE_IDENTIFIER));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            Identifiers = deserializer.Deserialize<List<ushort>>(ZclDataType.Get(DataType.N_X_ATTRIBUTE_IDENTIFIER));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("ReadAttributesCommand [");
            builder.Append(base.ToString());
            builder.Append(", Identifiers=");
            builder.Append(Identifiers);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
