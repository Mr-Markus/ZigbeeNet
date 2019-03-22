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
            CodeTypeDeclaration protocolClass = new CodeTypeDeclaration(className);

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

            foreach (var group in commandParameterGroup)
            {
                foreach (var parameter in group.Parameters)
                {
                    if (parameter.AutoSize != null)
                    {
                        continue;
                    }

                    // Constant...
                    if (parameter.Value != null && parameter.Value.Length != 0)
                    {
                        continue;
                    }

                    if (!string.IsNullOrEmpty(parameter.Description))
                    {
                        StringBuilder parameterStringBuilder = new StringBuilder();
                        OutputWithLineBreak(parameterStringBuilder, "    ", parameter.Description);

                        CreateParameterDefinition(codeNamespace, protocolClass, parameterStringBuilder, parameter);
                    }
                }
            }          

            foreach (ParameterGroup group in responseParameterGroup)
            {
                foreach (Parameter parameter in group.Parameters)
                {
                    if (parameter.AutoSize != null)
                    {
                        continue;
                    }
                    // Constant
                    if (!string.IsNullOrEmpty(parameter.Value))
                    {
                        continue;
                    }

                    StringBuilder stringBuilder = new StringBuilder();
                    stringBuilder.AppendLine("Response field");
                    if (bool.TrueString.Equals(group.Multiple))
                    {
                        stringBuilder.AppendLine("Field accepts multiple responses.");
                    }
                    if (!string.IsNullOrEmpty(parameter.Description))
                    {
                        OutputWithLineBreak(stringBuilder, "    ", parameter.Description);
                    }
                    CreateParameterDefinition(codeNamespace, protocolClass, stringBuilder, parameter);
                }
            }




            GenerateCode(compileUnit, className);
        }

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

        private static void CreateCompileUnit(out CodeCompileUnit compileUnit, out CodeNamespace codeNamespace, string namespaceString)
        {
            compileUnit = new CodeCompileUnit();
            codeNamespace = new CodeNamespace("ZigBeeNet.Hardware.Digi.XBee.Internal.Protocol");
            compileUnit.Namespaces.Add(codeNamespace);
        }

        private static void AddCodeComment(CodeTypeMember codeTypeMember, StringBuilder stringBuilder)
        {
            codeTypeMember.Comments.Add(new CodeCommentStatement("<summary>", true));
            codeTypeMember.Comments.Add(new CodeCommentStatement(stringBuilder.ToString(), true));
            codeTypeMember.Comments.Add(new CodeCommentStatement("</summary>", true));
        }

        private static CodeMemberField CreateCodeMemberField(string memberName, string typeString, MemberAttributes memberAttributes, bool initializeMember)
        {
            CodeMemberField codeMemberField = new CodeMemberField();
            codeMemberField.Name = $"_{memberName.ToLowerCamelCase()}";
            codeMemberField.Type = new CodeTypeReference(new CodeTypeParameter(typeString));

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
                sourceFile = $@"D:\Development\HomeAutomation\Digi_XBee\ZigbeeNet\src\ZigBeeNet.Hardware.Digi.XBee\Internal\Protocol\{sourceFile}{provider.FileExtension}";
            }
            else
            {
                sourceFile = $@"D:\Development\HomeAutomation\Digi_XBee\ZigbeeNet\src\ZigBeeNet.Hardware.Digi.XBee\Internal\Protocol\{sourceFile}.{provider.FileExtension}";
            }

            var codeGeneratorOptions = new CodeGeneratorOptions
            {
                BracingStyle = "C",
            };
            IndentedTextWriter tw = new IndentedTextWriter(new StreamWriter(sourceFile, false), "    ");
            //provider.GenerateCodeFromType(codeTypeDeclaration, tw, codeGeneratorOptions);
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
                        AddNamespaceImport(codeNamespace, ($"{_zigbeeSecurityPackage}.ZigBeeKey"));
                        return new CodeTypeReference("ZigBeeKey");
                    }
                case "IeeeAddress":
                    {
                        AddNamespaceImport(codeNamespace, ($"{_zigbeePackage}.IeeeAddress"));
                        return new CodeTypeReference("IeeeAddress");
                    }
                case "ExtendedPanId":
                    {
                        AddNamespaceImport(codeNamespace, ($"{_zigbeePackage}.ExtendedPanId"));
                        return new CodeTypeReference("ExtendedPanId");
                    }
                case "ZigBeeDeviceAddress":
                    {
                        AddNamespaceImport(codeNamespace, ($"{_zigbeePackage}.ZigBeeDeviceAddress"));
                        return new CodeTypeReference("ZigBeeDeviceAddress");
                    }
                case "ZigBeeGroupAddress":
                    {
                        AddNamespaceImport(codeNamespace, ($"{_zigbeePackage}.ZigBeeGroupAddress"));
                        return new CodeTypeReference("ZigBeeGroupAddress");
                    }
                default:
                    {
                        //AddNamespaceImport(codeNamespace, ($"{_enumPackage}.{dataType}"));
                        return new CodeTypeReference(dataType);
                    }
            }
        }

        private void AddNamespaceImport(CodeNamespace codeNamespace, string namespaceToImport)
        {
            codeNamespace.Imports.Add(new CodeNamespaceImport(namespaceToImport));
        }
    }
}