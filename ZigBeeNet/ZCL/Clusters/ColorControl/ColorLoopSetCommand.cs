using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.ZCL.Clusters.ColorControl
{
    public class ColorLoopSetCommand : ZclCommand
    {
        public byte UpdateFlags { get; set; }

        public byte Action { get; set; }

        public byte Direction { get; set; }

        public ushort TransitionTime { get; set; }

        public ushort StartHue { get; set; }

        public ColorLoopSetCommand()
        {
            GenericCommand = false;
            ClusterId = 768;
            CommandId = 67;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        public override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(UpdateFlags, ZclDataType.Get(DataType.BITMAP_8_BIT));
            serializer.Serialize(Action, ZclDataType.Get(DataType.ENUMERATION_8_BIT));
            serializer.Serialize(Direction, ZclDataType.Get(DataType.ENUMERATION_8_BIT));
            serializer.Serialize(TransitionTime, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            serializer.Serialize(StartHue, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
        }

        public override void Deserialize(ZclFieldDeserializer deserializer)
        {
            UpdateFlags = (byte)deserializer.Deserialize(ZclDataType.Get(DataType.BITMAP_8_BIT));
            Action = (byte)deserializer.Deserialize(ZclDataType.Get(DataType.ENUMERATION_8_BIT));
            Direction = (byte)deserializer.Deserialize(ZclDataType.Get(DataType.ENUMERATION_8_BIT));
            TransitionTime = (ushort)deserializer.Deserialize(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            StartHue = (ushort)deserializer.Deserialize(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder(170);
                builder.Append("ColorLoopSetCommand [");
                builder.Append(base.ToString());
                builder.Append(", updateFlags=");
                builder.Append(UpdateFlags);
                builder.Append(", action=");
                builder.Append(Action);
                builder.Append(", direction=");
                builder.Append(Direction);
                builder.Append(", transitionTime=");
                builder.Append(TransitionTime);
                builder.Append(", startHue=");
                builder.Append(StartHue);
                builder.Append(']');

            return builder.ToString();
        }
    }
}
