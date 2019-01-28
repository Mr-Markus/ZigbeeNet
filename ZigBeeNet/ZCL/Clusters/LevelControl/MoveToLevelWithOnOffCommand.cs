using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.ZCL.Clusters.LevelControl
{
    public class MoveToLevelWithOnOffCommand : ZclCommand
    {
        /**
         * Level command message field.
         */
        public byte Level { get; private set; }

        /**
         * Transition time command message field.
         */
        public ushort TransitionTime { get; private set; }

        /**
         * Default constructor.
         */
        public MoveToLevelWithOnOffCommand()
        {
            GenericCommand = false;
            ClusterId = 8;
            CommandId = 4;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        public MoveToLevelWithOnOffCommand(byte level, ushort transitionTime) : this()
        {
            Level = level;
            TransitionTime = transitionTime;
        }

        public override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(Level, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            serializer.Serialize(TransitionTime, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
        }

        public override void Deserialize(ZclFieldDeserializer deserializer)
        {
            Level = (byte)deserializer.Deserialize(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            TransitionTime = (ushort)deserializer.Deserialize(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder(89);
            builder.Append("MoveToLevelWithOnOffCommand [");
            builder.Append(base.ToString());
            builder.Append(", level=");
            builder.Append(Level);
            builder.Append(", transitionTime=");
            builder.Append(TransitionTime);
            builder.Append(']');
            return builder.ToString();
        }
    }
}
