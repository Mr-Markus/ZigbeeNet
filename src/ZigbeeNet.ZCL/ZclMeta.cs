using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Linq;

namespace ZigbeeNet.ZCL
{
    public class ZclMeta
    {
        private string _zclMetaDef;

        public string ZclMetaDef
        {
            get
            {
                if(string.IsNullOrEmpty(_zclMetaDef))
                {
                    Stream stream = Assembly.GetCallingAssembly().GetManifestResourceStream($"{Assembly.GetCallingAssembly().GetName().Name}.Defs.zcl_meta.json");

                    StreamReader reader = new StreamReader(stream);
                    _zclMetaDef = reader.ReadToEnd();
                }
                return _zclMetaDef;
            }
            set { _zclMetaDef = value; }
        }

        public List<ZclFoundationCommand> Foundation { get; set; }

        public List<ZclFunctionalCommand> Functional { get; set; }

        public ZclMeta()
        {
            Foundation = new List<ZclFoundationCommand>();
            Functional = new List<ZclFunctionalCommand>();

            Init();
        }

        private void Init()
        {
            JObject jObject = JsonConvert.DeserializeObject<JObject>(ZclMetaDef);
        }

        private void LoadFoundation(JObject root)
        {
            JToken jFoundation = root.GetValue("foundation");

            foreach (JProperty command in jFoundation)
            {
                ZclFoundationCommand zclCommand = new ZclFoundationCommand();

                zclCommand.Name = command.Name;
                zclCommand.KnownBufLen = command.Value["knownBufLen"].Value<int>();

                var paramCollection = command.Value["params"];

                foreach (JObject pObject in paramCollection)
                {
                    foreach (var p in pObject)
                    {
                        ZclCommandParam param = new ZclCommandParam()
                        {
                            Name = p.Key,
                            DataType = p.Value.ToObject<string>()
                        };
                        zclCommand.Params.Add(param);
                    }
                }

                Foundation.Add(zclCommand);
            }
        }

        private void LoadFunctional(JObject root)
        {
            JToken jFunctional = root.GetValue("functional");

            foreach (JProperty cluster in jFunctional)
            {
                string clusterName = cluster.Name;

                foreach (JObject jToken in cluster)
                {
                    foreach (var cmd in jToken)
                    {
                        ZclFunctionalCommand zclCommand = new ZclFunctionalCommand();

                        zclCommand.Name = cmd.Key;
                        zclCommand.Cluster = cluster.Name;
                        zclCommand.Direction = (Direction)cmd.Value["dir"].ToObject<int>();

                        var paramCollection = cmd.Value["params"];

                        foreach (JObject pObject in paramCollection)
                        {
                            foreach (var p in pObject)
                            {
                                ZclCommandParam param = new ZclCommandParam()
                                {
                                    Name = p.Key,
                                    DataType = p.Value.ToObject<string>()
                                };

                                zclCommand.Params.Add(param);
                            }
                        }

                        Functional.Add(zclCommand);
                    }
                }
            }
        }

        public ZclFoundationCommand GetFoundationCommand(string cmd)
        {
            return Foundation.SingleOrDefault(fc => fc.Name.Equals(cmd));
        }

        public ZclFunctionalCommand GetFunctionalCommand(string cluster, string cmd)
        {
            return Functional.SingleOrDefault(fc => fc.Cluster.Equals(cluster) && fc.Name.Equals(cmd));
        }
    }
}
