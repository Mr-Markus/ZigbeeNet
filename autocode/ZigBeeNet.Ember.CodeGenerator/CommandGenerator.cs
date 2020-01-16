using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ZigBeeNet.Ember.CodeGenerator.Entities;
using ZigBeeNet.Ember.CodeGenerator.Enumerations;
using ZigBeeNet.Ember.CodeGenerator.Extensions;
using ZigBeeNet.Ember.CodeGenerator.Helper;
using ZigBeeNet.Ember.CodeGenerator.Xml;

namespace ZigBeeNet.Ember.CodeGenerator
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="ZigBeeNet.Ember.CodeGenerator.ClassGenerator" />
    public class CommandGenerator : ClassGenerator
    {
        // TODO: rename packages to namespace. packages are not available in C#
        private const string _zigbeePackage = "ZigBeeNet";
        private const string _zigbeeSecurityPackage = "ZigBeeNet.Security";

        private const string _ashPackage = "ZigBeeNet.Hardware.Ember.Internal.Ash";
        private const string _ezspPackage = "ZigBeeNet.Hardware.Ember.Ezsp";
        private const string _serializerPackage = "ZigBeeNet.Hardware.Ember.Internal.Serializer";
        private const string _ezspCommandPackage = "ZigBeeNet.Hardware.Ember.Ezsp.Command";
        private const string _ezspStructurePackage = "ZigBeeNet.Hardware.Ember.Ezsp.Structure";

        private readonly Dictionary<int, string> _events = new Dictionary<int, string>();

        public void Go(Protocol protocol)
        {
           
            string className;
            foreach (Command command in protocol.Commands)
            {
                if (command.Name.EndsWith("Handler"))
                {
                    className = "Ezsp" + command.Name.UpperCaseFirstCharacter();
                    CreateCommandClass(className, command, command.ResponseParameters);
                }
                else
                {
                    className = "Ezsp" + command.Name.UpperCaseFirstCharacter() + "Request";
                    CreateCommandClass(className, command, command.CommandParameters);

                    className = "Ezsp" + command.Name.UpperCaseFirstCharacter() + "Response";
                    CreateCommandClass(className, command, command.ResponseParameters);
                }
            }

            CreateEzspFrame(protocol);

            foreach (Structure structure in protocol.Structures)
            {
                CreateStructureClass(structure);
            }
            foreach (Enumeration enumeration in protocol.Enumerations)
            {
                CreateEnumClass(enumeration);
            }
        }

        private void CreateEzspFrame(Protocol protocol)
        {
            string className = "EzspFrame";
            Console.WriteLine($"Processing EzspFrame");
            CreateCompileUnit(out CodeCompileUnit compileUnit, out CodeNamespace codeNamespace, _ezspPackage);
            AddNamespaceImport(codeNamespace, "System.Collections.Generic");
            AddNamespaceImport(codeNamespace, _ezspCommandPackage);

            CodeTypeDeclaration protocolClass = new CodeTypeDeclaration(className)
            {
                IsClass = true,
                IsPartial = true,
                TypeAttributes = System.Reflection.TypeAttributes.Public | System.Reflection.TypeAttributes.Abstract
            };
            codeNamespace.Types.Add(protocolClass);

            CodeMemberField codeMemberField = new CodeMemberField(typeof(Dictionary<int, Type>), "_ezspHandlerDict");
            StringBuilder initExpression = new StringBuilder();
            initExpression.AppendLine("new Dictionary<int, System.Type>() {");
            foreach(Command command in protocol.Commands)
            {
                string commandClassName = "Ezsp" + command.Name.UpperCaseFirstCharacter();
                if (!command.Name.EndsWith("Handler"))
                    commandClassName += "Response";

                initExpression.AppendLine($"            {{ 0x{command.Id:X2}, typeof({commandClassName}) }},");
            }
            initExpression.AppendLine("        }");

            codeMemberField.InitExpression = new CodeSnippetExpression(initExpression.ToString());
            codeMemberField.Attributes = MemberAttributes.Static | MemberAttributes.Private;

            protocolClass.Members.Add(codeMemberField);

            GenerateCode(compileUnit, className + "AutoGen", "/");
        }

        private void CreateEnumClass(Enumeration enumeration)
        {
            string className = enumeration.Name.UpperCaseFirstCharacter();
            Console.WriteLine($"Processing enum class {enumeration.Name} [{className}()]");

            CreateCompileUnit(out CodeCompileUnit compileUnit, out CodeNamespace codeNamespace, _ezspStructurePackage);
            CodeTypeDeclaration protocolClass = new CodeTypeDeclaration(className)
            {
                IsEnum = true,
                TypeAttributes = System.Reflection.TypeAttributes.Public
            };
            codeNamespace.Types.Add(protocolClass);

            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Class to implement the Ember Enumeration <b>{enumeration.Name}</b>");
            if (enumeration.Description != null && enumeration.Description.Trim().Length > 0)
            {
                sb.AppendLine("<p>");
                OutputWithLineBreak(sb, "", enumeration.Description);
                sb.Append("</p>");
            }
            IList<ICodeCommentEntity> codeComment = new List<ICodeCommentEntity>
                {
                    new CodeCommentEntity
                    {
                        Tag = CodeCommentTag.Summary,
                        DocumentationText = sb.ToString()
                    }
                };
            AddCodeComment(protocolClass, codeComment, true);

            bool hasValues = false;
            foreach (var value in enumeration.Values)
            {
                if (value.EnumValue != null)
                {
                    hasValues = true;
                    break;
                }
            }

            sb.Clear();
            codeComment.Clear();
            sb.AppendLine("Default unknown value");
            codeComment.Add(
                    new CodeCommentEntity
                    {
                        Tag = CodeCommentTag.Summary,
                        DocumentationText = sb.ToString()
                    }
                );
            if (hasValues)
            {
                CodeMemberField codeMemberField = new CodeMemberField(className, "UNKNOWN = -1");
                AddCodeComment(codeMemberField, codeComment, true);
                protocolClass.Members.Add(codeMemberField);
            }
            else
            {
                CodeMemberField codeMemberField = new CodeMemberField(className, "UNKNOWN");
                AddCodeComment(codeMemberField, codeComment, true);
                protocolClass.Members.Add(codeMemberField);
            }

            foreach (var value in enumeration.Values)
            {
                sb.Clear();
                codeComment.Clear();
                CodeMemberField codeMemberField;
                if (hasValues)
                {
                    OutputWithLineBreak(sb, "    ", $"[{value.EnumValue}] {value.Description}");
                    codeMemberField = new CodeMemberField(className, $"{value.Name.ToConstant()} = {string.Format("0x{0:X4}", value.EnumValue)}");
                }
                else
                {
                    OutputWithLineBreak(sb, "    ", value.Description);
                    codeMemberField = new CodeMemberField(className, $"{value.Name.ToConstant()}");
                }

                codeComment.Add(
                    new CodeCommentEntity
                    {
                        Tag = CodeCommentTag.Summary,
                        DocumentationText = sb.ToString()
                    });
                AddCodeComment(codeMemberField, codeComment, true);
                protocolClass.Members.Add(codeMemberField);
            }

            GenerateCode(compileUnit, className, "Structure/");
        }

        private void CreateCommandClass(string className, Command command, List<Parameter> parameters)
        {
            Console.WriteLine($"Processing command class {command.Name}  [{className}()]");

            CreateCompileUnit(out CodeCompileUnit compileUnit, out CodeNamespace codeNamespace, _ezspCommandPackage);
            CodeTypeDeclaration protocolClass = new CodeTypeDeclaration(className)
            {
                IsClass = true,
                TypeAttributes = System.Reflection.TypeAttributes.Public
            };
            AddNamespaceImport(codeNamespace, _serializerPackage);

            StringBuilder descriptionStringBuilder = new StringBuilder();
            descriptionStringBuilder.AppendLine($"Class to implement the Ember EZSP command \" {command.Name} \".");
            if (!string.IsNullOrEmpty(command.Description))
            {
                OutputWithLineBreak(descriptionStringBuilder, "", command.Description);
            }
            descriptionStringBuilder.AppendLine(" This class provides methods for processing EZSP commands.");
            ICodeCommentEntity descriptionCodeCommentEntity = new CodeCommentEntity
            {
                Tag = CodeCommentTag.Summary,
                DocumentationText = descriptionStringBuilder.ToString()
            };
            CodeCommentStatement descriptionCodeComment = CodeCommentHelper.BuildCodeCommentStatement(descriptionCodeCommentEntity, true);
            protocolClass.Comments.Add(descriptionCodeComment);

            if(className.EndsWith("Request"))
                protocolClass.BaseTypes.Add("EzspFrameRequest");
            else
                protocolClass.BaseTypes.Add("EzspFrameResponse");

            codeNamespace.Types.Add(protocolClass);

            CodeMemberField frameIdCodeMember = CreateCodeConstantMember("FRAME_ID", typeof(int), command.Id);
            protocolClass.Members.Add(frameIdCodeMember);

            CreateParameters(codeNamespace, protocolClass, parameters);
            CreateParameterSetters(parameters, codeNamespace, protocolClass);
            CreateParameterGetter(parameters, codeNamespace, protocolClass);
            if (className.EndsWith("Request"))
            {
                CreateRequestConstructor(protocolClass);
                CreateRequestSerializer(parameters, protocolClass);
            }
            else
            {
                CreateResponseAndHandlerConstructor(parameters, protocolClass);
            }
            CreateToStringOverride(className, parameters, protocolClass);
            
            GenerateCode(compileUnit, className, "Command/");
        }

        private void CreateStructureClass(Structure structure)
        {
            string className = structure.Name.UpperCaseFirstCharacter();
            Console.WriteLine($"Processing structure class {structure.Name}  [{className}()]");

            CreateCompileUnit(out CodeCompileUnit compileUnit, out CodeNamespace codeNamespace, _ezspStructurePackage);
            CodeTypeDeclaration protocolClass = new CodeTypeDeclaration(className)
            {
                IsClass = true,
                TypeAttributes = System.Reflection.TypeAttributes.Public
            };
            AddNamespaceImport(codeNamespace, _serializerPackage);

            StringBuilder descriptionStringBuilder = new StringBuilder();
            descriptionStringBuilder.AppendLine($"Class to implement the Ember Structure \" {structure.Name} \".");
            if (!string.IsNullOrEmpty(structure.Description))
            {
                OutputWithLineBreak(descriptionStringBuilder, "", structure.Description);
            }
            ICodeCommentEntity descriptionCodeCommentEntity = new CodeCommentEntity
            {
                Tag = CodeCommentTag.Summary,
                DocumentationText = descriptionStringBuilder.ToString()
            };
            CodeCommentStatement descriptionCodeComment = CodeCommentHelper.BuildCodeCommentStatement(descriptionCodeCommentEntity, true);
            protocolClass.Comments.Add(descriptionCodeComment);

            codeNamespace.Types.Add(protocolClass);

            CreateStructureConstructor(protocolClass);
            CreateStructureConstructor(protocolClass, true);
            CreateParameters(codeNamespace, protocolClass, structure.Parameters);
            CreateParameterSetters(structure.Parameters, codeNamespace, protocolClass);
            CreateParameterGetter(structure.Parameters, codeNamespace, protocolClass);
            CreateStructureSerializer(structure.Parameters, protocolClass);
            CreateStructureDeserializer(structure.Parameters, protocolClass);
            CreateToStringOverride(className, structure.Parameters, protocolClass);

            GenerateCode(compileUnit, className, "Structure/");
        }

        private void CreateStructureConstructor(CodeTypeDeclaration protocolClass, bool withDeserializerParameter = false)
        {
            CodeConstructor codeConstructor = new CodeConstructor();
            codeConstructor.Attributes = MemberAttributes.Public;
            if(withDeserializerParameter)
            {
                codeConstructor.Parameters.Add(new CodeParameterDeclarationExpression("EzspDeserializer", "deserializer"));
                CodeMethodInvokeExpression codeMethodInvokeExpression = new CodeMethodInvokeExpression(null, "Deserialize");
                codeMethodInvokeExpression.Parameters.Add(new CodeSnippetExpression("deserializer"));
                codeConstructor.Statements.Add(codeMethodInvokeExpression);
            }
            protocolClass.Members.Add(codeConstructor);
        }

        private void CreateStructureSerializer(List<Parameter> parameters, CodeTypeDeclaration protocolClass)
        {
            CodeMemberMethod serializerMethod = CreateMethod("Serialize", new CodeParameterDeclarationExpressionCollection { new CodeParameterDeclarationExpression("EzspSerializer", "serializer") }, new CodeTypeReference(typeof(int[])), null);

            IEnumerable<ICodeCommentEntity> codeComment = new List<ICodeCommentEntity>
                {
                    new CodeCommentEntity
                    {
                        Tag = CodeCommentTag.Summary,
                        DocumentationText = "Serialise the contents of the EZSP structure."
                    }
                };
            AddCodeComment(serializerMethod, codeComment, true);
            foreach (Parameter parameter in parameters)
            {
                CodeMethodInvokeExpression codeMethodInvokeExpression = new CodeMethodInvokeExpression(new CodeTypeReferenceExpression($"serializer"), "Serialize" + GetTypeSerializer(parameter.DataType));
                codeMethodInvokeExpression.Parameters.Add(new CodeSnippetExpression($"_{parameter.Name}"));
                serializerMethod.Statements.Add(codeMethodInvokeExpression);
            }

            CodeMethodReturnStatement codeMethodReturnStatement = new CodeMethodReturnStatement(new CodeMethodInvokeExpression(new CodeTypeReferenceExpression($"serializer"), "GetPayload"));
            serializerMethod.Statements.Add(codeMethodReturnStatement);
            protocolClass.Members.Add(serializerMethod);
        }

        private void CreateStructureDeserializer(List<Parameter> parameters, CodeTypeDeclaration protocolClass)
        {
            CodeMemberMethod deserializerMethod = CreateMethod("Deserialize", new CodeParameterDeclarationExpressionCollection { new CodeParameterDeclarationExpression("EzspDeserializer", "deserializer") }, null, null);

            IEnumerable<ICodeCommentEntity> codeComment = new List<ICodeCommentEntity>
                {
                    new CodeCommentEntity
                    {
                        Tag = CodeCommentTag.Summary,
                        DocumentationText = "Deserialise the contents of the EZSP structure."
                    }
                };
            AddCodeComment(deserializerMethod, codeComment, true);
            foreach (Parameter parameter in parameters)
            {
                CodeMethodInvokeExpression codeMethodInvokeExpression = new CodeMethodInvokeExpression(new CodeTypeReferenceExpression($"deserializer"), "Deserialize" + GetTypeSerializer(parameter.DataType));
                if (parameter.DataType.Contains("[") && parameter.DataType.Contains("]") && !parameter.DataType.Contains("[]"))
                {
                    int length = int.Parse(parameter.DataType.Substring(parameter.DataType.IndexOf("[") + 1, parameter.DataType.IndexOf("]") - parameter.DataType.IndexOf("[") - 1));
                    codeMethodInvokeExpression.Parameters.Add(new CodeSnippetExpression(length.ToString()));
                }

                CodeAssignStatement codeAssignStatement = new CodeAssignStatement(new CodeVariableReferenceExpression($"_{parameter.Name}"), codeMethodInvokeExpression);
                deserializerMethod.Statements.Add(codeAssignStatement);

            }
            protocolClass.Members.Add(deserializerMethod);
        }

        private void CreateRequestConstructor(CodeTypeDeclaration protocolClass)
        {
            CodeMemberField codeMemberField = new CodeMemberField(new CodeTypeReference("EzspSerializer"), "_serializer");
            protocolClass.Members.Add(codeMemberField);

            CodeConstructor codeConstructor = new CodeConstructor();
            codeConstructor.Attributes = MemberAttributes.Public;

            CodeAssignStatement codeAssignStatement = new CodeAssignStatement(new CodeVariableReferenceExpression($"_frameId"), new CodeVariableReferenceExpression("FRAME_ID"));
            codeConstructor.Statements.Add(codeAssignStatement);

            codeAssignStatement = new CodeAssignStatement(new CodeVariableReferenceExpression($"_serializer"), new CodeObjectCreateExpression(new CodeTypeReference("EzspSerializer")));
            codeConstructor.Statements.Add(codeAssignStatement);

            protocolClass.Members.Add(codeConstructor);
        }

        private void CreateRequestSerializer(List<Parameter> parameters, CodeTypeDeclaration protocolClass)
        {
            CodeMemberMethod serializerMethod = CreateMethodOverride("Serialize", null, new CodeTypeReference(typeof(int[])), null);

            IEnumerable<ICodeCommentEntity> codeComment = new List<ICodeCommentEntity>
                {
                    new CodeCommentEntity
                    {
                        Tag = CodeCommentTag.Summary,
                        DocumentationText = "Method for serializing the command fields"
                    }
                };
            AddCodeComment(serializerMethod, codeComment, true);

            CodeMethodInvokeExpression codeMethodInvokeSerializeHeader = new CodeMethodInvokeExpression(null, "SerializeHeader");
            codeMethodInvokeSerializeHeader.Parameters.Add(new CodeSnippetExpression("_serializer"));
            serializerMethod.Statements.Add(codeMethodInvokeSerializeHeader);

            foreach (Parameter parameter in parameters)
            {
                CodeMethodInvokeExpression codeMethodInvokeExpression = new CodeMethodInvokeExpression(new CodeTypeReferenceExpression($"_serializer"), "Serialize" + GetTypeSerializer(parameter.DataType));
                
                if (parameter.AutoSize != null)
                {
                    codeMethodInvokeExpression.Parameters.Add(new CodeSnippetExpression($"_{parameter.AutoSize}.Length"));
                }
                else
                {
                    codeMethodInvokeExpression.Parameters.Add(new CodeSnippetExpression($"_{parameter.Name}"));
                }
                serializerMethod.Statements.Add(codeMethodInvokeExpression);
            }

            CodeMethodReturnStatement codeMethodReturnStatement = new CodeMethodReturnStatement(new CodeMethodInvokeExpression(new CodeTypeReferenceExpression($"_serializer"), "GetPayload"));
            serializerMethod.Statements.Add(codeMethodReturnStatement);
            protocolClass.Members.Add(serializerMethod);
        }

        private void CreateResponseAndHandlerConstructor(List<Parameter> parameters, CodeTypeDeclaration protocolClass)
        {
            CodeConstructor codeConstructor = new CodeConstructor();
            codeConstructor.Attributes = MemberAttributes.Public;
            codeConstructor.Parameters.Add(new CodeParameterDeclarationExpression(typeof(int[]), "inputBuffer")); 
            codeConstructor.BaseConstructorArgs.Add(new CodeVariableReferenceExpression("inputBuffer"));

            Dictionary<string, string> autoSizers = new Dictionary<string, string>();
            foreach (Parameter parameter in parameters)
            {
                CodeMethodInvokeExpression codeMethodInvokeExpression = new CodeMethodInvokeExpression(new CodeTypeReferenceExpression($"deserializer"), "Deserialize" + GetTypeSerializer(parameter.DataType));

                if (parameter.AutoSize != null)
                {
                    CodeAssignStatement lengthAssignStatement = new CodeAssignStatement(new CodeVariableReferenceExpression($"int {parameter.Name}"), codeMethodInvokeExpression);
                    codeConstructor.Statements.Add(lengthAssignStatement);
                    autoSizers.Add(parameter.AutoSize, parameter.Name);
                    continue;
                }

                if (autoSizers.ContainsKey(parameter.Name) && autoSizers[parameter.Name] != null)
                {
                    codeMethodInvokeExpression.Parameters.Add(new CodeSnippetExpression(autoSizers[parameter.Name]));
                }
                else if (parameter.DataType.Contains("[") && parameter.DataType.Contains("]") && !parameter.DataType.Contains("[]"))
                {
                    int length = int.Parse(parameter.DataType.Substring(parameter.DataType.IndexOf("[") + 1, parameter.DataType.IndexOf("]") - parameter.DataType.IndexOf("[") - 1));
                    codeMethodInvokeExpression.Parameters.Add(new CodeSnippetExpression(length.ToString()));
                }

                CodeAssignStatement codeAssignStatement = new CodeAssignStatement(new CodeVariableReferenceExpression($"_{parameter.Name}"), codeMethodInvokeExpression);
                codeConstructor.Statements.Add(codeAssignStatement);

            }

            protocolClass.Members.Add(codeConstructor);
        }

        private void CreateToStringOverride(string className, List<Parameter> parameters, CodeTypeDeclaration protocolClass)
        {
            CodeMemberMethod codeMemberMethod = CreateMethodOverride("ToString", null, new CodeTypeReference(typeof(string)), null);

            int parameterCount = parameters.Count;

            if (parameterCount == 0)
            {
                codeMemberMethod.Statements.Add(new CodeMethodReturnStatement(new CodeMethodInvokeExpression(new CodeBaseReferenceExpression(), "ToString")));
            }
            else
            {
                CodeObjectCreateExpression codeObjectCreateExpression = new CodeObjectCreateExpression(new CodeTypeReference(typeof(StringBuilder)));
                //codeObjectCreateExpression.Parameters.Add(new CodeSnippetExpression($"{className.Length + 3 * ((parameterCount + 1) * 30)}"));
                CodeAssignStatement codeAssignStatement = new CodeAssignStatement(new CodeVariableReferenceExpression("System.Text.StringBuilder builder"), codeObjectCreateExpression);
                codeMemberMethod.Statements.Add(codeAssignStatement);

                CreateToString(codeMemberMethod, className, parameters);


                CodeMethodInvokeExpression codeMethodInvokeExpression = new CodeMethodInvokeExpression(new CodeTypeReferenceExpression($"builder"), "Append");
                codeMethodInvokeExpression.Parameters.Add(new CodeSnippetExpression("']'"));
                codeMemberMethod.Statements.Add(codeMethodInvokeExpression);

                codeMemberMethod.Statements.Add(new CodeMethodReturnStatement(new CodeMethodInvokeExpression(new CodeTypeReferenceExpression($"builder"), "ToString")));
            }

            protocolClass.Members.Add(codeMemberMethod);
        }

        private void CreateToString(CodeMemberMethod codeMemberMethod, string className, List<Parameter> parameters)
        {
            bool first = true;

            foreach (var parameter in parameters)
            {
                if (parameter.AutoSize != null)
                {
                    continue;
                }

                if (first)
                {
                    CodeMethodInvokeExpression codeMethodInvokeExpression = new CodeMethodInvokeExpression(new CodeTypeReferenceExpression($"builder"), "Append");
                    codeMethodInvokeExpression.Parameters.Add(new CodeSnippetExpression($"\"{className} [{parameter.Name}=\""));
                    codeMemberMethod.Statements.Add(codeMethodInvokeExpression);
                }
                else
                {
                    CodeMethodInvokeExpression codeMethodInvokeExpression = new CodeMethodInvokeExpression(new CodeTypeReferenceExpression($"builder"), "Append");
                    codeMethodInvokeExpression.Parameters.Add(new CodeSnippetExpression($"\", {parameter.Name}=\""));
                    codeMemberMethod.Statements.Add(codeMethodInvokeExpression);
                }

                first = false;
                CodeConditionStatement codeConditionStatement = null;
                CodeExpressionStatement codeExpressionStatement = null;
                if (parameter.DataType.Contains("[") || parameter.DataType.Equals("Data"))
                {
                    codeConditionStatement = new CodeConditionStatement(
                        new CodeSnippetExpression($"_{parameter.Name} == null"),
                        new CodeStatement[] { new CodeSnippetStatement($"                builder.Append(\"null\");") },
                        new CodeStatement[] { new CodeIterationStatement(
                        new CodeSnippetStatement("int cnt = 0"),
                        new CodeSnippetExpression($"cnt < _{parameter.Name}.Length"),
                        new CodeSnippetStatement("cnt++"),
                        new CodeStatement[]
                        {
                            new CodeConditionStatement(
                                new CodeSnippetExpression($"cnt > 0"),
                                new CodeStatement[] { new CodeSnippetStatement($"                        builder.Append(' ');") }),
                            new CodeExpressionStatement(new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(null, "builder.Append"), new CodeExpression[] { new CodeFieldReferenceExpression(null, FormatParameterString(parameter)) }))
                        })
                        });
                    codeMemberMethod.Statements.Add(codeConditionStatement);
                }
                else
                {
                    codeExpressionStatement = new CodeExpressionStatement(new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(null, "builder.Append"), new CodeExpression[] { new CodeFieldReferenceExpression(null, FormatParameterString(parameter)) }));
                }

                if (codeExpressionStatement != null)
                {
                    codeMemberMethod.Statements.Add(codeExpressionStatement);
                }
            }
        }

        private void CreateParameterGetter(List<Parameter> parameters, CodeNamespace codeNamespace, CodeTypeDeclaration protocolClass)
        {
            foreach (Parameter parameter in parameters)
            {
                if (parameter.AutoSize != null)
                {
                    continue;
                }

                StringBuilder stringBuilder = new StringBuilder();
                if (!string.IsNullOrEmpty(parameter.Description))
                {
                    OutputWithLineBreak(stringBuilder, "    ", parameter.Description);
                }

                //stringBuilder.Clear();
                if (parameter.Multiple)
                {
                    stringBuilder.AppendLine($" Return the {parameter.Name} as a set of <see cref=\"{GetTypeClass(parameter.DataType, null).BaseType}\"/>");
                }
                else
                {
                    stringBuilder.AppendLine($" Return the {parameter.Name} as <see cref=\"{GetTypeClass(parameter.DataType, null).BaseType}\"/>");
                }

                CodeMemberMethod getMethod;
                string methodName = $"Get{parameter.Name.UpperCaseFirstCharacter()}";

                if (methodName == "GetType")//we don't want to hide object.GetType()
                    methodName = "GetType2";
                if (parameter.Multiple)
                {
                    getMethod = CreateMethod(methodName, null, new CodeTypeReference($"HashSet<{GetTypeClass(parameter.DataType, codeNamespace).BaseType}>"), null);
                }
                else
                {
                    getMethod = CreateMethod(methodName, null, GetTypeClass(parameter.DataType, codeNamespace), null);
                }

                // TODO: Test if this is appropriate. Especially if the return type is a list.
                getMethod.Statements.Add(new CodeMethodReturnStatement(new CodeArgumentReferenceExpression($"_{parameter.Name}")));

                IEnumerable<ICodeCommentEntity> codeComment = new List<ICodeCommentEntity>
                {
                    new CodeCommentEntity
                    {
                        Tag = CodeCommentTag.Summary,
                        DocumentationText = stringBuilder.ToString()
                    }
                };
                AddCodeComment(getMethod, codeComment, true);

                protocolClass.Members.Add(getMethod);
            }
        }

        private void CreateParameterSetters(List<Parameter> parameters, CodeNamespace codeNamespace, CodeTypeDeclaration protocolClass)
        {
            foreach (Parameter parameter in parameters)
            {
                if (parameter.AutoSize != null)
                {
                    continue;
                }


                StringBuilder stringBuilder = new StringBuilder();
                if (!string.IsNullOrEmpty(parameter.Description))
                {
                    OutputWithLineBreak(stringBuilder, "    ", parameter.Description);
                }

                CodeFieldReferenceExpression parameterReference = new CodeFieldReferenceExpression(null, $"_{parameter.Name}");
                if (parameter.Multiple)
                {
                    IList<string[]> methodStrings = new List<string[]>
                    {
                        new[] { "Add", "", "", "Add" },
                        new[] { "Remove", "", "", "Remove" },
                        new[] { "Set", "IEnumerable<", ">", "UnionWith" },
                    };

                    foreach (string[] methodString in methodStrings)
                    {
                        CodeMemberMethod memberMethod = CreateMethod($"{methodString[0]}{parameter.Name.UpperCaseFirstCharacter()}", new CodeParameterDeclarationExpressionCollection
                        {
                            new CodeParameterDeclarationExpression(new CodeTypeReference(new CodeTypeParameter($"{methodString[1]}{GetTypeClass(parameter.DataType, codeNamespace).BaseType}{methodString[2]}")), $"{parameter.Name}")
                        }, null, null);
                        CodeMethodInvokeExpression invokeExpression = new CodeMethodInvokeExpression(parameterReference, $"{methodString[3]}", new CodeTypeReferenceExpression($"{parameter.Name}"));
                        memberMethod.Statements.Add(invokeExpression);

                        IList<ICodeCommentEntity> codeComment = new List<ICodeCommentEntity>
                        {
                            new CodeCommentEntity
                            {
                                Tag = CodeCommentTag.Summary,
                                DocumentationText = $"The {parameter.Name} to {methodString[0].ToLower()} to the set as <see cref=\"{parameter.DataType}\"/>"
                            }
                        };

                        AddCodeComment(memberMethod, codeComment, true);

                        protocolClass.Members.Add(memberMethod);
                    }
                }
                else
                {
                    CodeMemberMethod setMethod = CreateMethod($"Set{parameter.Name.UpperCaseFirstCharacter()}", new CodeParameterDeclarationExpressionCollection
                    {
                        new CodeParameterDeclarationExpression(GetTypeClass(parameter.DataType, codeNamespace), $"{parameter.Name}"),
                    }, null, null);

                    IList<ICodeCommentEntity> codeComment = new List<ICodeCommentEntity>
                        {
                            new CodeCommentEntity
                            {
                                Tag = CodeCommentTag.Summary,
                                DocumentationText = $"The {parameter.Name} to set as <see cref=\"{parameter.DataType}\"/>"
                            }
                        };

                    AddCodeComment(setMethod, codeComment, true);

                    setMethod.Statements.Add(new CodeAssignStatement(parameterReference, new CodeArgumentReferenceExpression($"{parameter.Name}")));

                    protocolClass.Members.Add(setMethod);
                }
            }
        }

        private void CreateParameters(CodeNamespace codeNamespace, CodeTypeDeclaration protocolClass, IList<Parameter> parameters)
        {
            foreach (var parameter in parameters)
            {
                if (parameter.AutoSize != null)
                {
                    continue;
                }

                StringBuilder stringBuilder = new StringBuilder();

                if (!string.IsNullOrEmpty(parameter.Description))
                {
                    OutputWithLineBreak(stringBuilder, "    ", parameter.Description);
                }
                CreateParameterDefinition(codeNamespace, protocolClass, stringBuilder, parameter);
            }
        }

        /// <summary>
        /// Method for creating a parameter definition.
        /// </summary>
        /// <param name="codeNamespace">The code namespace.</param>
        /// <param name="codeTypeDeclaration">The code type declaration.</param>
        /// <param name="codeComment">The code comment.</param>
        /// <param name="parameter">The parameter.</param>
        private void CreateParameterDefinition(CodeNamespace codeNamespace, CodeTypeDeclaration codeTypeDeclaration, StringBuilder codeComment, Parameter parameter)
        {
            CodeMemberField codeMemberField;
            CodeTypeReference parameterType = GetTypeClass(parameter.DataType, codeNamespace);
            if (parameter.Multiple)
            {
                AddNamespaceImport(codeNamespace, "System.Collections.Generic");
                codeMemberField = CreateCodeMemberField(parameter.Name, new CodeTypeReference($"HashSet<{parameterType.BaseType}>"), MemberAttributes.Private, true);
            }
            else
            {
                codeMemberField = CreateCodeMemberField(parameter.Name, parameterType, MemberAttributes.Private, false);
            }

            IEnumerable<ICodeCommentEntity> codeCommentEntity = new List<ICodeCommentEntity>
                {
                    new CodeCommentEntity
                    {
                        Tag = CodeCommentTag.Summary,
                        DocumentationText = codeComment.ToString(),
                    }
                };
            AddCodeComment(codeMemberField, codeCommentEntity, true);

            codeTypeDeclaration.Members.Add(codeMemberField);
        }

        /// <summary>
        /// Method for creating a property.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="propertyType">Type of the property.</param>
        /// <param name="propertyComments">The property comments.</param>
        /// <param name="hasGet">if set to <c>true</c> [has get].</param>
        /// <param name="hasSet">if set to <c>true</c> [has set].</param>
        /// <returns></returns>
        private CodeMemberProperty CreateProperty(string propertyName, CodeTypeReference propertyType, StringBuilder propertyComments, bool hasGet, bool hasSet)
        {
            CodeMemberProperty codeMemberProperty = new CodeMemberProperty
            {
                Attributes = MemberAttributes.Public | MemberAttributes.Final,
                Name = propertyName,
                HasGet = hasGet,
                HasSet = hasSet,
                Type = propertyType,
            };

            IEnumerable<ICodeCommentEntity> codeCommentEntity = new List<ICodeCommentEntity>
                {
                    new CodeCommentEntity
                    {
                        Tag = CodeCommentTag.Summary,
                        DocumentationText = propertyComments.ToString(),
                    }
                };
            AddCodeComment(codeMemberProperty, codeCommentEntity, true);

            return codeMemberProperty;
        }

        /// <summary>
        /// Method for creating a method.
        /// </summary>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="declarationExpressionCollection">The declaration expression collection.</param>
        /// <param name="returnType">Type of the return.</param>
        /// <param name="returnStatement">The return statement.</param>
        /// <returns></returns>
        private CodeMemberMethod CreateMethod(string methodName, CodeParameterDeclarationExpressionCollection declarationExpressionCollection, CodeTypeReference returnType, CodeMethodReturnStatement returnStatement)
        {
            CodeMemberMethod codeMemberMethod = new CodeMemberMethod
            {
                Attributes = MemberAttributes.Public | MemberAttributes.Final,
                Name = methodName
            };

            if (declarationExpressionCollection != null)
            {
                codeMemberMethod.Parameters.AddRange(declarationExpressionCollection);
            }

            if (returnType != null)
            {
                codeMemberMethod.ReturnType = returnType;
            }

            if (returnStatement != null)
            {
                codeMemberMethod.Statements.Add(returnStatement);
            }
            return codeMemberMethod;
        }

        private CodeMemberMethod CreateMethodOverride(string methodName, CodeParameterDeclarationExpressionCollection declarationExpressionCollection, CodeTypeReference returnType, CodeMethodReturnStatement returnStatement)
        {
            CodeMemberMethod codeMemberMethod = CreateMethod(methodName, declarationExpressionCollection, returnType, returnStatement);
            codeMemberMethod.Attributes = MemberAttributes.Public | MemberAttributes.Override;
            return codeMemberMethod;
        }

        private static void CreateCompileUnit(out CodeCompileUnit compileUnit, out CodeNamespace codeNamespace, string namespaceString)
        {
            compileUnit = new CodeCompileUnit();
            codeNamespace = new CodeNamespace(namespaceString);
            compileUnit.Namespaces.Add(codeNamespace);
        }

        private void AddCodeComment(CodeTypeMember codeTypeMember, IEnumerable<ICodeCommentEntity> codeComments, bool isDocComment)
        {
            if (isDocComment)
            {
                CodeCommentStatementCollection descriptionCodeComment = CodeCommentHelper.BuildCodeCommentStatementCollection(codeComments);
                codeTypeMember.Comments.AddRange(descriptionCodeComment);
            }
            else
            {
                foreach (var codeComment in codeComments)
                {
                    CodeCommentStatement descriptionCodeComment = CodeCommentHelper.BuildCodeCommentStatement(codeComment, isDocComment);
                    codeTypeMember.Comments.Add(descriptionCodeComment);
                }
            }
        }

        /// <summary>
        /// Method for creating a member variable.
        /// </summary>
        /// <param name="memberName">Name of the member.</param>
        /// <param name="codeTypeReference">The type string.</param>
        /// <param name="memberAttributes">The member attributes.</param>
        /// <param name="initializeMember">if set to <c>true</c> [initialize member].</param>
        /// <returns></returns>
        private static CodeMemberField CreateCodeMemberField(string memberName, CodeTypeReference codeTypeReference, MemberAttributes memberAttributes, bool initializeMember)
        {
            CodeMemberField codeMemberField = new CodeMemberField
            {
                Name = $"_{memberName}",
                Type = codeTypeReference
            };

            if (initializeMember)
            {
                codeMemberField.InitExpression = new CodeObjectCreateExpression(codeTypeReference, new CodeExpression[] { });
            }
            codeMemberField.Attributes = MemberAttributes.Private;

            return codeMemberField;
        }

        private static CodeAssignStatement CreateCodeAssignStatement(string parameterName, object parameterValue)
        {
            return new CodeAssignStatement(new CodeVariableReferenceExpression($"_{parameterName.ToLowerCamelCase()}"), new CodePrimitiveExpression(parameterValue));
        }

        private static CodeMemberField CreateCodeConstantMember(string constantName, Type constantType, object constantValue)
        {
            // When declaring a public constant type member field, you must set a particular
            // access and scope mask to the member attributes of the field in order for the
            // code generator to properly generate the field as a constant field.

            // Declares an integer field using a CodeMemberField
            CodeMemberField constPublicField = new CodeMemberField(constantType, constantName);

            // Resets the access and scope mask bit flags of the member attributes of the field
            // before setting the member attributes of the field to public and constant.
            constPublicField.Attributes = (constPublicField.Attributes & ~MemberAttributes.AccessMask & ~MemberAttributes.ScopeMask) | MemberAttributes.Public | MemberAttributes.Const;

            constPublicField.InitExpression = new CodePrimitiveExpression(constantValue);
            return constPublicField;
        }

        private static void GenerateCode(CodeCompileUnit codeCompileUnit, string sourceFile, string folder)
        {
            CodeDomProvider provider = CodeDomProvider.CreateProvider("CSharp");


            if (provider.FileExtension[0] == '.')
            {
                sourceFile = $@"..\..\..\..\..\libraries\ZigBeeNet.Hardware.Ember\Ezsp\{folder}{sourceFile}{provider.FileExtension}";
            }
            else
            {
                sourceFile = $@"..\..\..\..\..\libraries\ZigBeeNet.Hardware.Ember\Ezsp\{folder}{sourceFile}.{provider.FileExtension}";
            }

            var codeGeneratorOptions = new CodeGeneratorOptions
            {
                BracingStyle = "C",
            };
            IndentedTextWriter tw = new IndentedTextWriter(new StreamWriter(sourceFile, false), "    ");
            provider.GenerateCodeFromCompileUnit(codeCompileUnit, tw, codeGeneratorOptions);
            tw.Close();
        }

        protected CodeTypeReference GetTypeClass(string dataType, CodeNamespace codeNamespace)
        {
            string modifier = "";
            string dataTypeLocal = dataType;
            if (dataType.Contains("["))
            {
                dataTypeLocal = dataTypeLocal.Substring(0, dataTypeLocal.IndexOf("["));
                modifier = "[]";
            }
            switch (dataTypeLocal)
            {
                case "EmberNodeId":
                case "EmberCounterType":
                case "EmberGpSecurityFrameCounter":
                case "int8s":
                case "uint8_u":
                case "uint8_t":
                case "uint16_t":
                case "uint32_t":
                    {
                        if(modifier == "[]")
                            return new CodeTypeReference(typeof(int[]));
                        else
                            return new CodeTypeReference(typeof(int));
                    }
                case "bool":
                    {
                        return new CodeTypeReference(typeof(bool));
                    }
                case "String":
                    {
                        return new CodeTypeReference(typeof(string));
                    }
                case "EmberEUI64":
                    {
                        return new CodeTypeReference("IeeeAddress");
                    }
                default:
                    {
                        if(codeNamespace != null)
                            AddNamespaceImport(codeNamespace, _ezspStructurePackage);
                        return new CodeTypeReference(dataTypeLocal + modifier);
                    }
            }
        }

        private string GetTypeSerializer(string dataType)
        {
            string dataTypeLocal = dataType;
            if (dataType.Contains("["))
            {
                dataTypeLocal = dataTypeLocal.Substring(0, dataTypeLocal.IndexOf("[") + 1);
            }
            switch (dataTypeLocal)
            {
                case "EmberCounterType":
                case "uint8_t":
                case "uint8_u":
                    return "UInt8";
                case "EmberNodeId":
                case "uint16_t":
                    return "UInt16";
                case "uint32_t":
                case "EmberGpSecurityFrameCounter":
                    return "UInt32";
                case "uint8_t[":
                case "uint8_u[":
                    return "UInt8Array";
                case "int8s":
                    return "Int8S";
                case "uint16_t[":
                    return "UInt16Array";
                case "Bool":
                    return "Boolean";
                case "EmberEUI64":
                    return "EmberEui64";
                case "bool":
                    return "Bool";
                case "EmberAesMmoHashContext":
                    return "EmberAesMmoHashContext";
                case "EzspValueId":
                    return "EzspValueId";
                case "EmberKeyStruct":
                    return "EmberKeyStruct";
                case "EmberPowerMode":
                    return "EmberPowerMode";
                case "ExtendedPanId":
                    return "ExtendedPanId";
                case "EmberGpAddress":
                    return "EmberGpAddress";
                case "EmberGpSecurityLevel":
                    return "EmberGpSecurityLevel";
                case "EmberGpKeyType":
                    return "EmberGpKeyType";
                case "EmberGpProxyTableEntry":
                    return "EmberGpProxyTableEntry";
                case "EmberGpSinkListEntry[":
                    return "EmberGpSinkListEntry";
                case "EzspMfgTokenId":
                case "EmberLibraryId":
                case "EmberLibraryStatus":
                case "EmberCertificateData":
                case "EmberCertificate283k1Data":
                case "EmberPublicKeyData":
                case "EmberPublicKey283k1Data":
                case "EmberPrivateKeyData":
                case "EmberPrivateKey283k1Data":
                case "EmberSmacData":
                case "EmberTransientKeyData":
                case "EmberGpSinkListEntry":
                case "EmberGpSinkTableEntry":
                    return dataTypeLocal;
                default:
                    return dataType;
            }
        }

        private void AddNamespaceImport(CodeNamespace codeNamespace, string namespaceToImport)
        {
            codeNamespace.Imports.Add(new CodeNamespaceImport(namespaceToImport));
        }
    }
}