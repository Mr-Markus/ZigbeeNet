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
    public int AttributeIdentifier;
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
        serializer.AppendZigBeeType(AttributeIdentifier, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
        serializer.AppendZigBeeType(AttributeDataType.Id, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
        serializer.AppendZigBeeType(AttributeValue, AttributeDataType);
    }

    public void Deserialize(IZigBeeDeserializer deserializer)
    {
        AttributeIdentifier = (int)deserializer.ReadZigBeeType(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
        AttributeDataType = ZclDataType.Get((int)deserializer.ReadZigBeeType(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER)));
        AttributeValue = deserializer.ReadZigBeeType(AttributeDataType);
    }

    public override string ToString()
    {
        return "Write Attribute Record: attributeDataType=" + AttributeDataType + ", attributeIdentifier="
                + AttributeIdentifier + ", attributeValue=" + AttributeValue;
    }
}
}
