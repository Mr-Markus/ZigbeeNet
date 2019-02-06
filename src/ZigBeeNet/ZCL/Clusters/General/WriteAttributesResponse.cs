using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.ZCL.Clusters.General
{
    /**
     * Write Attributes Response value object class.
     * <p>
     * Cluster: <b>General</b>. Command is sent <b>TO</b> the server.
     * This command is a <b>generic</b> command used across the profile.
     * <p>
     * The write attributes response command is generated in response to a write
     * attributes command.
     * <p>
     * Code is auto-generated. Modifications may be overwritten!
     */
    public class WriteAttributesResponse : ZclCommand
    {
        /**
         * Records command message field.
         */
        public List<WriteAttributeStatusRecord> Records { get; set; }

        /**
         * Default constructor.
         */
        public WriteAttributesResponse()
        {
            GenericCommand = true;
            CommandId = 4;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        public override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(Records, ZclDataType.Get(DataType.N_X_WRITE_ATTRIBUTE_STATUS_RECORD));
        }

        public override void Deserialize(ZclFieldDeserializer deserializer)
        {
            Records = (List<WriteAttributeStatusRecord>)deserializer.Deserialize(ZclDataType.Get(DataType.N_X_WRITE_ATTRIBUTE_STATUS_RECORD));
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder()
                .Append("WriteAttributesResponse [")
                .Append(base.ToString())
                .Append(", records=")
                .Append(Records)
                .Append(']');

            return builder.ToString();
        }
    }
}
