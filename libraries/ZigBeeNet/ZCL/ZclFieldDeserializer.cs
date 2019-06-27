using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.Serialization;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.ZCL
{
    public class ZclFieldDeserializer
    {
        public IZigBeeDeserializer Deserializer { get; private set; }

        public int RemainingLength
        {
            get
            {
                return Deserializer.GetSize() - Deserializer.GetPosition();
            }
        }

        public bool IsEndOfStream => Deserializer.IsEndOfStream();

        public ZclFieldDeserializer(IZigBeeDeserializer deserializer)
        {
            Deserializer = deserializer;
        }

        public object Deserialize(ZclDataType dataType)
        {
            return Deserialize<object>(dataType);
        }

        public T Deserialize<T>(ZclDataType dataType)
        {
            if (typeof(IZclListItemField).IsAssignableFrom(dataType.DataClass))
            {
                List<object> list = new List<object>();
                try
                {
                    while (Deserializer.GetSize() - Deserializer.GetPosition() > 0)
                    {
                        var item = (IZclListItemField)Activator.CreateInstance(dataType.DataClass);

                        item.Deserialize(Deserializer);

                        list.Add(item);
                    }

                }
                catch (IndexOutOfRangeException)
                {
                    // Eat the exception - this terminates the list!
                }

                var listType = typeof(List<>);
                var constructedListType = listType.MakeGenericType(dataType.DataClass);
                var instance = (System.Collections.IList)Activator.CreateInstance(constructedListType);

                foreach (var item in list)
                {
                    instance.Add(item);
                }
                return (T)instance;
            }

            return Deserializer.ReadZigBeeType<T>(dataType.DataType);
        }
    }
}
