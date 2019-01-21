using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.ZCL.Clusters.LevelControl
{
    public class MoveCommand : ZclCommand
    {
        /**
         * Move mode command message field.
         */
        private int moveMode;

        /**
         * Rate command message field.
         */
        private int rate;

        /**
         * Default constructor.
         */
        public MoveCommand()
        {
            GenericCommand = false;
            ClusterId = 8;
            CommandId = 1;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        /**
         * Gets Move mode.
         *
         * @return the Move mode
         */
        public int getMoveMode()
        {
            return moveMode;
        }

        /**
         * Sets Move mode.
         *
         * @param moveMode the Move mode
         */
        public void SetMoveMode(int moveMode)
        {
            this.moveMode = moveMode;
        }

        /**
         * Gets Rate.
         *
         * @return the Rate
         */
        public int getRate()
        {
            return rate;
        }

        /**
         * Sets Rate.
         *
         * @param rate the Rate
         */
        public void SetRate(int rate)
        {
            this.rate = rate;
        }

        public override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(moveMode, ZclDataType.Get(DataType.ENUMERATION_8_BIT));
            serializer.Serialize(rate, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
        }

        public override void Deserialize(ZclFieldDeserializer deserializer)
        {
            moveMode = (int)deserializer.Deserialize(ZclDataType.Get(DataType.ENUMERATION_8_BIT));
            rate = (int)deserializer.Deserialize(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder(66);
            builder.Append("MoveCommand [");
            builder.Append(base.ToString());
            builder.Append(", moveMode=");
            builder.Append(moveMode);
            builder.Append(", rate=");
            builder.Append(rate);
            builder.Append(']');
            return builder.ToString();
        }
    }
}
