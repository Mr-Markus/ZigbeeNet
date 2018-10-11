using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace ZigbeeNet.CC
{
    public class ZdoMetaItem
    {
        public ZDO Request { get; set; }

        public ZDO ResponseInd { get; set; }

        public ApiType ApiType { get; set; }

        public string[] Suffix { get; set; }
    }
}
