// License text here
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.ZCL.Protocol;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Clusters.Alarms;


namespace ZigBeeNet.ZCL.Clusters.Alarms
{
    /// <summary>
    /// Reset Alarm Command value object class.
    /// <para>
    /// Cluster: Alarms. Command is sent TO the server.
    /// This command is a specific command used for the Alarms cluster.
    /// </para>
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class ResetAlarmCommand : ZclCommand
    {
        /// <summary>
        /// Alarm code command message field.
        /// </summary>
        public byte AlarmCode { get; set; }

        /// <summary>
        /// Cluster identifier command message field.
        /// </summary>
        public ushort ClusterIdentifier { get; set; }


        /// <summary>
        /// Default constructor.
        /// </summary>
        public ResetAlarmCommand()
        {
            GenericCommand = false;
            ClusterId = 9;
            CommandId = 0;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(AlarmCode, ZclDataType.Get(DataType.ENUMERATION_8_BIT));
            serializer.Serialize(ClusterIdentifier, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            AlarmCode = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.ENUMERATION_8_BIT));
            ClusterIdentifier = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("ResetAlarmCommand [");
            builder.Append(base.ToString());
            builder.Append(", AlarmCode=");
            builder.Append(AlarmCode);
            builder.Append(", ClusterIdentifier=");
            builder.Append(ClusterIdentifier);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
