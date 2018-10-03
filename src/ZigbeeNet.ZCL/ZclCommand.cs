using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet.ZCL
{
    public class ZclCommand
    {
        public string Name { get; set; }

        public List<ZclCommandParam> Params { get; }

        public ZclCommand()
        {
            Params = new List<ZclCommandParam>();
        }
    }
}
