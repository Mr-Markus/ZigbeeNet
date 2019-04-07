using System.Collections.Generic;
using System.Xml.Serialization;

namespace ZigBeeNet.Digi.XBee.CodeGenerator.Xml
{
    public class Enumeration
    {
        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("description")]
        public string Description { get; set; }

        [XmlElement("format")]
        public string Format { get; set; }

        [XmlElement("values")]
        public List<Value> Values { get; set; }
    }
}
