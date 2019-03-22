using System.Collections.Generic;
using System.Xml.Serialization;

namespace ZigBeeNet.Digi.XBee.CodeGenerator.Xml
{
    public class ParameterGroup
    {
        [XmlElement("multiple")]
        public bool Multiple { get; set; }

        [XmlElement("parameters")]
        public List<Parameter> Parameters { get; set; }

        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("required")]
        public bool Required { get; set; }

        [XmlElement("complete")]
        public bool Complete { get; set; }
    }
}
