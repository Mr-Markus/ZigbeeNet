using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.ZCL.Clusters.General
{
    /**
     * Read Reporting Configuration Command value object class.
     * <p>
     * Cluster: <b>General</b>. Command is sent <b>TO</b> the server.
     * This command is a <b>generic</b> command used across the profile.
     * <p>
     * The Read Reporting Configuration command is used to read the configuration
     * details of the reporting mechanism for one or more of the attributes of a cluster.
     * <p>
     * Code is auto-generated. Modifications may be overwritten!
     */
    public class ReadReportingConfigurationCommand : ZclCommand
    {
        /**
         * Records command message field.
         */
        public List<AttributeRecord> Records { get; set; }

        /**
         * Default constructor.
         */
        public ReadReportingConfigurationCommand()
        {
            GenericCommand = true;
            CommandId = 8;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        public override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(Records, ZclDataType.Get(DataType.N_X_ATTRIBUTE_RECORD));
        }

        public override void Deserialize(ZclFieldDeserializer deserializer)
        {
            Records = (List<AttributeRecord>)deserializer.Deserialize(ZclDataType.Get(DataType.N_X_ATTRIBUTE_RECORD));
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder()
                .Append("ReadReportingConfigurationCommand [")
                .Append(base.ToString())
                .Append(", records=")
                .Append(Records)
                .Append(']');

            return builder.ToString();
        }
    }
}
