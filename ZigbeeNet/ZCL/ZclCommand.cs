using BinarySerialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ZigbeeNet.ZCL
{
    public abstract class ZclCommand : ZigbeeCommand
    {
        public bool GenericCommand { get; set; }

        public byte CommandId { get; set; }

        public ZclCommandDirection Direction { get; set; }
    }
}
