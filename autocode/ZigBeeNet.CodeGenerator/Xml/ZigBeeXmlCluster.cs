using System;
using System.Collections.Generic;

namespace ZigBeeNet.CodeGenerator.Xml
{
    public class ZigBeeXmlCluster
    {
        public string Name { get; set; }
        public int Code { get; set; }
        public List<ZigBeeXmlDescription> Description { get; set; }
        public List<ZigBeeXmlCommand> Commands { get; set; }
        public List<ZigBeeXmlAttribute> Attributes { get; set; }
        public List<ZigBeeXmlConstant> Constants { get; set; }
        public List<ZigBeeXmlStructure> Structures { get; set; }
    }
}