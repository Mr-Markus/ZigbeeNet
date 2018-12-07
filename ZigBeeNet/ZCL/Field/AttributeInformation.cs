using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.Serialization;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.ZCL.Field
{
    public class AttributeInformation : IZclListItemField, IComparable<AttributeInformation>
    {
        /**
         * The ZigBee attribute data type.
         */
        public ZclDataType AttributeDataType { get; private set; }

        /**
         * The ZigBee attribute identifier number within the cluster.
         */
        public int Identifier { get; private set; }



        public void Serialize(IZigBeeSerializer serializer)
        {
            serializer.AppendZigBeeType(Identifier, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            serializer.AppendZigBeeType(AttributeDataType, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
        }

        public void Deserialize(IZigBeeDeserializer deserializer)
        {
            Identifier = (int)deserializer.ReadZigBeeType(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            AttributeDataType = (ZclDataType)deserializer.ReadZigBeeType(ZclDataType.Get(DataType.ZIGBEE_DATA_TYPE));
        }

        public int CompareTo(AttributeInformation other)
        {
            return Identifier.CompareTo(other.Identifier);
        }

        public override string ToString()
        {
            return "Attribute Information [dataType=" + AttributeDataType + ", identifier=" + Identifier + "]";
        }

    }
}
