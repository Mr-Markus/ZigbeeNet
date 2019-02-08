using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.CodeGenerator.Zcl
{
    public class Command
    {
        public int CommandId;
        public string CommandLabel;
        public List<string> CommandDescription;
        public string CommandType;
        public string DataType;
        public string DataTypeClass;
        public string NameUpperCamelCase;
        public string NameLowerCamelCase;
        public string ResponseCommand;
        // public String responseRequest;
        // public String responseResponse;

        public Dictionary<string, string> ResponseMatchers;
        public SortedDictionary<int, Field> Fields = new SortedDictionary<int, Field>();
    }
}
