using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using ZigBeeNet.CodeGenerator.Xml;

namespace ZigBeeNet.CodeGenerator
{
    public class ZigBeeZclClusterGenerator : ZigBeeBaseClassGenerator
    {
        public ZigBeeZclClusterGenerator(string sourceRootPath, List<ZigBeeXmlCluster> clusters, string generatedDate, Dictionary<string, string> dependencies)
            : base(sourceRootPath, generatedDate)
        {
            this._dependencies = dependencies;

            foreach (ZigBeeXmlCluster cluster in clusters)
            {
                // Suppress GENERAL cluster as it's not really a cluster!
                if (cluster.Name.Equals("GENERAL", StringComparison.InvariantCultureIgnoreCase))
                {
                    continue;
                }

                try
                {
                    GenerateZclClusterClasses(cluster);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
        }

        private void GenerateZclClusterClasses(ZigBeeXmlCluster cluster)
        {
            string packagePath = GetPackagePath(_sourceRootPath, packageZclCluster);
            String className = "Zcl" + StringToUpperCamelCase(cluster.Name) + "Cluster";
            TextWriter @out = GetClassOut(packagePath, className);

            OutputLicense(@out);
            @out.WriteLine();

            ImportsClear();

            int commandsServer = 0;
            int commandsClient = 0;
            foreach (ZigBeeXmlCommand command in cluster.Commands)
            {
                //ImportsAdd(packageRoot + packageZclCluster + "." + StringToLowerCamelCase(cluster.Name).ToLower() + "."
                //        + StringToUpperCamelCase(command.Name));

                if (command.Source.Equals("server"))
                {
                    commandsServer++;
                }
                if (command.Source.Equals("client"))
                {
                    commandsClient++;
                }
            }

            List<ZigBeeXmlAttribute> attributesClient = new List<ZigBeeXmlAttribute>();
            List<ZigBeeXmlAttribute> attributesServer = new List<ZigBeeXmlAttribute>();
            foreach (ZigBeeXmlAttribute attribute in cluster.Attributes)
            {
                if (attribute.Side.Equals("server"))
                {
                    attributesServer.Add(attribute);
                }
                if (attribute.Side.Equals("client"))
                {
                    attributesClient.Add(attribute);
                }
            }

            ImportsAdd("System");
            ImportsAdd("System.Collections.Concurrent");
            ImportsAdd("System.Collections.Generic");
            ImportsAdd("System.Linq");
            ImportsAdd("System.Text");
            ImportsAdd("System.Threading");
            ImportsAdd("System.Threading.Tasks");
            ImportsAdd("ZigBeeNet.Security");
            ImportsAdd("ZigBeeNet.ZCL.Protocol");
            ImportsAdd("ZigBeeNet.ZCL.Field");
            if (commandsClient > 0)
            {
                ImportsAdd("ZigBeeNet.ZCL.Clusters." + cluster.Name.Replace("/", "").Replace(" ", "").Replace("(", "").Replace(")", ""));
            }

            OutputImports(@out);
            @out.WriteLine();

            @out.WriteLine("namespace ZigBeeNet.ZCL.Clusters");
            @out.WriteLine("{");
            @out.WriteLine("    /// <summary>");
            @out.WriteLine("    /// " + cluster.Name + " cluster implementation (Cluster ID " + "0x" + cluster.Code.ToString("X4") + ").");
            if (cluster.Description.Count != 0)
            {
                @out.WriteLine("    ///");
                OutputWithLinebreak(@out, "    ", cluster.Description);
            }

            @out.WriteLine("    ///");
            @out.WriteLine("    /// Code is auto-generated. Modifications may be overwritten!");

            @out.WriteLine("    /// </summary>");

            @out.WriteLine("    public class " + className + " : ZclCluster");
            @out.WriteLine("    {");
            @out.WriteLine("        /// <summary>");
            @out.WriteLine("        /// The ZigBee Cluster Library Cluster ID");
            @out.WriteLine("        /// </summary>");
            @out.WriteLine("        public const ushort CLUSTER_ID = 0x" + cluster.Code.ToString("X4") + ";");
            @out.WriteLine();
            @out.WriteLine("        /// <summary>");
            @out.WriteLine("        /// The ZigBee Cluster Library Cluster Name");
            @out.WriteLine("        /// </summary>");
            @out.WriteLine("        public const string CLUSTER_NAME = \"" + cluster.Name + "\";");
            @out.WriteLine();

            if (cluster.Attributes.Count != 0)
            {
                @out.WriteLine("        // Attribute constants");
                foreach (ZigBeeXmlAttribute attribute in cluster.Attributes)
                {
                    if (attribute.ArrayStart != null && attribute.ArrayCount != null && attribute.ArrayCount > 0)
                    {
                        int? arrayCount = attribute.ArrayStart;
                        int? arrayStep = attribute.ArrayStep == null ? 1 : attribute.ArrayStep;
                        for (int count = 0; count < attribute.ArrayCount; count++)
                        {
                            if (attribute.Description.Count != 0)
                            {
                                @out.WriteLine();
                                @out.WriteLine("        /// <summary>");
                                OutputWithLinebreak(@out, "        ", attribute.Description);
                                @out.WriteLine("     /// </summary>");
                            }

                            String name = Regex.Replace(attribute.Name, "\\{\\{count\\}\\}", arrayCount.ToString()); //attribute.Name.replaceAll("\\{\\{count\\}\\}", arrayCount));
                            @out.WriteLine("        public const ushort " + GetEnum(name) + " = 0x" + (attribute.Code + arrayCount).Value.ToString("X4") + ";");
                            arrayCount += arrayStep;
                        }
                    }
                    else
                    {
                        if (attribute.Description.Count != 0)
                        {
                            @out.WriteLine();
                            @out.WriteLine("        /// <summary>");
                            OutputWithLinebreak(@out, "        ", attribute.Description);
                            @out.WriteLine("        /// </summary>");
                        }
                        @out.WriteLine("        public const ushort " + GetEnum(attribute.Name) + " = 0x" + attribute.Code.ToString("X4") + ";");
                    }
                }
                @out.WriteLine();
            }

            @out.WriteLine("        protected override Dictionary<ushort, ZclAttribute> InitializeClientAttributes()");
            @out.WriteLine("        {");
            CreateInitializeAttributes(@out, cluster.Name, attributesClient);
            @out.WriteLine();

            @out.WriteLine("        protected override Dictionary<ushort, ZclAttribute> InitializeServerAttributes()");
            @out.WriteLine("        {");
            CreateInitializeAttributes(@out, cluster.Name, attributesServer);
            @out.WriteLine();


            if (commandsServer != 0)
            {
                @out.WriteLine("        protected override Dictionary<ushort, Func<ZclCommand>> InitializeServerCommands()");
                @out.WriteLine("        {");
                @out.WriteLine("            Dictionary<ushort, Func<ZclCommand>> commandMap = new Dictionary<ushort, Func<ZclCommand>>(" + commandsServer + ");");
                @out.WriteLine();
                foreach (ZigBeeXmlCommand command in cluster.Commands)
                {
                    if (command.Source.Equals("server", StringComparison.InvariantCultureIgnoreCase))
                    {
                        @out.WriteLine("            commandMap.Add(0x" + command.Code.ToString("X4") + ", () => new " + StringToUpperCamelCase(command.Name) + "());");
                    }
                }
                @out.WriteLine();

                @out.WriteLine("            return commandMap;");
                @out.WriteLine("        }");
                @out.WriteLine();
            }

            if (commandsClient != 0)
            {
                @out.WriteLine("        protected override Dictionary<ushort, Func<ZclCommand>> InitializeClientCommands()");
                @out.WriteLine("        {");
                @out.WriteLine("            Dictionary<ushort, Func<ZclCommand>> commandMap = new Dictionary<ushort, Func<ZclCommand>>(" + commandsClient + ");");
                @out.WriteLine();
                foreach (ZigBeeXmlCommand command in cluster.Commands)
                {
                    if (command.Source.Equals("client", StringComparison.InvariantCultureIgnoreCase))
                    {
                        @out.WriteLine("            commandMap.Add(0x" + command.Code.ToString("X4") + ", () => new " + StringToUpperCamelCase(command.Name) + "());");
                    }
                }
                @out.WriteLine();

                @out.WriteLine("            return commandMap;");
                @out.WriteLine("        }");
                @out.WriteLine();
            }

            @out.WriteLine("        /// <summary>");
            @out.WriteLine("        /// Default constructor to create a " + cluster.Name + " cluster.");
            @out.WriteLine("        ///");
            @out.WriteLine("        /// <param name=\"zigbeeEndpoint\"> the ZigBeeEndpoint this cluster is contained within </param>");
            @out.WriteLine("        /// </summary>");
            @out.WriteLine("        public " + className + "(ZigBeeEndpoint zigbeeEndpoint)");
            @out.WriteLine("            :base(zigbeeEndpoint, CLUSTER_ID, CLUSTER_NAME)");
            @out.WriteLine("        {");
            @out.WriteLine("        }");

            foreach (ZigBeeXmlCommand command in cluster.Commands)
            {
                @out.WriteLine();
                @out.WriteLine("        /// <summary>");
                @out.WriteLine("        /// The " + command.Name);
                if (command.Description.Count != 0)
                {
                    @out.WriteLine("        ///");
                    OutputWithLinebreak(@out, "        ", command.Description);
                }
                @out.WriteLine("        ///");

                LinkedList<ZigBeeXmlField> fields = new LinkedList<ZigBeeXmlField>(command.Fields);
                foreach (ZigBeeXmlField field in fields)
                {
                    @out.WriteLine("        /// <param name=\"" + StringToLowerCamelCase(field.Name) + "\" <see cref=\"" + GetDataTypeClass(field) + "\"> " + field.Name + "</ param >");
                }

                @out.WriteLine("        /// <returns> the command result Task </returns>");
                @out.WriteLine("        /// </summary>");
                @out.Write("        public Task<CommandResult> " + StringToUpperCamelCase(command.Name) + "(");

                bool first = true;
                foreach (ZigBeeXmlField field in fields)
                {
                    if (first == false)
                    {
                        @out.Write(", ");
                    }
                    @out.Write(GetDataTypeClass(field) + " " + StringToLowerCamelCase(field.Name));
                    first = false;
                }

                @out.WriteLine(")");
                @out.WriteLine("        {");
                if (fields.Count == 0)
                {
                    @out.WriteLine("            return Send(new " + StringToUpperCamelCase(command.Name) + "());");
                }
                else
                {
                    @out.WriteLine("            " + StringToUpperCamelCase(command.Name) + " command = new " + StringToUpperCamelCase(command.Name) + "();");
                    @out.WriteLine();
                    @out.WriteLine("            // Set the fields");

                    foreach (ZigBeeXmlField field in fields)
                    {
                        @out.WriteLine("            command." + StringToUpperCamelCase(field.Name) + " = " + StringToLowerCamelCase(field.Name) + ";");
                    }
                    @out.WriteLine();
                    @out.WriteLine("            return Send(command);");
                }
                @out.WriteLine("        }");
            }

            @out.WriteLine("    }");
            @out.WriteLine("}");

            @out.Flush();
            @out.Close();
        }

        private void CreateInitializeAttributes(TextWriter @out, string clusterName, List<ZigBeeXmlAttribute> attributes)
        {
            @out.WriteLine("            Dictionary<ushort, ZclAttribute> attributeMap = new Dictionary<ushort, ZclAttribute>(" + attributes.Count + ");");

            if (attributes.Count != 0)
            {
                @out.WriteLine();
                foreach (ZigBeeXmlAttribute attribute in attributes)
                {
                    if (attribute.ArrayStart != null && attribute.ArrayCount != null && attribute.ArrayCount > 0)
                    {
                        int? ArrayCount = attribute.ArrayStart;
                        int? arrayStep = attribute.ArrayStep == null ? 1 : attribute.ArrayStep;
                        for (int count = 0; count < attribute.ArrayCount; count++)
                        {
                            string name = Regex.Replace(attribute.Name, "\\{\\{count\\}\\}", ArrayCount.ToString());
                            //String name = attribute.Name,"\\{\\{count\\}\\}", Integer.toString(ArrayCount));
                            @out.WriteLine("            attributeMap.Add(" + GetEnum(name) + ", " + DefineAttribute(attribute, clusterName, name, 0) + ");");
                            ArrayCount += arrayStep;
                        }
                    }
                    else
                    {
                        @out.WriteLine("            attributeMap.Add(" + GetEnum(attribute.Name) + ", " + DefineAttribute(attribute, clusterName, attribute.Name, 0) + ");");
                    }
                }
            }
            @out.WriteLine();
            @out.WriteLine("            return attributeMap;");
            @out.WriteLine("        }");
        }

        private string DefineAttribute(ZigBeeXmlAttribute attribute, string clusterName, string attributeName, int count)
        {
            return "new ZclAttribute(this, " + GetEnum(attributeName) + ", \"" + attributeName + "\", " + "ZclDataType.Get(DataType."
                    + attribute.Type + "), " + (!attribute.Optional).ToString().ToLower() + ", " + true.ToString().ToLower() 
                    + ", " + attribute.Writable.ToString().ToLower() + ", " + attribute.Reportable.ToString().ToLower() + ")";
        }

        private string GetEnum(string name)
        {
            return "ATTR_" + StringToConstantEnum(name);
        }
    }
}
