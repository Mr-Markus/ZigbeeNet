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
    /// Select Available Emergency Credit value object class.
    ///
    /// Cluster: Prepayment. Command ID 0x00 is sent TO the server.
    /// This command is a specific command used for the Prepayment cluster.
    ///
    /// FIXME: This command is sent to the Metering Device to activate the use of any Emergency
    /// Credit available on the Metering Device.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class SelectAvailableEmergencyCredit : ZclCommand
    {
        /// <summary>
        /// The cluster ID to which this command belongs.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0705;

        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x00;

        /// <summary>
        /// Command Issue Date Time command message field.
        /// </summary>
        public DateTime CommandIssueDateTime { get; set; }

        /// <summary>
        /// Originating Device command message field.
        /// </summary>
        public byte OriginatingDevice { get; set; }

        /// <summary>
        /// Site ID command message field.
        /// </summary>
        public ByteArray SiteId { get; set; }

        /// <summary>
        /// Meter Serial Number command message field.
        /// </summary>
        public ByteArray MeterSerialNumber { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public SelectAvailableEmergencyCredit()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(CommandIssueDateTime, ZclDataType.Get(DataType.UTCTIME));
            serializer.Serialize(OriginatingDevice, ZclDataType.Get(DataType.ENUMERATION_8_BIT));
            serializer.Serialize(SiteId, ZclDataType.Get(DataType.OCTET_STRING));
            serializer.Serialize(MeterSerialNumber, ZclDataType.Get(DataType.OCTET_STRING));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            CommandIssueDateTime = deserializer.Deserialize<DateTime>(ZclDataType.Get(DataType.UTCTIME));
            OriginatingDevice = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.ENUMERATION_8_BIT));
            SiteId = deserializer.Deserialize<ByteArray>(ZclDataType.Get(DataType.OCTET_STRING));
            MeterSerialNumber = deserializer.Deserialize<ByteArray>(ZclDataType.Get(DataType.OCTET_STRING));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("SelectAvailableEmergencyCredit [");
            builder.Append(base.ToString());
            builder.Append(", CommandIssueDateTime=");
            builder.Append(CommandIssueDateTime);
            builder.Append(", OriginatingDevice=");
            builder.Append(OriginatingDevice);
            builder.Append(", SiteId=");
            builder.Append(SiteId);
            builder.Append(", MeterSerialNumber=");
            builder.Append(MeterSerialNumber);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
