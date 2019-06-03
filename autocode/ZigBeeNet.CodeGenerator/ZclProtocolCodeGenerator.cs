using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ZigBeeNet.CodeGenerator.Zcl;
using Attribute = ZigBeeNet.CodeGenerator.Zcl.Attribute;

namespace ZigBeeNet.CodeGenerator
{
    public static class ZclProtocolCodeGenerator
    {
        private const int _lineLen = 120;

        private static string _generatedDate;
        private static string _outRootPath;

        /// <summary>
        /// The main method for running the code generator.
        /// </summary>
        public static void Generate(string[] args = null)
        {
            _generatedDate = DateTime.UtcNow.ToShortDateString() + " - " + DateTime.UtcNow.ToShortTimeString();

            string definitionFilePathZcl = "./Resources/zcl_definition.md";
            string definitionFilePathOta = "./Resources/ota_definition.md";

            if (args != null && args.Length > 0)
            {
                _outRootPath = args[0];
            }

            Context contextZcl = new Context();
            FileInfo definitionFileZcl = new FileInfo(definitionFilePathZcl);
            FileInfo definitionFileOta = new FileInfo(definitionFilePathOta);

            if (!definitionFileZcl.Exists)
            {
                Console.WriteLine("ZCL definition file does not exist: " + definitionFilePathZcl);
            }
            else if (!definitionFileOta.Exists)
            {
                Console.WriteLine("OTA definition file does not exist: " + definitionFilePathZcl);
            }
            else
            {
                try
                {
                    contextZcl.Lines = new List<string>(File.ReadAllLines(definitionFilePathZcl, Encoding.UTF8));
                    contextZcl.Lines.AddRange(File.ReadAllLines(definitionFilePathOta, Encoding.UTF8));

                    GenerateZclCode(contextZcl);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Reading lines from Zcl/Ota definition file failed:");
                    Console.WriteLine(e.ToString());
                }
            }
        }

        private static void GenerateZclCode(Context context)
        {
            ZclProtocolDefinitionParser.ParseProfiles(context);

            try
            {
                GenerateAttributeEnumeration(context);
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to generate attribute enum classes.");
                Console.WriteLine(e.ToString());

                return;
            }

            try
            {
                GenerateFieldEnumeration(context);
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to generate field enum classes.");
                Console.WriteLine(e.ToString());

                return;
            }

            try
            {
                GenerateZclCommandClasses(context);
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to generate zcl command classes.");
                Console.WriteLine(e.ToString());

                return;
            }

            try
            {
                GenerateZclClusterClasses(context);
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to generate cluster classes.");
                Console.WriteLine(e.ToString());

                return;
            }
        }

        private static void OutputClassDoc(StringBuilder code, string description)
        {
            code.AppendLine("   /// <summary>");
            code.AppendLine("   /// " + description);
            code.AppendLine("   ///");
            code.AppendLine("   /// Code is auto-generated. Modifications may be overwritten!");
            code.AppendLine("   ///");
            code.AppendLine("   /// </summary>");
        }

