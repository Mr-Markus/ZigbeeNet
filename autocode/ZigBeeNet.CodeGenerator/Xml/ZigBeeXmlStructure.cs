using System;
using System.Collections.Generic;

namespace ZigBeeNet.CodeGenerator.Xml
{
    public class ZigBeeXmlStructure
    {
        public string Name { get; set; }
        public string ClassName { get; set; }
        public List<ZigBeeXmlDescription> Description { get; set; }
        public List<ZigBeeXmlField> Fields { get; set; }
    }
}