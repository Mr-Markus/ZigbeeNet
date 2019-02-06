using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.ZCL.Clusters.ColorControl
{
    public class MoveColorCommand : ZclCommand
    {
        public short RateX { get; set; }

        public short RateY { get; set; }

        public MoveColorCommand()
        {
            GenericCommand = false;
            ClusterId = 768;
            CommandId = 8;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        public override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(RateX, ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER));
            serializer.Serialize(RateY, ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER));
        }

    public override void Deserialize(ZclFieldDeserializer deserializer)
        {
            RateX = (short)deserializer.Deserialize(ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER));
            RateY = (short)deserializer.Deserialize(ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER));
        }

    public override string ToString()
        {
            StringBuilder builder = new StringBuilder(69);
                builder.Append("MoveColorCommand [");
                builder.Append(base.ToString());
                builder.Append(", rateX=");
                builder.Append(RateX);
                builder.Append(", rateY=");
                builder.Append(RateY);
                builder.Append(']');
            return builder.ToString();
        }
    }
}
