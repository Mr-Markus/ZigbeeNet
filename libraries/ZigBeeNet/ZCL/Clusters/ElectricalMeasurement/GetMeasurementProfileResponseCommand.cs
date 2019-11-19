using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.Security;
using ZigBeeNet.ZCL.Clusters.ElectricalMeasurement;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Protocol;


namespace ZigBeeNet.ZCL.Clusters.ElectricalMeasurement
{
    /// <summary>
    /// Get Measurement Profile Response Command value object class.
    ///
    /// Cluster: Electrical Measurement. Command ID 0x01 is sent FROM the server.
    /// This command is a specific command used for the Electrical Measurement cluster.
    ///
    /// Returns the electricity measurement profile. The electricity measurement profile
    /// includes information regarding the amount of time used to capture data related to the
    /// flow of electricity as well as the intervals thes
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class GetMeasurementProfileResponseCommand : ZclCommand
    {
        /// <summary>
        /// The cluster ID to which this command belongs.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0B04;

        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x01;

        /// <summary>
        /// Start Time command message field.
        /// </summary>
        public uint StartTime { get; set; }

        /// <summary>
        /// Status command message field.
        /// </summary>
        public byte Status { get; set; }

        /// <summary>
        /// Profile Interval Period command message field.
        /// </summary>
        public byte ProfileIntervalPeriod { get; set; }

        /// <summary>
        /// Number Of Intervals Delivered command message field.
        /// </summary>
        public byte NumberOfIntervalsDelivered { get; set; }

        /// <summary>
        /// Attribute ID command message field.
        /// </summary>
        public ushort AttributeId { get; set; }

        /// <summary>
        /// Intervals command message field.
        /// </summary>
        public byte Intervals { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public GetMeasurementProfileResponseCommand()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
            CommandDirection = ZclCommandDirection.SERVER_TO_CLIENT;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(StartTime, ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER));
            serializer.Serialize(Status, ZclDataType.Get(DataType.ENUMERATION_8_BIT));
            serializer.Serialize(ProfileIntervalPeriod, ZclDataType.Get(DataType.ENUMERATION_8_BIT));
            serializer.Serialize(NumberOfIntervalsDelivered, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            serializer.Serialize(AttributeId, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            serializer.Serialize(Intervals, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            StartTime = deserializer.Deserialize<uint>(ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER));
            Status = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.ENUMERATION_8_BIT));
            ProfileIntervalPeriod = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.ENUMERATION_8_BIT));
            NumberOfIntervalsDelivered = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            AttributeId = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            Intervals = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("GetMeasurementProfileResponseCommand [");
            builder.Append(base.ToString());
            builder.Append(", StartTime=");
            builder.Append(StartTime);
            builder.Append(", Status=");
            builder.Append(Status);
            builder.Append(", ProfileIntervalPeriod=");
            builder.Append(ProfileIntervalPeriod);
            builder.Append(", NumberOfIntervalsDelivered=");
            builder.Append(NumberOfIntervalsDelivered);
            builder.Append(", AttributeId=");
            builder.Append(AttributeId);
            builder.Append(", Intervals=");
            builder.Append(Intervals);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
