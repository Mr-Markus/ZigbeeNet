using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.CodeGenerator.Zcl
{
    public class Profile
    {
        public int ProfileId;
        public string ProfileName;
        public string ProfileAbbreviation;
        public string ProfileType;
        public SortedDictionary<int, Cluster> Clusters = new SortedDictionary<int, Cluster>();
    }
}
