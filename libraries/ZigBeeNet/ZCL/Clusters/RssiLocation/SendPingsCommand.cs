using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.Security;
using ZigBeeNet.ZCL.Clusters.RssiLocation;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Protocol;


namespace ZigBeeNet.ZCL.Clusters.RssiLocation
{
    /// <summary>
    /// Send Pings Command value object class.
    ///
    /// Cluster: RSSI Location. Command ID 0x05 is sent TO the server.
    /// This command is a specific command used for the RSSI Location cluster.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class SendPingsCommand : ZclCommand
    {
        /// <summary>
        /// The cluster ID to which this command belongs.
        /// </summary>
        public const ushort CLUSTER_ID = 0x000B;

        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x05;

        /// <summary>
        /// Target Address command message field.
        /// </summary>
        public IeeeAddress TargetAddress { get; set; }

        /// <summary>
        /// Number RSSI Measurements command message field.
        /// </summary>
        public byte NumberRssiMeasurements { get; set; }

        /// <summary>
        /// Calculation Period command message field.
        /// </summary>
        public ushort CalculationPeriod { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public SendPingsCommand()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(TargetAddress, ZclDataType.Get(DataType.IEEE_ADDRESS));
            serializer.Serialize(NumberRssiMeasurements, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            serializer.Serialize(CalculationPeriod, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            TargetAddress = deserializer.Deserialize<IeeeAddress>(ZclDataType.Get(DataType.IEEE_ADDRESS));
            NumberRssiMeasurements = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            CalculationPeriod = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("SendPingsCommand [");
            builder.Append(base.ToString());
            builder.Append(", TargetAddress=");
            builder.Append(TargetAddress);
            builder.Append(", NumberRssiMeasurements=");
            builder.Append(NumberRssiMeasurements);
            builder.Append(", CalculationPeriod=");
            builder.Append(CalculationPeriod);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
