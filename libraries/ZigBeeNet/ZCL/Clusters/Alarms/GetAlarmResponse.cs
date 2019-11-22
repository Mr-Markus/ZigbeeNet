using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.Security;
using ZigBeeNet.ZCL.Clusters.Alarms;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Protocol;


namespace ZigBeeNet.ZCL.Clusters.Alarms
{
    /// <summary>
    /// Get Alarm Response value object class.
    ///
    /// Cluster: Alarms. Command ID 0x01 is sent FROM the server.
    /// This command is a specific command used for the Alarms cluster.
    ///
    /// If there is at least one alarm record in the alarm table then the status field is set to
    /// SUCCESS. The alarm code, cluster identifier and time stamp fields shall all be present
    /// and shall take their values from the item in the alarm table that they are reporting.If
    /// there are no more alarms logged in the alarm table then the status field is set to
    /// NOT_FOUND and the alarm code, cluster identifier and time stamp fields shall be
    /// omitted.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class GetAlarmResponse : ZclCommand
    {
        /// <summary>
        /// The cluster ID to which this command belongs.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0009;

        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x01;

        /// <summary>
        /// Status command message field.
        /// </summary>
        public byte Status { get; set; }

        /// <summary>
        /// Alarm Code command message field.
        /// </summary>
        public byte AlarmCode { get; set; }

        /// <summary>
        /// Cluster Identifier command message field.
        /// </summary>
        public ushort ClusterIdentifier { get; set; }

        /// <summary>
        /// Timestamp command message field.
        /// </summary>
        public uint Timestamp { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public GetAlarmResponse()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
            CommandDirection = ZclCommandDirection.SERVER_TO_CLIENT;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(Status, ZclDataType.Get(DataType.ENUMERATION_8_BIT));
            serializer.Serialize(AlarmCode, ZclDataType.Get(DataType.ENUMERATION_8_BIT));
            serializer.Serialize(ClusterIdentifier, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            serializer.Serialize(Timestamp, ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            Status = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.ENUMERATION_8_BIT));
            AlarmCode = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.ENUMERATION_8_BIT));
            ClusterIdentifier = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            Timestamp = deserializer.Deserialize<uint>(ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("GetAlarmResponse [");
            builder.Append(base.ToString());
            builder.Append(", Status=");
            builder.Append(Status);
            builder.Append(", AlarmCode=");
            builder.Append(AlarmCode);
            builder.Append(", ClusterIdentifier=");
            builder.Append(ClusterIdentifier);
            builder.Append(", Timestamp=");
            builder.Append(Timestamp);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
