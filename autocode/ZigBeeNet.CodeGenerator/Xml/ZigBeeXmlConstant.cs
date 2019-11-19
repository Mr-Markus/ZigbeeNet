using System;
using System.Collections.Generic;
using System.Numerics;

namespace ZigBeeNet.CodeGenerator.Xml
{
    public class ZigBeeXmlConstant
    {
        public string Name { get; set; }
        public string ClassName { get; set; }
        public List<ZigBeeXmlDescription> Description { get; set; }
        public Dictionary<BigInteger, string> Values { get; set; }
    }
}