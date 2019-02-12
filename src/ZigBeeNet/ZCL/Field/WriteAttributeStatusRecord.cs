using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.Serialization;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.ZCL.Field
{
    public class WriteAttributeStatusRecord : IZclListItemField
    {
        /**
         * The status.
         */
        public byte Status;
        /**
         * The attribute identifier.
         */
        public ushort AttributeIdentifier;


        public void Serialize(IZigBeeSerializer serializer)
        {
            serializer.AppendZigBeeType(Status, DataType.UNSIGNED_8_BIT_INTEGER);
            serializer.AppendZigBeeType(AttributeIdentifier, DataType.UNSIGNED_16_BIT_INTEGER);
        }

        public void Deserialize(IZigBeeDeserializer deserializer)
        {
            Status = deserializer.ReadZigBeeType<byte>(DataType.UNSIGNED_8_BIT_INTEGER);
            if (Status != 0)
            {
                AttributeIdentifier = deserializer.ReadZigBeeType<ushort>(DataType.UNSIGNED_16_BIT_INTEGER);
            }
        }

        public override string ToString()
        {
            return "Write Attribute Status Record: status=" + Status + ", attributeIdentifier=" + AttributeIdentifier;
        }
    }
}
