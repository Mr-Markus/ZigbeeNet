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
    public static class ZclMeta
    {
        private static string zclMetaFile = LoadCmdFile();

        public static List<ZclGlobalCommand> _globalCommands;
        public static List<ZclGlobalCommand> GlobalCommands
        {
            get
            {
                if (_globalCommands == null)
                {
                    _globalCommands = new List<ZclGlobalCommand>();

                    string zclMetaFile = LoadCmdFile();

                    JObject jObject = JsonConvert.DeserializeObject<JObject>(zclMetaFile);

                    LoadGlobalCommands(jObject);
                }

                return _globalCommands;
            }
        }

        public static List<ZclClusterCommand> _clusterCommands;
        public static List<ZclClusterCommand> ClusterCommands
        {
            get
            {
                if(_clusterCommands == null)
                {
                    _clusterCommands = new List<ZclClusterCommand>();

                    string zclMetaFile = LoadCmdFile();

                    JObject jObject = JsonConvert.DeserializeObject<JObject>(zclMetaFile);

                    LoadClusterCommands(jObject);
                }

                return _clusterCommands;
            }
        }

        private static List<ZclCluster> _clusters;
        public static List<ZclCluster> Clusters
        {
            get
            {
                if(_clusters == null)
                {
                    _clusters = new List<ZclCluster>();

                    string clusterFile = LoadClusterFile();

                    JObject jClusters = JsonConvert.DeserializeObject<JObject>(clusterFile);

                    LoadClusters(jClusters);
                }

                return _clusters;
            }
        }

        public static List<ZclCommandParam> GetGlobalCommandParams(byte cmdId)
        {
            return GlobalCommands.Single(c => c.Id == cmdId).Params;
        }

        public static List<ZclCommandParam> GetClusterCommandParams(ushort clusterId, byte cmdId)
        {
            return ClusterCommands.Single(c => c.Id == cmdId && c.Cluster.Id == clusterId).Params;
        }

        private static string LoadCmdFile()
        {
            using (Stream stream = Assembly.GetCallingAssembly().GetManifestResourceStream($"{Assembly.GetCallingAssembly().GetName().Name}.Defs.cmd_defs.json"))
            {
                StreamReader reader = new StreamReader(stream);
                return reader.ReadToEnd();
            }
        }

        private static string LoadClusterFile()
        {
            using (Stream stream = Assembly.GetCallingAssembly().GetManifestResourceStream($"{Assembly.GetCallingAssembly().GetName().Name}.Defs.cluster_defs.json"))
            {

                StreamReader reader = new StreamReader(stream);
                return reader.ReadToEnd();
            }
        }

        private static void LoadClusters(JObject root)
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
                        zclAttribute.DataType = (DataType)info.GetValue("type").Value<int>();
                    }

                    cl.Attributes.Add(zclAttribute);
                }

                JToken reqCmds = cluster["cmd"];

                foreach (JProperty cmd in reqCmds)
                {
                    ZclClusterCommand fc = ClusterCommands.SingleOrDefault(f => f.ClusterName == cl.Name && f.Name == cmd.Name && f.Direction == Direction.ClientToServer);
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
                    ZclClusterCommand fc = ClusterCommands.SingleOrDefault(f => f.ClusterName == cl.Name && f.Name == cmd.Name && f.Direction == Direction.ServerToClient);
                    if (fc != null)
                    {
                        fc.Id = cmd.Value.Value<byte>();
                        fc.Cluster = cl;

                        cl.Responses.Add(fc);
                    }
                }

                _clusters.Add(cl);
            }
        }

        private static void LoadGlobalCommands(JObject root)
        {
            JToken jFoundation = root.GetValue("foundation");

            foreach (JProperty command in jFoundation)
            {
                ZclGlobalCommand zclCommand = new ZclGlobalCommand();

                zclCommand.Id = command.Value["id"].Value<byte>();
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

        private static void LoadClusterCommands(JObject root)
        {
            JToken jFunctional = root.GetValue("functional");

            foreach (JProperty cluster in jFunctional)
            {
                string clusterName = cluster.Name;

                foreach (JObject jToken in cluster)
                {
                    foreach (var cmd in jToken)
                    {
                        ZclClusterCommand zclCommand = new ZclClusterCommand();

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

                        _clusterCommands.Add(zclCommand);
                    }
                }
            }
        }

        public static ZclGlobalCommand GetGlobalCommand(string cmd)
        {
            return GlobalCommands.Single(fc => fc.Name.Equals(cmd));
        }

        public static ZclGlobalCommand GetGlobalCommand(byte cmdId)
        {
            return GlobalCommands.Single(fc => fc.Id.Equals(cmdId));
        }

        public static ZclClusterCommand GetClusterCommand(string cluster, string cmd)
        {
            return ClusterCommands.Single(fc => fc.Cluster.Equals(cluster) && fc.Name.Equals(cmd));
        }
    }
}
