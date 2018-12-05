using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.Serialization;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.ZCL.Fileld
{
    public class AttributeStatusRecord : IZclListItemField
    {
    /**
     * The status.
     */
    public ZclStatus Status { get; private set; }
        /**
         * The direction.
         */
        public bool Direction { get; private set; }
        /**
         * The attribute identifier.
         */
        public int AttributeIdentifier { get; private set; }



    public void Serialize(IZigBeeSerializer serializer)
    {
        serializer.AppendZigBeeType(Status, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
        if (Status == ZclStatus.SUCCESS)
        {
            serializer.AppendZigBeeType(Direction, ZclDataType.Get(DataType.BOOLEAN));
            serializer.AppendZigBeeType(AttributeIdentifier, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
        }
    }

    public void Deserialize(IZigBeeDeserializer deserializer)
    {
        Status = (ZclStatus)deserializer.ReadZigBeeType(ZclDataType.Get(DataType.ZCL_STATUS));
        Direction = (bool)deserializer.ReadZigBeeType(ZclDataType.Get(DataType.BOOLEAN));
        AttributeIdentifier = (int)deserializer.ReadZigBeeType(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
    }

    public override string ToString()
    {
        return "Attribute Status Record: status=" + Status + ", direction=" + Direction + ", attributeIdentifier="
                + AttributeIdentifier;
    }
}
}
