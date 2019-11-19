using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Xml.Linq;

namespace ZigBeeNet.CodeGenerator.Xml
{
    public class ZigBeeXmlParser
    {
        private List<string> files = new List<string>();

        public ZigBeeXmlParser()
        {
        }

        public void AddFile(string filename)
        {
            files.Add(filename);
        }

        public List<ZigBeeXmlCluster> ParseClusterConfiguration()
        {
            List<ZigBeeXmlCluster> clusters = new List<ZigBeeXmlCluster>();

            try
            {
                foreach (string file in files)
                {
                    Console.WriteLine("Parsing cluster file: " + file);
                    // Load the class definitions
                    //File fXmlFile = new File(file);

                    XDocument doc = XDocument.Load(new FileStream(file, FileMode.Open));

                    //DocumentBuilderFactory dbFactory = DocumentBuilderFactory.newInstance();
                    //DocumentBuilder dBuilder = dbFactory.newDocumentBuilder();
                    //Document doc = dBuilder.parse(fXmlFile);
                    //doc.getDocumentElement().normalize();

                    // Get all cluster specific definitions
                    var nList = doc.Elements("cluster"); //.getElementsByTagName("cluster");
                    ZigBeeXmlCluster cluster = (ZigBeeXmlCluster)ProcessNode(nList.ElementAt(0));
                    clusters.Add(cluster);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }

            return clusters;
        }

        public ZigBeeXmlGlobal ParseGlobalConfiguration()
        {
            ZigBeeXmlGlobal globals = new ZigBeeXmlGlobal();
            globals.Constants = new List<ZigBeeXmlConstant>();

            try
            {
                foreach (string file in files)
                {
                    Console.WriteLine("Parsing globals file: " + file);
                    // Load the class definitions
                    //FileStream fXmlFile = new FileStream(file, FileMode.Open);
                    XDocument doc = XDocument.Load(new FileStream(file, FileMode.Open));

                    //DocumentBuilderFactory dbFactory = DocumentBuilderFactory.newInstance();
                    //DocumentBuilder dBuilder = dbFactory.newDocumentBuilder();
                    //Document doc = dBuilder.parse(fXmlFile);
                    //doc.getDocumentElement().normalize();

                    // Get all global specific definitions
                    var nList = doc.Elements("zigbee");
                    ZigBeeXmlGlobal global = (ZigBeeXmlGlobal)ProcessNode(nList.ElementAt(0));
                    globals.Constants.AddRange(global.Constants);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }

            return globals;
        }

        object ProcessNode(XElement node)
        {
            var nodes = node.Elements();
            XElement e;

            switch (node.Name.LocalName)
            {
                case "zigbee":
                    ZigBeeXmlGlobal global = new ZigBeeXmlGlobal();
                    global.Constants = new List<ZigBeeXmlConstant>();
                    for (int temp = 0; temp < nodes.Count(); temp++)
                    {
                        if (nodes.ElementAt(temp).Name.LocalName.Equals("constant"))
                        {
                            global.Constants.Add((ZigBeeXmlConstant)ProcessNode(nodes.ElementAt(temp)));
                        }
                    }
                    Console.WriteLine("Done: Global");
                    return global;

                case "cluster":
                    ZigBeeXmlCluster cluster = new ZigBeeXmlCluster();
                    e = node;
                    cluster.Code = (int)GetInteger(e.Attribute("code").Value).Value;

                    cluster.Description = new List<ZigBeeXmlDescription>();
                    cluster.Commands = new List<ZigBeeXmlCommand>();
                    cluster.Attributes = new List<ZigBeeXmlAttribute>();
                    cluster.Constants = new List<ZigBeeXmlConstant>();
                    cluster.Structures = new List<ZigBeeXmlStructure>();

                    for (int temp = 0; temp < nodes.Count(); temp++)
                    {
                        if (nodes.ElementAt(temp).Name.LocalName.Equals("name"))
                        {
                            cluster.Name = nodes.ElementAt(temp).Value.Trim();
                        }
                        if (nodes.ElementAt(temp).Name.LocalName.Equals("description"))
                        {
                            cluster.Description.Add((ZigBeeXmlDescription)ProcessNode(nodes.ElementAt(temp)));
                        }
                        if (nodes.ElementAt(temp).Name.LocalName.Equals("command"))
                        {
                            cluster.Commands.Add((ZigBeeXmlCommand)ProcessNode(nodes.ElementAt(temp)));
                        }
                        if (nodes.ElementAt(temp).Name.LocalName.Equals("attribute"))
                        {
                            cluster.Attributes.Add((ZigBeeXmlAttribute)ProcessNode(nodes.ElementAt(temp)));
                        }
                        if (nodes.ElementAt(temp).Name.LocalName.Equals("constant"))
                        {
                            cluster.Constants.Add((ZigBeeXmlConstant)ProcessNode(nodes.ElementAt(temp)));
                        }
                        if (nodes.ElementAt(temp).Name.LocalName.Equals("struct"))
                        {
                            cluster.Structures.Add((ZigBeeXmlStructure)ProcessNode(nodes.ElementAt(temp)));
                        }
                    }

                    Console.WriteLine("Done: Cluster - " + cluster.Name);
                    return cluster;

                case "attribute":
                    ZigBeeXmlAttribute attribute = new ZigBeeXmlAttribute();
                    attribute.Description = new List<ZigBeeXmlDescription>();
                    e = node;

                    attribute.Code = (int)GetInteger(e.Attribute("code").Value).Value;
                    attribute.Side = e.Attribute("side").Value;
                    attribute.Writable = bool.Parse(e.Attribute("writable").Value);
                    attribute.Reportable = bool.Parse(e.Attribute("reportable").Value);
                    attribute.Optional = bool.Parse(e.Attribute("optional").Value);
                    attribute.Type = e.Attribute("type").Value.Trim();
                    attribute.ImplementationClass = e.Attribute("class")?.Value.Trim();
                    attribute.MinimumValue = GetInteger(e.Attribute("minimum")?.Value);
                    attribute.MaximumValue = GetInteger(e.Attribute("maximum")?.Value);
                    attribute.DefaultValue = GetInteger(e.Attribute("default")?.Value);

                    if (GetInteger(e.Attribute("arraycount")?.Value) != null)
                    {
                        attribute.ArrayCount = (int)GetInteger(e.Attribute("arraycount").Value);
                    }
                    if (GetInteger(e.Attribute("arraystart")?.Value) != null)
                    {
                        attribute.ArrayStart = (int)GetInteger(e.Attribute("arraystart").Value);
                    }
                    if (GetInteger(e.Attribute("arraystep")?.Value) != null)
                    {
                        attribute.ArrayStep = (int)GetInteger(e.Attribute("arraystep").Value);
                    }

                    for (int temp = 0; temp < nodes.Count(); temp++)
                    {
                        if (nodes.ElementAt(temp).Name.LocalName.Equals("name"))
                        {
                            attribute.Name = nodes.ElementAt(temp).Value.Trim();
                        }
                        if (nodes.ElementAt(temp).Name.LocalName.Equals("description"))
                        {
                            attribute.Description.Add((ZigBeeXmlDescription)ProcessNode(nodes.ElementAt(temp)));
                        }
                    }

                    return attribute;

                case "command":
                    ZigBeeXmlCommand command = new ZigBeeXmlCommand();
                    command.Fields = new List<ZigBeeXmlField>();
                    command.Description = new List<ZigBeeXmlDescription>();

                    e = node;

                    command.Name = e.Element("name").Value.Trim();
                    command.Code = (int)GetInteger(e.Attribute("code").Value);
                    command.Source = e.Attribute("source").Value;

                    for (int temp = 0; temp < nodes.Count(); temp++)
                    {
                        if (nodes.ElementAt(temp).Name.LocalName.Equals("name"))
                        {
                            command.Name = nodes.ElementAt(temp).Value.Trim();
                        }
                        if (nodes.ElementAt(temp).Name.LocalName.Equals("description"))
                        {
                            command.Description.Add((ZigBeeXmlDescription)ProcessNode(nodes.ElementAt(temp)));
                        }
                        if (nodes.ElementAt(temp).Name.LocalName.Equals("field"))
                        {
                            command.Fields.Add((ZigBeeXmlField)ProcessNode(nodes.ElementAt(temp)));
                        }
                        if (nodes.ElementAt(temp).Name.LocalName.Equals("response"))
                        {
                            command.Response = (ZigBeeXmlResponse)ProcessNode(nodes.ElementAt(temp));
                        }
                    }
                    Console.WriteLine("Done: Command - " + command.Name);
                    return command;

                case "field":
                    ZigBeeXmlField field = new ZigBeeXmlField();
                    field.Description = new List<ZigBeeXmlDescription>();

                    e = node;
                    if (e.Attribute("completeOnZero")?.Value.Length > 0)
                    {
                        string x = e.Attribute("completeOnZero").Value;
                        Console.WriteLine(x);
                    }
                    field.Type = e.Attribute("type")?.Value.Trim();
                    field.CompleteOnZero = "true".Equals(e.Attribute("completeOnZero")?.Value.Trim());
                    field.ImplementationClass = e.Attribute("class")?.Value.Trim();
                    field.Array = e.Attribute("array") != null ? bool.Parse(e.Attribute("array").Value) : false;
                    for (int temp = 0; temp < nodes.Count(); temp++)
                    {
                        if (nodes.ElementAt(temp).Name.LocalName.Equals("name"))
                        {
                            field.Name = nodes.ElementAt(temp).Value.Trim();
                        }
                        if (nodes.ElementAt(temp).Name.LocalName.Equals("sizer"))
                        {
                            field.Sizer = nodes.ElementAt(temp).Value.Trim();
                        }
                        if (nodes.ElementAt(temp).Name.LocalName.Equals("description"))
                        {
                            field.Description.Add((ZigBeeXmlDescription)ProcessNode(nodes.ElementAt(temp)));
                        }
                        if (nodes.ElementAt(temp).Name.LocalName.Equals("conditional"))
                        {
                            field.Condition = (ZigBeeXmlCondition)ProcessNode(nodes.ElementAt(temp));
                        }
                    }
                    Console.WriteLine("Done: Field - " + field.Name);
                    return field;

                case "constant":
                    ZigBeeXmlConstant constant = new ZigBeeXmlConstant();
                    constant.Description = new List<ZigBeeXmlDescription>();

                    e = node;

                    constant.ClassName = e.Attribute("class").Value.Trim();

                    constant.Values = new Dictionary<BigInteger, string>();

                    for (int temp = 0; temp < nodes.Count(); temp++)
                    {
                        if (nodes.ElementAt(temp).Name.LocalName.Equals("name"))
                        {
                            constant.Name = nodes.ElementAt(temp).Value.Trim();
                        }
                        if (nodes.ElementAt(temp).Name.LocalName.Equals("description"))
                        {
                            constant.Description.Add((ZigBeeXmlDescription)ProcessNode(nodes.ElementAt(temp)));
                        }
                        if (nodes.ElementAt(temp).Name.LocalName.Equals("value"))
                        {
                            e = nodes.ElementAt(temp);
                            string name = e.Attribute("name").Value.Trim();
                            BigInteger value = GetInteger(e.Attribute("code").Value.Trim()).Value;
                            constant.Values.Add(value, name);
                        }
                    }

                    return constant;

                case "struct":
                    ZigBeeXmlStructure structure = new ZigBeeXmlStructure();
                    structure.Fields = new List<ZigBeeXmlField>();
                    structure.Description = new List<ZigBeeXmlDescription>();

                    e = node;

                    structure.Name = e.Attribute("name")?.Value.Trim();
                    structure.ClassName = e.Attribute("class")?.Value.Trim();

                    for (int temp = 0; temp < nodes.Count(); temp++)
                    {
                        if (nodes.ElementAt(temp).Name.LocalName.Equals("name"))
                        {
                            structure.Name = nodes.ElementAt(temp).Value.Trim();
                        }
                        if (nodes.ElementAt(temp).Name.LocalName.Equals("description"))
                        {
                            structure.Description.Add((ZigBeeXmlDescription)ProcessNode(nodes.ElementAt(temp)));
                        }
                        if (nodes.ElementAt(temp).Name.LocalName.Equals("field"))
                        {
                            structure.Fields.Add((ZigBeeXmlField)ProcessNode(nodes.ElementAt(temp)));
                        }
                    }
                    Console.WriteLine("Done: Structure - " + structure.Name);
                    return structure;

                case "description":
                    ZigBeeXmlDescription description = new ZigBeeXmlDescription();

                    e = node;

                    // TODO: use string.IsNullOrEmpty() instead ?
                    //if (nodes.ElementAt(0) != null && nodes.ElementAt(0).Value != null)
                    if (e != null)
                    {
                        //description.Description = nodes.ElementAt(0).Value.Trim();
                        description.Description = e.Value.Trim();
                    }
                    if (e.Attribute("format") != null)
                    {
                        description.Format = e.Attribute("format").Value.Trim();
                    }

                    return description;

                case "conditional":
                    ZigBeeXmlCondition condition = new ZigBeeXmlCondition();

                    e = node;
                    for (int temp = 0; temp < nodes.Count(); temp++)
                    {
                        if (nodes.ElementAt(temp).Name.LocalName.Equals("field"))
                        {
                            condition.Field = nodes.ElementAt(temp).Value.Trim();
                        }
                        if (nodes.ElementAt(temp).Name.LocalName.Equals("operator"))
                        {
                            condition.Operator = nodes.ElementAt(temp).Value.Trim();
                        }
                        if (nodes.ElementAt(temp).Name.LocalName.Equals("value"))
                        {
                            condition.Value = nodes.ElementAt(temp).Value.Trim();
                        }
                    }
                    Console.WriteLine("Done: Condition - " + condition.Field);
                    return condition;

                case "response":
                    ZigBeeXmlResponse response = new ZigBeeXmlResponse();
                    response.Matchers = new List<ZigBeeXmlMatcher>();

                    e = node;
                    response.Command = e.Attribute("command").Value.Trim();

                    for (int temp = 0; temp < nodes.Count(); temp++)
                    {
                        if (nodes.ElementAt(temp).Name.LocalName.Equals("matcher"))
                        {
                            response.Matchers.Add((ZigBeeXmlMatcher)ProcessNode(nodes.ElementAt(temp)));
                        }
                    }
                    Console.WriteLine("Done: Response - " + response.Command);
                    return response;

                case "matcher":
                    ZigBeeXmlMatcher matcher = new ZigBeeXmlMatcher();

                    e = node;
                    matcher.CommandField = e.Attribute("commandField").Value.Trim();
                    matcher.ResponseField = e.Attribute("responseField").Value.Trim();

                    Console.WriteLine("Done: Matcher - " + matcher.CommandField);
                    return matcher;
            }

            return null;
        }

        private static BigInteger? GetInteger(string value)
        {
            if (value == null || value.Length == 0)
            {
                return null;
            }

            string lwrValue = value.ToLower();
            try
            {
                if (lwrValue.StartsWith("0x"))
                {
                    var tmp = lwrValue.Substring(2);

                    // If value is a hexadecimal string, the Parse(String, NumberStyles) method interprets value as a negative number stored by using two's complement representation 
                    // if its first two hexadecimal digits are greater than or equal to 0x80. 
                    // In other words, the method interprets the highest-order bit of the first byte in value as the sign bit. To make sure that a hexadecimal string is correctly interpreted as a positive number, the first digit in value must have a value of zero. For example, the method interprets 0x80 as a negative value, but it interprets either 0x080 or 0x0080 as a positive value
                    // http://msdn.microsoft.com/en-us/library/dd268285.aspx

                    if (tmp.StartsWith("-"))
                    {
                        var unSigned = BigInteger.Parse("0" + tmp.Substring(1), System.Globalization.NumberStyles.HexNumber);
                        return BigInteger.Negate(unSigned);
                    }

                    return BigInteger.Parse("0" + tmp, System.Globalization.NumberStyles.HexNumber);
                }
                else
                {
                    return BigInteger.Parse(lwrValue);
                }
            }
            catch (FormatException e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
    }
}