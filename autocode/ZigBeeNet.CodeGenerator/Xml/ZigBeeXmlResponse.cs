using System;
using System.Collections.Generic;

namespace ZigBeeNet.CodeGenerator.Xml
{
    public class ZigBeeXmlResponse
    {
        public string Command { get; set; }
        public List<ZigBeeXmlMatcher> Matchers { get; set; }
    }
}