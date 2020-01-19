using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using ZigBeeNet.CodeGenerator.Xml;

namespace ZigBeeNet.CodeGenerator
{
    public class ZigBeeZclConstantGenerator : ZigBeeBaseClassGenerator 
    {

        public ZigBeeZclConstantGenerator(string sourceRootPath, List<ZigBeeXmlCluster> clusters, string generatedDate, Dictionary<string, string> dependencies)
                : base(sourceRootPath, generatedDate)
        {
            this._dependencies = dependencies;

            foreach (ZigBeeXmlCluster cluster in clusters) 
            {
                try 
                {
                    GenerateZclClusterConstants(cluster);
                } 
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
        }

        /*
        public ZigBeeZclConstantGenerator(string sourceRootPath, ZigBeeXmlConstant constant, string generatedDate) 
            : base(sourceRootPath, generatedDate)
        {
            try 
            {
                GenerateZclConstant(constant);
            } 
            catch (IOException e) 
            {
                Console.WriteLine(e.ToString());
            }
        }
        */

        private void GenerateZclClusterConstants(ZigBeeXmlCluster cluster)
        {
            if (cluster.Constants == null) 
            {
                return;
            }

            string packageRoot = GetZclClusterCommandPackage(cluster);

            foreach (ZigBeeXmlConstant constant in cluster.Constants) 
            {    
                GenerateZclConstant(packageRoot, constant);
            }
        }

        private void GenerateZclConstant(string packageRoot, ZigBeeXmlConstant constant)
        {
            string packagePath = GetPackagePath(_sourceRootPath, packageRoot);
            string className = constant.ClassName;
            TextWriter @out = GetClassOut(packagePath, className);

            OutputLicense(@out);

            ImportsAdd("System");
            ImportsAdd("System.Collections.Generic");
            ImportsAdd("System.Text");

            OutputImports(@out);

            @out.WriteLine("namespace ZigBeeNet" + packageRoot);
            @out.WriteLine("{");

            @out.WriteLine("    /// <summary>");
            @out.WriteLine("    /// " + constant.Name + " value enumeration");

            if (constant.Description.Count > 0)
            {
                @out.WriteLine("    ///");
                OutputWithLinebreak(@out, "    ", constant.Description);
            }

            @out.WriteLine("    ///");
            @out.WriteLine("    /// Code is auto-generated. Modifications may be overwritten!");
            @out.WriteLine("    /// </summary>");
            OutputClassGenerated(@out);

            @out.WriteLine("    public enum " + className);
            @out.WriteLine("    {");

            bool first = true;

            foreach (BigInteger key in constant.Values.Keys)
            {
                string value = constant.Values[key];

                if (!first)
                {
                    @out.WriteLine(",");
                }
                first = false;
                @out.WriteLine("        // " + value);

                @out.Write("        " + StringToConstant(value) + " = 0x" + key.ToString("X4"));
            }

            @out.WriteLine();
            @out.WriteLine("    }");
            @out.WriteLine("}");

            @out.Flush();
            @out.Close();
        }

    }
}
