using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using ZigBeeNet.CodeGenerator.Xml;

namespace ZigBeeNet.CodeGenerator
{

    public class ZigBeeBaseFieldGenerator : ZigBeeBaseClassGenerator
    {
        private const string OPERATOR_LOGIC_AND = "LOGIC_AND";
        private const string OPERATOR_EQUAL = "EQUAL";
        private const string OPERATOR_NOT_EQUAL = "NOT_EQUAL";
        private const string OPERATOR_GREATER_THAN = "GREATER_THAN";
        private const string OPERATOR_GREATER_THAN_OR_EQUAL = "GREATER_THAN_OR_EQUAL";
        private const string OPERATOR_LESS_THAN = "LESS_THAN";
        private const string OPERATOR_LESS_THAN_OR_EQUAL = "LESS_THAN_OR_EQUAL";

        public ZigBeeBaseFieldGenerator(string sourceRootPath, string generatedDate)
            : base(sourceRootPath, generatedDate)
        {
        }

        protected void GenerateFields(TextWriter @out, string parentClass, string className, List<ZigBeeXmlField> fields, List<string> reservedFields)
        {
            if (fields.Count > 0)
            {
                @out.WriteLine();
                if (parentClass == "IZigBeeSerializable")
                {
                    @out.Write("        public");
                }
                else
                {
                    @out.Write("        internal override");
                }
                @out.WriteLine(" void Serialize(ZclFieldSerializer serializer)");
                @out.WriteLine("        {");
                if (parentClass.StartsWith("Zdo"))
                {
                    @out.WriteLine("            base.Serialize(serializer);");
                    @out.WriteLine();
                }

                foreach (ZigBeeXmlField field in fields)
                {
                    // if (reservedFields.contains(StringToLowerCamelCase(field.Name))) {
                    // continue;
                    // }

                    // Rules...
                    // if listSizer == null, then just output the field
                    // if listSizer != null and contains && then check the param bit

                    if (GetAutoSized(fields, StringToLowerCamelCase(field.Name)) != null)
                    {
                        ZigBeeXmlField sizedField = GetAutoSized(fields, StringToLowerCamelCase(field.Name));
                        @out.WriteLine("        serializer.serialize(" + StringToUpperCamelCase(sizedField.Name) + ".Count, ZclDataType.Get(DataType." + field.Type + "));");
                        continue;
                    }

                    if (field.Sizer != null)
                    {
                        @out.WriteLine("            for (int cnt = 0; cnt < " + StringToUpperCamelCase(field.Name) + ".Count; cnt++)");
                        @out.WriteLine("            {");
                        @out.WriteLine("                serializer.serialize(" + StringToLowerCamelCase(field.Name) + ".Get(cnt), ZclDataType.Get(DataType." + field.Type + "));");
                        @out.WriteLine("            }");
                    }
                    else if (field.Condition != null)
                    {
                        if (field.Condition.Value.Equals("statusResponse"))
                        {
                            // Special case where a ZclStatus may be sent, or, a list of results.
                            // This checks for a single response
                            @out.WriteLine("            if (Status == ZclStatus.SUCCESS)");
                            @out.WriteLine("            {");
                            @out.WriteLine("                serializer.Serialize(Status, ZclDataType.Get(DataType.ZCL_STATUS));");
                            @out.WriteLine("                return;");
                            @out.WriteLine("            }");
                            continue;
                        }
                        else if (field.Condition.Operator.Equals(OPERATOR_LOGIC_AND))
                        {
                            @out.WriteLine("            if ((" + UpperCaseFirstCharacter(field.Condition.Field) + " & " + field.Condition.Value + ") != 0)");
                            @out.WriteLine("            {");
                        }
                        else
                        {
                            @out.WriteLine("            if (" + UpperCaseFirstCharacter(field.Condition.Field) + " " + GetOperator(field.Condition.Operator) + " " + field.Condition.Value + ")");
                            @out.WriteLine("            {");
                        }
                        @out.WriteLine("                serializer.Serialize(" + StringToUpperCamelCase(field.Name)+ ", ZclDataType.Get(DataType." + field.Type + "));");
                        @out.WriteLine("            }");
                    }
                    else
                    {
                        if (field.Type != null && !string.IsNullOrEmpty(field.Type))
                        {
                            @out.WriteLine("            serializer.Serialize(" + StringToUpperCamelCase(field.Name) + ", ZclDataType.Get(DataType." + field.Type + "));");
                        }
                        else
                        {
                            @out.WriteLine("            " + StringToUpperCamelCase(field.Name) + ".Serialize(serializer);");
                        }
                    }
                }
                @out.WriteLine("        }");

                @out.WriteLine();
                if (parentClass == "IZigBeeSerializable")
                {
                    @out.Write("        public");
                }
                else
                {
                    @out.Write("        internal override");
                }
                @out.WriteLine(" void Deserialize(ZclFieldDeserializer deserializer)");
                @out.WriteLine("        {");
                if (parentClass.StartsWith("Zdo"))
                {
                    @out.WriteLine("            base.Deserialize(deserializer);");
                    @out.WriteLine();
                }
                bool first = true;
                foreach (ZigBeeXmlField field in fields)
                {
                    if (field.Sizer != null)
                    {
                        if (first)
                        {
                            @out.WriteLine("        // Create lists");
                            first = false;
                        }
                        @out.WriteLine("        " + StringToUpperCamelCase(field.Name) + " = new Array"+ GetDataTypeClass(field) + "();");
                    }
                }
                if (first == false)
                {
                    @out.WriteLine();
                }
                foreach (ZigBeeXmlField field in fields)
                {
                    // if (reservedFields.contains(StringToLowerCamelCase(field.Name))) {
                    // continue;
                    // }

                    if (field.CompleteOnZero)
                    {
                        @out.WriteLine("            if (deserializer.IsEndOfStream())");
                        @out.WriteLine("            {");
                        @out.WriteLine("                return;");
                        @out.WriteLine("            }");
                    }
                    if (GetAutoSized(fields, StringToLowerCamelCase(field.Name)) != null)
                    {
                        @out.WriteLine("            ushort " + StringToUpperCamelCase(field.Name) + " = (" + GetDataTypeClass(field) + ") deserializer.Deserialize(ZclDataType.Get(DataType." + field.Type + "));");
                        continue;
                    }

                    if (field.Sizer != null)
                    {
                        var startIndex = GetDataTypeClass(field).IndexOf('<') + 1;
                        var length = GetDataTypeClass(field).IndexOf('>') - startIndex;

                        string dataType = GetDataTypeClass(field).Substring(startIndex, length);

                        @out.WriteLine("            if (" + field.Sizer + " != null)");
                        @out.WriteLine("            {");
                        @out.WriteLine("                for (int cnt = 0; cnt < " + field.Sizer + "; cnt++)");
                        @out.WriteLine("                {");
                        @out.WriteLine("                    " + StringToUpperCamelCase(field.Name) + ".Add((" + dataType + ") deserializer.Deserialize(ZclDataType.Get(DataType." + field.Type + ")));");
                        @out.WriteLine("                }");
                        @out.WriteLine("            }");
                    }
                    else if (field.Condition != null)
                    {
                        if (field.Condition.Value.Equals("statusResponse"))
                        {
                            // Special case where a ZclStatus may be sent, or, a list of results.
                            // This checks for a single response
                            @out.WriteLine("            if (deserializer.RemainingLength == 1)");
                            @out.WriteLine("            {");
                            @out.WriteLine("                Status = deserializer.Deserialize<ZclStatus>(ZclDataType.Get(DataType.ZCL_STATUS));");
                            @out.WriteLine("                return;");
                            @out.WriteLine("            }");
                            continue;
                        }
                        else if (field.Condition.Operator.Equals(OPERATOR_LOGIC_AND))
                        {
                            @out.WriteLine("            if ((" + UpperCaseFirstCharacter(field.Condition.Field) + " & " + field.Condition.Value + ") != 0)");
                            @out.WriteLine("            {");
                        }
                        else
                        {
                            @out.WriteLine("            if (" + UpperCaseFirstCharacter(field.Condition.Field) + " " + GetOperator(field.Condition.Operator) + " " + field.Condition.Value + ")");
                            @out.WriteLine("            {");
                        }
                        @out.WriteLine("                " + StringToUpperCamelCase(field.Name) + " = deserializer.Deserialize<" + GetDataTypeClass(field) + ">(ZclDataType.Get(DataType." + field.Type + "));");
                        @out.WriteLine("            }");
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(field.Type))
                        {
                            @out.WriteLine("            " + StringToUpperCamelCase(field.Name) + " = deserializer.Deserialize<" + GetDataTypeClass(field) + ">(ZclDataType.Get(DataType." + field.Type + "));");
                        }
                        else
                        {
                            @out.WriteLine("            " + StringToUpperCamelCase(field.Name) + " = new " + GetDataTypeClass(field) + "();");
                            @out.WriteLine("            " + StringToUpperCamelCase(field.Name) + ".Deserialize(deserializer);");
                        }
                    }

                    if (field.Name.ToLower().Equals("status") && field.Type.Equals("ZDO_STATUS"))
                    {
                        @out.WriteLine("            if (status != ZdoStatus.SUCCESS)");
                        @out.WriteLine("            {");
                        @out.WriteLine("                // Don't read the full response if we have an error");
                        @out.WriteLine("                return;");
                        @out.WriteLine("            }");
                    }
                }
                @out.WriteLine("        }");
            }
        }

