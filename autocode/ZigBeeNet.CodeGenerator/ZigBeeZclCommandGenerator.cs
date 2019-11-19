using System;
using System.Collections.Generic;
using System.IO;
using ZigBeeNet.CodeGenerator.Xml;

namespace ZigBeeNet.CodeGenerator
{
    public class ZigBeeZclCommandGenerator : ZigBeeBaseFieldGenerator
    {

        public ZigBeeZclCommandGenerator(string sourceRootPath, List<ZigBeeXmlCluster> clusters, string generatedDate, Dictionary<string, string> dependencies)
            : base(sourceRootPath, generatedDate)
        {
            this._dependencies = dependencies;

            foreach (ZigBeeXmlCluster cluster in clusters)
            {
                try
                {
                    GenerateZclClusterCommands(cluster);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
        }

        private void GenerateZclClusterCommands(ZigBeeXmlCluster cluster)
        {

            foreach (ZigBeeXmlCommand command in cluster.Commands)
            {
                string packageRoot = GetZclClusterCommandPackage(cluster);
                string packagePath = GetPackagePath(_sourceRootPath, packageRoot);

                string className = StringToUpperCamelCase(command.Name);
                TextWriter @out = GetClassOut(packagePath, className);

                // List of fields that are handled internally by super class
                List<string> reservedFields = new List<string>();

                ImportsClear();

                OutputLicense(@out);

                ImportsAdd("System");
                ImportsAdd("System.Collections.Generic");
                ImportsAdd("System.Linq");
                ImportsAdd("System.Text");
                ImportsAdd("ZigBeeNet.Security");
                ImportsAdd("ZigBeeNet.ZCL.Protocol");
                ImportsAdd("ZigBeeNet.ZCL.Field");
                ImportsAdd("ZigBeeNet.ZCL.Clusters." + cluster.Name.Replace("/", "").Replace(" ", "").Replace("(", "").Replace(")", ""));

                OutputImports(@out);

                //@out.WriteLine("package " + packageRoot + ";");
                @out.WriteLine();
                //ImportsAdd("javax.annotation.Generated");

                //if (command.Response != null)
                //{
                //    ImportsAdd(packageRootPrefix + ".transaction.ZigBeeTransactionMatcher");
                //    ImportsAdd(packageRootPrefix + ".ZigBeeCommand");

                //    ImportsAdd(packageRoot + "." + command.Response.Command);
                //}

                string commandExtends = "";
                if (packageRoot.Contains(".zcl.", StringComparison.InvariantCultureIgnoreCase))
                {
                    //ImportsAdd(packageRootPrefix + packageZcl + ".ZclCommand");
                    //ImportsAdd(packageRootPrefix + packageZclProtocol + ".ZclCommandDirection");
                    commandExtends = "ZclCommand";
                    reservedFields.Add("ManufacturerCode");
                }
                else
                {
                    if (command.Name.Contains("Response"))
                    {
                        commandExtends = "ZdoResponse";
                        reservedFields.Add("Status");
                    }
                    else
                    {
                        commandExtends = "ZdoRequest";
                    }
                    //mportsAdd(packageRootPrefix + packageZdp + "." + commandExtends);
                }

                //if (command.Fields.Count > 0)
                //{
                //    ImportsAdd(packageRootPrefix + packageZcl + ".ZclFieldSerializer");
                //    ImportsAdd(packageRootPrefix + packageZcl + ".ZclFieldDeserializer");
                //    ImportsAdd(packageRootPrefix + packageZclProtocol + ".ZclDataType");
                //}

                //foreach (ZigBeeXmlField field in command.Fields)
                //{
                //    ImportsAddClass(field);
                //}

                //OutputImports(@out);

                //@out.WriteLine();
                @out.WriteLine("namespace ZigBeeNet.ZCL.Clusters." + cluster.Name.Replace("/", "").Replace(" ", "").Replace("(", "").Replace(")", ""));
                @out.WriteLine("{");

                @out.WriteLine("    /// <summary>");
                @out.WriteLine("    /// " + command.Name + " value object class.");

                @out.WriteLine("    ///");
                if (packageRoot.Contains(".zcl.", StringComparison.InvariantCultureIgnoreCase))
                {
                    @out.WriteLine("    /// Cluster: " + cluster.Name + ". Command ID 0x"
                            + command.Code.ToString("X2") + " is sent "
                            + (command.Source.Equals("client") ? "TO" : "FROM") + " the server.");
                    @out.WriteLine("    /// This command is " + ((cluster.Name.Equals("GENERAL", StringComparison.InvariantCultureIgnoreCase))
                            ? "a generic command used across the profile."
                            : "a specific command used for the " + cluster.Name + " cluster."));
                }

                if (command.Description.Count > 0)
                {
                    @out.WriteLine("    ///");
                    OutputWithLinebreak(@out, "    ", command.Description);
                }

                @out.WriteLine("    ///");
                @out.WriteLine("    /// Code is auto-generated. Modifications may be overwritten!");
                @out.WriteLine("    /// </summary>");
                OutputClassGenerated(@out);


                @out.Write("    public class " + className + " : " + commandExtends);
                if (command.Response != null)
                {
                    @out.Write(", IZigBeeTransactionMatcher");
                }
                @out.WriteLine();
                @out.WriteLine("    {");

                if (commandExtends.Equals("ZclCommand"))
                {
                    if (!cluster.Name.Equals("GENERAL", StringComparison.InvariantCultureIgnoreCase))
                    {
                        @out.WriteLine("        /// <summary>");
                        @out.WriteLine("        /// The cluster ID to which this command belongs.");
                        @out.WriteLine("        /// </summary>");
                        @out.WriteLine("        public const ushort CLUSTER_ID = 0x" + cluster.Code.ToString("X4") + ";");
                        @out.WriteLine();
                    }
                    @out.WriteLine("        /// <summary>");
                    @out.WriteLine("        /// The command ID.");
                    @out.WriteLine("        /// </summary>");
                    @out.WriteLine("        public const byte COMMAND_ID = 0x" + command.Code.ToString("X2") + ";");
                    @out.WriteLine();
                }
                else
                {
                    @out.WriteLine("        /// <summary>");
                    @out.WriteLine("        /// The ZDO cluster ID.");
                    @out.WriteLine("        /// </summary>");
                    @out.WriteLine("        public const ushort CLUSTER_ID = 0x" + command.Code.ToString("X4") + ";");
                    @out.WriteLine();
                }

                foreach (ZigBeeXmlField field in command.Fields)
                {
                    if (reservedFields.Contains(StringToUpperCamelCase(field.Name)))
                    {
                        continue;
                    }
                    if (GetAutoSized(command.Fields, StringToLowerCamelCase(field.Name)) != null)
                    {
                        continue;
                    }

                    @out.WriteLine("        /// <summary>");
                    @out.WriteLine("        /// " + field.Name + " command message field.");
                    if (field.Description.Count != 0)
                    {
                        @out.WriteLine("        /// ");
                        OutputWithLinebreak(@out, "        ", field.Description);
                    }
                    @out.WriteLine("        /// </summary>");
                    @out.WriteLine("        public " + GetDataTypeClass(field) + " " + StringToUpperCamelCase(field.Name) + " { get; set; }");
                    @out.WriteLine();
                }

                @out.WriteLine("        /// <summary>");
                @out.WriteLine("        /// Default constructor.");
                @out.WriteLine("        /// </summary>");
                @out.WriteLine("        public " + className + "()");
                @out.WriteLine("        {");

                if (!cluster.Name.Equals("GENERAL", StringComparison.InvariantCultureIgnoreCase))
                {
                    @out.WriteLine("            ClusterId = CLUSTER_ID;");
                }
                if (commandExtends.Equals("ZclCommand"))
                {
                    @out.WriteLine("            CommandId = COMMAND_ID;");
                    @out.WriteLine("            GenericCommand = " + (cluster.Name.Equals("GENERAL", StringComparison.InvariantCultureIgnoreCase) ? "true" : "false") + ";");
                    @out.WriteLine("            CommandDirection = ZclCommandDirection." + (command.Source.Equals("client") ? "CLIENT_TO_SERVER" : "SERVER_TO_CLIENT") + ";");
                }
                @out.WriteLine("        }");

                GenerateFields(@out, commandExtends, className, command.Fields, reservedFields);

                /*
                if (command.Response != null)
                {
                    @out.WriteLine();
                    //@out.WriteLine("    @Override");
                    @out.WriteLine("    public override bool IsTransactionMatch(ZigBeeCommand request, ZigBeeCommand response) {");
                    if (command.Response.Matchers.Count == 0)
                    {
                        @out.WriteLine("        return (response is " + command.Response.Command + ")");
                        @out.WriteLine("                && ((ZdoRequest) request).GetDestinationAddress().Equals((("
                                + command.Response.Command + ") response).GetSourceAddress());");
                    }
                    else
                    {
                        @out.WriteLine("        if (!(response is " + command.Response.Command + ")) {");
                        @out.WriteLine("            return false;");
                        @out.WriteLine("        }");
                        @out.WriteLine();
                        @out.Write("        return ");

                        bool first = true;
                        foreach (ZigBeeXmlMatcher matcher in command.Response.Matchers)
                        {
                            if (first == false)
                            {
                                @out.WriteLine();
                                @out.Write("                && ");
                            }
                            first = false;
                            @out.WriteLine("(((" + StringToUpperCamelCase(command.Name) + ") request).get"
                                    + matcher.CommandField + "()");
                            @out.Write("                .Equals(((" + command.Response.Command + ") response).get"
                                    + matcher.ResponseField + "()))");
                        }
                        @out.WriteLine(";");
                    }
                    @out.WriteLine("    }");
                }
                */

                GenerateToString(@out, className, command.Fields, reservedFields);

                @out.WriteLine("    }");
                @out.WriteLine("}");

                @out.Flush();
                @out.Close();
            }
        }

    }
}