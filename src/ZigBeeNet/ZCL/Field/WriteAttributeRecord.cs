using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.Serialization;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.ZCL.Field
{
    public class WriteAttributeRecord : IZclListItemField
    {
    /**
     * The attribute identifier.
     */
    public ushort AttributeIdentifier;
        /**
         * The attribute data type.
         */
        public ZclDataType AttributeDataType;

        /**
         * The attribute data type.
         */
        public object AttributeValue;


    public void Serialize(IZigBeeSerializer serializer)
    {
        serializer.AppendZigBeeType(AttributeIdentifier, DataType.UNSIGNED_16_BIT_INTEGER);
        serializer.AppendZigBeeType(AttributeDataType.Id, DataType.UNSIGNED_8_BIT_INTEGER);
        serializer.AppendZigBeeType(AttributeValue, AttributeDataType.DataType);
    }

    public void Deserialize(IZigBeeDeserializer deserializer)
    {
        AttributeIdentifier = deserializer.ReadZigBeeType<ushort>(DataType.UNSIGNED_16_BIT_INTEGER);
        AttributeDataType = ZclDataType.Get(deserializer.ReadZigBeeType<byte>(DataType.UNSIGNED_8_BIT_INTEGER));
        AttributeValue = deserializer.ReadZigBeeType<object>(AttributeDataType.DataType);
    }

    public override string ToString()
    {
        return "Write Attribute Record: attributeDataType=" + AttributeDataType + ", attributeIdentifier="
                + AttributeIdentifier + ", attributeValue=" + AttributeValue;
    }
}
}
