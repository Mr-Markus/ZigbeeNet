// License text here
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.ZCL.Protocol;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Clusters.PollControl;


namespace ZigBeeNet.ZCL.Clusters.PollControl
{
    /// <summary>
    /// Set Long Poll Interval Command value object class.
    /// <para>
    /// Cluster: Poll Control. Command is sent TO the server.
    /// This command is a specific command used for the Poll Control cluster.
    ///
    /// The Set Long Poll Interval command is used to set the Read Only LongPollInterval attribute.
    /// <br>
    /// When the Poll Control Server receives the Set Long Poll Interval Command, it SHOULD check its internal minimal limit and the attributes
    /// relationship if the new Long Poll Interval is acceptable. If the new value is acceptable, the new value SHALL be saved to the
    /// LongPollInterval attribute. If the new value is not acceptable, the Poll Control Server SHALL send a default response of INVALID_VALUE and
    /// the LongPollInterval attribute value is not updated.
    /// </para>
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class SetLongPollIntervalCommand : ZclCommand
    {
        /// <summary>
        /// New Long Poll Interval command message field.
        /// </summary>
        public ushort NewLongPollInterval { get; set; }


        /// <summary>
        /// Default constructor.
        /// </summary>
        public SetLongPollIntervalCommand()
        {
            GenericCommand = false;
            ClusterId = 32;
            CommandId = 2;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(NewLongPollInterval, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            NewLongPollInterval = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("SetLongPollIntervalCommand [");
            builder.Append(base.ToString());
            builder.Append(", NewLongPollInterval=");
            builder.Append(NewLongPollInterval);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
