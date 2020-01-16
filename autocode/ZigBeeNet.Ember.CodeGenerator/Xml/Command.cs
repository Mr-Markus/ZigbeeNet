using System.Collections.Generic;
using System.Xml.Serialization;

namespace ZigBeeNet.Ember.CodeGenerator.Xml
{
    public class Command
    {
        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("id")]
        public int Id { get; set; }

        [XmlElement("description")]
        public string Description { get; set; }

        [XmlElement("command_parameters")]
        public List<Parameter> CommandParameters { get; set; }

        [XmlElement("response_parameters")]
        public List<Parameter> ResponseParameters { get; set; }

    }
}
