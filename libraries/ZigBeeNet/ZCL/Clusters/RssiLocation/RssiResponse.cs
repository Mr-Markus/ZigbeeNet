using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.Security;
using ZigBeeNet.ZCL.Clusters.RSSILocation;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Protocol;


namespace ZigBeeNet.ZCL.Clusters.RSSILocation
{
    /// <summary>
    /// RSSI Response value object class.
    ///
    /// Cluster: RSSI Location. Command ID 0x04 is sent TO the server.
    /// This command is a specific command used for the RSSI Location cluster.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class RssiResponse : ZclCommand
    {
        /// <summary>
        /// The cluster ID to which this command belongs.
        /// </summary>
        public const ushort CLUSTER_ID = 0x000B;

        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x04;

        /// <summary>
        /// Replying Device command message field.
        /// </summary>
        public IeeeAddress ReplyingDevice { get; set; }

        /// <summary>
        /// Coordinate 1 command message field.
        /// </summary>
        public short Coordinate1 { get; set; }

        /// <summary>
        /// Coordinate 2 command message field.
        /// </summary>
        public short Coordinate2 { get; set; }

        /// <summary>
        /// Coordinate 3 command message field.
        /// </summary>
        public short Coordinate3 { get; set; }

        /// <summary>
        /// RSSI command message field.
        /// </summary>
        public sbyte Rssi { get; set; }

        /// <summary>
        /// Number RSSI Measurements command message field.
        /// </summary>
        public byte NumberRssiMeasurements { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public RssiResponse()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(ReplyingDevice, ZclDataType.Get(DataType.IEEE_ADDRESS));
            serializer.Serialize(Coordinate1, ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER));
            serializer.Serialize(Coordinate2, ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER));
            serializer.Serialize(Coordinate3, ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER));
            serializer.Serialize(Rssi, ZclDataType.Get(DataType.SIGNED_8_BIT_INTEGER));
            serializer.Serialize(NumberRssiMeasurements, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            ReplyingDevice = deserializer.Deserialize<IeeeAddress>(ZclDataType.Get(DataType.IEEE_ADDRESS));
            Coordinate1 = deserializer.Deserialize<short>(ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER));
            Coordinate2 = deserializer.Deserialize<short>(ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER));
            Coordinate3 = deserializer.Deserialize<short>(ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER));
            Rssi = deserializer.Deserialize<sbyte>(ZclDataType.Get(DataType.SIGNED_8_BIT_INTEGER));
            NumberRssiMeasurements = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("RssiResponse [");
            builder.Append(base.ToString());
            builder.Append(", ReplyingDevice=");
            builder.Append(ReplyingDevice);
            builder.Append(", Coordinate1=");
            builder.Append(Coordinate1);
            builder.Append(", Coordinate2=");
            builder.Append(Coordinate2);
            builder.Append(", Coordinate3=");
            builder.Append(Coordinate3);
            builder.Append(", Rssi=");
            builder.Append(Rssi);
            builder.Append(", NumberRssiMeasurements=");
            builder.Append(NumberRssiMeasurements);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
