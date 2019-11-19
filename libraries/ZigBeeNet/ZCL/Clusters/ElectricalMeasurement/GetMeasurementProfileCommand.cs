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
    /// Get Measurement Profile Command value object class.
    ///
    /// Cluster: Electrical Measurement. Command ID 0x01 is sent TO the server.
    /// This command is a specific command used for the Electrical Measurement cluster.
    ///
    /// Retrieves an electricity measurement profile from the electricity measurement
    /// server for a specific attribute ID requested.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class GetMeasurementProfileCommand : ZclCommand
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
        /// Attribute ID command message field.
        /// </summary>
        public ushort AttributeId { get; set; }

        /// <summary>
        /// Start Time command message field.
        /// </summary>
        public uint StartTime { get; set; }

        /// <summary>
        /// Number Of Intervals command message field.
        /// </summary>
        public byte NumberOfIntervals { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public GetMeasurementProfileCommand()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(AttributeId, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            serializer.Serialize(StartTime, ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER));
            serializer.Serialize(NumberOfIntervals, ZclDataType.Get(DataType.ENUMERATION_8_BIT));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            AttributeId = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            StartTime = deserializer.Deserialize<uint>(ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER));
            NumberOfIntervals = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.ENUMERATION_8_BIT));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("GetMeasurementProfileCommand [");
            builder.Append(base.ToString());
            builder.Append(", AttributeId=");
            builder.Append(AttributeId);
            builder.Append(", StartTime=");
            builder.Append(StartTime);
            builder.Append(", NumberOfIntervals=");
            builder.Append(NumberOfIntervals);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
