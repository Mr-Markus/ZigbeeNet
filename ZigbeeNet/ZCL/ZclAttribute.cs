using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet.ZCL
{
    public class ZclAttribute
    {
        public string Name { get; set; }

        public ushort Id { get; set; }

        public DataType DataType { get; set; }

        public ZclAttribute()
        {

        }
    }
}
