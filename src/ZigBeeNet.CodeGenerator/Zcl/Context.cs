using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.CodeGenerator.Zcl
{
    public class Context
    {
        public List<string> Lines;

        public Profile Profile;
        public Cluster Cluster;
        public Command Command;

        public bool Received;
        public bool Generated;
        public bool Attribute;

        public Dictionary<string, DataType> DataTypes = new Dictionary<string, DataType>();
        public SortedDictionary<int, Profile> Profiles = new SortedDictionary<int, Profile>();
    }
}
