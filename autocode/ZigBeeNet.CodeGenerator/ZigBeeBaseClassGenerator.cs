using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using ZigBeeNet.CodeGenerator.Xml;

namespace ZigBeeNet.CodeGenerator
{
    public abstract class ZigBeeBaseClassGenerator
    {
        // TODO: check if fields can be private
        protected string _generatedDate;
        protected Dictionary<string, string> _dependencies;

        protected int _lineLen = 80;
        protected string _sourceRootPath = @"C:\temp\csharp";
        protected List<string> _importList = new List<string>();

        protected static string packageRoot = "";//"com.zsmartsystems.zigbee";
        protected static string packageZcl = ".ZCL";
        protected static string packageZclField = packageZcl + ".Field";
        protected string packageZclCluster = packageZcl + ".Clusters";
        protected string packageZclProtocol = packageZcl + ".Protocol";
        protected string packageZclProtocolCommand;

        protected static string packageZdp = ".ZDO";
        protected static string packageZdpField = packageZdp + ".Field";
        protected string packageZdpCommand = packageZdp + ".Command";
        protected string packageZdpTransaction = packageZdp + ".Transaction";
        protected string packageZdpDescriptors = packageZdpField;

        protected static List<string> standardTypes = new List<string>();
        protected static Dictionary<string, string> customTypes = new Dictionary<string, string>();
        protected static List<string> fixedCaseAcronyms = new List<string>();

        public ZigBeeBaseClassGenerator(string sourceRootPath, string generatedDate)
        {
            if(!string.IsNullOrEmpty(sourceRootPath))
                _sourceRootPath = sourceRootPath;
            _generatedDate = generatedDate;
            packageZclProtocolCommand = packageZclCluster;
        }

        static ZigBeeBaseClassGenerator()
        {
            fixedCaseAcronyms.Add("AA");
            fixedCaseAcronyms.Add("AC");
            fixedCaseAcronyms.Add("AAA");
            fixedCaseAcronyms.Add("ACE");
            fixedCaseAcronyms.Add("APS");
            fixedCaseAcronyms.Add("CIE");
            fixedCaseAcronyms.Add("CR");
            fixedCaseAcronyms.Add("CO");
            fixedCaseAcronyms.Add("CO2");
            fixedCaseAcronyms.Add("DC");
            fixedCaseAcronyms.Add("DRLC");
            fixedCaseAcronyms.Add("DST");
            fixedCaseAcronyms.Add("ECC");
            fixedCaseAcronyms.Add("ECDSA");
            fixedCaseAcronyms.Add("EUI");
            fixedCaseAcronyms.Add("FC");
            fixedCaseAcronyms.Add("HAN");
            fixedCaseAcronyms.Add("HW");
            fixedCaseAcronyms.Add("ID");
            fixedCaseAcronyms.Add("IAS");
            fixedCaseAcronyms.Add("IEEE");
            fixedCaseAcronyms.Add("LQI");
            fixedCaseAcronyms.Add("MAC");
            fixedCaseAcronyms.Add("MMO");
            fixedCaseAcronyms.Add("NWK");
            fixedCaseAcronyms.Add("PIN");
            fixedCaseAcronyms.Add("PIR");
            fixedCaseAcronyms.Add("RMS");
            fixedCaseAcronyms.Add("RSSI");
            fixedCaseAcronyms.Add("SMAC");
            fixedCaseAcronyms.Add("SW");
            fixedCaseAcronyms.Add("UTC");
            fixedCaseAcronyms.Add("WAN");
            fixedCaseAcronyms.Add("WD");
            fixedCaseAcronyms.Add("XY");
            fixedCaseAcronyms.Add("ZCL");

            fixedCaseAcronyms.Add("may");
            fixedCaseAcronyms.Add("shall");
            fixedCaseAcronyms.Add("should");

            fixedCaseAcronyms.Add("ZigBee");

            standardTypes.Add("Integer");
            standardTypes.Add("Boolean");
            standardTypes.Add("Object");
            standardTypes.Add("Long");
            standardTypes.Add("Double");
            standardTypes.Add("String");
            standardTypes.Add("int[]");

            customTypes.Add("IeeeAddress", packageRoot + ".IeeeAddress");
            customTypes.Add("ByteArray", packageRoot + packageZclField + ".ByteArray");
            customTypes.Add("ZclStatus", packageRoot + packageZcl + ".ZclStatus");
            customTypes.Add("ZdoStatus", packageRoot + packageZdp + ".ZdoStatus");
            customTypes.Add("BindingTable", packageRoot + packageZdpField + ".BindingTable");
            customTypes.Add("NeighborTable", packageRoot + packageZdpField + ".NeighborTable");
            customTypes.Add("RoutingTable", packageRoot + packageZdpField + ".RoutingTable");
            customTypes.Add("Calendar", "java.util.Calendar");
            customTypes.Add("ImageUpgradeStatus", packageRoot + packageZclField + ".ImageUpgradeStatus");
        }

        protected string StringToConstantEnum(string value)
        {
            return StringToConstant(value).Replace("_", "");
        }

