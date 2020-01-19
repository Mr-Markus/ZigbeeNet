using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.Security;
using ZigBeeNet.ZCL.Clusters.Price;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Protocol;


namespace ZigBeeNet.ZCL.Clusters.Price
{
    /// <summary>
    /// Get Credit Payment Command value object class.
    ///
    /// Cluster: Price. Command ID 0x0E is sent TO the server.
    /// This command is a specific command used for the Price cluster.
    ///
    /// This command initiates PublishCreditPayment commands for the requested credit
    /// payment information.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class GetCreditPaymentCommand : ZclCommand
    {
        /// <summary>
        /// The cluster ID to which this command belongs.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0700;

        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x0E;

        /// <summary>
        /// Latest End Time command message field.
        /// 
        /// UTCTime stamp indicating the latest CreditPaymentDate of records to be returned
        /// by the corresponding PublishCreditPayment commands. The first returned
        /// PublishCreditPayment command shall be the most recent record with its
        /// CreditPaymentDate equal to or older than the Latest End Time provided.
        /// </summary>
        public DateTime LatestEndTime { get; set; }

        /// <summary>
        /// Number Of Records command message field.
        /// 
        /// An 8-bit integer that represents the maximum number of PublishCreditPayment
        /// commands that the CLIENT is willing to receive in response to this command. A value
        /// of 0 would indicate all available PublishCreditPayment commands shall be
        /// returned. If more than one record is requested, the PublishCreditPayment
        /// commands should be returned with descending ordered CreditPaymentDate. If fewer
        /// records are available than are being requested, only those available are
        /// returned.
        /// </summary>
        public byte NumberOfRecords { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public GetCreditPaymentCommand()
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

            builder.Append("GetCreditPaymentCommand [");
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
