using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.CodeGenerator.Zcl
{
    public class Cluster
    {
        public int ClusterId;
        public List<string> ClusterDescription;
        public string ClusterName;
        public string ClusterType;
        public string NameUpperCamelCase;
        public string NameLowerCamelCase;
        public SortedDictionary<int, Command> Received = new SortedDictionary<int, Command>();
        public SortedDictionary<int, Command> Generated = new SortedDictionary<int, Command>();
        public SortedDictionary<int, Attribute> Attributes = new SortedDictionary<int, Attribute>();
    }
}
