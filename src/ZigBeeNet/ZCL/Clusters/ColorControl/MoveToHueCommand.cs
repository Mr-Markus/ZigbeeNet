using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.ZCL.Clusters.ColorControl
{
    public class MoveToHueCommand : ZclCommand
    {
        public byte Hue { get; set; }

        public byte Direction { get; set; }

        public ushort TransitionTime { get; set; }

        public MoveToHueCommand()
        {
            GenericCommand = false;
            ClusterId = 768;
            CommandId = 0;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        public override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(Hue, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            serializer.Serialize(Direction, ZclDataType.Get(DataType.ENUMERATION_8_BIT));
            serializer.Serialize(TransitionTime, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
        }

        public override void Deserialize(ZclFieldDeserializer deserializer)
        {
            Hue = (byte)deserializer.Deserialize(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            Direction = (byte)deserializer.Deserialize(ZclDataType.Get(DataType.ENUMERATION_8_BIT));
            TransitionTime = (ushort)deserializer.Deserialize(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder(105);
                builder.Append("MoveToHueCommand [");
                builder.Append(base.ToString());
                builder.Append(", hue=");
                builder.Append(Hue);
                builder.Append(", direction=");
                builder.Append(Direction);
                builder.Append(", transitionTime=");
                builder.Append(TransitionTime);
                builder.Append(']');
            return builder.ToString();
        }
    }
}
