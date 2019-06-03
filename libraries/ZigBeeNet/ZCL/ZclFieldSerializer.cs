using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.Serialization;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.ZCL
{
    public class ZclFieldSerializer
    {
        public IZigBeeSerializer Serializer { get; private set; }

        public byte[] Payload
        {
            get => Serializer.Payload;
        }

        public ZclFieldSerializer(IZigBeeSerializer serializer)
        {
            Serializer = serializer;
        }

        public void Serialize(object value, ZclDataType dataType)
        {
            if (value is IZclListItemField fieldValue)
            {
                List<IZclListItemField> list = (List<IZclListItemField>)value;

                foreach (IZclListItemField item in list)
                {
                    item.Serialize(Serializer);
                }
                return;
            }

            Serializer.AppendZigBeeType(value, dataType.DataType);
        }
    }
}