        protected string StringToConstant(string value)
        {
            // value = value.replaceAll("\\(.*?\\) ?", "");
            value = value.Trim();
            value = value.Replace("+", "_Plus");
            value = value.Replace("(", "_");
            value = value.Replace(")", "_");
            value = value.Replace(" ", "_");
            value = value.Replace("-", "_");
            value = value.Replace(".", "_");
            value = value.Replace("/", "_");
            value = value.Replace("#", "_");
            value = Regex.Replace(value, "_+", "_");
            if (value.EndsWith("_"))
            {
                value = value.Substring(0, value.Length - 1);
            }
            return value.ToUpper();
        }

        private string ToProperCase(string s)
        {
            return s.Substring(0, 1).ToUpper() + s.Substring(1).ToLower();
        }

        private string ToCamelCase(string value)
        {
            // value = value.replaceAll("\\(.*?\\) ?", "");
            value = value.Replace("(", "_");
            value = value.Replace(")", "_");
            value = value.Replace("+", "_Plus");
            value = value.Replace(" ", "_");
            value = value.Replace("-", "_");
            value = value.Replace(".", "_");
            value = value.Replace("/", "_");
            value = value.Replace("#", "_");
            value = Regex.Replace(value, "_+", "_");
            string[] parts = value.Split("_", StringSplitOptions.RemoveEmptyEntries);
            string camelCaseString = "";

            foreach (string part in parts)
            {
                camelCaseString += ToProperCase(part);
            }

            return camelCaseString;
        }

        protected string StringToUpperCamelCase(string value)
        {
            return ToCamelCase(value);
        }

        protected string StringToLowerCamelCase(string value)
        {
            string cc = ToCamelCase(value);

            return cc.Substring(0, 1).ToLower() + cc.Substring(1);
        }

        protected string UpperCaseFirstCharacter(string val)
        {
            return val.Substring(0, 1).ToUpper() + val.Substring(1);
        }

        protected string LowerCaseFirstCharacter(string val)
        {
            return val.Substring(0, 1).ToLower() + val.Substring(1);
        }
            
        protected TextWriter GetClassOut(string packageFilePath, string className)
        {
            //Directory.CreateDirectory(packageFile.)
            //packageFile.mkdirs();
            FileStream classFile = File.Create(packageFilePath + Path.DirectorySeparatorChar.ToString() + className + ".cs");
            Console.WriteLine("Generating: " + classFile.Name);
            //FileOutputStream fileOutputStream = new FileOutputStream(classFile, false);
            return new StreamWriter(classFile);
        }

        protected void ImportsClear()
        {
            _importList.Clear();
        }

        protected void ImportsAdd(string importClass)
        {
            if (_importList.Contains(importClass))
            {
                return;
            }

            _importList.Add(importClass);
        }

        protected void OutputImports(TextWriter @out)
        {
            _importList.Sort();

            foreach (var item in _importList)
            {
                @out.WriteLine("using " + item + ";");
            }

            @out.WriteLine();
            //bool found = false;

            //foreach (string importClass in _importList)
            //{
            //    if (!importClass.StartsWith("java."))
            //    {
            //        continue;
            //    }

            //    found = true;

            //    @out.WriteLine("import " + importClass + ";");
            //}

            //if (found)
            //{
            //    @out.WriteLine();
            //    found = false;
            //}

            //foreach (string importClass in _importList)
            //{
            //    if (!importClass.StartsWith("javax."))
            //    {
            //        continue;
            //    }
            //    found = true;
            //    @out.WriteLine("import " + importClass + ";");
            //}

            //if (found)
            //{
            //    @out.WriteLine();
            //    found = false;
            //}

            //foreach (string importClass in _importList)
            //{
            //    if (importClass.StartsWith("java.") || importClass.StartsWith("javax."))
            //    {
            //        continue;
            //    }

            //    @out.WriteLine("import " + importClass + ";");
            //}
        }

        protected void OutputWithLinebreak(TextWriter @out, string indent, List<ZigBeeXmlDescription> descriptions)
        {
            foreach (ZigBeeXmlDescription description in descriptions)
            {
                if (description.Description == null)
                {
                    continue;
                }
                string[] words = Regex.Split(description.Description, "\\s+");

                if (words.Length == 0)
                {
                    continue;
                }

                @out.Write(indent + "///");

                int len = 2 + indent.Length;

                foreach (string word in words)
                {
                    if (word.Equals("note:", StringComparison.InvariantCultureIgnoreCase))
                    {
                        if (len > 2)
                        {
                            @out.WriteLine();
                        }

                        @out.Write(indent + "/// Note:");

                        continue;
                    }

                    if (len + word.Length > _lineLen)
                    {
                        @out.WriteLine();
                        @out.Write(indent + "///");
                        len = 2 + indent.Length;
                    }

                    @out.Write(" ");

                    var wordToWrite = word;
                    foreach (string acronym in fixedCaseAcronyms)
                    {
                        if (acronym.Equals(word, StringComparison.InvariantCultureIgnoreCase))
                        {
                            wordToWrite = acronym;
                        }
                    }

                    @out.Write(wordToWrite);
                    len += word.Length;
                }

                if (len != 2 + indent.Length)
                {
                    @out.WriteLine();
                }
            }
        }

