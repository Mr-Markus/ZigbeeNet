using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.ZCL.Clusters.General
{
    /**
     * Write Attributes No Response value object class.
     * <p>
     * Cluster: <b>General</b>. Command is sent <b>TO</b> the server.
     * This command is a <b>generic</b> command used across the profile.
     * <p>
     * The write attributes no response command is generated when a device wishes to
     * change the value of one or more attributes located on another device but does not
     * require a response. Each write attribute record shall contain the identifier and the
     * actual value of the attribute to be written.
     * <p>
     * Code is auto-generated. Modifications may be overwritten!
     */
    public class WriteAttributesNoResponse : ZclCommand
    {
        /**
         * Records command message field.
         */
        private List<WriteAttributeRecord> records;

        /**
         * Default constructor.
         */
        public WriteAttributesNoResponse()
        {
            GenericCommand = true;
            CommandId = 5;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        public override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(records, ZclDataType.Get(DataType.N_X_WRITE_ATTRIBUTE_RECORD));
        }

        public override void Deserialize(ZclFieldDeserializer deserializer)
        {
            records = (List<WriteAttributeRecord>)deserializer.Deserialize(ZclDataType.Get(DataType.N_X_WRITE_ATTRIBUTE_RECORD));
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder()
                .Append("WriteAttributesNoResponse [")
                .Append(base.ToString())
                .Append(", records=")
                .Append(records)
                .Append(']');

            return builder.ToString();
        }
    }
}
