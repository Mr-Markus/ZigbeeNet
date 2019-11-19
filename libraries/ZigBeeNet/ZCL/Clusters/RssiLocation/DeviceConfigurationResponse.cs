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
    /// Device Configuration Response value object class.
    ///
    /// Cluster: RSSI Location. Command ID 0x00 is sent FROM the server.
    /// This command is a specific command used for the RSSI Location cluster.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class DeviceConfigurationResponse : ZclCommand
    {
        /// <summary>
        /// The cluster ID to which this command belongs.
        /// </summary>
        public const ushort CLUSTER_ID = 0x000B;

        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x00;

        /// <summary>
        /// Status command message field.
        /// </summary>
        public byte Status { get; set; }

        /// <summary>
        /// Power command message field.
        /// </summary>
        public short Power { get; set; }

        /// <summary>
        /// Path Loss Exponent command message field.
        /// </summary>
        public ushort PathLossExponent { get; set; }

        /// <summary>
        /// Calculation Period command message field.
        /// </summary>
        public ushort CalculationPeriod { get; set; }

        /// <summary>
        /// Number RSSI Measurements command message field.
        /// </summary>
        public byte NumberRssiMeasurements { get; set; }

        /// <summary>
        /// Reporting Period command message field.
        /// </summary>
        public ushort ReportingPeriod { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public DeviceConfigurationResponse()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
            CommandDirection = ZclCommandDirection.SERVER_TO_CLIENT;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(Status, ZclDataType.Get(DataType.ENUMERATION_8_BIT));
            serializer.Serialize(Power, ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER));
            serializer.Serialize(PathLossExponent, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            serializer.Serialize(CalculationPeriod, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            serializer.Serialize(NumberRssiMeasurements, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            serializer.Serialize(ReportingPeriod, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            Status = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.ENUMERATION_8_BIT));
            Power = deserializer.Deserialize<short>(ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER));
            PathLossExponent = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            CalculationPeriod = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            NumberRssiMeasurements = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            ReportingPeriod = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("DeviceConfigurationResponse [");
            builder.Append(base.ToString());
            builder.Append(", Status=");
            builder.Append(Status);
            builder.Append(", Power=");
            builder.Append(Power);
            builder.Append(", PathLossExponent=");
            builder.Append(PathLossExponent);
            builder.Append(", CalculationPeriod=");
            builder.Append(CalculationPeriod);
            builder.Append(", NumberRssiMeasurements=");
            builder.Append(NumberRssiMeasurements);
            builder.Append(", ReportingPeriod=");
            builder.Append(ReportingPeriod);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
