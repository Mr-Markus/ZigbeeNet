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
    /// Alarm Command value object class.
    /// <para>
    /// Cluster: Alarms. Command is sent FROM the server.
    /// This command is a specific command used for the Alarms cluster.
    ///
    /// The alarm command signals an alarm situation on the sending device.
    /// <br>
    /// An alarm command is generated when a  cluster  which has alarm functionality detects an alarm
    /// condition, e.g., an attribute has taken on a value that is outside a ‘safe’ range. The details
    /// are given by individual cluster specifications.
    /// </para>
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class AlarmCommand : ZclCommand
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
        public AlarmCommand()
        {
            GenericCommand = false;
            ClusterId = 9;
            CommandId = 0;
            CommandDirection = ZclCommandDirection.SERVER_TO_CLIENT;
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

            builder.Append("AlarmCommand [");
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
