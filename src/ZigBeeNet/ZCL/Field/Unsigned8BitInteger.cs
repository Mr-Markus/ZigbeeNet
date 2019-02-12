using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.Serialization;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.ZCL.Field
{
    public class Unsigned8BitInteger : IZclListItemField
    {
    /**
     * The attribute identifier.
     */
    public byte Value { get; private set; }


    public void Serialize(IZigBeeSerializer serializer)
    {
        serializer.AppendZigBeeType(Value, DataType.UNSIGNED_8_BIT_INTEGER);
    }

    public void Deserialize(IZigBeeDeserializer deserializer)
    {
        Value = deserializer.ReadZigBeeType<byte>(DataType.UNSIGNED_8_BIT_INTEGER);
    }

    public override string ToString()
    {
        return "Unsigned 8 Bit Integer: value=" + Value;
    }
}
}
