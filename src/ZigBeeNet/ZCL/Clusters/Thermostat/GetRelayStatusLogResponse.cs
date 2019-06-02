// License text here
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.ZCL.Protocol;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Clusters.Thermostat;


namespace ZigBeeNet.ZCL.Clusters.Thermostat
{
    /// <summary>
    /// Get Relay Status Log Response value object class.
    /// <para>
    /// Cluster: Thermostat. Command is sent FROM the server.
    /// This command is a specific command used for the Thermostat cluster.
    /// </para>
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class GetRelayStatusLogResponse : ZclCommand
    {
        /// <summary>
        /// Time of day command message field.
        /// </summary>
        public ushort TimeOfDay { get; set; }

        /// <summary>
        /// Relay Status command message field.
        /// </summary>
        public byte RelayStatus { get; set; }

        /// <summary>
        /// Local Temperature command message field.
        /// </summary>
        public ushort LocalTemperature { get; set; }

        /// <summary>
        /// Humidity command message field.
        /// </summary>
        public byte Humidity { get; set; }

        /// <summary>
        /// Setpoint command message field.
        /// </summary>
        public ushort Setpoint { get; set; }

        /// <summary>
        /// Unread Entries command message field.
        /// </summary>
        public ushort UnreadEntries { get; set; }


        /// <summary>
        /// Default constructor.
        /// </summary>
        public GetRelayStatusLogResponse()
        {
            GenericCommand = false;
            ClusterId = 513;
            CommandId = 1;
            CommandDirection = ZclCommandDirection.SERVER_TO_CLIENT;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(TimeOfDay, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            serializer.Serialize(RelayStatus, ZclDataType.Get(DataType.BITMAP_8_BIT));
            serializer.Serialize(LocalTemperature, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            serializer.Serialize(Humidity, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            serializer.Serialize(Setpoint, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            serializer.Serialize(UnreadEntries, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            TimeOfDay = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            RelayStatus = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.BITMAP_8_BIT));
            LocalTemperature = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            Humidity = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            Setpoint = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            UnreadEntries = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("GetRelayStatusLogResponse [");
            builder.Append(base.ToString());
            builder.Append(", TimeOfDay=");
            builder.Append(TimeOfDay);
            builder.Append(", RelayStatus=");
            builder.Append(RelayStatus);
            builder.Append(", LocalTemperature=");
            builder.Append(LocalTemperature);
            builder.Append(", Humidity=");
            builder.Append(Humidity);
            builder.Append(", Setpoint=");
            builder.Append(Setpoint);
            builder.Append(", UnreadEntries=");
            builder.Append(UnreadEntries);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
