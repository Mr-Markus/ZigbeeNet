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
    /// Read Reporting Configuration Response value object class.
    /// <para>
    /// Cluster: General. Command is sent TO the server.
    /// This command is a generic command used across the profile.
    ///
    /// The Read Reporting Configuration Response command is used to respond to a
    /// Read Reporting Configuration command.
    /// </para>
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class ReadReportingConfigurationResponse : ZclCommand
    {
        /// <summary>
        /// Records command message field.
        /// </summary>
        public List<AttributeReportingConfigurationRecord> Records { get; set; }


        /// <summary>
        /// Default constructor.
        /// </summary>
        public ReadReportingConfigurationResponse()
        {
            GenericCommand = true;
            CommandId = 9;
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

            builder.Append("ReadReportingConfigurationResponse [");
            builder.Append(base.ToString());
            builder.Append(", Records=");
            builder.Append(Records);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
