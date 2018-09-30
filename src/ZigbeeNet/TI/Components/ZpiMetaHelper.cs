using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet.TI.Components
{
    public class ZpiMetaHelper
    {
        public string ZmtDefsPath { get; set; }

        public ZpiMetaHelper()
        {
            
        }

        private Dictionary<string, Dictionary<string, int>> _zmtDefs;
        public Dictionary<string, Dictionary<string, int>> ZmtDefs
        {
            get
            {
                if (_zmtDefs == null)
                {
                    _zmtDefs = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, int>>>(ZmtDefsPath);
                }
                return _zmtDefs;
            }
        }

        public Dictionary<string, int> ParamTypes
        {
            get
            {
                return ZmtDefs["ParamType"];
            }
        }

        public Dictionary<string, int> SYS
        {
            get
            {
                return ZmtDefs["SYS"];
            }
        }

        public Dictionary<string, int> MAC
        {
            get
            {
                return ZmtDefs["MAC"];
            }
        }

        public Dictionary<string, int> AF
        {
            get
            {
                return ZmtDefs["AF"];
            }
        }

        public Dictionary<string, int> ZDO
        {
            get
            {
                return ZmtDefs["ZDO"];
            }
        }

        public Dictionary<string, int> SAPI
        {
            get
            {
                return ZmtDefs["SAPI"];
            }
        }

        public Dictionary<string, int> UTIL
        {
            get
            {
                return ZmtDefs["UTIL"];
            }
        }

        public Dictionary<string, int> DBG
        {
            get
            {
                return ZmtDefs["DBG"];
            }
        }

        public Dictionary<string, int> APP
        {
            get
            {
                return ZmtDefs["APP"];
            }
        }

        public Dictionary<string, int> DEBUG
        {
            get
            {
                return ZmtDefs["DEBUG"];
            }
        }
    }
}
