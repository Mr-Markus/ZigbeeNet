using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.Serialization;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.ZCL.Field
{
    public class ReadAttributeStatusRecord : IZclListItemField
    {
        /// <summary>
        /// The attribute identifier.
        /// </summary>
        public ushort AttributeIdentifier { get; set; }

        /// <summary>
        /// The status.
        /// </summary>
        public ZclStatus Status { get; set; }

        /// <summary>
        /// The attribute data type.
        /// </summary>
        public ZclDataType AttributeDataType { get; set; }

        /// <summary>
        /// The attribute data type.
        /// </summary>
        public object AttributeValue { get; set; }

        public ReadAttributeStatusRecord()
        {

        }

        public ReadAttributeStatusRecord(ushort attributeIdentifier)
        {
            AttributeIdentifier = attributeIdentifier;
        }

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
