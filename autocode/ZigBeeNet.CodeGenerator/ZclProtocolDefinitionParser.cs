using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.CodeGenerator.Zcl;
using Attribute = ZigBeeNet.CodeGenerator.Zcl.Attribute;

namespace ZigBeeNet.CodeGenerator
{
    public class ZclProtocolDefinitionParser
    {
        public static void ParseProfiles(Context context)
        {
            while (context.Lines.Count > 0)
            {
                string line = context.Lines[0];
                context.Lines.RemoveAt(0);

                if (line.StartsWith("# ") && line.Contains("["))
                {
                    context.Profile = new Profile
                    {
                        ProfileName = GetHeaderTitle(line),
                        ProfileAbbreviation = GetHeaderAbbreviation(line)
                    };
                    context.Profile.ProfileType = CodeGeneratorUtil.LabelToEnumerationValue(context.Profile.ProfileName);
                    context.Profile.ProfileId = GetHeaderId(line);
                    context.Profiles.Add(context.Profile.ProfileId, context.Profile);

                    Console.WriteLine("Profile: " + context.Profile.ProfileName + " " + CodeGeneratorUtil.ToHex(context.Profile.ProfileId));

                    ParseFunctionalDomains(context);
                }
            }
        }

        private static void ParseFunctionalDomains(Context context)
        {
            while (context.Lines.Count > 0)
            {
                string line = context.Lines[0];
                context.Lines.RemoveAt(0);

                // Returning to previous level.
                if (line.StartsWith("# ") && line.Contains("["))
                {
                    context.Lines.Insert(0, line);
                    return;
                }

                if (line.StartsWith("# "))
                {
                    string functionalDomainName = GetHeaderTitle(line);

                    Console.WriteLine(" Functional domain: " + functionalDomainName);

                    ParseClusters(context);
                }
            }
        }

        private static void ParseClusters(Context context)
        {
            while (context.Lines.Count > 0)
            {
                string line = context.Lines[0];
                context.Lines.RemoveAt(0);

                // Returning to previous level.
                if (line.StartsWith("# "))
                {
                    context.Lines.Insert(0, line);
                    return;
                }

                if (line.StartsWith("## "))
                {
                    context.Cluster = new Cluster();
                    context.Cluster.ClusterName = GetHeaderTitle(line);
                    context.Cluster.ClusterDescription = new List<string>();
                    context.Cluster.ClusterType = CodeGeneratorUtil.LabelToEnumerationValue(context.Cluster.ClusterName);
                    context.Cluster.ClusterId = GetHeaderId(line);
                    context.Cluster.NameUpperCamelCase = CodeGeneratorUtil.LabelToUpperCamelCase(context.Cluster.ClusterName);
                    context.Cluster.NameLowerCamelCase = CodeGeneratorUtil.UpperCamelCaseToLowerCamelCase(context.Cluster.ClusterName);
                    context.Profile.Clusters.Add(context.Cluster.ClusterId, context.Cluster);
                    Console.WriteLine("  (" + CodeGeneratorUtil.ToHex(context.Cluster.ClusterId) + ") " + context.Cluster.ClusterName);

                    ParseDirections(context);
                }
            }
        }

        private static void ParseDirections(Context context)
        {
            bool addBreak = false;
            while (context.Lines.Count > 0)
            {
                string line = context.Lines[0];
                context.Lines.RemoveAt(0);

                // Returning to previous level.
                if (line.StartsWith("# ") || line.StartsWith("## "))
                {
                    context.Lines.Insert(0, line);
                    return;
                }

                if (line.StartsWith("### "))
                {
                    addBreak = false;

                    context.Received = line.ToLower().Contains("received");
                    context.Generated = line.ToLower().Contains("generated");
                    context.Attribute = line.ToLower().Contains("attributes");
                    if (context.Received)
                    {
                        Console.WriteLine("   Received:");
                    }
                    else if (context.Generated)
                    {
                        Console.WriteLine("   Generated:");
                    }
                    else if (context.Attribute)
                    {
                        Console.WriteLine("   Attributes:");

                        ParseAttributes(context);
                        continue;
                    }
                    else
                    {
                        Console.WriteLine("   Unknown:");
                    }

                    ParseCommands(context);

                    continue;
                }

                if (context.Cluster.ClusterDescription.Count == 0 && line.Trim().Length == 0)
                {
                    continue;
                }
                if (line.Trim().Length == 0)
                {
                    addBreak = true;
                    continue;
                }
                if (addBreak && context.Cluster.ClusterDescription.Count > 0)
                {
                    context.Cluster.ClusterDescription.Add("<p>");
                    addBreak = false;
                }
                context.Cluster.ClusterDescription.Add(line.Trim());
            }
        }

