using System;
using System.Collections.Generic;
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
            if(dataType.DataClass.IsAssignableFrom(typeof(IZclListItemField)))
            {
                //List<IZclListItemField> list = new List<IZclListItemField>();
                //try
                //{
                //    while (Deserializer.GetSize() - Deserializer.GetPosition() > 0)
                //    {
                        var item = (IZclListItemField)Activator.CreateInstance(dataType.DataClass);

                        item.Deserialize(Deserializer);

                return (T)item;

                        //list.Add(item);
                //    }

                //}
                //catch (IndexOutOfRangeException)
                //{
                //    // Eat the exception - this terminates the list!
                //}
                //return list;
            }

            return Deserializer.ReadZigBeeType<T>(dataType.DataType);
        }
    }
}
