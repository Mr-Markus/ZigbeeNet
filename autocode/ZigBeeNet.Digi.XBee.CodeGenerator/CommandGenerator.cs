using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ZigBeeNet.Digi.XBee.CodeGenerator.Entities;
using ZigBeeNet.Digi.XBee.CodeGenerator.Enumerations;
using ZigBeeNet.Digi.XBee.CodeGenerator.Extensions;
using ZigBeeNet.Digi.XBee.CodeGenerator.Helper;
using ZigBeeNet.Digi.XBee.CodeGenerator.Xml;

namespace ZigBeeNet.Digi.XBee.CodeGenerator
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="ZigBeeNet.Digi.XBee.CodeGenerator.ClassGenerator" />
    public class CommandGenerator : ClassGenerator
    {
        // TODO: rename packages to namespace. packages are not available in C#
        private const string _zigbeePackage = "ZigBeeNet";
        private const string _zigbeeSecurityPackage = "ZigBeeNet.Security";
        private const string _internalPackage = "ZigBeeNet.Hardware.Digi.XBee.Internal";
        private const string _commandPackage = "ZigBeeNet.Hardware.Digi.XBee.Internal.Protocol";
        private const string _enumPackage = "ZigBeeNet.Hardware.Digi.XBee.Internal.Protocol";

        private readonly Dictionary<int, string> _events = new Dictionary<int, string>();

        public void Go(Protocol protocol)
        {
            // Create "API" commands for AT commands
            foreach (Command atCommand in protocol.AT_Commands)
            {
                Parameter idParameter = new Parameter
                {
                    Name = "Frame ID",
                    DataType = "uint8",
                    Multiple = false,
                    Bitfield = false
                };

                Parameter stateParameter = new Parameter
                {
                    Name = "Command Status",
                    DataType = "CommandStatus",
                    Multiple = false,
                    Bitfield = false
                };

                Parameter atParameter = new Parameter
                {
                    Name = "AT Parameter",
                    DataType = "AtCommand",
                    Value = atCommand.CommandProperty
                };

                string description = "AT Command <b>" + atCommand.CommandProperty + "</b></p>" + atCommand.Description;

                if (atCommand.Getter)
                {
                    Command command = new Command
                    {
                        Id = 0x08,
                        Name = "Get " + atCommand.Name,
                        Description = description,
                        CommandParameters = new List<ParameterGroup>(),
                        ResponseParameters = new List<ParameterGroup>()
                    };

                    ParameterGroup commandGroup = new ParameterGroup
                    {
                        Parameters = new List<Parameter>()
                    };

                    commandGroup.Parameters.Add(idParameter);
                    commandGroup.Parameters.Add(atParameter);
                    command.CommandParameters.Add(commandGroup);
                    protocol.Commands.Add(command);
                }

                if (atCommand.Setter)
                {
                    Command command = new Command
                    {
                        Id = 0x08,
                        Name = "Set " + atCommand.Name,
                        Description = description,
                        CommandParameters = new List<ParameterGroup>(),
                        ResponseParameters = new List<ParameterGroup>()
                    };
                    ParameterGroup commandGroup = new ParameterGroup
                    {
                        Parameters = new List<Parameter>()
                    };
                    commandGroup.Parameters.Add(idParameter);
                    commandGroup.Parameters.Add(atParameter);

                    if (atCommand.CommandParameters != null && atCommand.CommandParameters.Count != 0)
                    {
                        commandGroup.Parameters.AddRange(atCommand.CommandParameters[0].Parameters);
                    }

                    command.CommandParameters.Add(commandGroup);
                    protocol.Commands.Add(command);
                }

                Command response = new Command
                {
                    Id = 0x88,
                    Name = atCommand.Name,
                    Description = description,
                    CommandParameters = new List<ParameterGroup>(),
                    ResponseParameters = new List<ParameterGroup>()
                };
                ParameterGroup responseGroup = new ParameterGroup
                {
                    Parameters = new List<Parameter>()
                };
                responseGroup.Parameters.Add(idParameter);
                responseGroup.Parameters.Add(atParameter);
                responseGroup.Parameters.Add(stateParameter);
                if (atCommand.ResponseParameters != null && atCommand.ResponseParameters.Count != 0)
                {
                    responseGroup.Parameters.AddRange(atCommand.ResponseParameters[0].Parameters);
                }
                response.ResponseParameters.Add(responseGroup);
                protocol.Commands.Add(response);
            }

            string packageName;
            string className;
            foreach (Command command in protocol.Commands)
            {
                packageName = _commandPackage;
                if (command.CommandParameters.Count > 0)
                {
                    className = "XBee" + command.Name.ToUpperCamelCase() + "Command";
                }
                else
                {
                    string responseType = "Event";
                    foreach (Parameter parameter in command.ResponseParameters[0].Parameters)
                    {
                        if (parameter.Name.ToUpper().Equals("FRAME ID"))
                        {
                            responseType = "Response";
                        }

                    }
                    className = "XBee" + command.Name.ToUpperCamelCase() + responseType;
                }

                CreateCommandClass(packageName, className, command, command.CommandParameters,
                        command.ResponseParameters);
            }

            foreach (Enumeration enumeration in protocol.Enumerations)
            {
                CreateEnumClass(enumeration);
            }

            CreateEventFactory("Event", protocol);
            CreateEventFactory("Response", protocol);
        }

        private void CreateEventFactory(string className, Protocol protocol)
        {
            Console.WriteLine($"Processing factory class [XBee{className}()]");

            CreateCompileUnit(out CodeCompileUnit compileUnit, out CodeNamespace codeNamespace, "ZigBeeNet.Hardware.Digi.XBee.Internal.Protocol");
            CodeTypeDeclaration protocolClass = new CodeTypeDeclaration($"XBee{className}Factory")
            {
                IsClass = true,
                TypeAttributes = System.Reflection.TypeAttributes.Public
            };
            codeNamespace.Imports.Add(new CodeNamespaceImport("Serilog"));
            codeNamespace.Imports.Add(new CodeNamespaceImport("System"));
            codeNamespace.Imports.Add(new CodeNamespaceImport("System.Collections.Concurrent"));
            codeNamespace.Imports.Add(new CodeNamespaceImport("System.Collections.Generic"));

            codeNamespace.Types.Add(protocolClass);

            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Helper factory class to create <see cref=\"XBee{className}Factory\"/> classes.");
            IList<ICodeCommentEntity> codeComment = new List<ICodeCommentEntity>
                {
                    new CodeCommentEntity
                    {
                        Tag = CodeCommentTag.Summary,
                        DocumentationText = sb.ToString()
                    }
                };
            AddCodeComment(protocolClass, codeComment, true);

            CodeMemberField eventsMember = new CodeMemberField
            {
                Name = "_events",
                Attributes = MemberAttributes.Private | MemberAttributes.Static,
                Type = new CodeTypeReference("IDictionary<int?, Type>"),
                InitExpression = new CodeObjectCreateExpression("ConcurrentDictionary<int?, Type>")
            };
            protocolClass.Members.Add(eventsMember);

            if (className.Equals("Response"))
            {
                CodeMemberField atCommands = new CodeMemberField
                {
                    Name = "_atCommands",
                    Attributes = MemberAttributes.Private | MemberAttributes.Static,
                    Type = new CodeTypeReference("IDictionary<int?, Type>"),
                    InitExpression = new CodeObjectCreateExpression("ConcurrentDictionary<int?, Type>")
                };
                protocolClass.Members.Add(atCommands);
            }

            IDictionary<int, string> sortedEvents = new Dictionary<int, string>();
            foreach (Command command in protocol.Commands)
            {
                if (command.CommandParameters.Count > 0)
                {
                    continue;
                }
                string responseType = "Event";
                foreach (Parameter parameter in command.ResponseParameters[0].Parameters)
                {
                    if (parameter.Name.ToUpper().Equals("FRAME ID"))
                    {
                        responseType = "Response";
                        break;
                    }
                }
                if (className != responseType)
                {
                    continue;
                }

                if (sortedEvents.TryGetValue(command.Id, out string value))
                {
                    continue;
                }

                string eventClassName = $"XBee{command.Name.ToUpperCamelCase()}{responseType}";
                sortedEvents.Add(command.Id, eventClassName);
            }

            CodeTypeConstructor codeTypeConstructor = new CodeTypeConstructor();
            foreach (KeyValuePair<int, string> sortedEvent in sortedEvents)
            {
                CodeMethodInvokeExpression addDictionaryItem = new CodeMethodInvokeExpression(new CodeTypeReferenceExpression($"_events"), "Add");
                addDictionaryItem.Parameters.Add(new CodeSnippetExpression($"{string.Format("0x{0:X2}", sortedEvent.Key)}, typeof({sortedEvent.Value})"));
                codeTypeConstructor.Statements.Add(addDictionaryItem);
            }
            protocolClass.Members.Add(codeTypeConstructor);

            if (className.Equals("Response"))
            {
                IDictionary<string, string> sortedAt = new SortedDictionary<string, string>();
                foreach (Command atCommand in protocol.AT_Commands)
                {
                    sortedAt.Add(atCommand.CommandProperty, atCommand.Name);
                }

                foreach (string cmd in sortedAt.Keys)
                {
                    char[] cmdChars = cmd.ToCharArray();
                    int cmdInt = Convert.ToInt32(cmdChars[1] + (cmdChars[0] << 8));

                    CodeMethodInvokeExpression addDictionaryItem = new CodeMethodInvokeExpression(new CodeTypeReferenceExpression($"_atCommands"), "Add");
                    addDictionaryItem.Parameters.Add(new CodeSnippetExpression($"{string.Format("0x{0:X4}", cmdInt)}, typeof(XBee{sortedAt[cmd].ToUpperCamelCase()}Response)"));
                    codeTypeConstructor.Statements.Add(addDictionaryItem);
                }
            }

            CodeParameterDeclarationExpressionCollection getXBeeFrameMethodParameters = new CodeParameterDeclarationExpressionCollection
            {
                new CodeParameterDeclarationExpression(typeof(int[]), "data")
            };
            CodeMemberMethod getXBeeFrameMethod = CreateMethod("GetXBeeFrame", getXBeeFrameMethodParameters, new CodeTypeReference($"IXBee{className}"), null);
            getXBeeFrameMethod.Attributes = MemberAttributes.Static | MemberAttributes.Public;

            if (className.Equals("Response"))
            {
                CodeAssignStatement codeAssignStatement = new CodeAssignStatement(new CodeSnippetExpression("Type xbeeClass"), new CodeSnippetExpression("null"));
                getXBeeFrameMethod.Statements.Add(codeAssignStatement);

                CodeConditionStatement atCommandCorrelationCondition = new CodeConditionStatement(
                    new CodeSnippetExpression($"data[2] == 0x88"),
                    new CodeStatement[] { new CodeSnippetStatement($"                xbeeClass = _atCommands[(data[4] << 8) + data[5]];") });
                getXBeeFrameMethod.Statements.Add(atCommandCorrelationCondition);

                CodeConditionStatement apiCommandCorrelationCondition = new CodeConditionStatement(
                    new CodeSnippetExpression($"xbeeClass == null"),
                    new CodeStatement[] { new CodeSnippetStatement($"                xbeeClass = _events[data[2]];") });
                getXBeeFrameMethod.Statements.Add(apiCommandCorrelationCondition);
            }
            else
            {
                CodeAssignStatement codeAssignStatement = new CodeAssignStatement(new CodeSnippetExpression("Type xbeeClass"), new CodeSnippetExpression("_events[data[2]]"));
                getXBeeFrameMethod.Statements.Add(codeAssignStatement);
            }

            CodeConditionStatement noHandlerFoundCondition = new CodeConditionStatement(
                new CodeSnippetExpression($"xbeeClass == null"),
                new CodeStatement[] { new CodeSnippetStatement($"                return null;") });
            getXBeeFrameMethod.Statements.Add(noHandlerFoundCondition);

            CodeTryCatchFinallyStatement codeTryStatement = new CodeTryCatchFinallyStatement();
            codeTryStatement.TryStatements.Add(new CodeAssignStatement(new CodeSnippetExpression($"IXBee{className} xbeeFrame"), new CodeSnippetExpression($"Activator.CreateInstance(xbeeClass) as IXBee{className}")));
            codeTryStatement.TryStatements.Add(new CodeMethodInvokeExpression(new CodeTypeReferenceExpression($"xbeeFrame"), "Deserialize", new CodeVariableReferenceExpression("data")));
            codeTryStatement.TryStatements.Add(new CodeMethodReturnStatement(new CodeVariableReferenceExpression("xbeeFrame")));

            CodeCatchClause catchClause = new CodeCatchClause("ex", new CodeTypeReference("Exception"));
            catchClause.Statements.Add(new CodeMethodInvokeExpression(new CodeTypeReferenceExpression($"Log"), "Debug", new CodeVariableReferenceExpression($"\"Error creating instance of IXBeeResponse\", ex")));

            codeTryStatement.CatchClauses.Add(catchClause);
            getXBeeFrameMethod.Statements.Add(codeTryStatement);

            getXBeeFrameMethod.Statements.Add(new CodeMethodReturnStatement(new CodePrimitiveExpression(null)));
            protocolClass.Members.Add(getXBeeFrameMethod);

            GenerateCode(compileUnit, $"XBee{className}Factory", false);
        }

        private void CreateEnumClass(Enumeration enumeration)
        {
            string className = enumeration.Name.UpperCaseFirstCharacter();
            Console.WriteLine($"Processing enum class {enumeration.Name} [{className}()]");

            CreateCompileUnit(out CodeCompileUnit compileUnit, out CodeNamespace codeNamespace, "ZigBeeNet.Hardware.Digi.XBee.Internal.Protocol");
            CodeTypeDeclaration protocolClass = new CodeTypeDeclaration(className)
            {
                IsEnum = true,
                TypeAttributes = System.Reflection.TypeAttributes.Public
            };
            codeNamespace.Types.Add(protocolClass);

            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Class to implement the XBee Enumeration <b>{enumeration.Name}</b>");
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

            GenerateCode(compileUnit, className, true);
        }

        private void CreateCommandClass(string packageName, string className, Command command, List<ParameterGroup> commandParameterGroup, List<ParameterGroup> responseParameterGroup)
        {
            if (className == "XBeeZigBeeTransmitStatusCommand")
            {
                Console.WriteLine();
            }

            if (className.EndsWith("Event"))
            {
                _events.Add(command.Id, className);
            }

            if (className.EndsWith("Response"))
            {
                // Nothing todo here
            }

            Console.WriteLine($"Processing command class {command.Name}  [{className}()]");

            CreateCompileUnit(out CodeCompileUnit compileUnit, out CodeNamespace codeNamespace, "ZigBeeNet.Hardware.Digi.XBee.Internal.Protocol");
            CodeTypeDeclaration protocolClass = new CodeTypeDeclaration(className)
            {
                IsClass = true,
                TypeAttributes = System.Reflection.TypeAttributes.Public
            };

            StringBuilder descriptionStringBuilder = new StringBuilder();
            descriptionStringBuilder.AppendLine($"Class to implement the XBee command \" {command.Name} \".");
            if (!string.IsNullOrEmpty(command.Description))
            {
                OutputWithLineBreak(descriptionStringBuilder, "", command.Description);
            }
            descriptionStringBuilder.AppendLine(" This class provides methods for processing XBee API commands.");
            ICodeCommentEntity descriptionCodeCommentEntity = new CodeCommentEntity
            {
                Tag = CodeCommentTag.Summary,
                DocumentationText = descriptionStringBuilder.ToString()
            };
            CodeCommentStatement descriptionCodeComment = CodeCommentHelper.BuildCodeCommentStatement(descriptionCodeCommentEntity, true);
            protocolClass.Comments.Add(descriptionCodeComment);

            protocolClass.BaseTypes.Add("XBeeFrame");

            if (commandParameterGroup.Count > 0)
            {
                protocolClass.BaseTypes.Add("IXBeeCommand ");
            }

            if (className.EndsWith("Event"))
            {
                protocolClass.BaseTypes.Add("IXBeeEvent");
            }

            if (className.EndsWith("Response"))
            {
                protocolClass.BaseTypes.Add("IXBeeResponse ");
            }

            codeNamespace.Types.Add(protocolClass);

            CreateParameterGroups(codeNamespace, protocolClass, commandParameterGroup, null);

            CreateParameterGroups(codeNamespace, protocolClass, responseParameterGroup, (group, stringBuilder) =>
            {
                stringBuilder.AppendLine("Response field");
                if (bool.TrueString.Equals(group.Multiple))
                {
                    stringBuilder.AppendLine("Field accepts multiple responses.");
                }
            });

            CreateParameterSetters(commandParameterGroup, codeNamespace, protocolClass);

            CreateParameterGetter(responseParameterGroup, codeNamespace, protocolClass);

            CreateSerializerMethods(commandParameterGroup, command, protocolClass);

            CreateDeserializerMethods(responseParameterGroup, protocolClass);

            CreateToStringOverride(className, commandParameterGroup, responseParameterGroup, protocolClass);

            // Go on with line 414
            // Todo: Check if needed. Code is never hit in java solution

            GenerateCode(compileUnit, className, true);
        }

        private void CreateToStringOverride(string className, List<ParameterGroup> commandParameterGroup, List<ParameterGroup> responseParameterGroup, CodeTypeDeclaration protocolClass)
        {
            CodeMemberMethod codeMemberMethod = CreateMethodOverride("ToString", null, new CodeTypeReference(typeof(string)), null);

            int parameterCount = 0;
            foreach (ParameterGroup group in commandParameterGroup)
            {
                if (group.Parameters != null)
                {
                    parameterCount += group.Parameters.Count;
                }
            }

            foreach (ParameterGroup group in responseParameterGroup)
            {
                if (group.Parameters != null)
                {
                    parameterCount += group.Parameters.Count;
                }
            }

            if (parameterCount == 0 && !className.EndsWith("Command"))
            {
                codeMemberMethod.Statements.Add(new CodeMethodReturnStatement(new CodeMethodInvokeExpression(new CodeBaseReferenceExpression(), "ToString")));
            }
            else
            {
                CodeObjectCreateExpression codeObjectCreateExpression = new CodeObjectCreateExpression(new CodeTypeReference(typeof(StringBuilder)));
                codeObjectCreateExpression.Parameters.Add(new CodeSnippetExpression($"{className.Length + 3 * ((parameterCount + 1) * 30)}"));
                CodeAssignStatement codeAssignStatement = new CodeAssignStatement(new CodeVariableReferenceExpression("System.Text.StringBuilder builder"), codeObjectCreateExpression);
                codeMemberMethod.Statements.Add(codeAssignStatement);

                bool first = true;
                if (className.EndsWith("Command")
                    && commandParameterGroup != null
                    && commandParameterGroup.Count > 0
                    && commandParameterGroup[0].Parameters != null
                    && commandParameterGroup[0].Parameters.Count != 0)
                {
                    foreach (ParameterGroup group in commandParameterGroup)
                    {
                        CreateToString(codeMemberMethod, first, className, group);
                        first = false;
                    }
                }

                foreach (ParameterGroup group in responseParameterGroup)
                {
                    if (group == null || group.Parameters == null || group.Parameters.Count == 0)
                    {
                        continue;
                    }

                    if (group.Multiple)
                    {
                        if (first)
                        {
                            CodeMethodInvokeExpression firstCodeMethodInvokeExpression = new CodeMethodInvokeExpression(new CodeTypeReferenceExpression($"builder"), "Append");
                            firstCodeMethodInvokeExpression.Parameters.Add(new CodeSnippetExpression($"\"{className} [{group.Name.ToLowerCamelCase()}s=\""));
                            codeMemberMethod.Statements.Add(firstCodeMethodInvokeExpression);
                        }
                        else
                        {
                            CodeMethodInvokeExpression NotFistCodeMethodInvokeExpression = new CodeMethodInvokeExpression(new CodeTypeReferenceExpression($"builder"), "Append");
                            NotFistCodeMethodInvokeExpression.Parameters.Add(new CodeSnippetExpression($"\", {group.Name.ToLowerCamelCase()}s=\""));
                            codeMemberMethod.Statements.Add(NotFistCodeMethodInvokeExpression);
                        }
                        CodeMethodInvokeExpression methodInvokeExpression = new CodeMethodInvokeExpression(new CodeTypeReferenceExpression($"builder"), "Append");
                        methodInvokeExpression.Parameters.Add(new CodeSnippetExpression($"{group.Name.ToLowerCamelCase()}s"));
                        codeMemberMethod.Statements.Add(methodInvokeExpression);
                        first = false;
                        continue;
                    }
                    CreateToString(codeMemberMethod, first, className, group);
                    first = false;
                }

                if (className.EndsWith("Command"))
                {
                    if (first)
                    {
                        CodeMethodInvokeExpression firstCommandCodeMethodInvokeExpression = new CodeMethodInvokeExpression(new CodeTypeReferenceExpression($"builder"), "Append");
                        firstCommandCodeMethodInvokeExpression.Parameters.Add(new CodeSnippetExpression($"\"{className} [\""));
                        codeMemberMethod.Statements.Add(firstCommandCodeMethodInvokeExpression);
                    }
                }
                CodeMethodInvokeExpression codeMethodInvokeExpression = new CodeMethodInvokeExpression(new CodeTypeReferenceExpression($"builder"), "Append");
                codeMethodInvokeExpression.Parameters.Add(new CodeSnippetExpression("']'"));
                codeMemberMethod.Statements.Add(codeMethodInvokeExpression);

                codeMemberMethod.Statements.Add(new CodeMethodReturnStatement(new CodeMethodInvokeExpression(new CodeTypeReferenceExpression($"builder"), "ToString")));
            }

            protocolClass.Members.Add(codeMemberMethod);
        }

        private void CreateToString(CodeMemberMethod codeMemberMethod, bool first, string className, ParameterGroup group)
        {
            CodeConditionStatement CommandCodeConditionStatement = null;
            CodeExpressionStatement CommandCodeExpressionStatement = null;
            bool isCommandStatusParameter = false;
            foreach (var parameter in group.Parameters)
            {
                if (parameter.AutoSize != null)
                {
                    continue;
                }

                // Constant...
                if (!string.IsNullOrEmpty(parameter.Value))
                {
                    continue;
                }

                if (first)
                {
                    CodeMethodInvokeExpression codeMethodInvokeExpression = new CodeMethodInvokeExpression(new CodeTypeReferenceExpression($"builder"), "Append");
                    codeMethodInvokeExpression.Parameters.Add(new CodeSnippetExpression($"\"{className} [{parameter.Name.ToLowerCamelCase()}=\""));
                    codeMemberMethod.Statements.Add(codeMethodInvokeExpression);
                }
                else
                {
                    if (!isCommandStatusParameter)
                    {
                        CodeMethodInvokeExpression codeMethodInvokeExpression = new CodeMethodInvokeExpression(new CodeTypeReferenceExpression($"builder"), "Append");
                        codeMethodInvokeExpression.Parameters.Add(new CodeSnippetExpression($"\", {parameter.Name.ToLowerCamelCase()}=\""));
                        codeMemberMethod.Statements.Add(codeMethodInvokeExpression);
                    }
                }

                first = false;
                string sizer = "2";
                CodeConditionStatement codeConditionStatement = null;
                CodeExpressionStatement codeExpressionStatement = null;
                if (parameter.DataType.Contains("[]") || parameter.DataType.Equals("Data"))
                {
                    if (parameter.DataType.Contains("16"))
                    {
                        sizer = "4";
                    }
                    if (!isCommandStatusParameter)
                    {
                        codeConditionStatement = new CodeConditionStatement(
                            new CodeSnippetExpression($"_{parameter.Name.ToLowerCamelCase()} == null"),
                            new CodeStatement[] { new CodeSnippetStatement($"                builder.Append(\"null\");") },
                            new CodeStatement[] { new CodeIterationStatement(
                            new CodeSnippetStatement("int cnt = 0"),
                            new CodeSnippetExpression($"cnt < _{parameter.Name.ToLowerCamelCase()}.Length"),
                            new CodeSnippetStatement("cnt++"),
                            new CodeStatement[]
                            {
                                new CodeConditionStatement(
                                    new CodeSnippetExpression($"cnt > 0"),
                                    new CodeStatement[] { new CodeSnippetStatement($"                        builder.Append(' ');") }),
                                new CodeExpressionStatement(new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(null, "builder.Append"), new CodeExpression[] { new CodeFieldReferenceExpression(null, "string.Format(\"0x{0:X" + sizer + "}\", _" + FormatParameterString(parameter) + "[cnt])" ) }))
                            })
                            });
                        codeMemberMethod.Statements.Add(codeConditionStatement);
                    }
                    else
                    {
                        CommandCodeConditionStatement.TrueStatements.Add(new CodeExpressionStatement(new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(null, "builder.Append"), new CodeExpression[] { new CodeFieldReferenceExpression(null, $"\", {parameter.Name.ToLowerCamelCase()}=\"") })));
                        CommandCodeConditionStatement.TrueStatements.Add(new CodeConditionStatement(
                            new CodeSnippetExpression($"_{parameter.Name.ToLowerCamelCase()} == null"),
                            new CodeStatement[] { new CodeSnippetStatement($"                builder.Append(\"null\");") },
                            new CodeStatement[] { new CodeIterationStatement(
                            new CodeSnippetStatement("int cnt = 0"),
                            new CodeSnippetExpression($"cnt < _{parameter.Name.ToLowerCamelCase()}.Length"),
                            new CodeSnippetStatement("cnt++"),
                            new CodeStatement[]
                            {
                                new CodeConditionStatement(
                                    new CodeSnippetExpression($"cnt > 0"),
                                    new CodeStatement[] { new CodeSnippetStatement($"                        builder.Append(' ');") }),
                                new CodeExpressionStatement(new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(null, "builder.Append"), new CodeExpression[] { new CodeFieldReferenceExpression(null, "string.Format(\"0x{0:X" + sizer + "}\", _"+FormatParameterString(parameter)+"[cnt])") }))
                            })
                            }));
                        codeMemberMethod.Statements.Add(CommandCodeConditionStatement);
                        codeMemberMethod.Statements.Add(CommandCodeExpressionStatement);
                    }
                }
                else
                {
                    codeExpressionStatement = new CodeExpressionStatement(new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(null, "builder.Append"), new CodeExpression[] { new CodeFieldReferenceExpression(null, $"_{FormatParameterString(parameter)}") }));
                }

                if (GetTypeSerializer(parameter.DataType).Equals("CommandStatus")
                    && !(group.Parameters.IndexOf(parameter) == (group.Parameters.Count - 1)))
                {
                    CommandCodeConditionStatement = new CodeConditionStatement(
                    new CodeSnippetExpression("_commandStatus == CommandStatus.OK"),
                    new CodeStatement[] { });

                    CommandCodeExpressionStatement = codeExpressionStatement;
                    isCommandStatusParameter = true;
                }

                if (codeExpressionStatement != null)
                {
                    codeMemberMethod.Statements.Add(codeExpressionStatement);
                }
            }
        }

        private void CreateDeserializerMethods(List<ParameterGroup> responseParameterGroup, CodeTypeDeclaration protocolClass)
        {
            if (responseParameterGroup != null && responseParameterGroup.Count != 0)
            {
                CodeMemberMethod deserializerMethod = CreateMethod("Deserialize", new CodeParameterDeclarationExpressionCollection { new CodeParameterDeclarationExpression(typeof(int[]), "incomingData") }, null, null);

                IEnumerable<ICodeCommentEntity> codeComment = new List<ICodeCommentEntity>
                {
                    new CodeCommentEntity
                    {
                        Tag = CodeCommentTag.Summary,
                        DocumentationText = "Method for deserializing the fields for the response"
                    }
                };
                AddCodeComment(deserializerMethod, codeComment, true);

                CodeMethodInvokeExpression invokeExpression = new CodeMethodInvokeExpression(null, "InitializeDeserializer", new CodeVariableReferenceExpression("incomingData"));
                deserializerMethod.Statements.Add(invokeExpression);

                foreach (var group in responseParameterGroup)
                {
                    if (group.Parameters.Count == 0 && group.Required == false && group.Complete == false)
                    {
                        continue;
                    }

                    if (!group.Multiple && group.Parameters != null && group.Parameters.Count > 0)
                    {
                        // TODO: Ported from java code. Don't know if this is needed in the future...
                    }

                    if (group.Multiple)
                    {
                        CodeMethodInvokeExpression invokeExpressionIfIsMultiple = new CodeMethodInvokeExpression(null, $"{group.Name.ToLowerCamelCase()}s.Add", new CodeObjectCreateExpression(group.Name.ToUpperCamelCase(), new CodeVariableReferenceExpression("incomingData")));
                        deserializerMethod.Statements.Add(invokeExpressionIfIsMultiple);
                    }
                    else
                    {
                        CreateDeserializer(group, deserializerMethod);
                    }
                }

                protocolClass.Members.Add(deserializerMethod);
            }
        }

        private void CreateDeserializer(ParameterGroup group, CodeMemberMethod deserializerMethod)
        {
            IDictionary<string, string> autoSizers = new Dictionary<string, string>();
            string conditional = null;

            int cnt = 0;
            foreach (var parameter in group.Parameters)
            {
                // Constant ...
                if (!string.IsNullOrEmpty(parameter.Value))
                {
                    CodeMethodInvokeExpression codeMethodInvokeExpression = new CodeMethodInvokeExpression(null, $"Deserialize{GetTypeSerializer(parameter.DataType)}");
                    deserializerMethod.Statements.Add(codeMethodInvokeExpression);
                    continue;
                }

                CodeConditionStatement codeConditionStatement = null;
                if (!string.IsNullOrEmpty(parameter.Conditional))
                {
                    if (conditional == null)
                    {
                        codeConditionStatement = new CodeConditionStatement(new CodeSnippetExpression($"{parameter.Conditional}"));
                        conditional = parameter.Conditional;
                    }
                    else
                    {
                        if (!parameter.Conditional.Equals(conditional))
                        {
                            // New condition
                            codeConditionStatement?.TrueStatements.Add(new CodeConditionStatement(new CodeSnippetExpression($"{parameter.Conditional}")));
                            conditional = parameter.Conditional;
                        }
                    }
                }
                else if (conditional != null)
                {
                    conditional = null;
                    deserializerMethod.Statements.Add(codeConditionStatement);
                }

                cnt++;
                // TODO: Check where to add the comment below
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append($"Deserialize field \"{parameter.Name}\"");
                if (parameter.Optional == true)
                {
                    stringBuilder.Append("[optional]");
                }

                CodeMethodInvokeExpression deserializeInvokeExpression = new CodeMethodInvokeExpression();
                if (parameter.AutoSize != null)
                {
                    string deserilizerType = GetTypeSerializer(parameter.DataType);
                    CodeAssignStatement codeAssignStatement = new CodeAssignStatement(new CodeVariableReferenceExpression($"int {parameter.Name.ToLowerCamelCase()}"), new CodeMethodInvokeExpression(null, $"Deserialize{deserilizerType}"));
                    autoSizers.Add(parameter.AutoSize, parameter.Name.ToLowerCamelCase());
                    deserializerMethod.Statements.Add(codeAssignStatement);
                    continue;
                }
                if (autoSizers.TryGetValue(parameter.Name, out string temp))
                {
                    string deserilizerType = GetTypeSerializer(parameter.DataType);
                    CodeAssignStatement codeAssignStatement = new CodeAssignStatement(new CodeVariableReferenceExpression($"_{parameter.Name.ToLowerCamelCase()}"), new CodeMethodInvokeExpression(null, $"Deserialize{deserilizerType}", new CodeTypeReferenceExpression($"{autoSizers[parameter.Name]}")));
                    deserializerMethod.Statements.Add(codeAssignStatement);
                    continue;
                }
                if (parameter.DataType.Contains("[") && parameter.DataType.Contains("]") && !parameter.DataType.Contains("[]"))
                {
                    int length = Convert.ToInt32(parameter.DataType.Substring(parameter.DataType.IndexOf("[") + 1, parameter.DataType.IndexOf("]")));
                    string deserilizerType = GetTypeSerializer(parameter.DataType);
                    CodeAssignStatement codeAssignStatement = new CodeAssignStatement(new CodeVariableReferenceExpression($"_{parameter.Name.ToLowerCamelCase()}"), new CodeMethodInvokeExpression(null, $"Deserialize{deserilizerType}", new CodePrimitiveExpression(length)));
                    deserializerMethod.Statements.Add(codeAssignStatement);
                    continue;
                }
                if (parameter.Multiple)
                {
                    CodeIterationStatement endlessForLoop = new CodeIterationStatement(
                        new CodeSnippetStatement(""),
                        new CodeSnippetExpression(""),
                        new CodeSnippetStatement(""),
                        new CodeStatement[]
                        {
                            new CodeAssignStatement(new CodeVariableReferenceExpression($"{GetTypeClass(parameter.DataType, null).BaseType}tmp{parameter.Name.ToUpperCamelCase()}"), new CodeMethodInvokeExpression(null, $"Deserialize{GetTypeSerializer(parameter.DataType)}")),
                            new CodeConditionStatement(
                                new CodeSnippetExpression($"tmp{parameter.Name.ToUpperCamelCase()} == null"),
                                new CodeStatement[] { new CodeSnippetStatement($"                    break;") }),
                            new CodeExpressionStatement( new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(null, $"_{parameter.Name.ToLowerCamelCase()}.Add"), new CodeExpression[] { new CodeFieldReferenceExpression(null, $"tmp{parameter.Name.ToUpperCamelCase()}") }))
                        });
                    deserializerMethod.Statements.Add(endlessForLoop);
                }
                else
                {
                    CodeAssignStatement codeAssignStatement = new CodeAssignStatement(new CodeVariableReferenceExpression($"_{parameter.Name.ToLowerCamelCase()}"), new CodeMethodInvokeExpression(null, $"Deserialize{GetTypeSerializer(parameter.DataType)}"));
                    deserializerMethod.Statements.Add(codeAssignStatement);
                }
                if (GetTypeSerializer(parameter.DataType).Equals("CommandStatus") && !(group.Parameters.IndexOf(parameter) == (group.Parameters.Count - 1)))
                {
                    CodeConditionStatement commandStatusCodeConditionStatement = new CodeConditionStatement(
                        new CodeSnippetExpression($"_commandStatus != CommandStatus.OK || IsComplete()"),
                        new CodeStatement[] { new CodeSnippetStatement($"                    return;") });
                    deserializerMethod.Statements.Add(commandStatusCodeConditionStatement);
                }
                if (cnt < group.Parameters.Count)
                {
                    if (parameter.Optional)
                    {

                    }
                    else
                    {

                    }
                }
            }
        }

        private void CreateSerializerMethods(List<ParameterGroup> commandParameterGroup, Command command, CodeTypeDeclaration protocolClass)
        {
            if (commandParameterGroup != null && commandParameterGroup.Count != 0)
            {
                CodeMemberMethod serializerMethod = CreateMethod("Serialize", null, new CodeTypeReference(typeof(int[])), null);

                IEnumerable<ICodeCommentEntity> codeComment = new List<ICodeCommentEntity>
                {
                    new CodeCommentEntity
                    {
                        Tag = CodeCommentTag.Summary,
                        DocumentationText = "Method for serializing the command fields"
                    }
                };
                AddCodeComment(serializerMethod, codeComment, true);

                CodeConditionStatement codeConditionStatement = null;
                foreach (ParameterGroup group in commandParameterGroup)
                {
                    // TODO: Check if the command id is really needed as hex representation. Int is used for now.
                    CodeMethodInvokeExpression invokeExpression = new CodeMethodInvokeExpression(null, "SerializeCommand", new CodePrimitiveExpression(command.Id));
                    serializerMethod.Statements.Add(invokeExpression);

                    foreach (Parameter parameter in group.Parameters)
                    {
                        string valueName = parameter.Name.ToLowerCamelCase();
                        if (parameter.Optional)
                        {
                            codeConditionStatement = new CodeConditionStatement(new CodeSnippetExpression($"{parameter.Name.ToLowerCamelCase()} != null"));
                            serializerMethod.Statements.Add(codeConditionStatement);
                        }

                        if (!string.IsNullOrEmpty(parameter.Value))
                        {
                            var result = int.TryParse(parameter.Value, out int parameterValue);
                            CodePrimitiveExpression codePrimitiveExpression;
                            if (result)
                            {
                                codePrimitiveExpression = new CodePrimitiveExpression(parameterValue);
                            }
                            else
                            {
                                codePrimitiveExpression = new CodePrimitiveExpression(parameter.Value);
                            }

                            CodeMethodInvokeExpression invokeSerializeExpression = new CodeMethodInvokeExpression(null, $"Serialize{GetTypeSerializer(parameter.DataType)}", codePrimitiveExpression);
                            serializerMethod.Statements.Add(invokeSerializeExpression);
                            continue;
                        }

                        if (parameter.Multiple)
                        {
                            CodeAssignStatement codeAssign = new CodeAssignStatement(new CodeVariableReferenceExpression("first"), new CodePrimitiveExpression(true));
                            serializerMethod.Statements.Add(codeAssign);

                            CodeAssignStatement enumeratorAssignStatement = new CodeAssignStatement(new CodeVariableReferenceExpression("System.Collections.IEnumerator enumerator"), new CodeMethodInvokeExpression(new CodeTypeReferenceExpression($"{parameter.Name.ToLowerCamelCase()}"), "GetEnumerator"));
                            serializerMethod.Statements.Add(enumeratorAssignStatement);

                            // Creates a for loop that sets testInt to 0 and continues incrementing testInt by 1 each loop until testInt is not less than 10.
                            CodeIterationStatement forLoop = new CodeIterationStatement(
                                // initStatement parameter for pre-loop initialization.
                                new CodeSnippetStatement(""),
                                // testExpression parameter to test for continuation condition.
                                new CodeMethodInvokeExpression(new CodeTypeReferenceExpression("enumerator"), "MoveNext"),
                                // incrementStatement parameter indicates statement to execute after each iteration.
                                new CodeSnippetStatement(""),
                                // statements parameter contains the statements to execute during each interation of the loop.
                                new CodeStatement[] { new CodeAssignStatement(new CodeVariableReferenceExpression($"first{parameter.Name.ToUpperCamelCase()}"), new CodePrimitiveExpression(false)) });
                            serializerMethod.Statements.Add(forLoop);
                            valueName = "value";
                        }
                        else if (parameter.AutoSize != null)
                        {
                            CodeMethodInvokeExpression invokeSerializeExpression = new CodeMethodInvokeExpression(null, $"Serialize{GetTypeSerializer(parameter.DataType)}", new CodeTypeReferenceExpression($"_{parameter.AutoSize.ToLowerCamelCase()}.Length"));
                            serializerMethod.Statements.Add(invokeSerializeExpression);
                            continue;
                        }
                        CodeMethodInvokeExpression codeMethodInvokeExpression = new CodeMethodInvokeExpression(null, $"Serialize{GetTypeSerializer(parameter.DataType)}", new CodeTypeReferenceExpression($"_{valueName}"));
                        serializerMethod.Statements.Add(codeMethodInvokeExpression);
                    }
                }
                CodeMethodReturnStatement codeMethodReturnStatement = new CodeMethodReturnStatement(new CodeMethodInvokeExpression(null, "GetPayload"));
                serializerMethod.Statements.Add(codeMethodReturnStatement);
                protocolClass.Members.Add(serializerMethod);
            }
        }

        private void CreateParameterGetter(List<ParameterGroup> responseParameterGroup, CodeNamespace codeNamespace, CodeTypeDeclaration protocolClass)
        {
            foreach (ParameterGroup group in responseParameterGroup)
            {
                if (group.Multiple)
                {
                    AddNamespaceImport(codeNamespace, "System.Collections.Generic");
                    CodeMemberMethod memberMethod = CreateMethod($"Get{group.Name.ToUpperCamelCase()}s", null, new CodeTypeReference($"List<{group.Name.ToLowerCamelCase()}>"), new CodeMethodReturnStatement(new CodeArgumentReferenceExpression($"{group.Name.ToUpperCamelCase()}s")));

                    // TODO: Add a adequate comment to the method
                    //AddCodeComment(memberMethod, new StringBuilder($"The {parameter.Name.ToLowerCamelCase()} to {methodString[0].ToLower()} to the set as <see cref=\"{parameter.DataType}\"/>"));
                    protocolClass.Members.Add(memberMethod);
                    continue;
                }
                foreach (Parameter parameter in group.Parameters)
                {
                    if (parameter.AutoSize != null)
                    {
                        continue;
                    }

                    // Constant...
                    if (!string.IsNullOrEmpty(parameter.Value))
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
                        stringBuilder.AppendLine($" Return the {parameter.Name.ToLowerCamelCase()} as a list of <see cref=\"{GetTypeClass(parameter.DataType, null).BaseType}\"/>");
                    }
                    else
                    {
                        stringBuilder.AppendLine($" Return the {parameter.Name.ToLowerCamelCase()} as <see cref=\"{GetTypeClass(parameter.DataType, null).BaseType}\"/>");
                    }

                    CodeMemberMethod getMethod;
                    if (parameter.Multiple || parameter.Bitfield)
                    {
                        getMethod = CreateMethod($"Get{parameter.Name.ToUpperCamelCase()}", null, new CodeTypeReference($"List<{GetTypeClass(parameter.DataType, codeNamespace).BaseType}>"), null);
                    }
                    else
                    {
                        getMethod = CreateMethod($"Get{parameter.Name.ToUpperCamelCase()}", null, GetTypeClass(parameter.DataType, codeNamespace), null);
                    }

                    // TODO: Test if this is appropriate. Especially if the return type is a list.
                    getMethod.Statements.Add(new CodeMethodReturnStatement(new CodeArgumentReferenceExpression($"_{parameter.Name.ToLowerCamelCase()}")));

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
        }

        private void CreateParameterSetters(List<ParameterGroup> commandParameterGroup, CodeNamespace codeNamespace, CodeTypeDeclaration protocolClass)
        {
            foreach (ParameterGroup group in commandParameterGroup)
            {
                foreach (Parameter parameter in group.Parameters)
                {
                    if (parameter.AutoSize != null)
                    {
                        continue;
                    }

                    // Constant...
                    if (!string.IsNullOrEmpty(parameter.Value))
                    {
                        continue;
                    }

                    StringBuilder stringBuilder = new StringBuilder();
                    if (!string.IsNullOrEmpty(parameter.Description))
                    {
                        OutputWithLineBreak(stringBuilder, "    ", parameter.Description);
                    }

                    CodeFieldReferenceExpression parameterReference = new CodeFieldReferenceExpression(null, $"_{parameter.Name.ToLowerCamelCase()}");
                    if (parameter.Multiple || parameter.Bitfield)
                    {
                        IList<string[]> methodStrings = new List<string[]>
                        {
                            new[] { "Add", "", "", "Add" },
                            new[] { "Remove", "", "", "Remove" },
                            new[] { "Set", "IEnumerable<", ">", "AddRange" },
                        };

                        foreach (string[] methodString in methodStrings)
                        {
                            CodeMemberMethod memberMethod = CreateMethod($"{methodString[0]}{parameter.Name.ToUpperCamelCase()}", new CodeParameterDeclarationExpressionCollection
                            {
                                new CodeParameterDeclarationExpression(new CodeTypeReference(new CodeTypeParameter($"{methodString[1]}{GetTypeClass(parameter.DataType, codeNamespace).BaseType}{methodString[2]}")), $"{parameter.Name.ToLowerCamelCase()}")
                            }, null, null);
                            CodeMethodInvokeExpression invokeExpression = new CodeMethodInvokeExpression(parameterReference, $"{methodString[3]}", new CodeTypeReferenceExpression($"{parameter.Name.ToLowerCamelCase()}"));
                            memberMethod.Statements.Add(invokeExpression);

                            IList<ICodeCommentEntity> codeComment = new List<ICodeCommentEntity>
                            {
                                new CodeCommentEntity
                                {
                                    Tag = CodeCommentTag.Summary,
                                    DocumentationText = $"The {parameter.Name.ToLowerCamelCase()} to {methodString[0].ToLower()} to the set as"
                                }
                            };

                            ICodeCommentEntity temp = new CodeCommentEntity
                            {
                                Tag = CodeCommentTag.See,
                            };
                            temp.Attributes.Add(CodeCommentAttribute.Cref, parameter.DataType);
                            codeComment.Add(temp);
                            AddCodeComment(memberMethod, codeComment, true);

                            protocolClass.Members.Add(memberMethod);
                        }
                    }
                    else
                    {
                        CodeMemberMethod setMethod = CreateMethod($"Set{parameter.Name.ToUpperCamelCase()}", new CodeParameterDeclarationExpressionCollection
                        {
                            new CodeParameterDeclarationExpression(GetTypeClass(parameter.DataType, codeNamespace), $"{parameter.Name.ToLowerCamelCase()}"),
                        }, null, null);

                        IList<ICodeCommentEntity> codeComment = new List<ICodeCommentEntity>
                            {
                                new CodeCommentEntity
                                {
                                    Tag = CodeCommentTag.Summary,
                                    DocumentationText = $"The {parameter.Name.ToLowerCamelCase()} to set as",
                                }
                            };

                        ICodeCommentEntity temp = new CodeCommentEntity
                        {
                            Tag = CodeCommentTag.See,
                        };
                        temp.Attributes.Add(CodeCommentAttribute.Cref, parameter.DataType);
                        codeComment.Add(temp);
                        AddCodeComment(setMethod, codeComment, true);


                        setMethod.Statements.Add(new CodeAssignStatement(parameterReference, new CodeArgumentReferenceExpression($"{parameter.Name.ToLowerCamelCase()}")));

                        if (parameter.Minimum != null && parameter.Maximum != null)
                        {
                            setMethod.Statements.Add(CreateConditionStatement(parameter, $"The value passed is out of range. Range is {parameter.Minimum} to {parameter.Maximum}"));
                        }

                        else if (parameter.Minimum != null)
                        {
                            setMethod.Statements.Add(CreateConditionStatement(parameter, $"The value passed is out of range. Value must be greater than {parameter.Minimum}"));
                        }

                        else if (parameter.Maximum != null)
                        {
                            setMethod.Statements.Add(CreateConditionStatement(parameter, $"The value passed is out of range. Value must be less than {parameter.Maximum}"));
                        }
                        protocolClass.Members.Add(setMethod);
                    }
                }
            }
        }

        /// <summary>
        /// Method for creating a if-condition statement.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        private CodeConditionStatement CreateConditionStatement(Parameter parameter, string message)
        {
            return new CodeConditionStatement(
                new CodeSnippetExpression($"{parameter.Name.ToLowerCamelCase()} < {parameter.Minimum} || {parameter.Name.ToLowerCamelCase()} > {parameter.Maximum}"),
                new CodeStatement[] { new CodeThrowExceptionStatement(new CodeObjectCreateExpression(new CodeTypeReference(typeof(ArgumentOutOfRangeException)),
                                new CodePrimitiveExpression(message))) });
        }

        private void CreateParameterGroups(CodeNamespace codeNamespace, CodeTypeDeclaration protocolClass, IList<ParameterGroup> parameterGroups, Action<ParameterGroup, StringBuilder> action)
        {
            foreach (ParameterGroup group in parameterGroups)
            {
                foreach (var parameter in group.Parameters)
                {
                    if (parameter.AutoSize != null)
                    {
                        continue;
                    }

                    // Constant...
                    if (!string.IsNullOrEmpty(parameter.Value))
                    {
                        continue;
                    }

                    StringBuilder stringBuilder = new StringBuilder();

                    action?.Invoke(group, stringBuilder);

                    if (!string.IsNullOrEmpty(parameter.Description))
                    {
                        OutputWithLineBreak(stringBuilder, "    ", parameter.Description);
                    }
                    CreateParameterDefinition(codeNamespace, protocolClass, stringBuilder, parameter);
                }
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
            if (parameter.Multiple || parameter.Bitfield)
            {
                AddNamespaceImport(codeNamespace, "System.Collections.Generic");
                codeMemberField = CreateCodeMemberField(parameter.Name, new CodeTypeReference($"List<{parameterType.BaseType}>"), MemberAttributes.Private, true);
            }
            else
            {
                codeMemberField = CreateCodeMemberField(parameter.Name, parameterType, MemberAttributes.Private, false);

                // Todo: Test if the assignment works
                if (!string.IsNullOrEmpty(parameter.DefaultValue))
                {
                    CodeAssignStatement assignStatement = CreateCodeAssignStatement(parameter.Name, parameter.DefaultValue);
                }
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
                Name = $"_{memberName.ToLowerCamelCase()}",
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

        private static void GenerateCode(CodeCompileUnit codeCompileUnit, string sourceFile, bool isProtocolClass)
        {
            CodeDomProvider provider = CodeDomProvider.CreateProvider("CSharp");

            string protocolFolder = string.Empty;
            if (isProtocolClass)
            {
                protocolFolder = @"Protocol\";
            }


            if (provider.FileExtension[0] == '.')
            {
                sourceFile = $@"..\..\..\..\ZigBeeNet.Hardware.Digi.XBee\Internal\{protocolFolder}{sourceFile}{provider.FileExtension}";
            }
            else
            {
                sourceFile = $@"..\..\..\..\ZigBeeNet.Hardware.Digi.XBee\Internal\{protocolFolder}{sourceFile}.{provider.FileExtension}";
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
            switch (dataType)
            {
                case "uint8[]":
                    {
                        return new CodeTypeReference(typeof(int[]));
                    }
                case "Data":
                    {
                        return new CodeTypeReference(typeof(int[]));
                    }
                case "uint16[]":
                    {
                        return new CodeTypeReference(typeof(int[]));
                    }
                case "uint8":
                case "uint16":
                case "Integer":
                    {
                        return new CodeTypeReference(typeof(int));
                    }
                case "Boolean":
                    {
                        return new CodeTypeReference(typeof(bool));
                    }
                case "AtCommand":
                    {
                        return new CodeTypeReference(typeof(string));
                    }
                case "String":
                    {
                        return new CodeTypeReference(typeof(string));
                    }
                case "ZigBeeKey":
                    {
                        if (codeNamespace != null)
                        {
                            AddNamespaceImport(codeNamespace, ($"{_zigbeeSecurityPackage}"));
                        }
                        return new CodeTypeReference("ZigBeeKey");
                    }
                case "IeeeAddress":
                    {
                        return new CodeTypeReference("IeeeAddress");
                    }
                case "ExtendedPanId":
                    {
                        return new CodeTypeReference("ExtendedPanId");
                    }
                case "ZigBeeDeviceAddress":
                    {
                        return new CodeTypeReference("ZigBeeDeviceAddress");
                    }
                case "ZigBeeGroupAddress":
                    {
                        return new CodeTypeReference("ZigBeeGroupAddress");
                    }
                default:
                    {
                        //AddNamespaceImport(codeNamespace, ($"{_enumPackage}.{dataType}"));
                        return new CodeTypeReference(dataType);
                    }
            }
        }

        private string GetTypeSerializer(string dataType)
        {
            switch (dataType)
            {
                case "uint8[]":
                case "Data":
                    return "Data";
                case "uint16[]":
                    return "Int16Array";
                case "uint8":
                    return "Int8";
                case "uint16":
                    return "Int16";
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