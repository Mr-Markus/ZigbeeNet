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
    /// Read Reporting Configuration Response value object class.
    ///
    /// Cluster: General. Command ID 0x09 is sent TO the server.
    /// This command is a generic command used across the profile.
    ///
    /// The Read Reporting Configuration Response command is used to respond to a Read
    /// Reporting Configuration command.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class ReadReportingConfigurationResponse : ZclCommand
    {
        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x09;

        /// <summary>
        /// Records command message field.
        /// </summary>
        public List<AttributeReportingStatusRecord> Records { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ReadReportingConfigurationResponse()
        {
            CommandId = COMMAND_ID;
            GenericCommand = true;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(Records, ZclDataType.Get(DataType.N_X_ATTRIBUTE_REPORTING_STATUS_RECORD));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            Records = deserializer.Deserialize<List<AttributeReportingStatusRecord>>(ZclDataType.Get(DataType.N_X_ATTRIBUTE_REPORTING_STATUS_RECORD));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("ReadReportingConfigurationResponse [");
            builder.Append(base.ToString());
            builder.Append(", Records=");
            builder.Append(Records == null? "" : string.Join(", ", Records));
            builder.Append(']');

            return builder.ToString();
        }
    }
}