        private static void GenerateZclCommandClasses(Context context)
        {
            List<Profile> profiles = new List<Profile>(context.Profiles.Values);

            foreach (Profile profile in profiles)
            {
                List<Cluster> clusters = new List<Cluster>(profile.Clusters.Values);

                foreach (Cluster cluster in clusters)
                {
                    List<Command> commands = new List<Command>();

                    commands.AddRange(cluster.Received.Values);
                    commands.AddRange(cluster.Generated.Values);

                    foreach (Command command in commands)
                    {
                        string packageRoot = "ZigBeeNet.ZCL." + cluster.ClusterType.Replace("_", "").ToUpper();
                        string className = command.NameUpperCamelCase;

                        List<Field> fields = new List<Field>(command.Fields.Values);

                        var code = new StringBuilder();

                        CodeGeneratorUtil.OutputLicense(code);

                        code.AppendLine("using System;");
                        code.AppendLine("using System.Collections.Generic;");
                        code.AppendLine("using System.Linq;");
                        code.AppendLine("using System.Text;");
                        code.AppendLine("using ZigBeeNet.ZCL.Protocol;");
                        code.AppendLine("using ZigBeeNet.ZCL.Field;");
                        code.AppendLine("using ZigBeeNet.ZCL.Clusters." + cluster.ClusterName.Replace("/", "").Replace(" ", "").Replace("(", "").Replace(")", "") + ";");

                        foreach (Field field in fields)
                        {
                            string typeName;

                            if (field.DataTypeClass.StartsWith("List"))
                            {
                                typeName = field.DataTypeClass;
                                typeName = typeName.Substring(typeName.IndexOf("<") + 1);
                                typeName = typeName.Substring(0, typeName.IndexOf(">"));
                            }
                            else
                            {
                                typeName = field.DataTypeClass;
                            }

                            switch (typeName)
                            {
                                case "int":
                                case "bool":
                                case "object":
                                case "long":
                                case "string":
                                case "int[]":
                                    continue;
                                case "IeeeAddress":
                                    //code.AppendLine("");
                                    continue;
                                case "ZclStatus":
                                    //code.AppendLine("");
                                    continue;
                                case "ImageUpgradeStatus":
                                    //code.AppendLine("");
                                    continue;
                            }

                            //code.AppendLine("");
                        }

                        code.AppendLine();
                        code.AppendLine();
                        code.AppendLine("namespace ZigBeeNet.ZCL.Clusters." + cluster.ClusterName.Replace("/", "").Replace(" ", "").Replace("(", "").Replace(")", ""));
                        code.AppendLine("{");
                        code.AppendLine("    /// <summary>");
                        code.AppendLine("    /// " + command.CommandLabel + " value object class.");

                        code.AppendLine("    /// <para>");
                        code.AppendLine("    /// Cluster: " + cluster.ClusterName + ". Command is sent "
                                + (cluster.Received.ContainsValue(command) ? "TO" : "FROM") + " the server.");
                        code.AppendLine("    /// This command is " + ((cluster.ClusterType.Equals("GENERAL"))
                                ? "a generic command used across the profile."
                                : "a specific command used for the " + cluster.ClusterName + " cluster."));

                        if (command.CommandDescription.Count > 0)
                        {
                            code.AppendLine("    ///");
                            OutputWithLinebreak(code, "    ", command.CommandDescription);
                        }

                        code.AppendLine("    /// </para>");
                        code.AppendLine("    /// Code is auto-generated. Modifications may be overwritten!");

                        code.AppendLine("    /// </summary>");
                        code.AppendLine("    public class " + className + " : ZclCommand");
                        code.AppendLine("    {");

                        foreach (Field field in fields)
                        {
                            code.AppendLine("        /// <summary>");
                            code.AppendLine("        /// " + field.FieldLabel + " command message field.");
                            if (field.Description.Count != 0)
                            {
                                code.AppendLine("        ///");
                                OutputWithLinebreak(code, "        ", field.Description);
                            }
                            code.AppendLine("        /// </summary>");
                            code.AppendLine("        public " + field.DataTypeClass + " " + field.NameUpperCamelCase + " { get; set; }");
                            code.AppendLine();
                        }

                        code.AppendLine();
                        code.AppendLine("        /// <summary>");
                        code.AppendLine("        /// Default constructor.");
                        code.AppendLine("        /// </summary>");
                        code.AppendLine("        public " + className + "()");
                        code.AppendLine("        {");
                        code.AppendLine("            GenericCommand = " + ((cluster.ClusterType.Equals("GENERAL")) ? "true" : "false") + ";");

                        if (!cluster.ClusterType.Equals("GENERAL"))
                        {
                            code.AppendLine("            ClusterId = " + cluster.ClusterId + ";");
                        }

                        code.AppendLine("            CommandId = " + command.CommandId + ";");

                        code.AppendLine("            CommandDirection = ZclCommandDirection."
                                + (cluster.Received.ContainsValue(command) ? "CLIENT_TO_SERVER" : "SERVER_TO_CLIENT")
                                + ";");

                        code.AppendLine("        }");

                        if (fields.Count > 0)
                        {
                            code.AppendLine();
                            code.AppendLine("        internal override void Serialize(ZclFieldSerializer serializer)");
                            code.AppendLine("        {");
                            foreach (Field field in fields)
                            {
                                // Rules...
                                // if listSizer == null, then just output the field
                                // if listSizer != null and contains && then check the param bit

                                if (field.ListSizer != null)
                                {
                                    if (field.ListSizer.Equals("statusResponse"))
                                    {
                                        // Special case where a ZclStatus may be sent, or, a list of results.
                                        // This checks for a single response
                                        code.AppendLine("            if (Status == ZclStatus.SUCCESS)");
                                        code.AppendLine("            {");
                                        code.AppendLine("                serializer.Serialize(Status, ZclDataType.Get(DataType.ZCL_STATUS));");
                                        code.AppendLine("                return;");
                                        code.AppendLine("            }");
                                    }
                                    else if (field.ConditionOperator != null)
                                    {
                                        if (field.ConditionOperator == "&&")
                                        {
                                            code.AppendLine();
                                            code.AppendLine("            if ((" + CodeGeneratorUtil.LabelToUpperCamelCase(field.ListSizer) + " & " + field.Condition + ") != 0)");
                                            code.AppendLine("            {");
                                        }
                                        else
                                        {
                                            code.AppendLine();
                                            code.AppendLine("            if (" + CodeGeneratorUtil.LabelToUpperCamelCase(field.ListSizer) + " " + field.ConditionOperator + " " + field.Condition + ")");
                                            code.AppendLine("            {");
                                        }
                                        code.AppendLine("                serializer.Serialize(" + field.NameUpperCamelCase + ", ZclDataType.Get(DataType." + field.DataType + "));");
                                        code.AppendLine("            }");
                                    }
                                    else
                                    {
                                        code.AppendLine();
                                        code.AppendLine("            for (int cnt = 0; cnt < " + field.NameUpperCamelCase + ".Count; cnt++)");
                                        code.AppendLine("            {");
                                        code.AppendLine("                serializer.Serialize(" + field.NameUpperCamelCase + ".Get(cnt), ZclDataType.Get(DataType." + field.DataType + "));");
                                        code.AppendLine("            }");
                                    }
                                }
                                else
                                {
                                    code.AppendLine("            serializer.Serialize(" + field.NameUpperCamelCase + ", ZclDataType.Get(DataType." + field.DataType + "));");
                                }
                            }
                            code.AppendLine("        }");

                            code.AppendLine();
                            code.AppendLine("        internal override void Deserialize(ZclFieldDeserializer deserializer)");
                            code.AppendLine("        {");

                            foreach (Field field in fields)
                            {
                                if (field.ListSizer != null)
                                {
                                    if (field.ListSizer.Equals("statusResponse"))
                                    {
                                        // Special case where a ZclStatus may be sent, or, a list of results.
                                        // This checks for a single response
                                        code.AppendLine("            if (deserializer.RemainingLength == 1)");
                                        code.AppendLine("            {");
                                        code.AppendLine("                Status = deserializer.Deserialize<ZclStatus>(ZclDataType.Get(DataType.ZCL_STATUS));");
                                        code.AppendLine("                return;");
                                        code.AppendLine("            }");
                                    }
                                    else if (field.ConditionOperator != null)
                                    {
                                        if (field.ConditionOperator == "&&")
                                        {
                                            code.AppendLine();
                                            code.AppendLine("            if ((" + CodeGeneratorUtil.LabelToUpperCamelCase(field.ListSizer) + " & " + field.Condition + ") != 0)");
                                            code.AppendLine("            {");
                                        }
                                        else
                                        {
                                            code.AppendLine();
                                            code.AppendLine("            if (" + CodeGeneratorUtil.LabelToUpperCamelCase(field.ListSizer) + " " + field.ConditionOperator + " " + field.Condition + ")");
                                            code.AppendLine("            {");
                                        }
                                        code.AppendLine("                " + field.NameUpperCamelCase + " = deserializer.Deserialize<" + field.DataTypeClass + ">(ZclDataType.Get(DataType." + field.DataType + "));");
                                        code.AppendLine("            }");
                                    }
                                    else
                                    {
                                        code.AppendLine();
                                        code.AppendLine("            for (int cnt = 0; cnt < " + field.NameLowerCamelCase + ".Count; cnt++)");
                                        code.AppendLine("            {");
                                        code.AppendLine("                " + field.NameUpperCamelCase + " = deserializer.Deserialize<" + field.DataTypeClass + ">(ZclDataType.Get(DataType." + field.DataType + "));");
                                        code.AppendLine("            }");
                                    }
                                }
                                else
                                {
                                    code.AppendLine("            " + field.NameUpperCamelCase + " = deserializer.Deserialize<" + field.DataTypeClass + ">(ZclDataType.Get(DataType." + field.DataType + "));");
                                }
                            }
                            code.AppendLine("        }");
                        }

                        int fieldLen = 0;
                        foreach (Field field in fields)
                        {
                            fieldLen += field.NameLowerCamelCase.Length + 20;
                        }

                        code.AppendLine();
                        code.AppendLine("        public override string ToString()");
                        code.AppendLine("        {");
                        code.AppendLine("            var builder = new StringBuilder();");
                        code.AppendLine();
                        code.AppendLine("            builder.Append(\"" + className + " [\");");
                        code.AppendLine("            builder.Append(base.ToString());");
                        foreach (Field field in fields)
                        {
                            code.AppendLine("            builder.Append(\", " + field.NameUpperCamelCase + "=\");");
                            code.AppendLine("            builder.Append(" + field.NameUpperCamelCase + ");");
                        }
                        code.AppendLine("            builder.Append(\']\');");
                        code.AppendLine();
                        code.AppendLine("            return builder.ToString();");
                        code.AppendLine("        }");
                        code.AppendLine("    }");
                        code.AppendLine("}");

                        Console.WriteLine(code.ToString());

                        var outputPath = Path.Combine(_outRootPath, cluster.ClusterName.Replace("/", "").Replace(" ", "").Replace("(", "").Replace(")", ""));
                        var commmandClassFile = command.NameUpperCamelCase + ".cs";
                        var commandFullPath = Path.Combine(outputPath, commmandClassFile.Replace(" ", ""));

                        Directory.CreateDirectory(outputPath);

                        File.Delete(commandFullPath);

                        File.WriteAllText(commandFullPath, code.ToString(), Encoding.UTF8);
                    }
                }
            }
        }


