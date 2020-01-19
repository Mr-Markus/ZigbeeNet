using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.Security;
using ZigBeeNet.ZCL.Clusters.Prepayment;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Protocol;


namespace ZigBeeNet.ZCL.Clusters.Prepayment
{
    /// <summary>
    /// Get Top Up Log value object class.
    ///
    /// Cluster: Prepayment. Command ID 0x08 is sent TO the server.
    /// This command is a specific command used for the Prepayment cluster.
    ///
    /// FIXME: This command is sent to the Metering Device to retrieve the log of Top Up codes
    /// received by the meter.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class GetTopUpLog : ZclCommand
    {
        /// <summary>
        /// The cluster ID to which this command belongs.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0705;

        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x08;

        /// <summary>
        /// Latest End Time command message field.
        /// </summary>
        public DateTime LatestEndTime { get; set; }

        /// <summary>
        /// Number Of Records command message field.
        /// </summary>
        public byte NumberOfRecords { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public GetTopUpLog()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(LatestEndTime, ZclDataType.Get(DataType.UTCTIME));
            serializer.Serialize(NumberOfRecords, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            LatestEndTime = deserializer.Deserialize<DateTime>(ZclDataType.Get(DataType.UTCTIME));
            NumberOfRecords = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("GetTopUpLog [");
            builder.Append(base.ToString());
            builder.Append(", LatestEndTime=");
            builder.Append(LatestEndTime);
            builder.Append(", NumberOfRecords=");
            builder.Append(NumberOfRecords);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
