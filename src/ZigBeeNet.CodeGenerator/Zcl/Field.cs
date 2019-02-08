using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.CodeGenerator.Zcl
{
    public class Field
    {
        public int FieldId;
        public string FieldLabel;
        public string FieldType;
        public string DataType;
        public string DataTypeClass;
        public string NameUpperCamelCase;
        public string NameLowerCamelCase;
        public string ListSizer;
        public bool CompleteOnZero;
        public string Condition;
        public string ConditionOperator;
        public List<string> Description;
        public SortedDictionary<int, string> ValueMap;
    }
}
