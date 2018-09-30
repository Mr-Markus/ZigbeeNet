using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet
{
    public class RequestArgument
    {
        public string Name { get; set; }

        public DataType DataType { get; set; }

        public object Value { get; set; }

        public RequestArgument()
        {

        }

        public RequestArgument(string name, DataType dataType, object value)
        {
            Name = name;
            DataType = dataType;
            Value = value;
        }
    }
}
