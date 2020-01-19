using System;
using System.Collections.Generic;
using System.IO;
using ZigBeeNet.CodeGenerator.Xml;

namespace ZigBeeNet.CodeGenerator
{
    public class ZigBeeZclDependencyGenerator : ZigBeeBaseClassGenerator
    {
        private HashSet<string> _zclTypes = new HashSet<string>();

        public ZigBeeZclDependencyGenerator(string sourceRootPath, List<ZigBeeXmlCluster> clusters, string generatedDate)
            : base(sourceRootPath, generatedDate)
        {
            _dependencies = new Dictionary<string, string>();

            foreach (ZigBeeXmlCluster cluster in clusters)
            {
                try
                {
                    GenerateZclClusterDependencies(cluster, packageRoot);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
        }

        private void GenerateZclClusterDependencies(ZigBeeXmlCluster cluster, string packageRootPrefix)
        {
            if (cluster.Constants != null)
            {
                foreach (ZigBeeXmlConstant constant in cluster.Constants)
                {
                    string packageRoot = packageRootPrefix + packageZclProtocolCommand + "." + StringToLowerCamelCase(cluster.Name).Replace("_", "").ToLower();
                    string className = constant.ClassName;

                    if (_dependencies.ContainsKey(className))
                    {
                        throw new ArgumentException("Duplicate class definition: " + _dependencies[className] + " with " + packageRoot + "." + className);
                    }
                    else
                    {
                        _dependencies.Add(className, packageRoot + "." + className);
                    }
                }
            }

            if (cluster.Structures != null)
            {
                foreach (ZigBeeXmlStructure structure in cluster.Structures)
                {
                    string packageRoot = packageRootPrefix + packageZclProtocolCommand + "." + StringToLowerCamelCase(cluster.Name).Replace("_", "").ToLower();
                    string className = structure.ClassName;

                    if (_dependencies.ContainsKey(className))
                    {
                        throw new ArgumentException("Duplicate class definition: " + _dependencies[className] + " with " + packageRoot + "." + className);
                    }
                    else
                    {
                        _dependencies.Add(className, packageRoot + "." + className);
                    }

                    foreach (ZigBeeXmlField field in structure.Fields)
                    {
                        _zclTypes.Add(field.Type);
                    }
                }
            }

            if (cluster.Commands != null)
            {
                foreach (ZigBeeXmlCommand command in cluster.Commands)
                {
                    foreach (ZigBeeXmlField field in command.Fields)
                    {
                        _zclTypes.Add(field.Type);
                    }
                }
            }

            if (cluster.Attributes != null)
            {
                foreach (ZigBeeXmlAttribute attribute in cluster.Attributes)
                {
                    _zclTypes.Add(attribute.Type);
                }
            }
        }

        public Dictionary<string, string> GetDependencyMap()
        {
            return _dependencies;
        }

        public HashSet<string> GetZclTypeMap()
        {
            return _zclTypes;
        }
    }
}