using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.Security;
using ZigBeeNet.ZCL.Clusters.PollControl;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Protocol;


namespace ZigBeeNet.ZCL.Clusters.PollControl
{
    /// <summary>
    /// Set Short Poll Interval Command value object class.
    ///
    /// Cluster: Poll Control. Command ID 0x03 is sent TO the server.
    /// This command is a specific command used for the Poll Control cluster.
    ///
    /// The Set Short Poll Interval command is used to set the Read Only ShortPollInterval
    /// attribute. <br> When the Poll Control Server receives the Set Short Poll Interval
    /// Command, it should check its internal minimal limit and the attributes relationship if
    /// the new Short Poll Interval is acceptable. If the new value is acceptable, the new value
    /// shall be saved to the ShortPollInterval attribute. If the new value is not acceptable,
    /// the Poll Control Server shall send a default response of INVALID_VALUE and the
    /// ShortPollInterval attribute value is not updated.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class SetShortPollIntervalCommand : ZclCommand
    {
        /// <summary>
        /// The cluster ID to which this command belongs.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0020;

        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x03;

        /// <summary>
        /// New Short Poll Interval command message field.
        /// </summary>
        public ushort NewShortPollInterval { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public SetShortPollIntervalCommand()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
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
