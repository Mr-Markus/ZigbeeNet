using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.Serialization;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.ZCL.Field
{
    public class AttributeRecord : IZclListItemField
    {
    /// <summary>
     /// The direction.
     /// <p>
     /// The direction field specifies whether values of the attribute are reported (0x00), or
     /// whether reports of the attribute are received (0x01).
     ///
     /// </summary>
    public byte Direction { get; set; }

    /// <summary>
     /// The attribute identifier.
     /// </summary>
    public ushort AttributeIdentifier { get; set; }


    public void Serialize(IZigBeeSerializer serializer)
    {
        serializer.AppendZigBeeType(Direction, DataType.UNSIGNED_8_BIT_INTEGER);
        serializer.AppendZigBeeType(AttributeIdentifier, DataType.UNSIGNED_16_BIT_INTEGER);
    }

    public void Deserialize(IZigBeeDeserializer deserializer)
    {
        Direction = deserializer.ReadZigBeeType<byte>(DataType.UNSIGNED_8_BIT_INTEGER);
        AttributeIdentifier = deserializer.ReadZigBeeType<ushort>(DataType.UNSIGNED_16_BIT_INTEGER);
    }

    public override string ToString()
    {
        return "Attribute Record[ direction=" + Direction + ", attributeIdentifier=" + AttributeIdentifier + "]";
    }
}
}
