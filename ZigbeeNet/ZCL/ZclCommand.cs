using BinarySerialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.ZCL
{
    public abstract class ZclCommand : ZigBeeCommand
    {
        public bool IsGenericCommand { get; set; }

        public byte CommandId { get; set; }

        public ZclCommandDirection Direction { get; set; }
    }
}
