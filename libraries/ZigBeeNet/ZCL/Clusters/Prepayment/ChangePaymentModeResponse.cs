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
    /// Change Payment Mode Response value object class.
    ///
    /// Cluster: Prepayment. Command ID 0x02 is sent FROM the server.
    /// This command is a specific command used for the Prepayment cluster.
    ///
    /// FIXME: This command is send in response to the ChangePaymentMode Command.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class ChangePaymentModeResponse : ZclCommand
    {
        /// <summary>
        /// The cluster ID to which this command belongs.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0705;

        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x02;

        /// <summary>
        /// Friendly Credit command message field.
        /// </summary>
        public byte FriendlyCredit { get; set; }

        /// <summary>
        /// Friendly Credit Calendar ID command message field.
        /// </summary>
        public uint FriendlyCreditCalendarId { get; set; }

        /// <summary>
        /// Emergency Credit Limit command message field.
        /// </summary>
        public uint EmergencyCreditLimit { get; set; }

        /// <summary>
        /// Emergency Credit Threshold command message field.
        /// </summary>
        public uint EmergencyCreditThreshold { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ChangePaymentModeResponse()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
            CommandDirection = ZclCommandDirection.SERVER_TO_CLIENT;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(FriendlyCredit, ZclDataType.Get(DataType.BITMAP_8_BIT));
            serializer.Serialize(FriendlyCreditCalendarId, ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER));
            serializer.Serialize(EmergencyCreditLimit, ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER));
            serializer.Serialize(EmergencyCreditThreshold, ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            FriendlyCredit = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.BITMAP_8_BIT));
            FriendlyCreditCalendarId = deserializer.Deserialize<uint>(ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER));
            EmergencyCreditLimit = deserializer.Deserialize<uint>(ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER));
            EmergencyCreditThreshold = deserializer.Deserialize<uint>(ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("ChangePaymentModeResponse [");
            builder.Append(base.ToString());
            builder.Append(", FriendlyCredit=");
            builder.Append(FriendlyCredit);
            builder.Append(", FriendlyCreditCalendarId=");
            builder.Append(FriendlyCreditCalendarId);
            builder.Append(", EmergencyCreditLimit=");
            builder.Append(EmergencyCreditLimit);
            builder.Append(", EmergencyCreditThreshold=");
            builder.Append(EmergencyCreditThreshold);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
