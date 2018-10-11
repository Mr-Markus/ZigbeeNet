using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace ZigbeeNet.CC
{
    public static class ZdoMeta
    {
        private static string zdoMetaFile = LoadZdoMetaFile();

        private static string LoadZdoMetaFile()
        {
            using (Stream stream = Assembly.GetCallingAssembly().GetManifestResourceStream($"{Assembly.GetCallingAssembly().GetName().Name}.Defs.zdo_meta.json"))
            {
                StreamReader reader = new StreamReader(stream);
                return reader.ReadToEnd();
            }
        }

        private static Dictionary<ZDO, List<ZdoMetaItem>> _zdoObjects;

        public static Dictionary<ZDO, List<ZdoMetaItem>> ZdoObjects
        {
            get
            {
                if (_zdoObjects == null)
                {
                    _zdoObjects = new Dictionary<ZDO, List<ZdoMetaItem>>();

                    JObject jZdo = JsonConvert.DeserializeObject<JObject>(zdoMetaFile);

                    LoadZdo(jZdo);
                }
                return _zdoObjects;
            }
            set { _zdoObjects = value; }
        }

        private static void LoadZdo(JObject root)
        {
            foreach (JObject zdo in root.Values())
            {
            }
        }
    }
}
