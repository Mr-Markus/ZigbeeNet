using System;
using System.Collections.Generic;

namespace ZigBeeNet.CodeGenerator.Xml
{
    public class ZigBeeXmlCommand
    {
        public int Code { get; set; }
        public string Source { get; set; }
        public string Name { get; set; }
        public List<ZigBeeXmlDescription> Description { get; set; }
        public List<ZigBeeXmlField> Fields { get; set; }
        public ZigBeeXmlResponse Response { get; set; }
    }
}