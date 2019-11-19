using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using ZigBeeNet.CodeGenerator.Xml;

namespace ZigBeeNet.CodeGenerator
{
    public class ZigBeeZclClusterTypeGenerator : ZigBeeBaseClassGenerator
    {
        public ZigBeeZclClusterTypeGenerator(string sourceRootPath, List<ZigBeeXmlCluster> clusters, string generatedDate, Dictionary<string, string> dependencies)
                : base(sourceRootPath, generatedDate)
        {
            this._dependencies = dependencies;

            try
            {
                GenerateZclClusterTypes(clusters);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private void GenerateZclClusterTypes(List<ZigBeeXmlCluster> clusters)
        {
            string className = "ZclClusterType";
            string packageRoot = packageZclProtocol;
            string packagePath = GetPackagePath(_sourceRootPath, packageRoot);
            TextWriter @out = GetClassOut(packagePath, className);

            ImportsClear();
            OutputLicense(@out);

            ImportsAdd("System");
            ImportsAdd("System.Collections.Generic");
            ImportsAdd("ZigBeeNet.ZCL.Protocol");
            ImportsAdd("ZigBeeNet.ZCL.Clusters");

            OutputImports(@out);

            @out.WriteLine("namespace ZigBeeNet.ZCL.Protocol");
            @out.WriteLine("{");

            @out.WriteLine("    /// <summary>");
            @out.WriteLine("    /// Enumeration of ZigBee Clusters.");
            @out.WriteLine("    ///");
            @out.WriteLine("    /// Code is auto-generated. Modifications may be overwritten!");
            @out.WriteLine("    /// </summary>");
            OutputClassGenerated(@out);

            @out.WriteLine("    public enum ClusterType : ushort");
            @out.WriteLine("    {");

            foreach (ZigBeeXmlCluster cluster in clusters)
            {
                if (cluster.Name.Equals("GENERAL", StringComparison.InvariantCultureIgnoreCase))
                    continue;
                @out.WriteLine("        " + StringToConstant(cluster.Name) + " = 0x" + cluster.Code.ToString("X4") + ",");
            }
            @out.WriteLine("    }");
            @out.WriteLine();

            @out.WriteLine("    public class " + className);
            @out.WriteLine("    {");
            @out.WriteLine("        private static readonly Dictionary<ushort, ZclClusterType> _idValueMap;");
            @out.WriteLine();
            @out.WriteLine("        public ClusterType Type { get; private set; }");
            @out.WriteLine();
            @out.WriteLine("        public int ClusterId { get; private set; }");
            @out.WriteLine();
            //@out.WriteLine("        public ProfileType ProfileType { get; private set; }");
            @out.WriteLine("        public string Label { get; private set; }");
            @out.WriteLine();
            @out.WriteLine("        public Func<ZigBeeEndpoint, ZclCluster> ClusterFactory { get; private set; }");
            @out.WriteLine();
            @out.WriteLine("        private ZclClusterType(int clusterId, string label, ClusterType clusterType, Func<ZigBeeEndpoint, ZclCluster> clusterFactory)");
            @out.WriteLine("        {");
            @out.WriteLine("            this.ClusterId = clusterId;");
            //@out.WriteLine("            this.ProfileType = profileType;");
            @out.WriteLine("            this.Label = label;");
            @out.WriteLine("            this.Type = clusterType;");
            @out.WriteLine("            this.ClusterFactory = clusterFactory;");
            @out.WriteLine("        }");
            @out.WriteLine();
            @out.WriteLine("        public override string ToString()");
            @out.WriteLine("        {");
            @out.WriteLine("            return $\"{Label} ({ClusterId})\";");
            @out.WriteLine("        }");
            @out.WriteLine();
            @out.WriteLine("        static ZclClusterType()");
            @out.WriteLine("        {");
            @out.WriteLine("            _idValueMap = new Dictionary<ushort, ZclClusterType>");
            @out.WriteLine("            {");

            foreach (ZigBeeXmlCluster cluster in clusters)
            {
                if (cluster.Name.Equals("GENERAL", StringComparison.InvariantCultureIgnoreCase))
                    continue;
                @out.WriteLine("                { 0x" + cluster.Code.ToString("X4") + ", new ZclClusterType(0x" + cluster.Code.ToString("X4") + ", \"" + cluster.Name + "\", ClusterType." +
                    StringToConstant(cluster.Name) + ", (endpoint) => new Zcl" + StringToUpperCamelCase(cluster.Name) + "Cluster(endpoint)) },");
            }
            @out.WriteLine("            };");
            @out.WriteLine("        }");
            @out.WriteLine();

            @out.WriteLine("        public static ZclClusterType GetValueById(ushort clusterId)");
            @out.WriteLine("        {");
            @out.WriteLine("            if (_idValueMap.TryGetValue(clusterId, out ZclClusterType result))");
            @out.WriteLine("                return result;");
            @out.WriteLine("            else");
            @out.WriteLine("                return null;");
            @out.WriteLine("        }");
            @out.WriteLine();

            @out.WriteLine("        public static ZclClusterType GetValueById(ClusterType clusterId)");
            @out.WriteLine("        {");
            @out.WriteLine("            return _idValueMap[(ushort)clusterId];");
            @out.WriteLine("        }");
            @out.WriteLine("    }");
            @out.WriteLine("}");

            @out.Flush();
            @out.Close();

        }
    }
}
