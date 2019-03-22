using System.Collections.Generic;
using System.Xml.Serialization;

namespace ZigBeeNet.Digi.XBee.CodeGenerator.Xml
{
    public class Command
    {
        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("command")]
        public string CommandProperty { get; set; }

        [XmlElement("id")]
        public int Id { get; set; }

        [XmlElement("description")]
        public string Description { get; set; }

        [XmlElement("command_parameters")]
        public List<ParameterGroup> CommandParameters { get; set; }

        [XmlElement("response_parameters")]
        public List<ParameterGroup> ResponseParameters { get; set; }

        [XmlElement("getter")]
        public bool Getter { get; set; }

        [XmlElement("setter")]
        public bool Setter { get; set; }
    }
}
