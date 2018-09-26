using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet.TI.Models
{
    public class Command
    {
        public SubSystem SubSystem { get; set; }
        public int Type { get; set; }
        public int CmdId { get; set; }
        
        public Dictionary<string, int> RequestParams { get; set; }
        public Dictionary<string, int> ResponseParams { get; set; }

        public Command(SubSystem subSystem, int type, int cmdId)
        {
            SubSystem = subSystem;
            Type = type;
            CmdId = cmdId;

            RequestParams = new Dictionary<string, int>();
            ResponseParams = new Dictionary<string, int>();
        }

        public Command(SubSystem subSystem, JToken cmd)
        {
            SubSystem = subSystem;

            Type = cmd.Value<int>("type");
            CmdId = cmd.Value<int>("cmdId");

            IEnumerable<JToken> @params = cmd.Values("params");

            IEnumerable<JToken> req = @params.Values("req");
            IEnumerable<JToken> rsp = @params.Values("rsp");

        }
    }
}