        private static void OutputWithLinebreak(StringBuilder builder, string indent, List<string> lines)
        {
            foreach (string line in lines)
            {
                string[] words = line.Split(" ");
                if (words.Length == 0)
                {
                    return;
                }

                //builder.AppendLine();
                builder.Append(indent + "///");

                int len = 2;
                foreach (string word in words)
                {
                    if (len + word.Length > _lineLen)
                    {
                        builder.AppendLine();
                        builder.Append(indent + "///");
                        len = 2;
                    }

                    builder.Append(" ");
                    builder.Append(word);

                    len += word.Length;
                }

                builder.AppendLine();

                if (len != 0)
                {
                    Console.WriteLine();
                }
            }
        }

        private static void GenerateZclClusterClasses(Context context)
        {
            LinkedList<Profile> profiles = new LinkedList<Profile>(context.Profiles.Values);

            foreach (Profile profile in profiles)
            {
                List<Cluster> clusters = new List<Cluster>(profile.Clusters.Values);

                foreach (Cluster cluster in clusters)
                {
                    string className = "Zcl" + cluster.NameUpperCamelCase + "Cluster";

                    List<Command> commands = new List<Command>();
                    commands.AddRange(cluster.Received.Values);
                    commands.AddRange(cluster.Generated.Values);

                    var code = new StringBuilder();

                    CodeGeneratorUtil.OutputLicense(code);

                    code.AppendLine();
                    code.AppendLine("using System;");
                    code.AppendLine("using System.Collections.Concurrent;");
                    code.AppendLine("using System.Collections.Generic;");
                    code.AppendLine("using System.Linq;");
                    code.AppendLine("using System.Text;");
                    code.AppendLine("using System.Threading;");
                    code.AppendLine("using System.Threading.Tasks;");
                    code.AppendLine("using ZigBeeNet.DAO;");
                    code.AppendLine("using ZigBeeNet.ZCL.Protocol;");
                    code.AppendLine("using ZigBeeNet.ZCL.Field;");

                    if (commands.Count > 0)
                        code.AppendLine("using ZigBeeNet.ZCL.Clusters." + cluster.ClusterName.Replace("/", "").Replace(" ", "").Replace("(", "").Replace(")", "") + ";");

                    List<string> imports = new List<string>();

                    bool useList = false;
                    foreach (Command command in commands)
                    {
                        List<Field> fields = new List<Field>(command.Fields.Values);
                        Console.WriteLine("Checking command " + command.CommandLabel);

                        foreach (Field field in fields)
                        {
                            Console.WriteLine("Checking " + field.DataTypeClass);

                            string typeName;
                            if (field.DataTypeClass.StartsWith("List"))
                            {
                                useList = true;
                                typeName = field.DataTypeClass;
                                typeName = typeName.Substring(typeName.IndexOf("<") + 1);
                                typeName = typeName.Substring(0, typeName.IndexOf(">"));
                            }
                            else
                            {
                                typeName = field.DataTypeClass;
                            }

                            switch (typeName)
                            {
                                case "int":
                                case "bool":
                                case "object":
                                case "long":
                                case "string":
                                case "int[]":
                                    continue;
                                case "IeeeAddress":
                                    //imports.Add(packageRootPrefix + "." + typeName);
                                    Console.WriteLine("Adding " + typeName);
                                    continue;
                                case "ZclStatus":
                                    //imports.add(packageRootPrefix + packageZcl + ".ZclStatus");
                                    continue;
                                case "ImageUpgradeStatus":
                                    //imports.add(packageRootPrefix + packageZclField + ".ImageUpgradeStatus");
                                    continue;
                            }

                            //imports.Add(packageRootPrefix + packageName + "." + typeName);
                        }
                    }

                    if (useList)
                    {
                        // imports.add(packageRootPrefix + packageZclField);
                    }

                    bool addAttributeTypes = false;
                    bool readAttributes = false;
                    bool writeAttributes = false;

                    foreach (Attribute attribute in cluster.Attributes.Values)
                    {
                        if (attribute.AttributeAccess.ToLower().Contains("write"))
                        {
                            addAttributeTypes = true;
                            writeAttributes = true;
                        }
                        if (attribute.AttributeAccess.ToLower().Contains("read"))
                        {
                            readAttributes = true;
                        }

                        if ("Calendar".Equals(attribute.DataTypeClass))
                        {
                            //imports.add("");
                        }
                        if ("IeeeAddress".Equals(attribute.DataTypeClass))
                        {
                            //imports.add("c");
                        }
                        if ("ImageUpgradeStatus".Equals(attribute.DataTypeClass))
                        {
                            //imports.add(packageRootPrefix + packageZclField + ".ImageUpgradeStatus");
                        }
                    }

                    if (addAttributeTypes)
                    {
                        //imports.add("");
                    }

                    //imports.Add(packageRoot + _packageZcl + ".ZclCluster");

                    if (cluster.Attributes.Count != 0)
                    {
                        //imports.Add(packageRoot + _packageZclProtocol + ".ZclDataType");
                    }

                    if (commands.Count != 0)
                    {
                        //imports.Add(packageRoot + _packageZcl + ".ZclCommand");
                    }

                    // imports.Add(packageRoot + packageZcl + ".ZclCommandMessage");
                    // imports.Add(packageRoot + ".ZigBeeDestination");
                    // imports.Add(packageRoot + ".ZigBeeEndpoint");

                    if (cluster.Attributes.Count != 0 || commands.Count != 0)
                    {
                        //imports.Add(packageRoot + ".CommandResult");
                    }

                    //imports.Add(packageRoot + _packageZcl + ".ZclAttribute");

                    if (cluster.Attributes.Count != 0 || commands.Count != 0)
                    {
                        //imports.Add("");
                    }

                    // importsAadd("");

                    foreach (Attribute attribute in cluster.Attributes.Values)
                    {
                        if (attribute.AttributeAccess.ToLower().Contains("read"))
                        {
                            // imports.Add("");
                        }
                    }

                    foreach (Command command in commands)
                    {
                        //imports.Add(GetZclClusterCommandPackage(packageRoot, cluster));
                    }

                    if (cluster.Attributes.Count != 0)
                    {
                        //imports.Add(packageRoot + _packageZclProtocol + ".ZclClusterType");
                    }

                    List<string> importList = new List<string>();
                    importList.AddRange(imports);
                    importList = importList.Distinct().ToList();
                    importList.Sort();

                    // NOT USED
                    foreach (string importClass in importList)
                    {
                        code.AppendLine("using " + importClass + ";");
                    }

                    code.AppendLine();

                    code.AppendLine("namespace ZigBeeNet.ZCL.Clusters");
                    code.AppendLine("{");
                    code.AppendLine("    /// <summary>");
                    code.AppendLine("    /// " + cluster.ClusterName + "cluster implementation (Cluster ID 0x" + cluster.ClusterId.ToString("X4") + ").");

                    if (cluster.ClusterDescription.Count > 0)
                    {
                        code.AppendLine("    ///");
                    }

                    OutputWithLinebreak(code, "    ", cluster.ClusterDescription);

                    code.AppendLine("    ///");
                    code.AppendLine("    /// Code is auto-generated. Modifications may be overwritten!");

                    code.AppendLine("    /// </summary>");
                    code.AppendLine("    public class " + className + " : ZclCluster");
                    code.AppendLine("    {");
                    code.AppendLine("        /// <summary>");
                    code.AppendLine("        /// The ZigBee Cluster Library Cluster ID");
                    code.AppendLine("        /// </summary>");
                    code.AppendLine("        public const ushort CLUSTER_ID = 0x" + cluster.ClusterId.ToString("X4") + ";");
                    code.AppendLine();
                    code.AppendLine("        /// <summary>");
                    code.AppendLine("        /// The ZigBee Cluster Library Cluster Name");
                    code.AppendLine("        /// </summary>");
                    code.AppendLine("        public const string CLUSTER_NAME = \"" + cluster.ClusterName + "\";");
                    code.AppendLine();

                    if (cluster.Attributes.Count != 0)
                    {
                        code.AppendLine("        /* Attribute constants */");
                        code.AppendLine();

                        foreach (Attribute attribute in cluster.Attributes.Values)
                        {
                            code.AppendLine("        /// <summary>");
                            OutputWithLinebreak(code, "        ", attribute.AttributeDescription);
                            code.AppendLine("        /// </summary>");
                            code.AppendLine("        public const ushort " + attribute.EnumName + " = 0x" + attribute.AttributeId.ToString("X4") + ";");
                            code.AppendLine();
                        }

                        code.AppendLine();
                    }

                    code.AppendLine("        // Attribute initialisation");
                    code.AppendLine("        protected override Dictionary<ushort, ZclAttribute> InitializeAttributes()");
                    code.AppendLine("        {");
                    code.AppendLine("            Dictionary<ushort, ZclAttribute> attributeMap = new Dictionary<ushort, ZclAttribute>(" + cluster.Attributes.Count + ");");

                    if (cluster.Attributes.Count != 0)
                    {
                        code.AppendLine();
                        code.AppendLine("            ZclClusterType " + cluster.NameLowerCamelCase.Replace("/", "").Replace(" ", "").Replace("(", "").Replace(")", "") + " = ZclClusterType.GetValueById(ClusterType." + cluster.ClusterType + ");");
                        code.AppendLine();

                        foreach (Attribute attribute in cluster.Attributes.Values)
                        {
                            code.AppendLine("            attributeMap.Add(" + attribute.EnumName
                                    + ", new ZclAttribute(" + cluster.NameLowerCamelCase.Replace("/", "").Replace(" ", "").Replace("(", "").Replace(")", "") + ", " + attribute.EnumName
                                    + ", \"" + attribute.AttributeLabel + "\", " + "ZclDataType.Get(DataType." + attribute.DataType + ")"
                                    + ", " + "mandatory".Equals(attribute.AttributeImplementation.ToLower()).ToString().ToLower() + ", "
                                    + attribute.AttributeAccess.ToLower().Contains("read").ToString().ToLower() + ", "
                                    + attribute.AttributeAccess.ToLower().Contains("write").ToString().ToLower() + ", "
                                    + "mandatory".Equals(attribute.AttributeReporting.ToLower()).ToString().ToLower() + "));");
                        }
                    }

                    code.AppendLine();
                    code.AppendLine("            return attributeMap;");
                    code.AppendLine("        }");
                    code.AppendLine();

                    code.AppendLine("        /// <summary>");
                    code.AppendLine("        /// Default constructor to create a " + cluster.ClusterName + " cluster.");
                    code.AppendLine("        ///");
                    code.AppendLine("        /// <param name =\"zigbeeEndpoint\">The ZigBeeEndpoint</param>");
                    code.AppendLine("        /// </summary>");
                    code.AppendLine("        public " + className + "(ZigBeeEndpoint zigbeeEndpoint)");
                    code.AppendLine("            : base(zigbeeEndpoint, CLUSTER_ID, CLUSTER_NAME)");
                    code.AppendLine("        {");
                    code.AppendLine("        }");
                    code.AppendLine();

                    foreach (Attribute attribute in cluster.Attributes.Values)
                    {
                        DataTypeMap zclDataType = ZclDataType.Mapping[attribute.DataType];

                        if (attribute.AttributeAccess.ToLower().Contains("write"))
                        {
                            OutputAttributeDoc(code, "Set", attribute, zclDataType);
                            code.AppendLine("        public Task<CommandResult> Set" + attribute.NameUpperCamelCase.Replace("_", "") + "(object value)");
                            code.AppendLine("        {");
                            code.AppendLine("            return Write(_attributes[" + attribute.EnumName + "], value);");
                            code.AppendLine("        }");
                            code.AppendLine();
                        }

                        if (attribute.AttributeAccess.ToLower().Contains("read"))
                        {
                            OutputAttributeDoc(code, "Get", attribute, zclDataType);
                            code.AppendLine("        public Task<CommandResult> Get" + attribute.NameUpperCamelCase.Replace("_", "") + "Async()");
                            code.AppendLine("        {");
                            code.AppendLine("            return Read(_attributes[" + attribute.EnumName + "]);");
                            code.AppendLine("        }");
                            OutputAttributeDoc(code, "Synchronously Get", attribute, zclDataType);
                            code.AppendLine("        public " + attribute.DataTypeClass + " Get" + attribute.NameUpperCamelCase.Replace("_", "") + "(long refreshPeriod)");
                            code.AppendLine("        {");
                            code.AppendLine("            if (_attributes[" + attribute.EnumName + "].IsLastValueCurrent(refreshPeriod))");
                            code.AppendLine("            {");
                            code.AppendLine("                return (" + attribute.DataTypeClass + ")_attributes[" + attribute.EnumName + "].LastValue;");
                            code.AppendLine("            }");
                            code.AppendLine();
                            code.AppendLine("            return (" + attribute.DataTypeClass + ")ReadSync(_attributes[" + attribute.EnumName + "]);");
                            code.AppendLine("        }");
                            code.AppendLine();
                        }

                        if (attribute.AttributeAccess.ToLower().Contains("read") && attribute.AttributeReporting.ToLower().Equals("mandatory"))
                        {
                            OutputAttributeDoc(code, "Set reporting for", attribute, zclDataType);
                            if (zclDataType.Analogue)
                            {
                                code.AppendLine("        public Task<CommandResult> Set" + attribute.NameUpperCamelCase + "Reporting(ushort minInterval, ushort maxInterval, object reportableChange)");
                                code.AppendLine("        {");
                                code.AppendLine("            return SetReporting(_attributes[" + attribute.EnumName + "], minInterval, maxInterval, reportableChange);");
                            }
                            else
                            {
                                code.AppendLine("        public Task<CommandResult> Set" + attribute.NameUpperCamelCase + "Reporting(ushort minInterval, ushort maxInterval)");
                                code.AppendLine("        {");
                                code.AppendLine("            return SetReporting(_attributes[" + attribute.EnumName + "], minInterval, maxInterval);");
                            }
                            code.AppendLine("        }");
                            code.AppendLine();
                        }
                    }

                    foreach (Command command in commands)
                    {
                        code.AppendLine();
                        code.AppendLine("        /// <summary>");
                        code.AppendLine("        /// The " + command.CommandLabel);

                        if (command.CommandDescription.Count != 0)
                        {
                            code.AppendLine("        ///");
                            OutputWithLinebreak(code, "        ", command.CommandDescription);
                        }

                        code.AppendLine("        ///");

                        List<Field> fields = new List<Field>(command.Fields.Values);
                        
                        foreach (Field field in fields)
                        {
                            code.AppendLine("        /// <param name=\"" + field.NameLowerCamelCase + "\"><see cref=\"" + field.DataTypeClass + "\"/> " + field.FieldLabel + "</param>");
                        }

                        code.AppendLine("        /// <returns>The Task<CommandResult> command result Task</returns>");
                        code.AppendLine("        /// </summary>");
                        code.Append("        public Task<CommandResult> " + command.NameUpperCamelCase + "(");

                        bool first = true;

                        foreach (Field field in fields)
                        {
                            if (first == false)
                            {
                                code.Append(", ");
                            }

                            code.Append(field.DataTypeClass + " " + field.NameLowerCamelCase);
                            first = false;
                        }

                        code.AppendLine(")");
                        code.AppendLine("        {");
                        code.AppendLine("            " + command.NameUpperCamelCase + " command = new " + command.NameUpperCamelCase + "();");

                        if (fields.Count != 0)
                        {
                            code.AppendLine();
                            code.AppendLine("            // Set the fields");
                        }

                        foreach (Field field in fields)
                        {
                            code.AppendLine("            command." + field.NameUpperCamelCase + " = " + field.NameLowerCamelCase + ";");
                        }

                        code.AppendLine();
                        code.AppendLine("            return Send(command);");
                        code.AppendLine("        }");
                    }

                    if (cluster.Received.Count > 0)
                    {
                        code.AppendLine();
                        code.AppendLine("        public override ZclCommand GetCommandFromId(int commandId)");
                        code.AppendLine("        {");
                        code.AppendLine("            switch (commandId)");
                        code.AppendLine("            {");

                        foreach (Command command in cluster.Received.Values)
                        {
                            code.AppendLine("                case " + command.CommandId + ": // " + command.CommandType);
                            code.AppendLine("                    return new " + command.NameUpperCamelCase + "();");
                        }

                        code.AppendLine("                    default:");
                        code.AppendLine("                        return null;");
                        code.AppendLine("            }");
                        code.AppendLine("        }");
                    }

                    if (cluster.Generated.Count > 0)
                    {
                        code.AppendLine();
                        code.AppendLine("        public ZclCommand getResponseFromId(int commandId)");
                        code.AppendLine("        {");
                        code.AppendLine("            switch (commandId)");
                        code.AppendLine("            {");

                        foreach (Command command in cluster.Generated.Values)
                        {
                            code.AppendLine("                case " + command.CommandId + ": // " + command.CommandType);
                            code.AppendLine("                    return new " + command.NameUpperCamelCase + "();");
                        }

                        code.AppendLine("                    default:");
                        code.AppendLine("                        return null;");
                        code.AppendLine("            }");
                        code.AppendLine("        }");
                    }

                    code.AppendLine("    }");

                    code.AppendLine("}");

                    Console.WriteLine(code.ToString());

                    var commandClassFile = "Zcl" + cluster.NameUpperCamelCase + "Cluster.cs";
                    var clusterFullPath = Path.Combine(_outRootPath, commandClassFile);

                    Directory.CreateDirectory(_outRootPath);

                    File.Delete(clusterFullPath);

                    File.WriteAllText(clusterFullPath, code.ToString(), Encoding.UTF8);
                }
            }
        }

