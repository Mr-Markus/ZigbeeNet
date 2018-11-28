using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet.ZCL
{
    public class ZclAttribute
    {
        public string Name { get; set; }

        public ushort Id { get; set; }

        public ZclDataType DataType { get; set; }

        public bool Mandatory { get; set; }

        public bool Implemented { get; set; }

        public bool Readable { get; set; }

        public bool Writable { get; set; }

        public ZclAttribute()
        {

        }
    }
}
