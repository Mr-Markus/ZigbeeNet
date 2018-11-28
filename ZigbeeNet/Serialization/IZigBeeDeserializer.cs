using System;
using System.Collections.Generic;
using System.Text;
using ZigbeeNet.ZCL;

namespace ZigbeeNet.Serialization
{
    public interface IZigBeeDeserializer
    {
        bool IsEndOfStream();

        object ReadZigBeeType(ZclDataType type);

        int GetPosition();

        void Skip(int bytes);

        int GetSize();
    }
}
