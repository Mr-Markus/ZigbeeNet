using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet.TI.Components
{
    public class ZpiObject
    {
        public string Type { get; set; }
        public string SubSys { get; set; }
        public string Cmd { get; set; }
        public string CmdId { get; set; }
        public List<object> Args { get; set; }

        public ZpiObject(string subSystem, string cmd, params object[] args)
        {
            //TODO: get subsys by zpi_meta
        }
    }
}