        private static void ParseCommands(Context context)
        {
            bool addBreak = false;
            Field field = null;
            while (context.Lines.Count > 0)
            {
                string line = context.Lines[0];
                context.Lines.RemoveAt(0);

                // Returning to previous level.
                if (line.StartsWith("# ") || line.StartsWith("## ") || line.StartsWith("### "))
                {
                    context.Lines.Insert(0, line);
                    return;
                }

                if (line.StartsWith("##### Expected Response"))
                {
                    ParseExpectedResponse(context);
                    continue;
                }

                if (line.StartsWith("##### "))
                {
                    addBreak = false;

                    foreach (Field fieldLoop in context.Command.Fields.Values)
                    {
                        if (fieldLoop.FieldLabel.Equals(line.Trim().Substring(6)))
                        {
                            field = fieldLoop;
                            break;
                        }
                    }
                    if (field == null)
                    {
                        Console.WriteLine("Error finding field \"" + line.Trim().Substring(6) + "\"");
                    }
                    continue;
                }

                if (line.StartsWith("#### "))
                {
                    context.Command = new Command();
                    context.Command.CommandLabel = GetHeaderTitle(line).Trim();
                    string[] splits = context.Command.CommandLabel.Split(" ");

                    if ("RESPONSE".Equals(splits[splits.Length - 2].ToUpper()) && "COMMAND".Equals(splits[splits.Length - 1].ToUpper()))
                    {
                        StringBuilder sb = new StringBuilder();
                        for (int c = 0; c < splits.Length - 1; c++)
                        {
                            if (c != 0)
                            {
                                sb.Append(" ");
                            }
                            sb.Append(splits[c]);
                        }

                        context.Command.CommandLabel = sb.ToString();
                    }

                    context.Command.CommandDescription = new List<string>();
                    context.Command.CommandType = CodeGeneratorUtil.LabelToEnumerationValue(context.Command.CommandLabel);
                    context.Command.CommandId = GetHeaderId(line);
                    context.Command.NameUpperCamelCase = CodeGeneratorUtil.LabelToUpperCamelCase(context.Command.CommandLabel);
                    context.Command.NameLowerCamelCase = CodeGeneratorUtil.UpperCamelCaseToLowerCamelCase(context.Command.NameUpperCamelCase);

                    if (context.Received)
                    {
                        context.Cluster.Received.Add(context.Command.CommandId, context.Command);
                    }
                    else
                    {
                        context.Cluster.Generated.Add(context.Command.CommandId, context.Command);
                    }

                    Console.WriteLine("     (" + CodeGeneratorUtil.ToHex(context.Command.CommandId) + ") " + context.Command.CommandLabel);

                    ParseField(context);
                    continue;
                }

                if (field == null)
                {
                    continue;
                }

                if (line.StartsWith("|") && !line.StartsWith("|Id") && !line.StartsWith("|-"))
                {
                    string row = line.Trim().Substring(1, line.Length - 1);
                    string[] columns = row.Split("|");
                    int value = int.Parse(columns[0].Trim().Substring(2), System.Globalization.NumberStyles.HexNumber);
                    string label = columns[1].Trim();

                    field.ValueMap.Add(value, label);
                    continue;
                }
                if (line.StartsWith("|") && (line.StartsWith("|Id") || line.StartsWith("|-")))
                {
                    continue;
                }

                if (field.Description.Count == 0 && line.Trim().Length == 0)
                {
                    continue;
                }
                if (line.Trim().Length == 0)
                {
                    addBreak = true;
                    continue;
                }
                if (addBreak && field.Description.Count > 0)
                {
                    field.Description.Add("<p>");
                    addBreak = false;
                }
                field.Description.Add(line.Trim());
            }
        }

