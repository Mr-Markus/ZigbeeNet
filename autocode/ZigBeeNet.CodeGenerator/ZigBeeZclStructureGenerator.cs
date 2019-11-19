using System;
using System.Collections.Generic;
using System.IO;
using ZigBeeNet.CodeGenerator.Xml;

namespace ZigBeeNet.CodeGenerator
{
    public class ZigBeeZclStructureGenerator : ZigBeeBaseFieldGenerator 
    {
        public ZigBeeZclStructureGenerator(string sourceRootPath, List<ZigBeeXmlCluster> clusters, string generatedDate, Dictionary<string, string> dependencies)
            : base(sourceRootPath, generatedDate)
        {
            this._dependencies = dependencies;

            foreach (ZigBeeXmlCluster cluster in clusters) 
            {
                try 
                {
                    GenerateZclClusterStructures(cluster);
                } 
                catch (Exception e) 
                {
                    Console.WriteLine(e.ToString());
                }
            }
        }

        private void GenerateZclClusterStructures(ZigBeeXmlCluster cluster)
        {
            if (cluster.Structures == null) 
            {
                return;
            }

            foreach (ZigBeeXmlStructure structure in cluster.Structures) 
            {
                string packageRoot = GetZclClusterCommandPackage(cluster);
                string packagePath = GetPackagePath(_sourceRootPath, packageRoot);

                string className = structure.ClassName;
                TextWriter @out = GetClassOut(packagePath, className);

                ImportsClear();

                OutputLicense(@out);

                ImportsAdd("System");
                ImportsAdd("System.Collections.Generic");
                ImportsAdd("System.Linq");
                ImportsAdd("System.Text");
                ImportsAdd("ZigBeeNet.Serialization");
                ImportsAdd("ZigBeeNet.ZCL.Protocol");
                ImportsAdd("ZigBeeNet.ZCL.Field");
                ImportsAdd("ZigBeeNet.ZCL.Clusters." + cluster.Name.Replace("/", "").Replace(" ", "").Replace("(", "").Replace(")", ""));

                OutputImports(@out);

                @out.WriteLine("namespace ZigBeeNet.ZCL.Clusters." + cluster.Name.Replace("/", "").Replace(" ", "").Replace("(", "").Replace(")", ""));
                @out.WriteLine("{");

                @out.WriteLine("    /// <summary>");
                @out.WriteLine("    /// " + structure.Name + " structure implementation.");

                if (structure.Description.Count > 0) {
                    @out.WriteLine("    ///");
                    OutputWithLinebreak(@out, "    ", structure.Description);
                }

                @out.WriteLine("    ///");
                @out.WriteLine("    /// Code is auto-generated. Modifications may be overwritten!");
                @out.WriteLine("    /// </summary>");
                OutputClassGenerated(@out);

                @out.WriteLine("    public class " + className + " : IZigBeeSerializable");
                @out.WriteLine("    {");

                foreach (ZigBeeXmlField field in structure.Fields) 
                {
                    @out.WriteLine("        /// <summary>");
                    @out.WriteLine("        /// " + field.Name + " structure field.");
                    if (field.Description.Count != 0)
                    {
                        @out.WriteLine("        /// ");
                        OutputWithLinebreak(@out, "        ", field.Description);
                    }
                    @out.WriteLine("        /// </summary>");
                    @out.WriteLine("        public " + GetDataTypeClass(field) + " " + StringToUpperCamelCase(field.Name) + " { get; set; }");
                    @out.WriteLine();
                }
                @out.WriteLine();

                GenerateFields(@out, "IZigBeeSerializable", className, structure.Fields, new List<string>());
                GenerateToString(@out, className, structure.Fields, new List<string>());

                @out.WriteLine("    }");
                @out.WriteLine("}");

                @out.Flush();
                @out.Close();
            }
        }

    }
}
