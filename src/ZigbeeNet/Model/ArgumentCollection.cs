using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet
{
    public class ArgumentCollection
    {
        public ArgumentCollection()
        {
            Arguments = new List<RequestArgument>();
        }

        public List<RequestArgument> Arguments { get; set; }

        public void Add(string name, DataType dataType, object value)
        {
            RequestArgument requestArgument = new RequestArgument()
            {
                Name = name,
                DataType = dataType,
                Value = value
            };
        }
    }
}
