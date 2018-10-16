using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Linq;
using ZigbeeNet.CC.ZDO;

namespace ZigbeeNet.CC
{
    public static class ZdoMeta
    {
        private static string zdoMetaFile = LoadZdoReqRspMapFile();

        private static string LoadZdoReqRspMapFile()
        {
            using (Stream stream = Assembly.GetCallingAssembly().GetManifestResourceStream($"{Assembly.GetCallingAssembly().GetName().Name}.Defs.zdo_ReqRspMap.json"))
            {
                StreamReader reader = new StreamReader(stream);
                return reader.ReadToEnd();
            }
        }

        private static Dictionary<ZdoCommand, ZdoMetaItem> _zdoObjects;

        public static void Init()
        {
            _zdoObjects = new Dictionary<ZdoCommand, ZdoMetaItem>();

            JObject jZdo = JsonConvert.DeserializeObject<JObject>(zdoMetaFile);

            LoadZdo(jZdo);
        }

        public static Dictionary<ZdoCommand, ZdoMetaItem> ZdoObjects
        {
            get
            {
                if (_zdoObjects == null)
                {
                    _zdoObjects = new Dictionary<ZdoCommand, ZdoMetaItem>();

                    JObject jZdo = JsonConvert.DeserializeObject<JObject>(zdoMetaFile);

                    LoadZdo(jZdo);
                }
                return _zdoObjects;
            }
            set { _zdoObjects = value; }
        }

        private static void LoadZdo(JObject root)
        {
            foreach (var zdo in root.Values())
            {
                if (zdo.HasValues)
                {
                    ZdoMetaItem itm = new ZdoMetaItem()
                    {
                        Request = (ZdoCommand)zdo.Value<int>("id"),
                        ResponseInd = (ZdoCommand)zdo.Value<int>("indid"),
                        ApiType = (ApiType)zdo.Value<int>("apiType")
                    };

                    if(zdo.Value<JArray>("suffix").HasValues)
                    {
                        itm.Suffix = zdo.Value<JArray>("suffix").Values<string>().ToArray();
                    }

                    _zdoObjects.Add(itm.Request, itm);
                }
            }
        }
    }
}
