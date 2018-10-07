using BinarySerialization;
using System;
using System.Collections.Generic;
using System.Text;
using ZigbeeNet.ZCL;
using System.Linq;

namespace ZigbeeNet.CC
{
    public class ArgumentCollection
    {
        public ArgumentCollection()
        {
            Arguments = new List<ZpiArgument>();
        }

        [FieldOrder(0)]
        public List<ZpiArgument> Arguments { get; set; }

        public void AddOrUpdate(ZpiArgument zpiArgument)
        {
            AddOrUpdate(zpiArgument.Name, zpiArgument.ParamType, zpiArgument.Value);
        }

        public void AddOrUpdate(string name, ParamType paramType, object value = null)
        {
            ZpiArgument zpiArgument = Arguments.SingleOrDefault(a => a.Name == name && a.ParamType == paramType);

            if (zpiArgument == null)
            {
                ZpiArgument requestArgument = new ZpiArgument()
                {
                    Name = name,
                    ParamType = paramType
                };
                if(value != null)
                {
                    requestArgument.Value = value;
                }

                Arguments.Add(requestArgument);
            } else
            {
                zpiArgument.Value = value;
            }
        }

        public object this[string key]
        {
            get
            {
                ZpiArgument zpiArgument = Arguments.SingleOrDefault(a => a.Name == key);

                if(zpiArgument != null)
                {
                    return zpiArgument.Value;
                } else
                {
                    throw new IndexOutOfRangeException($"Argument with key {key} not found");
                }
            }
            set
            {
                ZpiArgument zpiArgument = Arguments.SingleOrDefault(a => a.Name == key);

                if (zpiArgument != null)
                {
                    zpiArgument.Value = value;
                }
                else
                {
                    throw new IndexOutOfRangeException($"Argument with key {key} not found. Use AddOrUpdate method");
                }
            }
        }
    }
}
