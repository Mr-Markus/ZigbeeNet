using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.CodeGenerator.Zcl
{
	public class Cluster
	{
		public int ClusterId { get; set; }
		public List<string> ClusterDescription { get; set; }
		public string ClusterName { get; set; }
		public string ClusterType { get; set; }
		public string NameUpperCamelCase { get; set; }
		public string NameLowerCamelCase { get; set; }
		public SortedDictionary<int, Command> Received { get; set; } = new SortedDictionary<int, Command>();
		public SortedDictionary<int, Command> Generated { get; set; } = new SortedDictionary<int, Command>();
		public SortedDictionary<int, Attribute> Attributes { get; set; } = new SortedDictionary<int, Attribute>();
	}
}
