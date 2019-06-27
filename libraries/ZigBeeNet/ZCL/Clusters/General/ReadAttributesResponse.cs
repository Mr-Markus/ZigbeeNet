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
    /// Read Attributes Response value object class.
    /// <para>
    /// Cluster: General. Command is sent TO the server.
    /// This command is a generic command used across the profile.
    ///
    /// The read attributes response command is generated in response to a read attributes
    /// or read attributes structured command. The command frame shall contain a read
    /// attribute status record for each attribute identifier specified in the original read
    /// attributes or read attributes structured command. For each read attribute status
    /// record, the attribute identifier field shall contain the identifier specified in the
    /// original read attributes or read attributes structured command.
    /// </para>
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class ReadAttributesResponse : ZclCommand
    {
        /// <summary>
        /// Records command message field.
        /// </summary>
        public List<ReadAttributeStatusRecord> Records { get; set; }


        /// <summary>
        /// Default constructor.
        /// </summary>
        public ReadAttributesResponse()
        {
            GenericCommand = true;
            CommandId = 1;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(Records, ZclDataType.Get(DataType.N_X_READ_ATTRIBUTE_STATUS_RECORD));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            Records = deserializer.Deserialize<List<ReadAttributeStatusRecord>>(ZclDataType.Get(DataType.N_X_READ_ATTRIBUTE_STATUS_RECORD));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("ReadAttributesResponse [");
            builder.Append(base.ToString());
            builder.Append(", Records=");
            builder.Append(Records);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
