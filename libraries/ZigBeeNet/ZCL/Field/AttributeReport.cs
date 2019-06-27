using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.Serialization;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.ZCL.Field
{
    public class AttributeReport : IZclListItemField
    {
        /// <summary>
         /// The attribute identifier.
         /// </summary>
        public ushort AttributeIdentifier { get; private set; }
        /// <summary>
         /// The attribute data type.
         /// </summary>
        public ZclDataType AttributeDataType { get; private set; }
        /// <summary>
         /// The attribute data type.
         /// </summary>
        public object AttributeValue { get; private set; }


        public void Serialize(IZigBeeSerializer serializer)
        {
            serializer.AppendZigBeeType(AttributeIdentifier, DataType.UNSIGNED_16_BIT_INTEGER);
            serializer.AppendZigBeeType(AttributeDataType.Id, DataType.UNSIGNED_8_BIT_INTEGER);
            serializer.AppendZigBeeType(AttributeValue, AttributeDataType.DataType);
        }


        public void Deserialize(IZigBeeDeserializer deserializer)
        {
            AttributeIdentifier = deserializer.ReadZigBeeType<ushort>(DataType.UNSIGNED_16_BIT_INTEGER);

            byte dataType = deserializer.ReadZigBeeType<byte>(DataType.UNSIGNED_8_BIT_INTEGER);

            AttributeDataType = ZclDataType.Get((DataType)dataType);
            AttributeValue = deserializer.ReadZigBeeType<object>(AttributeDataType.DataType);
        }

        public override string ToString()
        {
            return "Attribute Report: attributeDataType=" + AttributeDataType + ", attributeIdentifier="
                    + AttributeIdentifier + ", attributeValue=" + AttributeValue;
        }
    }
}
