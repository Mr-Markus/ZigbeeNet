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
        public ZclDataType AttributeDataType { get; set; }

        /**
         * The ZigBee attribute identifier number within the cluster.
         */
        public ushort Identifier { get; set; }



        public void Serialize(IZigBeeSerializer serializer)
        {
            serializer.AppendZigBeeType(Identifier, DataType.UNSIGNED_16_BIT_INTEGER);
            serializer.AppendZigBeeType(AttributeDataType, DataType.UNSIGNED_8_BIT_INTEGER);
        }

        public void Deserialize(IZigBeeDeserializer deserializer)
        {
            Identifier = deserializer.ReadZigBeeType<ushort>(DataType.UNSIGNED_16_BIT_INTEGER);
            AttributeDataType = deserializer.ReadZigBeeType<ZclDataType>(DataType.ZIGBEE_DATA_TYPE);
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
