using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ZigBeeNet.Digi.XBee.CodeGenerator.Extensions;
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
                    Value = '"' + atCommand.CommandProperty + '"'
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

        private void CreateEventFactory(string v, Protocol protocol)
        {
            throw new NotImplementedException();
        }

        private void CreateEnumClass(Enumeration enumeration)
        {
            throw new NotImplementedException();
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

            Console.WriteLine("Processing command class " + command.Name + "  [" + className + "()]");

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
            descriptionStringBuilder.AppendLine("This class provides methods for processing XBee API commands.");
            AddCodeComment(protocolClass, descriptionStringBuilder);

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

            // CreateSerializerMethods
            if (commandParameterGroup != null && commandParameterGroup.Count != 0)
            {
                CodeMemberMethod serializerMethod = CreateMethod("Serialize", null, new CodeTypeReference(typeof(int[])), null);
                AddCodeComment(serializerMethod, new StringBuilder().AppendLine("Method for serializing the command fields"));

                CodeConditionStatement codeConditionStatement = null;
                foreach (ParameterGroup group in commandParameterGroup)
                {
                    // TODO: Check if the command id is really needed as hex representation. Int is used for now.
                    CodeMethodInvokeExpression invokeExpression = new CodeMethodInvokeExpression(new CodeThisReferenceExpression(), "SerializeCommand", new CodePrimitiveExpression(command.Id /*+ command.Id.ToString("X2")*/));
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

                            CodeMethodInvokeExpression invokeSerializeExpression = new CodeMethodInvokeExpression(new CodeThisReferenceExpression(), $"Serialize{GetTypeSerializer(parameter.DataType)}", codePrimitiveExpression);
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
                            CodeMethodInvokeExpression invokeSerializeExpression = new CodeMethodInvokeExpression(new CodeThisReferenceExpression(), $"Serialize{GetTypeSerializer(parameter.DataType)}", new CodeTypeReferenceExpression($"_{parameter.AutoSize.ToLowerCamelCase()}.Length"));
                            serializerMethod.Statements.Add(invokeSerializeExpression);
                            continue;
                        }
                    }
                }
                CodeMethodReturnStatement codeMethodReturnStatement = new CodeMethodReturnStatement(new CodeMethodInvokeExpression(new CodeThisReferenceExpression(), "GetPayload"));
                serializerMethod.Statements.Add(codeMethodReturnStatement);
                protocolClass.Members.Add(serializerMethod);
            }

            // Go on with line 316

            GenerateCode(compileUnit, className);
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

                    stringBuilder.Clear();
                    if (parameter.Multiple)
                    {
                        stringBuilder.Append($"Return the {parameter.Name.ToLowerCamelCase()} as a list of <see cref=\"{GetTypeClass(parameter.DataType, null).BaseType}\"/>");
                    }
                    else
                    {
                        stringBuilder.Append($"Return the {parameter.Name.ToLowerCamelCase()} as <see cref=\"{GetTypeClass(parameter.DataType, null).BaseType}\"/>");
                    }

                    CodeMemberMethod getMethod;
                    if (parameter.Multiple || parameter.Bitfield)
                    {
                        getMethod = CreateMethod($"Get{parameter.Name.ToUpperCamelCase()}", null, new CodeTypeReference($"List<{GetTypeClass(parameter.DataType, codeNamespace).BaseType}>"), null);
                    }
                    else
                    {
                        getMethod = CreateMethod($"Get{parameter.Name.ToUpperCamelCase()}", null, new CodeTypeReference($"{GetTypeClass(parameter.DataType, codeNamespace).BaseType}"), null);
                    }

                    // TODO: Test if this is appropriate. Especially if the return type is a list.
                    getMethod.Statements.Add(new CodeMethodReturnStatement(new CodeArgumentReferenceExpression($"_{parameter.Name.ToLowerCamelCase()}")));
                    AddCodeComment(getMethod, stringBuilder);
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

                    CodeFieldReferenceExpression parameterReference = new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), $"_{parameter.Name.ToLowerCamelCase()}");
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
                            AddCodeComment(memberMethod, new StringBuilder($"The {parameter.Name.ToLowerCamelCase()} to {methodString[0].ToLower()} to the set as <see cref=\"{parameter.DataType}\"/>"));
                            protocolClass.Members.Add(memberMethod);
                        }
                    }
                    else
                    {
                        CodeMemberMethod setMethod = CreateMethod($"Set{parameter.Name.ToUpperCamelCase()}", new CodeParameterDeclarationExpressionCollection
                        {
                            new CodeParameterDeclarationExpression(new CodeTypeReference(new CodeTypeParameter($"{GetTypeClass(parameter.DataType, codeNamespace).BaseType}")), $"{parameter.Name.ToLowerCamelCase()}"),
                        }, null, null);
                        AddCodeComment(setMethod, new StringBuilder($"The {parameter.Name.ToLowerCamelCase()} to set as <see cref=\"{parameter.DataType}\"/>"));
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
                codeMemberField = CreateCodeMemberField(parameter.Name, $"List<{parameterType.BaseType}>", MemberAttributes.Private, true);
            }
            else
            {
                codeMemberField = CreateCodeMemberField(parameter.Name, parameterType.BaseType, MemberAttributes.Private, false);

                // Todo: Test if the assignment works
                if (!string.IsNullOrEmpty(parameter.DefaultValue))
                {
                    CodeAssignStatement assignStatement = CreateCodeAssignStatement(parameter.Name, parameter.DefaultValue);
                }
            }
            AddCodeComment(codeMemberField, codeComment);

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
            AddCodeComment(codeMemberProperty, propertyComments);
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

        private static void CreateCompileUnit(out CodeCompileUnit compileUnit, out CodeNamespace codeNamespace, string namespaceString)
        {
            compileUnit = new CodeCompileUnit();
            codeNamespace = new CodeNamespace("ZigBeeNet.Hardware.Digi.XBee.Internal.Protocol");
            compileUnit.Namespaces.Add(codeNamespace);
        }

        /// <summary>
        /// Method for adding a comment to a code type member.
        /// </summary>
        /// <param name="codeTypeMember">The code type member.</param>
        /// <param name="stringBuilder">The string builder.</param>
        private static void AddCodeComment(CodeTypeMember codeTypeMember, StringBuilder stringBuilder)
        {
            codeTypeMember.Comments.Add(new CodeCommentStatement("<summary>", true));
            codeTypeMember.Comments.Add(new CodeCommentStatement(stringBuilder.ToString(), true));
            codeTypeMember.Comments.Add(new CodeCommentStatement("</summary>", true));
        }

        /// <summary>
        /// Method for creating a member variable.
        /// </summary>
        /// <param name="memberName">Name of the member.</param>
        /// <param name="typeString">The type string.</param>
        /// <param name="memberAttributes">The member attributes.</param>
        /// <param name="initializeMember">if set to <c>true</c> [initialize member].</param>
        /// <returns></returns>
        private static CodeMemberField CreateCodeMemberField(string memberName, string typeString, MemberAttributes memberAttributes, bool initializeMember)
        {
            CodeMemberField codeMemberField = new CodeMemberField
            {
                Name = $"_{memberName.ToLowerCamelCase()}",
                Type = new CodeTypeReference(new CodeTypeParameter(typeString))
            };

            if (initializeMember)
            {
                codeMemberField.InitExpression = new CodeObjectCreateExpression(typeString, new CodeExpression[] { });
            }
            codeMemberField.Attributes = MemberAttributes.Private;

            return codeMemberField;
        }

        private static CodeAssignStatement CreateCodeAssignStatement(string parameterName, object parameterValue)
        {
            return new CodeAssignStatement(new CodeVariableReferenceExpression($"_{parameterName.ToLowerCamelCase()}"), new CodePrimitiveExpression(parameterValue));
        }

        private static void GenerateCode(CodeCompileUnit codeCompileUnit, string sourceFile)
        {
            CodeDomProvider provider = CodeDomProvider.CreateProvider("CSharp");

            if (provider.FileExtension[0] == '.')
            {
                sourceFile = $@"..\..\..\..\src\ZigBeeNet.Hardware.Digi.XBee\Internal\Protocol\{sourceFile}{provider.FileExtension}";
            }
            else
            {
                sourceFile = $@"..\..\..\..\src\ZigBeeNet.Hardware.Digi.XBee\Internal\Protocol\{sourceFile}.{provider.FileExtension}";
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
                            AddNamespaceImport(codeNamespace, ($"{_zigbeeSecurityPackage}.ZigBeeKey"));
                        }
                        return new CodeTypeReference("ZigBeeKey");
                    }
                case "IeeeAddress":
                    {
                        if (codeNamespace != null)
                        {
                            AddNamespaceImport(codeNamespace, ($"{_zigbeePackage}.IeeeAddress"));
                        }
                        return new CodeTypeReference("IeeeAddress");
                    }
                case "ExtendedPanId":
                    {
                        if (codeNamespace != null)
                        {
                            AddNamespaceImport(codeNamespace, ($"{_zigbeePackage}.ExtendedPanId"));
                        }
                        return new CodeTypeReference("ExtendedPanId");
                    }
                case "ZigBeeDeviceAddress":
                    {
                        if (codeNamespace != null)
                        {
                            AddNamespaceImport(codeNamespace, ($"{_zigbeePackage}.ZigBeeDeviceAddress"));
                        }
                        return new CodeTypeReference("ZigBeeDeviceAddress");
                    }
                case "ZigBeeGroupAddress":
                    {
                        if (codeNamespace != null)
                        {
                            AddNamespaceImport(codeNamespace, ($"{_zigbeePackage}.ZigBeeGroupAddress"));
                        }
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
                    return dataType.ToUpper();
            }
        }

        private void AddNamespaceImport(CodeNamespace codeNamespace, string namespaceToImport)
        {
            codeNamespace.Imports.Add(new CodeNamespaceImport(namespaceToImport));
        }
    }
}