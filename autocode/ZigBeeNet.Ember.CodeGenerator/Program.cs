﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using ZigBeeNet.Ember.CodeGenerator.Xml;

namespace ZigBeeNet.Ember.CodeGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            Protocol protocol;
            try
            {
                XmlReaderSettings settings = new XmlReaderSettings();
                using (StreamReader reader = File.OpenText("./Resources/ezsp_protocol.xml"))
                {
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(reader.ReadToEnd());
                    doc.Normalize();
                    XmlNodeList nList = doc.GetElementsByTagName("protocol");

                    protocol = (Protocol)ProcessNode(nList.Item(0));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                Console.WriteLine(e.Message);
                return;
            }

            Console.WriteLine("Generating code...");

            try
            {
                new CommandGenerator().Go(protocol);
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.StackTrace);
                Console.WriteLine(e.Message);
            }
        }

        private static object ProcessNode(XmlNode node)
        {
            Console.WriteLine($"Current Element: {node.Name}");

            XmlNodeList nodes = node.ChildNodes;

            switch (node.Name)
            {
                case "protocol":
                    {
                        Protocol protocol = new Protocol
                        {
                            Commands = new List<Command>(),
                            Structures = new List<Structure>(),
                            Enumerations = new List<Enumeration>()
                        };

                        for (int i = 0; i < nodes.Count; i++)
                        {
                            switch (nodes[i].Name)
                            {
                                case "command":
                                    {
                                        protocol.Commands.Add((Command)ProcessNode(nodes[i]));
                                    }
                                    break;
                                case "structure":
                                    {
                                        protocol.Structures.Add((Structure)ProcessNode(nodes[i]));
                                    }
                                    break;
                                case "enum":
                                    {
                                        protocol.Enumerations.Add((Enumeration)ProcessNode(nodes[i]));
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }
                        return protocol;
                    }
                case "command":
                    {
                        Command command = new Command
                        {
                            CommandParameters = new List<Parameter>(),
                            ResponseParameters = new List<Parameter>(),
                        };

                        for (int i = 0; i < nodes.Count; i++)
                        {
                            switch (nodes[i].Name)
                            {
                                case "name":
                                    {
                                        command.Name = nodes[i].InnerText;
                                    }
                                    break;
                                case "id":
                                    {
                                        string id = nodes[i].InnerText;
                                        if (id.StartsWith("0x"))
                                        {
                                            command.Id = Convert.ToInt16(id.Substring(2), 16);
                                        }
                                        else
                                        {
                                            command.Id = Convert.ToInt32(id.Substring(2));
                                        }
                                    }
                                    break;
                                case "description":
                                    {
                                        command.Description = nodes[i].InnerText;
                                    }
                                    break;
                                case "command_parameters":
                                    {
                                        command.CommandParameters = (List<Parameter>)ProcessNode(nodes[i]);
                                    }
                                    break;
                                case "response_parameters":
                                    {
                                        command.ResponseParameters = (List<Parameter>)ProcessNode(nodes[i]);
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }
                        Console.WriteLine($"Done: Command - {command.Name}");
                        return command;
                    }
                case "command_parameters":
                case "response_parameters":
                case "parameters":
                    {
                        List<Parameter> parameters = new List<Parameter>();

                        for (int i = 0; i < nodes.Count; i++)
                        {
                            switch (nodes[i].Name)
                            {
                                case "parameter":
                                    {
                                        parameters.Add((Parameter)ProcessNode(nodes[i]));
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }
                        return parameters;
                    }
                case "parameter":
                    {
                        Parameter parameter = new Parameter();
                        for (int i = 0; i < nodes.Count; i++)
                        {
                            switch (nodes[i].Name)
                            {
                                case "data_type":
                                    {
                                        parameter.DataType = nodes[i].InnerText;
                                        parameter.Multiple = nodes[i].Attributes["multiple"]?.Value.Equals("true")??false;
                                    }
                                    break;
                                case "name":
                                    {
                                        parameter.Name = nodes[i].InnerText;
                                    }
                                    break;
                                case "multiple":
                                    {
                                        parameter.Multiple = nodes[i].InnerText.ToLower().Equals("true");
                                    }
                                    break;
                                case "description":
                                    {
                                        parameter.Description = nodes[i].InnerText;
                                    }
                                    break;
                                case "auto_size":
                                    {
                                        parameter.AutoSize = nodes[i].InnerText;
                                    }
                                    break;
                                case "display":
                                    {
                                        string display = nodes[i].InnerText;
                                        if (display.Contains("[") && display.Contains("]"))
                                        {
                                            parameter.DisplayType = display.Substring(0, display.IndexOf("["));
                                            parameter.DisplayLength = Convert.ToInt32(display.Substring(display.IndexOf("[") + 1, display.IndexOf("]") - display.IndexOf("[") - 1));
                                        }
                                        else
                                        {
                                            parameter.DisplayType = display;
                                            parameter.DisplayLength = 0;
                                        }
                            }
                                    break;
                                default:
                                    break;
                            }
                        }
                        Console.WriteLine($"Done: Parameter - {parameter.Name}");
                        return parameter;
                    }
                case "structure":
                    {
                        Structure structure = new Structure
                        {
                            Parameters = new List<Parameter>()
                        };

                        for (int i = 0; i < nodes.Count; i++)
                        {
                            switch (nodes[i].Name)
                            {
                                case "name":
                                    {
                                        structure.Name = nodes[i].InnerText;
                                    }
                                    break;
                                case "description":
                                    {
                                        structure.Description = nodes[i].InnerText;
                                    }
                                    break;
                                case "parameters":
                                    {
                                        structure.Parameters = (List<Parameter>)ProcessNode(nodes[i]);
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }
                        Console.WriteLine($"Done: Structure - {structure.Name}");
                        return structure;
                    }
                case "enum":
                    {
                        Enumeration enumeration = new Enumeration
                        {
                            Values = new List<Value>()
                        };
                        for (int i = 0; i < nodes.Count; i++)
                        {
                            switch (nodes[i].Name)
                            {
                                case "name":
                                    {
                                        enumeration.Name = nodes[i].InnerText;
                                    }
                                    break;
                                case "description":
                                    {
                                        enumeration.Description = nodes[i].InnerText;
                                    }
                                    break;
                                case "values":
                                    {
                                        enumeration.Values = (List<Value>)ProcessNode(nodes[i]);
                                    }
                                    break;
                                case "format":
                                    {
                                        enumeration.Format = nodes[i].InnerText;
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }
                        Console.WriteLine($"Done: Enum - {enumeration.Name}");
                        return enumeration;
                    }
                case "values":
                    {
                        List<Value> values = new List<Value>();

                        for (int i = 0; i < nodes.Count; i++)
                        {
                            switch (nodes[i].Name)
                            {
                                case "value":
                                    {
                                        values.Add((Value)ProcessNode(nodes[i]));
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }
                        return values;
                    }
                case "value":
                    {
                        Value value = new Value();

                        for (int i = 0; i < nodes.Count; i++)
                        {
                            switch (nodes[i].Name)
                            {
                                case "name":
                                    {
                                        value.Name = nodes[i].InnerText;
                                    }
                                    break;
                                case "enum_value":
                                    {
                                        string id = nodes[i].InnerText;
                                        value.EnumValue = GetInteger(id);
                                    }
                                    break;
                                case "description":
                                    {
                                        value.Description = nodes[i].InnerText;
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }
                        Console.WriteLine($"Done: Value - {value.Name}");
                        return value;
                    }
            }
            return null;
        }

        private static int GetInteger(string value)
        {
            if (value.StartsWith("0x"))
            {
                return Convert.ToInt32(value.Substring(2), 16);
            }
            else
            {
                return Convert.ToInt32(value);
            }
        }
    }
}