        private static void GenerateAttributeEnumeration(Context context)
        {
            List<Profile> profiles = new List<Profile>(context.Profiles.Values);

            foreach (Profile profile in profiles)
            {
                List<Cluster> clusters = new List<Cluster>(profile.Clusters.Values);
                foreach (Cluster cluster in clusters)
                {
                    if (cluster.Attributes.Count != 0)
                    {
                        foreach (Attribute attribute in cluster.Attributes.Values)
                        {
                            if (attribute.ValueMap.Count == 0)
                            {
                                continue;
                            }

                            string packageRoot = "ZigBeeNet.ZCL.Clusters.";

                            string className = attribute.NameUpperCamelCase;

                            OutputEnum(packageRoot, className, attribute.ValueMap, cluster.ClusterName, attribute.AttributeLabel);
                        }
                    }
                }
            }
        }

        private static void GenerateFieldEnumeration(Context context)
        {
            List<Profile> profiles = new List<Profile>(context.Profiles.Values);

            foreach (Profile profile in profiles)
            {
                List<Cluster> clusters = new List<Cluster>(profile.Clusters.Values);
                foreach (Cluster cluster in clusters)
                {
                    List<Command> commands = new List<Command>();
                    commands.AddRange(cluster.Received.Values);
                    commands.AddRange(cluster.Generated.Values);

                    if (commands.Count != 0)
                    {
                        foreach (Command command in commands)
                        {
                            foreach (Field field in command.Fields.Values)
                            {
                                if (field.ValueMap.Count == 0)
                                {
                                    continue;
                                }

                                string packageRoot = "ZigBeeNet.ZCL.Clusters.";

                                string className = field.NameUpperCamelCase;

                                OutputEnum(packageRoot, className, field.ValueMap, cluster.ClusterName, field.FieldLabel);
                            }
                        }
                    }
                }
            }
        }

