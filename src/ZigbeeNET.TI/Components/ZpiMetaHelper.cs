using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet.TI.Components
{
    public class ZpiMetaHelper
    {
        private JObject _zpiMeta;

        public ZpiMetaHelper()
        {
            _zpiMeta = JObject.Parse(""); //TODO: get json from zpi_meta.json

            if (_zpiMeta == null)
            {
                throw new Exception("JSON file not found or loaded");
            }
        }

        public string Get(string subSys, string cmd)
        {
            JToken ss = _zpiMeta.GetValue(subSys);

            if(ss != null)
            {
                return ss.Value<string>(cmd);
            }
            else
            {
                return null;
            }
        }
    }
}
