using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.CodeGenerator.Zcl
{
	public class Command
	{
		public int CommandId { get; set; }
		public string CommandLabel { get; set; }
		public List<string> CommandDescription { get; set; }
		public string CommandType { get; set; }
		public string DataType { get; set; }
		public string DataTypeClass { get; set; }
		public string NameUpperCamelCase { get; set; }
		public string NameLowerCamelCase { get; set; }
		public string ResponseCommand { get; set; }
		// public String responseRequest;
		// public String responseResponse;

		public Dictionary<string, string> ResponseMatchers { get; set; }
		public SortedDictionary<int, Field> Fields { get; set; } = new SortedDictionary<int, Field>();
	}
}
