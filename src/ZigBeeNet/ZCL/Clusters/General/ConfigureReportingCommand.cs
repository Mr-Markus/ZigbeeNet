using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.ZCL.Clusters.General
{
    /**
     * Configure Reporting Command value object class.
     * <p>
     * Cluster: <b>General</b>. Command is sent <b>TO</b> the server.
     * This command is a <b>generic</b> command used across the profile.
     * <p>
     * The Configure Reporting command is used to configure the reporting mechanism
     * for one or more of the attributes of a cluster.
     * <br>
     * The individual cluster definitions specify which attributes shall be available to this
     * reporting mechanism, however specific implementations of a cluster may make
     * additional attributes available.
     * <p>
     * Code is auto-generated. Modifications may be overwritten!
     */
    public class ConfigureReportingCommand : ZclCommand
    {
        /**
         * Records command message field.
         */
        public List<AttributeReportingConfigurationRecord> Records { get; set; }

        /**
         * Default constructor.
         */
        public ConfigureReportingCommand()
        {
            GenericCommand = true;
            CommandId = 6;
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
                .Append("ConfigureReportingCommand [")
                .Append(base.ToString())
                .Append(", records=")
                .Append(Records)
                .Append(']');

            return builder.ToString();
        }
    }
}
