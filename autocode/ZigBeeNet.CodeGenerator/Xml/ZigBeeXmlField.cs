using System;
using System.Collections.Generic;

namespace ZigBeeNet.CodeGenerator.Xml
{
    public class ZigBeeXmlField
    {
        public string Name { get; set; }
        public List<ZigBeeXmlDescription> Description { get; set; }
        public string Type { get; set; }
        public string ImplementationClass { get; set; }
        public bool Array { get; set; }
        public string Sizer { get; set; }
        public ZigBeeXmlCondition Condition { get; set; }
        public bool CompleteOnZero { get; set; }
    }
}