        private static void ParseField(Context context)
        {
            int fieldIndex = 0;
            bool addBreak = false;
            while (context.Lines.Count > 0)
            {
                string line = context.Lines[0];
                context.Lines.RemoveAt(0);

                // Returning to previous level.
                if (line.StartsWith("#"))
                {
                    context.Lines.Insert(0, line);
                    return;
                }

                if (line.StartsWith("|") && !line.StartsWith("|Field Name") && !line.StartsWith("|-"))
                {
                    string row = line.Trim().Substring(1, line.Length - 1);
                    string[] columns = row.Split("|");//row.Split("\\|");
                    Field field = new Field();
                    field.Description = new List<string>();
                    field.FieldId = fieldIndex;
                    field.ValueMap = new SortedDictionary<int, string>();

                    field.FieldLabel = columns[0].Trim();
                    if (field.FieldLabel.Contains("["))
                    {
                        string option = field.FieldLabel.Substring(field.FieldLabel.IndexOf("[") + 1, field.FieldLabel.IndexOf("]"));
                        field.FieldLabel = field.FieldLabel.Substring(0, field.FieldLabel.IndexOf("["));
                        field.CompleteOnZero = true;
                    }

                    field.FieldType = context.Command.CommandType + "_" + CodeGeneratorUtil.LabelToEnumerationValue(field.FieldLabel);
                    //field.NameUpperCamelCase = CodeGeneratorUtil.LabelToEnumerationValue(field.FieldLabel);
                    field.NameUpperCamelCase = CodeGeneratorUtil.LabelToUpperCamelCase(field.FieldLabel);
                    field.NameLowerCamelCase = CodeGeneratorUtil.UpperCamelCaseToLowerCamelCase(field.NameUpperCamelCase);

                    string dataTypeName = columns[1].Trim();
                    if (dataTypeName.Contains("["))
                    {
                        string fieldString = SubstringBetween(dataTypeName, "[", "]"); //dataTypeName.Substring(dataTypeName.IndexOf("[") + 1, dataTypeName.IndexOf("]"));

                        if (fieldString.Length != 0)
                        {
                            string conditionOperator = "";
                            string condition = "";
                            if (fieldString.Contains("&&"))
                            {
                                conditionOperator = "&&";
                            }
                            if (fieldString.Contains(">="))
                            {
                                conditionOperator = ">=";
                            }
                            if (fieldString.Contains("=="))
                            {
                                conditionOperator = "==";
                            }

                            if (conditionOperator.Length != 0)
                            {
                                field.ListSizer = fieldString.Substring(0, fieldString.IndexOf(conditionOperator));
                                condition = fieldString
                                        .Substring(fieldString.IndexOf(conditionOperator) + conditionOperator.Length);

                                field.Condition = condition;
                                field.ConditionOperator = conditionOperator;
                            }
                            else
                            {
                                field.ListSizer = fieldString;
                            }

                            field.ListSizer = CodeGeneratorUtil.LabelToUpperCamelCase(field.ListSizer);
                            field.ListSizer = CodeGeneratorUtil.UpperCamelCaseToLowerCamelCase(field.ListSizer);

                            dataTypeName = dataTypeName.Substring(0, dataTypeName.IndexOf("["));
                        }
                    }

                    field.DataType = CodeGeneratorUtil.LabelToEnumerationValue(dataTypeName);

                    DataType dataType = new DataType();
                    dataType.DataTypeName = dataTypeName;
                    dataType.DataTypeType = field.DataType;

                    dataType.DataTypeClass = ZclDataType.Mapping[field.DataType].DataClass;
                    if (dataType.DataTypeClass == null)
                    {
                        throw new InvalidOperationException("Type not mapped: " + field.DataType);
                    }

                    field.DataTypeClass = dataType.DataTypeClass;

                    context.DataTypes[field.DataType] = dataType;
                    context.Command.Fields.Add(field.FieldId, field);
                    Console.WriteLine("      (" + CodeGeneratorUtil.ToHex(fieldIndex) + ") " + field.FieldLabel + ": " + dataType.DataTypeName);
                    fieldIndex++;
                }

                if (line.StartsWith("|Id") || line.StartsWith("|-"))
                {
                    continue;
                }

                if (line.StartsWith("|"))
                {
                    addBreak = false;
                    continue;
                }

                if (context.Command.CommandDescription.Count == 0 && line.Trim().Length == 0)
                {
                    continue;
                }
                if (line.Trim().Length == 0)
                {
                    addBreak = true;
                    continue;
                }
                if (addBreak)
                {
                    context.Command.CommandDescription.Add("<br>");
                    addBreak = false;
                }
                context.Command.CommandDescription.Add(line.Trim());
            }
        }

