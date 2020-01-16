using System.Xml.Serialization;

namespace ZigBeeNet.Ember.CodeGenerator.Xml
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

        [XmlElement("multiple")]
        public bool Multiple { get; set; }

        [XmlElement("display_type")]
        public string DisplayType { get; set; }

        [XmlElement("display_length")]
        public int DisplayLength { get; set; }

    }
}
