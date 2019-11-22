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
    /// Location Data Notification Command value object class.
    ///
    /// Cluster: RSSI Location. Command ID 0x02 is sent FROM the server.
    /// This command is a specific command used for the RSSI Location cluster.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class LocationDataNotificationCommand : ZclCommand
    {
        /// <summary>
        /// The cluster ID to which this command belongs.
        /// </summary>
        public const ushort CLUSTER_ID = 0x000B;

        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x02;

        /// <summary>
        /// Location Type command message field.
        /// </summary>
        public byte LocationType { get; set; }

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
        /// Power command message field.
        /// </summary>
        public short Power { get; set; }

        /// <summary>
        /// Path Loss Exponent command message field.
        /// </summary>
        public ushort PathLossExponent { get; set; }

        /// <summary>
        /// Location Method command message field.
        /// </summary>
        public byte LocationMethod { get; set; }

        /// <summary>
        /// Quality Measure command message field.
        /// </summary>
        public byte QualityMeasure { get; set; }

        /// <summary>
        /// Location Age command message field.
        /// </summary>
        public ushort LocationAge { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public LocationDataNotificationCommand()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
            CommandDirection = ZclCommandDirection.SERVER_TO_CLIENT;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(LocationType, ZclDataType.Get(DataType.DATA_8_BIT));
            serializer.Serialize(Coordinate1, ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER));
            serializer.Serialize(Coordinate2, ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER));
            serializer.Serialize(Coordinate3, ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER));
            serializer.Serialize(Power, ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER));
            serializer.Serialize(PathLossExponent, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            serializer.Serialize(LocationMethod, ZclDataType.Get(DataType.ENUMERATION_8_BIT));
            serializer.Serialize(QualityMeasure, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            serializer.Serialize(LocationAge, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            LocationType = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.DATA_8_BIT));
            Coordinate1 = deserializer.Deserialize<short>(ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER));
            Coordinate2 = deserializer.Deserialize<short>(ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER));
            Coordinate3 = deserializer.Deserialize<short>(ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER));
            Power = deserializer.Deserialize<short>(ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER));
            PathLossExponent = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            LocationMethod = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.ENUMERATION_8_BIT));
            QualityMeasure = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            LocationAge = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("LocationDataNotificationCommand [");
            builder.Append(base.ToString());
            builder.Append(", LocationType=");
            builder.Append(LocationType);
            builder.Append(", Coordinate1=");
            builder.Append(Coordinate1);
            builder.Append(", Coordinate2=");
            builder.Append(Coordinate2);
            builder.Append(", Coordinate3=");
            builder.Append(Coordinate3);
            builder.Append(", Power=");
            builder.Append(Power);
            builder.Append(", PathLossExponent=");
            builder.Append(PathLossExponent);
            builder.Append(", LocationMethod=");
            builder.Append(LocationMethod);
            builder.Append(", QualityMeasure=");
            builder.Append(QualityMeasure);
            builder.Append(", LocationAge=");
            builder.Append(LocationAge);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
