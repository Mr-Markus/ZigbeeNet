using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.ZCL.Clusters.ColorControl
{
    public class EnhancedStepHueCommand : ZclCommand
    {
        public byte StepMode { get; set; }

        public ushort StepSize { get; set; }

        public ushort TransitionTime { get; set; }

        public EnhancedStepHueCommand()
        {
            GenericCommand = false;
            ClusterId = 768;
            CommandId = 65;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        public override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(StepMode, ZclDataType.Get(DataType.ENUMERATION_8_BIT));
            serializer.Serialize(StepSize, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            serializer.Serialize(TransitionTime, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
        }

        public override void Deserialize(ZclFieldDeserializer deserializer)
        {
            StepMode = (byte)deserializer.Deserialize(ZclDataType.Get(DataType.ENUMERATION_8_BIT));
            StepSize = (ushort)deserializer.Deserialize(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            TransitionTime = (ushort)deserializer.Deserialize(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder(115);
                builder.Append("EnhancedStepHueCommand [");
                builder.Append(base.ToString());
                builder.Append(", stepMode=");
                builder.Append(StepMode);
                builder.Append(", stepSize=");
                builder.Append(StepSize);
                builder.Append(", transitionTime=");
                builder.Append(TransitionTime);
                builder.Append(']');
            return builder.ToString();
        }
    }
}
