﻿using System.Collections.Generic;
using System.Xml.Serialization;

namespace ZigBeeNet.Ember.CodeGenerator.Xml
{
    [XmlRootAttribute("protocol")]
    public class Protocol
    {
        [XmlElement("command")]
        public List<Command> Commands { get; set; }

        [XmlElement("structure")]
        public List<Structure> Structures { get; set; }

        [XmlElement("enum")]
        public List<Enumeration> Enumerations { get; set; }
    }
}
