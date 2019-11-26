using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.Security;
using ZigBeeNet.ZCL.Clusters.General;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Protocol;


namespace ZigBeeNet.ZCL.Clusters.General
{
    /// <summary>
    /// Configure Reporting Command value object class.
    ///
    /// Cluster: General. Command ID 0x06 is sent TO the server.
    /// This command is a generic command used across the profile.
    ///
    /// The Configure Reporting command is used to configure the reporting mechanism for one or
    /// more of the attributes of a cluster. <br> The individual cluster definitions specify
    /// which attributes shall be available to this reporting mechanism, however specific
    /// implementations of a cluster may make additional attributes available.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class ConfigureReportingCommand : ZclCommand
    {
        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x06;

        /// <summary>
        /// Records command message field.
        /// </summary>
        public List<AttributeReportingConfigurationRecord> Records { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ConfigureReportingCommand()
        {
            CommandId = COMMAND_ID;
            GenericCommand = true;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(Records, ZclDataType.Get(DataType.N_X_ATTRIBUTE_REPORTING_CONFIGURATION_RECORD));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            Records = deserializer.Deserialize<List<AttributeReportingConfigurationRecord>>(ZclDataType.Get(DataType.N_X_ATTRIBUTE_REPORTING_CONFIGURATION_RECORD));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("ConfigureReportingCommand [");
            builder.Append(base.ToString());
            builder.Append(", Records=");
            builder.Append(string.Join(", ", Records));
            builder.Append(']');

            return builder.ToString();
        }
    }
}
