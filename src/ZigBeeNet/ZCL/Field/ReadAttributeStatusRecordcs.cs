using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.Serialization;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.ZCL.Field
{
    public class ReadAttributeStatusRecord : IZclListItemField
    {
        /**
         * The attribute identifier.
         */
        public ushort AttributeIdentifier { get; private set; }

        /**
         * The status.
         */
        public ZclStatus Status { get; private set; }

        /**
         * The attribute data type.
         */
        public ZclDataType AttributeDataType { get; private set; }

        /**
         * The attribute data type.
         */
        public object AttributeValue { get; private set; }


        public void Serialize(IZigBeeSerializer serializer)
        {
            serializer.AppendZigBeeType(AttributeIdentifier, DataType.UNSIGNED_16_BIT_INTEGER);
            serializer.AppendZigBeeType(Status, DataType.ZCL_STATUS);
            serializer.AppendZigBeeType((byte)AttributeDataType.Id, DataType.UNSIGNED_8_BIT_INTEGER);
            serializer.AppendZigBeeType(AttributeValue, AttributeDataType.DataType);
        }

        public void Deserialize(IZigBeeDeserializer deserializer)
        {
            AttributeIdentifier = deserializer.ReadZigBeeType<ushort>(DataType.UNSIGNED_16_BIT_INTEGER);
            Status = deserializer.ReadZigBeeType<ZclStatus>(DataType.ZCL_STATUS);
            if (Status == ZclStatus.SUCCESS)
            {
                AttributeDataType = ZclDataType.Get(deserializer.ReadZigBeeType<byte>(DataType.UNSIGNED_8_BIT_INTEGER));
                AttributeValue = deserializer.ReadZigBeeType<object>(AttributeDataType.DataType);
            }
        }

        public override string ToString()
        {
            return "ReadAttributeStatusRecord [attributeDataType=" + AttributeDataType + ", attributeIdentifier="
                    + AttributeIdentifier + ", status=" + Status + ", attributeValue=" + AttributeValue + "]";
        }
    }
}
