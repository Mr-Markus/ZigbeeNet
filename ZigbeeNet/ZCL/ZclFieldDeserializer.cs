using System;
using System.Collections.Generic;
using System.Text;
using ZigbeeNet.Serialization;
using ZigbeeNet.ZCL.Protocol;

namespace ZigbeeNet.ZCL
{
    public class ZclFieldDeserializer
    {
        public IZigBeeDeserializer Deserializer { get; private set; }

        public int RemainingLength
        {
            get
            {
                return Deserializer.GetSize() - Deserializer.GetPosition();
            }
        }

        public bool IsEndOfStream => Deserializer.IsEndOfStream();

        public ZclFieldDeserializer(IZigBeeDeserializer deserializer)
        {
            Deserializer = deserializer;
        }

        public object Deserialize(ZclDataType dataType)
        {
            throw new NotImplementedException();
        }
    }
}
