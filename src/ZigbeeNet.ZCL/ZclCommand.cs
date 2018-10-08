using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet.ZCL
{
    public class ZclCommand
    {
        public byte Id { get; set; }
        public string Name { get; set; }
        
        public List<ZclCommandParam> Params { get; set; }

        public ZclCommand()
        {
            Params = new List<ZclCommandParam>();
        }
    }
}
