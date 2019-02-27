using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.Serialization;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.ZCL.Field
{
    public class AttributeStatusRecord : IZclListItemField
    {
    /// <summary>
     /// The status.
     /// </summary>
    public ZclStatus Status { get; private set; }
        /// <summary>
         /// The direction.
         /// </summary>
        public bool Direction { get; private set; }
        /// <summary>
         /// The attribute identifier.
         /// </summary>
        public int AttributeIdentifier { get; private set; }



    public void Serialize(IZigBeeSerializer serializer)
    {
        serializer.AppendZigBeeType(Status, DataType.UNSIGNED_8_BIT_INTEGER);
        if (Status == ZclStatus.SUCCESS)
        {
            serializer.AppendZigBeeType(Direction, DataType.BOOLEAN);
            serializer.AppendZigBeeType(AttributeIdentifier, DataType.UNSIGNED_16_BIT_INTEGER);
        }
    }

    public void Deserialize(IZigBeeDeserializer deserializer)
    {
        Status = deserializer.ReadZigBeeType<ZclStatus>(DataType.ZCL_STATUS);
        Direction = deserializer.ReadZigBeeType<bool>(DataType.BOOLEAN);
        AttributeIdentifier = deserializer.ReadZigBeeType<ushort>(DataType.UNSIGNED_16_BIT_INTEGER);
    }

    public override string ToString()
    {
        return "Attribute Status Record: status=" + Status + ", direction=" + Direction + ", attributeIdentifier="
                + AttributeIdentifier;
    }
}
}
