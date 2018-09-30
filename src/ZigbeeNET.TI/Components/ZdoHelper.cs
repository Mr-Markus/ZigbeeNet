using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet.TI.Components
{
    public class ZdoHelper
    {
        private JToken _zdoReqRspMap;
        private JToken _zdoIndSuffix;

        public ZdoHelper()
        {
            JObject o = JObject.Parse(""); //TODO: get json from zdo_meta.json

            if (o == null)
            {
                throw new Exception("JSON file not found or loaded");
            }

            _zdoReqRspMap = o.GetValue("zdoReqRspMap");
            _zdoIndSuffix = o.GetValue("zdoIndSuffix");
        }

        /// <summary>
        /// Check if request exists and returns a boolean value
        /// </summary>
        /// <param name="reqName">The name of the request</param>
        /// <returns>True if request exists, false if not</returns>
        public bool HasARequest(string reqName)
        {
            JToken req = _zdoReqRspMap[reqName];

            if(req != null && req["ind"] != null)
            {
                return true;
            }

            return false;
        }

        public string GetRequestType(string reqName)
        {
            JToken req = _zdoReqRspMap[reqName];

            if (req != null && req["apiType"] != null)
            {
                return req["apiType"].Value<string>();
            }

            return "rspless";
        }

        public string GenerateEventOfRequest(string reqName, Dictionary<string, object> valObj)
        {
            var meta = _zdoReqRspMap[reqName];
            string evtName = "";

            if (!HasARequest(reqName))
                return string.Empty;

            evtName = "ZDO:" + meta["ind"];

            if (meta.Value<string[]>("suffix").Length == 0)
                return evtName;

            foreach (string key in meta.Value<string[]>("suffix"))
            {
                //evtName = evtName + ':' + valObj[key].toString();
            }

            return evtName;
        }

        public string GenerateEventOfIndication(string indName, object msgData)
        {
            string[] meta = _zdoIndSuffix.Value<string[]>(indName);
            string evtName = "";

            evtName = "ZDO:" + indName;

            if (meta == null || (meta.Length == 0))
                return string.Empty;

            foreach (string key in meta)
            {
                //evtName = evtName + ':' + msgData[key].toString();
            }

            return evtName;
        }
    }
}
