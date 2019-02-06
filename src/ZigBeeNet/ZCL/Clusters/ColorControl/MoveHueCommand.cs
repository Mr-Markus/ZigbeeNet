using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.ZCL.Clusters.ColorControl
{
    public class MoveHueCommand : ZclCommand
    {
        public byte MoveMode { get; set; }

        public byte Rate { get; set; }

        public MoveHueCommand()
        {
            GenericCommand = false;
            ClusterId = 768;
            CommandId = 1;
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
            StringBuilder builder = new StringBuilder(69);
                builder.Append("MoveHueCommand [");
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