        private static void OutputEnum(string packageRoot, string className, SortedDictionary<int, string> valueMap, string parentName, string label)
        {
            var code = new StringBuilder();
            CodeGeneratorUtil.OutputLicense(code);

            parentName = parentName.Replace(" ", "").Replace("/", "").Replace(" ", "").Replace("(", "").Replace(")", "");

            code.AppendLine();
            code.AppendLine("using System;");
            code.AppendLine("using System.Collections.Generic;");
            code.AppendLine("using System.Text;");
            code.AppendLine();


            code.AppendLine();

            code.AppendLine("namespace " + packageRoot + parentName);
            code.AppendLine("{");
            OutputClassDoc(code, "Enumeration of " + parentName + " attribute " + label + " options.");
            code.AppendLine("   public enum " + className.Replace("/", "").Replace(" ", "").Replace("(", "").Replace(")", ""));
            code.AppendLine("   {");

            bool first = true;

            foreach (int key in valueMap.Keys)
            {
                string value = valueMap[key];

                if (!first)
                {
                    code.AppendLine(",");
                }

                first = false;

                code.Append("       " + CodeGeneratorUtil.LabelToEnumerationValue(value) + " = 0x" + key.ToString("X4"));
            }

            code.AppendLine();
            code.AppendLine("   }");
            code.AppendLine("}");

            var outputPath = Path.Combine(_outRootPath, parentName);
            var enumFile = className + ".cs";
            var enumFileFullPath = Path.Combine(outputPath, enumFile.Replace(" ", ""));

            Directory.CreateDirectory(outputPath);
            File.Delete(enumFileFullPath);
            File.WriteAllText(enumFileFullPath, code.ToString(), Encoding.UTF8);
        }

