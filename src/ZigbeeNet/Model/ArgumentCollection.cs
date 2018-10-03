using System;
using System.Collections.Generic;
using System.Text;
using ZigbeeNet.ZCL;

namespace ZigbeeNet
{
    public class ArgumentCollection
    {
        public ArgumentCollection()
        {
            Arguments = new List<ZpiArgument>();
        }

        public List<ZpiArgument> Arguments { get; set; }

        public void Add(string name, DataType dataType, object value)
        {
            ZpiArgument requestArgument = new ZpiArgument()
            {
                Name = name,
                DataType = dataType,
                Value = value
            };
        }
    }
}
