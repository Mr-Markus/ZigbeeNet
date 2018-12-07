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
        public int AttributeIdentifier { get; private set; }

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
            serializer.AppendZigBeeType((short)AttributeIdentifier, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            serializer.AppendZigBeeType(Status, ZclDataType.Get(DataType.ZCL_STATUS));
            serializer.AppendZigBeeType((byte)AttributeDataType.Id, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            serializer.AppendZigBeeType(AttributeValue, AttributeDataType);
        }

        public void Deserialize(IZigBeeDeserializer deserializer)
        {
            AttributeIdentifier = (int)deserializer.ReadZigBeeType(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            Status = (ZclStatus)deserializer.ReadZigBeeType(ZclDataType.Get(DataType.ZCL_STATUS));
            if (Status == ZclStatus.SUCCESS)
            {
                AttributeDataType = ZclDataType.Get((int)deserializer.ReadZigBeeType(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER)));
                AttributeValue = deserializer.ReadZigBeeType(AttributeDataType);
            }
        }

        public override string ToString()
        {
            return "ReadAttributeStatusRecord [attributeDataType=" + AttributeDataType + ", attributeIdentifier="
                    + AttributeIdentifier + ", status=" + Status + ", attributeValue=" + AttributeValue + "]";
        }
    }
}
