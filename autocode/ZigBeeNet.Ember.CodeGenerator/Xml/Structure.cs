using System.Collections.Generic;
using System.Xml.Serialization;

namespace ZigBeeNet.Ember.CodeGenerator.Xml
{
    public class Structure
    {
        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("description")]
        public string Description { get; set; }

        [XmlElement("parameters")]
        public List<Parameter> Parameters { get; set; }
    }
}
