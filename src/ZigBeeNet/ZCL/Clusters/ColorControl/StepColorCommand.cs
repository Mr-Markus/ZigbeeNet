using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.ZCL.Clusters.ColorControl
{
    public class StepColorCommand : ZclCommand
    {
        public short StepX { get; set; }

        public short StepY { get; set; }

        public ushort TransitionTime { get; set; }

        public StepColorCommand()
        {
            GenericCommand = false;
            ClusterId = 768;
            CommandId = 9;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        public override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(StepX, ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER));
            serializer.Serialize(StepY, ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER));
            serializer.Serialize(TransitionTime, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
        }

        public override void Deserialize(ZclFieldDeserializer deserializer)
        {
            StepX = (short)deserializer.Deserialize(ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER));
            StepY = (short)deserializer.Deserialize(ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER));
            TransitionTime = (ushort)deserializer.Deserialize(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder(103);
                builder.Append("StepColorCommand [");
                builder.Append(base.ToString());
                builder.Append(", stepX=");
                builder.Append(StepX);
                builder.Append(", stepY=");
                builder.Append(StepY);
                builder.Append(", transitionTime=");
                builder.Append(TransitionTime);
                builder.Append(']');
            return builder.ToString();
        }
    }
}
