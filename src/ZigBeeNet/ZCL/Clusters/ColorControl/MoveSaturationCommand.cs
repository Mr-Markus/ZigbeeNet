using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.ZCL.Clusters.ColorControl
{
    public class MoveSaturationCommand : ZclCommand
    {
        public byte MoveMode { get; set; }

        public byte Rate { get; set; }

        public MoveSaturationCommand()
        {
            GenericCommand = false;
            ClusterId = 768;
            CommandId = 4;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
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
            StringBuilder builder = new StringBuilder(76);
                builder.Append("MoveSaturationCommand [");
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
