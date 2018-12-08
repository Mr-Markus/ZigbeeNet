using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.ZCL.Clusters.General
{
    /**
     * Read Reporting Configuration Response value object class.
     * <p>
     * Cluster: <b>General</b>. Command is sent <b>TO</b> the server.
     * This command is a <b>generic</b> command used across the profile.
     * <p>
     * The Read Reporting Configuration Response command is used to respond to a
     * Read Reporting Configuration command.
     * <p>
     * Code is auto-generated. Modifications may be overwritten!
     */
    public class ReadReportingConfigurationResponse : ZclCommand
    {
        /**
         * Records command message field.
         */
        public List<AttributeReportingConfigurationRecord> Records { get; set; }

        /**
         * Default constructor.
         */
        public ReadReportingConfigurationResponse()
        {
            GenericCommand = true;
            CommandId = 9;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        public override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(Records, ZclDataType.Get(DataType.N_X_ATTRIBUTE_REPORTING_CONFIGURATION_RECORD));
        }

        public override void Deserialize(ZclFieldDeserializer deserializer)
        {
            Records = (List<AttributeReportingConfigurationRecord>)deserializer.Deserialize(ZclDataType.Get(DataType.N_X_ATTRIBUTE_REPORTING_CONFIGURATION_RECORD));
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder()
                .Append("ReadReportingConfigurationResponse [")
                .Append(base.ToString())
                .Append(", records=")
                .Append(Records)
                .Append(']');

            return builder.ToString();
        }
    }
}