        private static void ParseExpectedResponse(Context context)
        {
            context.Command.ResponseMatchers = new Dictionary<string, string>();
            while (context.Lines.Count > 0)
            {
                string line = context.Lines[0];
                context.Lines.RemoveAt(0);

                // Returning to previous level.
                if (line.StartsWith("#"))
                {
                    context.Lines.Insert(0, line);
                    return;
                }

                if (line.StartsWith("Packet: "))
                {
                    string cmd = line.Substring(7);
                    string[] splits = cmd.Split(" ");
                    StringBuilder sb = new StringBuilder();
                    for (int c = 0; c < splits.Length - 1; c++)
                    {
                        if (c != 0)
                        {
                            sb.Append(" ");
                        }
                        sb.Append(splits[c]);
                    }
                    context.Command.ResponseCommand = CodeGeneratorUtil.LabelToUpperCamelCase(line.Substring(7));
                }

                if (line.StartsWith("Match: "))
                {
                    string response = line.Substring(7).Trim();
                    string[] matcher = response.Split("==");

                    string responseRequest = GetMatcherResponse(matcher[0].Trim());
                    string responseResponse = GetMatcherResponse(matcher[1].Trim());
                    context.Command.ResponseMatchers.Add(responseRequest, responseResponse);
                }
            }
        }

        private static string GetMatcherResponse(string definition)
        {
            if (!definition.Contains("."))
            {
                return CodeGeneratorUtil.LabelToUpperCamelCase(definition.Trim());
            }

            String[] parts = definition.Split(".");
            parts[0] = CodeGeneratorUtil.LabelToUpperCamelCase(parts[0].Trim());
            parts[1] = CodeGeneratorUtil.LabelToUpperCamelCase(parts[1].Trim());
            return parts[0] + "().get" + parts[1];
        }

        private static string GetHeaderTitle(string line)
        {
            line = line.Substring(line.LastIndexOf("#") + 1);
            if (line.Contains("["))
            {
                return SubstringBefore(line, "[").Trim();
            }
            else
            {
                return line.Trim();
            }
        }

        private static string SubstringBefore(string input, string searchString)
        {
            var index = input.IndexOf(searchString);
            var res = input.Substring(0, index);

            return res;
        }

        private static string SubstringBetween(string input, string searchString1, string searchString2)
        {
            var res = input.Split(new string[] { searchString1 }, StringSplitOptions.None)[1].Split(searchString2)[0].Trim();

            return res;
        }

        private static string SubstringAfter(string input, string searchString)
        {
            var index = input.IndexOf(searchString);
            var res = input.Substring(index + 1);

            return res;
        }

        private static int GetHeaderId(string line)
        {
            string headerIdString = SubstringBetween(line, "[", "]").Trim();
            return CodeGeneratorUtil.FromHex(headerIdString);
        }

        private static string GetHeaderAbbreviation(string line)
        {
            return SubstringAfter(line, "]").Trim().ToLower();
        }

