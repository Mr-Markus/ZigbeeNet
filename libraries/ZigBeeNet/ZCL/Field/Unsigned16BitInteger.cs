using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.Serialization;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.ZCL.Field
{
    public class Unsigned16BitInteger : IZclListItemField
    {
    /// <summary>
     /// The attribute identifier.
     /// </summary>
    public ushort Value { get; private set; }


    public void Serialize(IZigBeeSerializer serializer)
    {
        serializer.AppendZigBeeType(Value, DataType.UNSIGNED_16_BIT_INTEGER);
    }

    public void Deserialize(IZigBeeDeserializer deserializer)
    {
        Value = deserializer.ReadZigBeeType<ushort>(DataType.UNSIGNED_16_BIT_INTEGER);
    }

    public override string ToString()
    {
        return "Unsigned 16 Bit Integer: value=" + Value;
    }
}
}