        private static void OutputAttributeDoc(StringBuilder code, string type, Attribute attribute, DataTypeMap zclDataType)
        {
            code.AppendLine();
            code.AppendLine("        /// <summary>");
            code.AppendLine("        /// " + type + " the " + attribute.AttributeLabel + " attribute [attribute ID" + attribute.AttributeId + "].");

            if (attribute.AttributeDescription.Count() != 0)
            {
                code.AppendLine("        ///");
                OutputWithLinebreak(code, "        ", attribute.AttributeDescription);
            }

            if ("Synchronously get".Equals(type))
            {
                code.AppendLine("        ///");
                code.AppendLine("        /// This method can return cached data if the attribute has already been received.");
                code.AppendLine("        /// The parameter refreshPeriod is used to control this. If the attribute has been received");
                code.AppendLine("        /// within refreshPeriod milliseconds, then the method will immediately return the last value");
                code.AppendLine("        /// received. If refreshPeriod is set to 0, then the attribute will always be updated.");
                code.AppendLine("        ///");
                code.AppendLine("        /// This method will block until the response is received or a timeout occurs unless the current value is returned.");
            }

            code.AppendLine("        ///");
            code.AppendLine("        /// The attribute is of type " + attribute.DataTypeClass + ".");
            code.AppendLine("        ///");
            code.AppendLine("        /// The implementation of this attribute by a device is " + attribute.AttributeImplementation.ToUpper());
            code.AppendLine("        ///");

            if ("Set reporting for".Equals(type))
            {
                code.AppendLine("        /// <param name=\"minInterval\">Minimum reporting period</param>");
                code.AppendLine("        /// <param name=\"maxInterval\">Maximum reporting period</param>");

                if (zclDataType.Analogue)
                {
                    code.AppendLine("        /// <param name=\"reportableChange\">Object delta required to trigger report</param>");
                }
            }
            else if ("Set".Equals(type))
            {
                code.AppendLine("        /// <param name=\"" + attribute.NameLowerCamelCase + "\">The " + attribute.DataTypeClass + " attribute value to be set</param>");
            }

            if ("Synchronously get".Equals(type))
            {
                code.AppendLine(
                        "        /// <param name=\"refreshPeriod\">The maximum age of the data (in milliseconds) before an update is needed</param>");
                code.AppendLine("        /// <returns>The " + attribute.DataTypeClass + " attribute value, or null on error</returns>");
            }
            else
            {
                code.AppendLine("        /// <returns>The Task<CommandResult> command result Task</returns>");
            }

            code.AppendLine("        /// </summary>");
        }

        //private static string GetFieldType(Field field)
        //{
        //    if (field.listSizer != null)
        //    {
        //        return "List<" + field.dataTypeClass + ">";
        //    }
        //    else
        //    {
        //        return field.dataTypeClass;
        //    }
        //}
    }
}
