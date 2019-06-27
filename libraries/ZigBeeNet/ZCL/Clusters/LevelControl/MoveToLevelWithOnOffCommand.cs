// License text here
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.ZCL.Protocol;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Clusters.LevelControl;


namespace ZigBeeNet.ZCL.Clusters.LevelControl
{
    /// <summary>
    /// Move to Level (with On/Off) Command value object class.
    /// <para>
    /// Cluster: Level Control. Command is sent TO the server.
    /// This command is a specific command used for the Level Control cluster.
    /// </para>
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class MoveToLevelWithOnOffCommand : ZclCommand
    {
        /// <summary>
        /// Level command message field.
        /// </summary>
        public byte Level { get; set; }

        /// <summary>
        /// Transition time command message field.
        /// </summary>
        public ushort TransitionTime { get; set; }


        /// <summary>
        /// Default constructor.
        /// </summary>
        public MoveToLevelWithOnOffCommand()
        {
            GenericCommand = false;
            ClusterId = 8;
            CommandId = 4;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(Level, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            serializer.Serialize(TransitionTime, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            Level = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            TransitionTime = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("MoveToLevelWithOnOffCommand [");
            builder.Append(base.ToString());
            builder.Append(", Level=");
            builder.Append(Level);
            builder.Append(", TransitionTime=");
            builder.Append(TransitionTime);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
