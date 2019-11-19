using System;
using System.Collections.Generic;
using System.Numerics;

namespace ZigBeeNet.CodeGenerator.Xml
{
    public class ZigBeeXmlAttribute
    {
        public string Name { get; set; }
        public List<ZigBeeXmlDescription> Description { get; set; }
        public int Code { get; set; }
        public int? ArrayStart { get; set; }
        public int? ArrayCount { get; set; }
        public int? ArrayStep { get; set; }
        public string Type { get; set; }
        public string ImplementationClass { get; set; }
        public string Side { get; set; }
        public bool Optional { get; set; }
        public bool Writable { get; set; }
        public bool Reportable { get; set; }
        public BigInteger? MinimumValue { get; set; }
        public BigInteger? MaximumValue { get; set; }
        public BigInteger? DefaultValue { get; set; }
    }
}