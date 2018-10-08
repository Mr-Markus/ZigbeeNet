using BinarySerialization;
using System;
using System.Collections.Generic;
using System.Text;
using ZigbeeNet.ZCL;

namespace ZigbeeNet.CC
{
    public class ZpiArgument
    {
        [Ignore()]
        public string Name { get; set; }

        [Ignore()]
        public ParamType ParamType { get; set; }

        [Ignore()]
        private object _value;

        [FieldOrder(0)]
        public object Value
        {
            get
            {
                return _value;
            }
            set
            {
                switch (ParamType)
                {
                    case ParamType.uint8:
                        _value = Convert.ToByte(value);
                        break;
                    case ParamType.uint16:
                        _value = Convert.ToUInt16(value);
                        break;
                    case ParamType.uint32:
                        _value = Convert.ToUInt32(value);
                        break;
                    case ParamType.longaddr:
                        _value = Convert.ToInt64(value);
                        break;                   
                    default:
                        throw new NotImplementedException($"ParamType {ParamType.ToString()} not implemented in ZpiArgument");
                }
            }
        }

        public ZpiArgument()
        {

        }

        public ZpiArgument(string name, ParamType paramType, object value)
        {
            Name = name;
            ParamType = paramType;
            Value = value;
        }

        public override string ToString()
        {
            return $"{this.Name}: {this.Value}";
        }
    }
}
