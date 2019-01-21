using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.ZCL.Clusters.LevelControl
{
    public class MoveToLevelCommand : ZclCommand
    {
        /**
         * Level command message field.
         */
        private byte level;

        /**
         * Transition time command message field.
         */
        private ushort transitionTime;

        /**
         * Default constructor.
         */
        public MoveToLevelCommand()
        {
            GenericCommand = false;
            ClusterId = 8;
            CommandId = 0;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        /**
         * Gets Level.
         *
         * @return the Level
         */
        public int getLevel()
        {
            return level;
        }

        /**
         * Sets Level.
         *
         * @param level the Level
         */
        public void SetLevel(byte level)
        {
            this.level = level;
        }

        /**
         * Gets Transition time.
         *
         * @return the Transition time
         */
        public int getTransitionTime()
        {
            return transitionTime;
        }

        /**
         * Sets Transition time.
         *
         * @param transitionTime the Transition time
         */
        public void SetTransitionTime(ushort transitionTime)
        {
            this.transitionTime = transitionTime;
        }

        public override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(level, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            serializer.Serialize(transitionTime, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
        }

        public override void Deserialize(ZclFieldDeserializer deserializer)
        {
            level = (byte)deserializer.Deserialize(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            transitionTime = (ushort)deserializer.Deserialize(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder(80);
            builder.Append("MoveToLevelCommand [");
            builder.Append(base.ToString());
            builder.Append(", level=");
            builder.Append(level);
            builder.Append(", transitionTime=");
            builder.Append(transitionTime);
            builder.Append(']');
            return builder.ToString();
        }
    }
}
