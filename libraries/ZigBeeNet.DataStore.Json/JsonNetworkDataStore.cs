using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using ZigBeeNet.Database;

namespace ZigBeeNet.DataStore.Json
{
    public class JsonNetworkDataStore : IZigBeeNetworkDataStore
    {
        private readonly string _dirname;

        public JsonNetworkDataStore(string dirname)
        {
            _dirname = dirname;

            if (!Directory.Exists(_dirname))
                Directory.CreateDirectory(_dirname);
        }

        private string GetFileName(IeeeAddress address)
        {
            return _dirname + "/" + address.ToString() + ".json";
        }

        public ISet<IeeeAddress> ReadNetworkNodes()
        {
            ISet<IeeeAddress> nodes = new HashSet<IeeeAddress>();

            try
            {
                if (Directory.Exists(_dirname))
                {
                    var jsonFiles = Directory.EnumerateFiles(_dirname, "*.json");

                    foreach (string file in jsonFiles)
                    {
                        try
                        {
                            IeeeAddress address = new IeeeAddress(file.Substring(file.Length - (16 + 5), 16));
                            nodes.Add(address);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return nodes;
        }


        public ZigBeeNodeDao ReadNode(IeeeAddress address)
        {
            ZigBeeNodeDao node = null;

            try
            {
                string filename = GetFileName(address);

                if (File.Exists(filename))
                {
                    node = JsonConvert.DeserializeObject<ZigBeeNodeDao>(File.ReadAllText(filename));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return node;
        }

        public void RemoveNode(IeeeAddress address)
        {
            try
            {
                string filename = GetFileName(address);

                if (File.Exists(filename))
                {
                    File.Delete(filename);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public void WriteNode(ZigBeeNodeDao node)
        {
            try
            {
                string filename = GetFileName(node.IeeeAddress);

                var settings = new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                };

                string json = JsonConvert.SerializeObject(node, Formatting.Indented, settings);

                File.WriteAllText(filename, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