        private static void ParseAttributes(Context context)
        {
            Attribute attribute = null;
            while (context.Lines.Count > 0)
            {
                string line = context.Lines[0];
                context.Lines.RemoveAt(0);

                // Returning to previous level.
                if (line.StartsWith("# ") || line.StartsWith("## ") || line.StartsWith("### "))
                {
                    context.Lines.Insert(0, line);
                    return;
                }

                if (line.StartsWith("|") && !line.StartsWith("|Id") && !line.StartsWith("|-"))
                {
                    ParseAttributeTable(context, line);
                }

                if (line.StartsWith("#### "))
                {
                    attribute = null;
                    foreach (Attribute attr in context.Cluster.Attributes.Values)
                    {
                        if (attr.AttributeLabel.Equals(GetHeaderTitle(line).Substring(0, GetHeaderTitle(line).IndexOf(" "))))
                        {
                            attribute = attr;
                            break;
                        }
                    }

                    if (attribute == null)
                    {
                        Console.WriteLine("***** Attribute not found: " + line);
                        continue;
                    }
                    ParseAttribute(context, attribute);
                    continue;
                }

            }
        }

        private static void ParseAttributeTable(Context context, string line)
        {
            string row = line.Trim().Substring(1, line.Length - 1);
            string[] columns = row.Split("|");
            Attribute attribute = new Attribute();
            attribute.ValueMap = new SortedDictionary<int, string>();
            attribute.AttributeId = int.Parse(columns[0].Trim().Substring(2), System.Globalization.NumberStyles.HexNumber);
            attribute.AttributeLabel = columns[1].Trim();
            attribute.AttributeDescription = new List<string>();
            attribute.AttributeAccess = columns[3].Trim();
            attribute.AttributeImplementation = columns[4].Trim();
            attribute.AttributeReporting = columns[5].Trim();
            attribute.NameUpperCamelCase = CodeGeneratorUtil.LabelToEnumerationValue(attribute.AttributeLabel);
            attribute.NameUpperCamelCase = CodeGeneratorUtil.LabelToUpperCamelCase(attribute.AttributeLabel);
            attribute.NameLowerCamelCase = CodeGeneratorUtil.UpperCamelCaseToLowerCamelCase(attribute.NameUpperCamelCase);
            attribute.DataType = CodeGeneratorUtil.LabelToEnumerationValue(columns[2].Trim());
            attribute.EnumName = "ATTR_" + attribute.AttributeLabel.ToUpper();
            DataType dataType = new DataType();
            dataType.DataTypeName = columns[2].Trim();
            dataType.DataTypeType = attribute.DataType;
            Console.WriteLine(" Type:::" + attribute.AttributeLabel + ":: " + dataType.DataTypeType);
            dataType.DataTypeClass = ZclDataType.Mapping[attribute.DataType].DataClass;

            if (dataType.DataTypeClass == null)
            {
                throw new ArgumentException("Type not mapped: " + attribute.DataType);
            }

            attribute.DataTypeClass = dataType.DataTypeClass;

            context.DataTypes[attribute.DataType] = dataType;
            context.Cluster.Attributes.Add(attribute.AttributeId, attribute);
        }

        private static void ParseAttribute(Context context, Attribute attribute)
        {
            bool addBreak = false;
            while (context.Lines.Count > 0)
            {
                string line = context.Lines[0];
                context.Lines.RemoveAt(0);

                // Returning to previous level.
                if (line.StartsWith("# ") || line.StartsWith("## ") || line.StartsWith("### ") || line.StartsWith("#### "))
                {
                    context.Lines.Insert(0, line);
                    return;
                }

                if (line.StartsWith("|") && !line.StartsWith("|Id") && !line.StartsWith("|-"))
                {
                    string row = line.Trim().Substring(1, line.Length - 1);
                    string[] columns = row.Split("|");
                    int value = int.Parse(columns[0].Trim().Substring(2), System.Globalization.NumberStyles.HexNumber);
                    String label = columns[1].Trim();

                    attribute.ValueMap[value] = label;
                    continue;
                }

                if (line.StartsWith("|Id") || line.StartsWith("|-"))
                {
                    continue;
                }

                if ((attribute.AttributeDescription.Count == 0 && line.Trim().Length == 0))
                {
                    continue;
                }
                if (line.Trim().Length == 0 && attribute.AttributeDescription.Count > 0)
                {
                    addBreak = true;
                    continue;
                }
                if (addBreak && attribute.AttributeDescription.Count > 0)
                {
                    attribute.AttributeDescription.Add("");
                    addBreak = false;
                }
                attribute.AttributeDescription.Add(line.Trim());
            }
        }
    }
}
