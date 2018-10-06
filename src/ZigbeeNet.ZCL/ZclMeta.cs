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
        public List<ZclFoundationCommand> GlobalCommands { get; set; }

        public List<ZclFunctionalCommand> ClusterCommands { get; set; }

        public List<ZclCluster> Clusters { get; set; }

        public ZclMeta()
        {
            GlobalCommands = new List<ZclFoundationCommand>();
            ClusterCommands = new List<ZclFunctionalCommand>();
            Clusters = new List<ZclCluster>();

            string zclMetaFile = LoadCmdFile();

            JObject jObject = JsonConvert.DeserializeObject<JObject>(zclMetaFile);

            LoadGlobalCommands(jObject);
            LoadClusterCommands(jObject);

            string clusterFile = LoadClusterFile();

            JObject jClusters = JsonConvert.DeserializeObject<JObject>(clusterFile);

            LoadClusters(jClusters);
        }

        private string LoadCmdFile()
        {
            using (Stream stream = Assembly.GetCallingAssembly().GetManifestResourceStream($"{Assembly.GetCallingAssembly().GetName().Name}.Defs.cmd_defs.json"))
            {
                StreamReader reader = new StreamReader(stream);
                return reader.ReadToEnd();
            }
        }

        private string LoadClusterFile()
        {
            using (Stream stream = Assembly.GetCallingAssembly().GetManifestResourceStream($"{Assembly.GetCallingAssembly().GetName().Name}.Defs.cluster_defs.json"))
            {

                StreamReader reader = new StreamReader(stream);
                return reader.ReadToEnd();
            }
        }

        private void LoadClusters(JObject root)
        {
            foreach (JObject cluster in root.Values())
            {
                ZclCluster cl = new ZclCluster();
                cl.Name = cluster.Path;
                cl.Id = cluster["id"].Value<ushort>();

                var attributes = cluster["attrId"];

                foreach (JProperty attr in attributes)
                {
                    ZclAttribute zclAttribute = new ZclAttribute();
                    zclAttribute.Name = attr.Name;

                    foreach (JObject info in attr)
                    {
                        zclAttribute.Id = info.GetValue("id").Value<ushort>();
                        zclAttribute.DataType = (DataType)info.GetValue("type").Value<int>(); //TODO: convert strings to int and then to enum
                    }

                    cl.Attributes.Add(zclAttribute);
                }

                JToken reqCmds = cluster["cmd"];

                foreach (JProperty cmd in reqCmds)
                {
                    ZclFunctionalCommand fc = ClusterCommands.SingleOrDefault(f => f.ClusterName == cl.Name && f.Name == cmd.Name && f.Direction == Direction.ClientToServer);
                    if (fc != null)
                    {
                        fc.Id = cmd.Value.Value<byte>();
                        fc.Cluster = cl;

                        cl.Requests.Add(fc);
                    }
                }

                var rspCmds = cluster["cmdRsp"];

                foreach (JProperty cmd in rspCmds)
                {
                    ZclFunctionalCommand fc = ClusterCommands.SingleOrDefault(f => f.ClusterName == cl.Name && f.Name == cmd.Name && f.Direction == Direction.ServerToClient);
                    if (fc != null)
                    {
                        fc.Id = cmd.Value.Value<byte>();
                        fc.Cluster = cl;

                        cl.Responses.Add(fc);
                    }
                }

                Clusters.Add(cl);
            }
        }

        private void LoadGlobalCommands(JObject root)
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
                            Name = p.Key
                        };
                        if (p.Value.Type == JTokenType.Integer)
                        {
                            param.DataType = (DataType)p.Value.ToObject<int>();
                        } else if (p.Value.Type == JTokenType.String)
                        {
                            param.SpecialType = p.Value.ToObject<string>();
                        } else
                        {
                            throw new NotImplementedException($"Param type {p.Value.Type.ToString()} not implemented");
                        }

                        zclCommand.Params.Add(param);
                    }
                }

                GlobalCommands.Add(zclCommand);
            }
        }

        private void LoadClusterCommands(JObject root)
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
                        zclCommand.ClusterName = cluster.Name;
                        zclCommand.Direction = (Direction)cmd.Value["dir"].ToObject<int>();

                        var paramCollection = cmd.Value["params"];

                        foreach (JObject pObject in paramCollection)
                        {
                            foreach (var p in pObject)
                            {
                                ZclCommandParam param = new ZclCommandParam()
                                {
                                    Name = p.Key
                                };
                                if (p.Value.Type == JTokenType.Integer)
                                {
                                    param.DataType = (DataType)p.Value.ToObject<int>();
                                }
                                else if (p.Value.Type == JTokenType.String)
                                {
                                    param.SpecialType = p.Value.ToObject<string>();
                                }
                                else
                                {
                                    throw new NotImplementedException($"Param type {p.Value.Type.ToString()} not implemented");
                                }

                                zclCommand.Params.Add(param);
                            }
                        }

                        ClusterCommands.Add(zclCommand);
                    }
                }
            }
        }

        public ZclFoundationCommand GetGlobalCommand(string cmd)
        {
            return GlobalCommands.SingleOrDefault(fc => fc.Name.Equals(cmd));
        }

        public ZclFunctionalCommand GetClusterCommand(string cluster, string cmd)
        {
            return ClusterCommands.SingleOrDefault(fc => fc.Cluster.Equals(cluster) && fc.Name.Equals(cmd));
        }
    }
}
