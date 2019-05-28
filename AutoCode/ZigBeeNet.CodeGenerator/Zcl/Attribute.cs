using System.Collections.Generic;

namespace ZigBeeNet.CodeGenerator.Zcl
{
	public class Attribute
	{
		public string AttributeLabel { get; set; }
		public List<string> AttributeDescription { get; set; }
		public string attributeType { get; set; }
		public string DataType { get; set; }
		public string DataTypeClass { get; set; }
		public string NameUpperCamelCase { get; set; }
		public string NameLowerCamelCase { get; set; }
		public string AttributeAccess { get; set; }
		public string AttributeReporting { get; set; }
		public string EnumName { get; set; }
		public int AttributeId { get; set; }
		public string AttributeImplementation { get; set; }
		public SortedDictionary<int, string> ValueMap { get; set; }
	}
}
