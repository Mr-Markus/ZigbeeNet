using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.ZCL.Clusters.General
{
    /**
     * Write Attributes Structured Command value object class.
     * <p>
     * Cluster: <b>General</b>. Command is sent <b>TO</b> the server.
     * This command is a <b>generic</b> command used across the profile.
     * <p>
     * The write attributes structured command is generated when a device wishes to
     * change the values of one or more attributes located on another device. Each write
     * attribute record shall contain the identifier and the actual value of the attribute, or
     * element thereof, to be written.
     * <p>
     * Code is auto-generated. Modifications may be overwritten!
     */
    public class WriteAttributesStructuredCommand : ZclCommand
    {
        /**
         * Status command message field.
         * <p>
         * Status is only provided if the command was successful, and the
         * attribute selector records are not included for successfully
         * written attributes, in order to save bandwidth.
         */
        public ZclStatus Status { get; set; }

        /**
         * Attribute selectors command message field.
         * <p>
         * Note that write attribute status records are not included for successfully
         * written attributes, in order to save bandwidth. In the case of successful
         * writing of all attributes, only a single  write attribute status record
         * SHALL be included in the command, with the status field set to SUCCESS and the
         * attribute identifier and selector fields omitted.
         */
        public object AttributeSelectors { get; set; }

        /**
         * Default constructor.
         */
        public WriteAttributesStructuredCommand()
        {
            GenericCommand = true;
            CommandId = 15;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        public override void Serialize(ZclFieldSerializer serializer)
        {
            if (Status == ZclStatus.SUCCESS)
            {
                serializer.Serialize(Status, ZclDataType.Get(DataType.ZCL_STATUS));
                return;
            }
            serializer.Serialize(AttributeSelectors, ZclDataType.Get(DataType.N_X_ATTRIBUTE_SELECTOR));
        }

        public override void Deserialize(ZclFieldDeserializer deserializer)
        {
            if (deserializer.RemainingLength == 1)
            {
                Status = (ZclStatus)deserializer.Deserialize(ZclDataType.Get(DataType.ZCL_STATUS));
                return;
            }
            AttributeSelectors = deserializer.Deserialize(ZclDataType.Get(DataType.N_X_ATTRIBUTE_SELECTOR));
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder()
                .Append("WriteAttributesStructuredCommand [")
                .Append(base.ToString())
                .Append(", status=")
                .Append(Status)
                .Append(", attributeSelectors=")
                .Append(AttributeSelectors)
                .Append(']');

            return builder.ToString();
        }
    }
}
