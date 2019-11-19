using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.ZCL;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.Serialization
{
    public interface IZigBeeSerializable
    {
        void Serialize(ZclFieldSerializer serializer);

        void Deserialize(ZclFieldDeserializer deserializer);
    }

}
