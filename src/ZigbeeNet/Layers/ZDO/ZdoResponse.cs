using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet
{
    public class ZdoResponse
    {
        public string Name { get; set; }
        public RequestType ApiType { get; set; }
        public string[] Suffix { get; set; }
    }
}
