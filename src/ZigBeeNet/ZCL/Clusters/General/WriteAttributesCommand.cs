using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.ZCL.Clusters.General
{
    /**
     * Write Attributes Command value object class.
     * <p>
     * Cluster: <b>General</b>. Command is sent <b>TO</b> the server.
     * This command is a <b>generic</b> command used across the profile.
     * <p>
     * The write attributes command is generated when a device wishes to change the
     * values of one or more attributes located on another device. Each write attribute
     * record shall contain the identifier and the actual value of the attribute to be
     * written.
     * <p>
     * Code is auto-generated. Modifications may be overwritten!
     */
    public class WriteAttributesCommand : ZclCommand
    {
        /**
         * Records command message field.
         */
        public List<WriteAttributeRecord> Records { get; set; }

        /**
         * Default constructor.
         */
        public WriteAttributesCommand()
        {
            GenericCommand = true;
            CommandId = 2;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        public override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(Records, ZclDataType.Get(DataType.N_X_WRITE_ATTRIBUTE_RECORD));
        }

        public override void Deserialize(ZclFieldDeserializer deserializer)
        {
            Records = (List<WriteAttributeRecord>)deserializer.Deserialize(ZclDataType.Get(DataType.N_X_WRITE_ATTRIBUTE_RECORD));
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder()
                .Append("WriteAttributesCommand [")
                .Append(base.ToString())
                .Append(", records=")
                .Append(Records)
                .Append(']');

            return builder.ToString();
        }
    }
}
