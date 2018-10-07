using BinarySerialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet.CC
{
    public class ZpiArgumentValue<TDataType>
    {
        [FieldOrder(0)]
        public TDataType Value { get; set; }

        public ZpiArgumentValue(TDataType value, ParamType dataType = ParamType.unkown)
        {
            Value = value;
        }
    }
}