        public string ReplaceFirst(string text, string search, string replace)
        {
            var regex = new Regex(Regex.Escape(search));
            var newText = regex.Replace(text, replace, 1);

            return newText;
        }

        protected void OutputLicense(TextWriter @out)
        {
            try
            {
                //@out.WriteLine("/**");
                //@out.WriteLine(" * EPL");
                //@out.WriteLine(" */");
            }
            catch (FileNotFoundException e)
            {
                // TODO Auto-generated catch block
                Console.WriteLine(e);
            }
            catch (IOException e)
            {
                // TODO Auto-generated catch block
                Console.WriteLine(e);
            }
        }

        protected string GetPackagePath(string sourceRootPath, string packageRoot)
        {
            // TODO: use Path.Combine()
            string packagePath = sourceRootPath + Path.DirectorySeparatorChar.ToString() + packageRoot.Replace(".", Path.DirectorySeparatorChar.ToString());

            if (!File.Exists(packagePath))
            {
                Directory.CreateDirectory(packagePath);
            }

            return packagePath;
        }

        protected void OutputClassGenerated(TextWriter @out)
        {
            //@out.WriteLine("@Generated(value = \"" + ZigBeeCodeGenerator.class.getName() + "\", date = \"" + generatedDate+ "\")");
        }

        protected string GetDataTypeClass(ZigBeeXmlAttribute attribute)
        {
            // if (attribute.implementationClass.isEmpty()) {
            if (ZclDataType.Mapping.ContainsKey(attribute.Type))
            {
                return ZclDataType.Mapping[attribute.Type].DataClass;
            }

            if (attribute.ImplementationClass != null && _dependencies.ContainsKey(attribute.ImplementationClass))
            {
                // importsAdd(dependencies.get(type));
                return attribute.ImplementationClass;
            }

            Console.WriteLine("Unknown data type " + attribute.Type);
            return "(UNKNOWN::" + attribute.Type + ")";
            // }
            // return attribute.implementationClass;
        }

        protected string GetDataTypeClass(ZigBeeXmlField field)
        {
            string dataType = "";

            // if (field.implementationClass.isEmpty()) {
            if (field.Type != null && ZclDataType.Mapping.ContainsKey(field.Type))
            {
                dataType = ZclDataType.Mapping[field.Type].DataClass;
            }
            else if (_dependencies.ContainsKey(field.ImplementationClass))
            {
                // importsAdd(dependencies.get(type));
                dataType = field.ImplementationClass;
            }

            if (string.IsNullOrEmpty(dataType))
            {
                Console.WriteLine("Unknown data type " + field.Type);
                return "(UNKNOWN::" + field.Type + ")";
            }

            if (field.Sizer == null)
            {
                return dataType;
            }
            else
            {
                return "List<" + dataType + ">";
            }

            // }
            // return field.implementationClass;
        }

        protected void ImportsAddClass(ZigBeeXmlField field)
        {
            ImportsAddClassInternal(GetDataTypeClass(field));
        }

        protected void ImportsAddClass(ZigBeeXmlAttribute attribute)
        {
            ImportsAddClassInternal(GetDataTypeClass(attribute));
        }

        protected void ImportsAddClassInternal(string type)
        {
            string typeName = type;
            if (type.StartsWith("List"))
            {
                ImportsAdd("java.util.List");

                var startIndex = typeName.IndexOf("<") + 1;
                var length = typeName.IndexOf(">") - startIndex;

                typeName = typeName.Substring(startIndex, length);
            }

            if (standardTypes.Contains(typeName))
            {
                return;
            }

            if (customTypes.ContainsKey(typeName))
            {
                ImportsAdd(customTypes[typeName]);
                return;
            }

            if (_dependencies.ContainsKey(type))
            {
                ImportsAdd(_dependencies[type]);
                return;
            }

            string packageName;
            if (type.Contains("Descriptor"))
            {
                packageName = packageZdpDescriptors;
            }
            else
            {
                packageName = packageZclField;
            }
            ImportsAdd(packageRoot + packageName + "." + typeName);
        }

        protected string GetZclClusterCommandPackage(ZigBeeXmlCluster cluster)
        {
            if (cluster.Name.StartsWith("ZDO"))
            {
                return packageRoot + packageZdpCommand;
            }
            else
            {
                return packageRoot + packageZclProtocolCommand + "." + StringToUpperCamelCase(cluster.Name).Replace("_", "");
            }
        }

        protected virtual ZigBeeXmlField GetAutoSized(List<ZigBeeXmlField> fields, string name)
        {
            foreach (ZigBeeXmlField field in fields)
            {
                if (field.Sizer != null)
                {
                    //Console.WriteLine();
                }
                if (name.Equals(field.Sizer))
                {
                    return field;
                }
            }
            return null;
        }
    }
}