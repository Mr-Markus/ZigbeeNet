using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.CodeGenerator.Zcl
{
	public class Context
	{
		public List<string> Lines { get; set; }

		public Profile Profile { get; set; }
		public Cluster Cluster { get; set; }
		public Command Command { get; set; }

		public bool Received { get; set; }
		public bool Generated { get; set; }
		public bool Attribute { get; set; }

		public Dictionary<string, DataType> DataTypes { get; set; } = new Dictionary<string, DataType>();
		public SortedDictionary<int, Profile> Profiles { get; set; } = new SortedDictionary<int, Profile>();
	}
}
