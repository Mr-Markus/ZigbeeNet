using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.ZCL.Clusters.ColorControl
{
    public class StepSaturationCommand : ZclCommand
    {
        public byte StepMode { get; set; }

        public byte StepSize { get; set; }

        public byte TransitionTime { get; set; }


        public StepSaturationCommand()
        {
            GenericCommand = false;
            ClusterId = 768;
            CommandId = 5;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        public override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(StepMode, ZclDataType.Get(DataType.ENUMERATION_8_BIT));
            serializer.Serialize(StepSize, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            serializer.Serialize(TransitionTime, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
        }

        public override void Deserialize(ZclFieldDeserializer deserializer)
        {
            StepMode = (byte)deserializer.Deserialize(ZclDataType.Get(DataType.ENUMERATION_8_BIT));
            StepSize = (byte)deserializer.Deserialize(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            TransitionTime = (byte)deserializer.Deserialize(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
        }

        public override String ToString()
        {
            StringBuilder builder = new StringBuilder(114);
                builder.Append("StepSaturationCommand [");
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
