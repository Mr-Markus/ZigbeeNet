using System.Collections.Generic;

namespace ZigBeeNet.CodeGenerator.Zcl
{
	public class Field
	{
		public int FieldId { get; set; }
		public string FieldLabel { get; set; }
		public string FieldType { get; set; }
		public string DataType { get; set; }
		public string DataTypeClass { get; set; }
		public string NameUpperCamelCase { get; set; }
		public string NameLowerCamelCase { get; set; }
		public string ListSizer { get; set; }
		public bool CompleteOnZero { get; set; }
		public string Condition { get; set; }
		public string ConditionOperator { get; set; }
		public List<string> Description { get; set; }
		public SortedDictionary<int, string> ValueMap { get; set; }
	}
}
