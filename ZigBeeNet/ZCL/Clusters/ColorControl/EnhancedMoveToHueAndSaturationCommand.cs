using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.ZCL.Clusters.ColorControl
{
    public class EnhancedMoveToHueAndSaturationCommand : ZclCommand
    {
        public ushort Hue { get; set; }

        public byte Saturation { get; set; }

        public ushort TransitionTime { get; set; }

        public EnhancedMoveToHueAndSaturationCommand()
        {
            GenericCommand = false;
            ClusterId = 768;
            CommandId = 66;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        public override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(Hue, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            serializer.Serialize(Saturation, ZclDataType.Get(DataType.ENUMERATION_8_BIT));
            serializer.Serialize(TransitionTime, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
        }

        public override void Deserialize(ZclFieldDeserializer deserializer)
        {
            Hue = (ushort)deserializer.Deserialize(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            Saturation = (byte)deserializer.Deserialize(ZclDataType.Get(DataType.ENUMERATION_8_BIT));
            TransitionTime = (ushort)deserializer.Deserialize(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder(127);
                builder.Append("EnhancedMoveToHueAndSaturationCommand [");
                builder.Append(base.ToString());
                builder.Append(", hue=");
                builder.Append(Hue);
                builder.Append(", saturation=");
                builder.Append(Saturation);
                builder.Append(", transitionTime=");
                builder.Append(TransitionTime);
                builder.Append(']');
            return builder.ToString();
        }
    }
}
