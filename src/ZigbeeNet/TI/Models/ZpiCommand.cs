using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet.TI.Models
{
    public class ZpiCommand
    {
        public int Type { get; set; }
        public int CmdId { get; set; }

        public List<Dictionary<string, Dictionary<string, int>>> Params { get; set; }

        public ZpiCommand(int type, int cmdId)
        {
            Type = type;
            CmdId = cmdId;
        }
    }
}
