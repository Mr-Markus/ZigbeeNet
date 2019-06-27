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
    /// Configure Reporting Response value object class.
    /// <para>
    /// Cluster: General. Command is sent TO the server.
    /// This command is a generic command used across the profile.
    ///
    /// The Configure Reporting Response command is generated in response to a
    /// Configure Reporting command.
    /// </para>
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class ConfigureReportingResponse : ZclCommand
    {
        /// <summary>
        /// Status command message field.
        ///
        /// Status is only provided if the command was successful, and the
        /// attribute status records are not included for successfully
        /// written attributes, in order to save bandwidth.
        /// </summary>
        public ZclStatus Status { get; set; }

        /// <summary>
        /// Records command message field.
        ///
        /// Note that attribute status records are not included for successfully
        /// configured attributes in order to save bandwidth.  In the case of successful
        /// configuration of all attributes, only a single attribute status record SHALL
        /// be included in the command, with the status field set to SUCCESS and the direction and
        /// attribute identifier fields omitted.
        /// </summary>
        public List<AttributeStatusRecord> Records { get; set; }


        /// <summary>
        /// Default constructor.
        /// </summary>
        public ConfigureReportingResponse()
        {
            GenericCommand = true;
            CommandId = 7;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            if (Status == ZclStatus.SUCCESS)
            {
                serializer.Serialize(Status, ZclDataType.Get(DataType.ZCL_STATUS));
                return;
            }
            serializer.Serialize(Records, ZclDataType.Get(DataType.N_X_ATTRIBUTE_STATUS_RECORD));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            if (deserializer.RemainingLength == 1)
            {
                Status = deserializer.Deserialize<ZclStatus>(ZclDataType.Get(DataType.ZCL_STATUS));
                return;
            }
            Records = deserializer.Deserialize<List<AttributeStatusRecord>>(ZclDataType.Get(DataType.N_X_ATTRIBUTE_STATUS_RECORD));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("ConfigureReportingResponse [");
            builder.Append(base.ToString());
            builder.Append(", Status=");
            builder.Append(Status);
            builder.Append(", Records=");
            builder.Append(Records);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
