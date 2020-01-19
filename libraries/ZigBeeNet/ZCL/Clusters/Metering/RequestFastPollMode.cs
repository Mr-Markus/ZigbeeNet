using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.Security;
using ZigBeeNet.ZCL.Clusters.Metering;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Protocol;


namespace ZigBeeNet.ZCL.Clusters.Metering
{
    /// <summary>
    /// Request Fast Poll Mode value object class.
    ///
    /// Cluster: Metering. Command ID 0x03 is sent TO the server.
    /// This command is a specific command used for the Metering cluster.
    ///
    /// The Request Fast Poll Mode command is generated when the metering client wishes to
    /// receive near real-time updates of InstantaneousDemand.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class RequestFastPollMode : ZclCommand
    {
        /// <summary>
        /// The cluster ID to which this command belongs.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0702;

        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x03;

        /// <summary>
        /// Fast Poll Update Period command message field.
        /// 
        /// Desired fast poll period not to be less than the FastPollUpdatePeriod attribute.
        /// </summary>
        public byte FastPollUpdatePeriod { get; set; }

        /// <summary>
        /// Duration command message field.
        /// 
        /// Desired duration for the server to remain in fast poll mode not to exceed 15 minutes.
        /// </summary>
        public byte Duration { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public RequestFastPollMode()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(FastPollUpdatePeriod, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            serializer.Serialize(Duration, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            FastPollUpdatePeriod = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            Duration = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("RequestFastPollMode [");
            builder.Append(base.ToString());
            builder.Append(", FastPollUpdatePeriod=");
            builder.Append(FastPollUpdatePeriod);
            builder.Append(", Duration=");
            builder.Append(Duration);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
