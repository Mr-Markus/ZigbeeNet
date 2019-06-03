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
    /// Set Short Poll Interval Command value object class.
    /// <para>
    /// Cluster: Poll Control. Command is sent TO the server.
    /// This command is a specific command used for the Poll Control cluster.
    ///
    /// The Set Short Poll Interval command is used to set the Read Only ShortPollInterval attribute.
    /// <br>
    /// When the Poll Control Server receives the Set Short Poll Interval Command, it SHOULD check its internal minimal limit and the attributes
    /// relationship if the new Short Poll Interval is acceptable. If the new value is acceptable, the new value SHALL be saved to the
    /// ShortPollInterval attribute. If the new value is not acceptable, the Poll Control Server SHALL send a default response of INVALID_VALUE
    /// and the ShortPollInterval attribute value is not updated.
    /// </para>
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class SetShortPollIntervalCommand : ZclCommand
    {
        /// <summary>
        /// New Short Poll Interval command message field.
        /// </summary>
        public ushort NewShortPollInterval { get; set; }


        /// <summary>
        /// Default constructor.
        /// </summary>
        public SetShortPollIntervalCommand()
        {
            GenericCommand = false;
            ClusterId = 32;
            CommandId = 3;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(NewShortPollInterval, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            NewShortPollInterval = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("SetShortPollIntervalCommand [");
            builder.Append(base.ToString());
            builder.Append(", NewShortPollInterval=");
            builder.Append(NewShortPollInterval);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
