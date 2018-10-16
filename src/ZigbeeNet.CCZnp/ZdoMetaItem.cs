using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using ZigbeeNet.CC.ZDO;

namespace ZigbeeNet.CC
{
    public class ZdoMetaItem
    {
        public ZdoCommand Request { get; set; }

        public ZdoCommand ResponseInd { get; set; }

        public ApiType ApiType { get; set; }

        public string[] Suffix { get; set; }
    }
}
