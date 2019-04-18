using System.Xml.Serialization;

namespace ZigBeeNet.Digi.XBee.CodeGenerator.Xml
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
