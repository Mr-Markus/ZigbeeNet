using System;
using System.Collections.Generic;
using System.Text;
using ZigbeeNet.Serialization;
using ZigbeeNet.ZCL.Protocol;

namespace ZigbeeNet.ZCL.Fileld
{
    public class WriteAttributeStatusRecord : IZclListItemField
    {
        /**
         * The status.
         */
        public int Status;
        /**
         * The attribute identifier.
         */
        public int AttributeIdentifier;


        public void Serialize(IZigBeeSerializer serializer)
        {
            serializer.AppendZigBeeType(Status, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            serializer.AppendZigBeeType(AttributeIdentifier, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
        }

        public void Deserialize(IZigBeeDeserializer deserializer)
        {
            Status = (int)deserializer.ReadZigBeeType(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            if (Status != 0)
            {
                AttributeIdentifier = (int)deserializer.ReadZigBeeType(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            }
        }

        public override string ToString()
        {
            return "Write Attribute Status Record: status=" + Status + ", attributeIdentifier=" + AttributeIdentifier;
        }
    }
}
