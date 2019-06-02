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
    /// Write Attributes Structured Command value object class.
    /// <para>
    /// Cluster: General. Command is sent TO the server.
    /// This command is a generic command used across the profile.
    ///
    /// The write attributes structured command is generated when a device wishes to
    /// change the values of one or more attributes located on another device. Each write
    /// attribute record shall contain the identifier and the actual value of the attribute, or
    /// element thereof, to be written.
    /// </para>
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class WriteAttributesStructuredCommand : ZclCommand
    {
        /// <summary>
        /// Status command message field.
        ///
        /// Status is only provided if the command was successful, and the
        /// attribute selector records are not included for successfully
        /// written attributes, in order to save bandwidth.
        /// </summary>
        public ZclStatus Status { get; set; }

        /// <summary>
        /// Attribute selectors command message field.
        ///
        /// Note that write attribute status records are not included for successfully
        /// written attributes, in order to save bandwidth. In the case of successful
        /// writing of all attributes, only a single  write attribute status record
        /// SHALL be included in the command, with the status field set to SUCCESS and the
        /// attribute identifier and selector fields omitted.
        /// </summary>
        public object AttributeSelectors { get; set; }


        /// <summary>
        /// Default constructor.
        /// </summary>
        public WriteAttributesStructuredCommand()
        {
            GenericCommand = true;
            CommandId = 15;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            if (Status == ZclStatus.SUCCESS)
            {
                serializer.Serialize(Status, ZclDataType.Get(DataType.ZCL_STATUS));
                return;
            }
            serializer.Serialize(AttributeSelectors, ZclDataType.Get(DataType.N_X_ATTRIBUTE_SELECTOR));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            if (deserializer.RemainingLength == 1)
            {
                Status = deserializer.Deserialize<ZclStatus>(ZclDataType.Get(DataType.ZCL_STATUS));
                return;
            }
            AttributeSelectors = deserializer.Deserialize<object>(ZclDataType.Get(DataType.N_X_ATTRIBUTE_SELECTOR));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("WriteAttributesStructuredCommand [");
            builder.Append(base.ToString());
            builder.Append(", Status=");
            builder.Append(Status);
            builder.Append(", AttributeSelectors=");
            builder.Append(AttributeSelectors);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
