using System.Xml.Serialization;

namespace ZigBeeNet.Digi.XBee.CodeGenerator.Xml
{
    public class Parameter
    {
        [XmlElement("data_type")]
        public string DataType { get; set; }

        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("description")]
        public string Description { get; set; }

        [XmlElement("auto_size")]
        public string AutoSize { get; set; }

        [XmlElement("conditional")]
        public string Conditional { get; set; }

        [XmlElement("default_value")]
        public string DefaultValue { get; set; }

        [XmlElement("multiple")]
        public bool Multiple { get; set; }

        [XmlElement("display_type")]
        public string DisplayType { get; set; }

        [XmlElement("display_length")]
        public int DisplayLength { get; set; }

        [XmlElement("optional")]
        public bool Optional { get; set; }

        [XmlElement("minimum")]
        public int? Minimum { get; set; }

        [XmlElement("maximum")]
        public int? Maximum { get; set; }

        [XmlElement("value")]
        public string Value { get; set; }

        [XmlElement("bitfield")]
        public bool Bitfield { get; set; }
    }
}