        private bool IsListType(ZigBeeXmlField field)
        {
            string dataType = GetDataTypeClass(field);

            return dataType.StartsWith("List<");
        }

        protected void GenerateToString(TextWriter @out, string className, List<ZigBeeXmlField> fields, List<string> reservedFields)
        {
            @out.WriteLine();
            @out.WriteLine("        public override string ToString()");
            @out.WriteLine("        {");
            @out.WriteLine("            var builder = new StringBuilder();");
            @out.WriteLine();
            @out.WriteLine("            builder.Append(\"" + className + " [\");");
            @out.WriteLine("            builder.Append(base.ToString());");
            foreach (ZigBeeXmlField field in fields)
            {
                // if (reservedFields.contains(stringToLowerCamelCase(field.name))) {
                // continue;
                // }
                if (GetAutoSized(fields, StringToLowerCamelCase(field.Name)) != null)
                {
                    continue;
                }

                @out.WriteLine("            builder.Append(\", " + StringToUpperCamelCase(field.Name) + "=\");");

                if (IsListType(field))
                {
                    @out.WriteLine("            builder.Append(string.Join(\", \", " + StringToUpperCamelCase(field.Name) + "));");
                }
                else
                {
                    @out.WriteLine("            builder.Append(" + StringToUpperCamelCase(field.Name) + ");");
                }
            }
            @out.WriteLine("            builder.Append(\']\');");
            @out.WriteLine();
            @out.WriteLine("            return builder.ToString();");
            @out.WriteLine("        }");
        }

        private string GetOperator(string @operator)
        {
            switch (@operator)
            {
                case OPERATOR_LOGIC_AND:
                    return "&&";
                case OPERATOR_EQUAL:
                    return "==";
                case OPERATOR_NOT_EQUAL:
                    return "!=";
                case OPERATOR_GREATER_THAN:
                    return ">";
                case OPERATOR_GREATER_THAN_OR_EQUAL:
                    return ">=";
                case OPERATOR_LESS_THAN:
                    return "<";
                case OPERATOR_LESS_THAN_OR_EQUAL:
                    return "<";
                default:
                    return "<<Unknown " + @operator +">>";
            }
        }

        protected ZigBeeXmlField GetAutoSized(List<ZigBeeXmlField> fields, string name)
        {
            foreach (ZigBeeXmlField field in fields)
            {
                if (field.Sizer != null)
                {
                    Console.WriteLine();
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