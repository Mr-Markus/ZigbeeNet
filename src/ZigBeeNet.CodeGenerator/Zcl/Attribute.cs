using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.CodeGenerator.Zcl
{
    public class Attribute
    {
        public string AttributeLabel;
        public List<string> AttributeDescription;
        public string attributeType;
        public string DataType;
        public string DataTypeClass;
        public string NameUpperCamelCase;
        public string NameLowerCamelCase;
        public string AttributeAccess;
        public string AttributeReporting;
        public string EnumName;
        public int AttributeId;
        public string AttributeImplementation;
        public SortedDictionary<int, string> ValueMap;
    }
}
