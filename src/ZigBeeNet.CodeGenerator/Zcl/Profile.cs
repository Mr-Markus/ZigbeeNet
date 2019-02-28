using System.Collections.Generic;

namespace ZigBeeNet.CodeGenerator.Zcl
{
	public class Profile
	{
		public int ProfileId { get; set; }
		public string ProfileName { get; set; }
		public string ProfileAbbreviation { get; set; }
		public string ProfileType { get; set; }
		public SortedDictionary<int, Cluster> Clusters { get; set; } = new SortedDictionary<int, Cluster>();
	}
}
