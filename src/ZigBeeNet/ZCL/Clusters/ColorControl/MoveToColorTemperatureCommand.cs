using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.ZCL.Clusters.ColorControl
{
    public class MoveToColorTemperatureCommand : ZclCommand
    {
        public ushort ColorTemperature { get; set; }

        public ushort TransitionTime { get; set; }

        public MoveToColorTemperatureCommand()
        {
            GenericCommand = false;
            ClusterId = 768;
            CommandId = 10;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        public override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(ColorTemperature, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            serializer.Serialize(TransitionTime, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
        }

        public override void Deserialize(ZclFieldDeserializer deserializer)
        {
            ColorTemperature = (ushort)deserializer.Deserialize(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            TransitionTime = (ushort)deserializer.Deserialize(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder(102);
                builder.Append("MoveToColorTemperatureCommand [");
                builder.Append(base.ToString());
                builder.Append(", colorTemperature=");
                builder.Append(ColorTemperature);
                builder.Append(", transitionTime=");
                builder.Append(TransitionTime);
                builder.Append(']');
            return builder.ToString();
        }
    }
}
