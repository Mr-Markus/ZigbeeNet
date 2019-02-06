using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.ZCL.Clusters.General
{
    /**
     * Write Attributes Structured Response value object class.
     * <p>
     * Cluster: <b>General</b>. Command is sent <b>TO</b> the server.
     * This command is a <b>generic</b> command used across the profile.
     * <p>
     * The write attributes structured response command is generated in response to a
     * write attributes structured command.
     * <p>
     * Code is auto-generated. Modifications may be overwritten!
     */
    public class WriteAttributesStructuredResponse : ZclCommand
    {
        /**
         * Status command message field.
         * <p>
         * Status is only provided if the command was successful, and the write
         * attribute status records are not included for successfully
         * written attributes, in order to save bandwidth.
         */
        public ZclStatus Status { get; set; }

        /**
         * Records command message field.
         * <p>
         * Note that write attribute status records are not included for successfully
         * written attributes, in order to save bandwidth.  In the case of successful
         * writing of all attributes, only a single write attribute status record
         * SHALL be included in the command, with the status field set to SUCCESS and the
         * attribute identifier field omitted.
         */
        public List<WriteAttributeStatusRecord> Records { get; set; }

        /**
         * Default constructor.
         */
        public WriteAttributesStructuredResponse()
        {
            GenericCommand = true;
            CommandId = 16;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        public override void Serialize(ZclFieldSerializer serializer)
        {
            if (Status == ZclStatus.SUCCESS)
            {
                serializer.Serialize(Status, ZclDataType.Get(DataType.ZCL_STATUS));
                return;
            }
            serializer.Serialize(Records, ZclDataType.Get(DataType.N_X_WRITE_ATTRIBUTE_STATUS_RECORD));
        }

        public override void Deserialize(ZclFieldDeserializer deserializer)
        {
            if (deserializer.RemainingLength == 1)
            {
                Status = (ZclStatus)deserializer.Deserialize(ZclDataType.Get(DataType.ZCL_STATUS));
                return;
            }
            Records = (List<WriteAttributeStatusRecord>)deserializer.Deserialize(ZclDataType.Get(DataType.N_X_WRITE_ATTRIBUTE_STATUS_RECORD));
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder()
                .Append("WriteAttributesStructuredResponse [")
                .Append(base.ToString())
                .Append(", status=")
                .Append(Status)
                .Append(", records=")
                .Append(Records)
                .Append(']');

            return builder.ToString();
        }
    }
}
