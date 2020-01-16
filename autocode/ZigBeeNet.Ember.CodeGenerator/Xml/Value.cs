using System.Xml.Serialization;

namespace ZigBeeNet.Ember.CodeGenerator.Xml
{
    public class Value
    {
        [XmlElement("name")]
        public string Name;

        [XmlElement("description")]
        public string Description;

        [XmlElement("enum_value")]
        public int? EnumValue;
    }
}
