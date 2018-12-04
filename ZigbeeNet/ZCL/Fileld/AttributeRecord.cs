using System;
using System.Collections.Generic;
using System.Text;
using ZigbeeNet.Serialization;
using ZigbeeNet.ZCL.Protocol;

namespace ZigbeeNet.ZCL.Fileld
{
    public class AttributeRecord : IZclListItemField
    {
    /**
     * The direction.
     * <p>
     * The direction field specifies whether values of the attribute are reported (0x00), or
     * whether reports of the attribute are received (0x01).
     *
     */
    public int Direction { get; private set; }

    /**
     * The attribute identifier.
     */
    public int AttributeIdentifier { get; private set; }


    public void Serialize(IZigBeeSerializer serializer)
    {
        serializer.AppendZigBeeType(Direction, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
        serializer.AppendZigBeeType(AttributeIdentifier, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
    }

    public void Deserialize(IZigBeeDeserializer deserializer)
    {
        Direction = (int)deserializer.ReadZigBeeType(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
        AttributeIdentifier = (int)deserializer.ReadZigBeeType(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
    }

    public override string ToString()
    {
        return "Attribute Record[ direction=" + Direction + ", attributeIdentifier=" + AttributeIdentifier + "]";
    }
}
}
