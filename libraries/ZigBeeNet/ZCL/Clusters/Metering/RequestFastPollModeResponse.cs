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
    /// Request Fast Poll Mode Response value object class.
    ///
    /// Cluster: Metering. Command ID 0x03 is sent FROM the server.
    /// This command is a specific command used for the Metering cluster.
    ///
    /// This command is generated when the client command Request Fast Poll Mode is received.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class RequestFastPollModeResponse : ZclCommand
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
        /// Applied Update Period command message field.
        /// 
        /// The period at which metering data shall be updated. This may be different than the
        /// requested fast poll. If the Request Fast Poll Rate is less than Fast Poll Update
        /// Period Attribute, it shall use the Fast Poll Update Period Attribute. Otherwise,
        /// the Applied Update Period shall be greater than or equal to the minimum Fast Poll
        /// Update Period Attribute and less than or equal to the Requested Fast Poll Rate.
        /// </summary>
        public byte AppliedUpdatePeriod { get; set; }

        /// <summary>
        /// Fast Poll Mode Endtime command message field.
        /// 
        /// UTC time that indicates when the metering server will terminate fast poll mode and
        /// resume updating at the rate specified by DefaultUpdatePeriod. For example, one or
        /// more metering clients may request fast poll mode while the metering server is
        /// already in fast poll mode. The intent is that the fast poll mode will not be extended
        /// since this scenario would make it possible to be in fast poll mode longer than 15
        /// minutes.
        /// </summary>
        public DateTime FastPollModeEndtime { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public RequestFastPollModeResponse()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
            CommandDirection = ZclCommandDirection.SERVER_TO_CLIENT;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(AppliedUpdatePeriod, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            serializer.Serialize(FastPollModeEndtime, ZclDataType.Get(DataType.UTCTIME));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            AppliedUpdatePeriod = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            FastPollModeEndtime = deserializer.Deserialize<DateTime>(ZclDataType.Get(DataType.UTCTIME));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("RequestFastPollModeResponse [");
            builder.Append(base.ToString());
            builder.Append(", AppliedUpdatePeriod=");
            builder.Append(AppliedUpdatePeriod);
            builder.Append(", FastPollModeEndtime=");
            builder.Append(FastPollModeEndtime);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
