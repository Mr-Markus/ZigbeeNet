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
                _value = value;
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
