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
        public byte MoveMode { get; private set; }

        /**
         * Rate command message field.
         */
        public byte Rate { get; private set; }

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

        public MoveCommand(byte moveMode, byte rate) : this()
        {
            this.MoveMode = moveMode;
            this.Rate = rate;
        }

        public override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(MoveMode, ZclDataType.Get(DataType.ENUMERATION_8_BIT));
            serializer.Serialize(Rate, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
        }

        public override void Deserialize(ZclFieldDeserializer deserializer)
        {
            MoveMode = (byte)deserializer.Deserialize(ZclDataType.Get(DataType.ENUMERATION_8_BIT));
            Rate = (byte)deserializer.Deserialize(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder(66);
            builder.Append("MoveCommand [");
            builder.Append(base.ToString());
            builder.Append(", moveMode=");
            builder.Append(MoveMode);
            builder.Append(", rate=");
            builder.Append(Rate);
            builder.Append(']');
            return builder.ToString();
        }
    }
}
