using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.ZCL;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.Serialization
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
