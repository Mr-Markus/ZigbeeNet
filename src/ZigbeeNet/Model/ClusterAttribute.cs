using System;
using System.Collections.Generic;
using System.Text;
using ZigbeeNet.ZCL;

namespace ZigbeeNet
{
    public class ClusterAttribute
    {
        public ushort Identifier { get; set; }

        public string Name { get; set; }

        public DataType DataType { get; set; }

        public ushort RangeFrom { get; set; }

        public ushort RangeTo { get; set; }

        public AttributeAccess Access { get; set; }

        public object Default { get; set; }

        public bool Mandatory { get; set; }

        public object Value { get; set; }

        public Status Status { get; set; }

        public string Description { get; set; }
    }
}
