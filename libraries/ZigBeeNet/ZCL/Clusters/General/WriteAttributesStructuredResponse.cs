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
    /// Write Attributes Structured Response value object class.
    /// <para>
    /// Cluster: General. Command is sent TO the server.
    /// This command is a generic command used across the profile.
    ///
    /// The write attributes structured response command is generated in response to a
    /// write attributes structured command.
    /// </para>
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class WriteAttributesStructuredResponse : ZclCommand
    {
        /// <summary>
        /// Status command message field.
        ///
        /// Status is only provided if the command was successful, and the write
        /// attribute status records are not included for successfully
        /// written attributes, in order to save bandwidth.
        /// </summary>
        public ZclStatus Status { get; set; }

        /// <summary>
        /// Records command message field.
        ///
        /// Note that write attribute status records are not included for successfully
        /// written attributes, in order to save bandwidth.  In the case of successful
        /// writing of all attributes, only a single write attribute status record
        /// SHALL be included in the command, with the status field set to SUCCESS and the
        /// attribute identifier field omitted.
        /// </summary>
        public List<WriteAttributeStatusRecord> Records { get; set; }


        /// <summary>
        /// Default constructor.
        /// </summary>
        public WriteAttributesStructuredResponse()
        {
            GenericCommand = true;
            CommandId = 16;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            if (Status == ZclStatus.SUCCESS)
            {
                serializer.Serialize(Status, ZclDataType.Get(DataType.ZCL_STATUS));
                return;
            }
            serializer.Serialize(Records, ZclDataType.Get(DataType.N_X_WRITE_ATTRIBUTE_STATUS_RECORD));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            if (deserializer.RemainingLength == 1)
            {
                Status = deserializer.Deserialize<ZclStatus>(ZclDataType.Get(DataType.ZCL_STATUS));
                return;
            }
            Records = deserializer.Deserialize<List<WriteAttributeStatusRecord>>(ZclDataType.Get(DataType.N_X_WRITE_ATTRIBUTE_STATUS_RECORD));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("WriteAttributesStructuredResponse [");
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
