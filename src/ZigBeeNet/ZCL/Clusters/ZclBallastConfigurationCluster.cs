﻿// License text here

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ZigBeeNet.DAO;
using ZigBeeNet.ZCL.Protocol;
using ZigBeeNet.ZCL.Field;

<summary>
Ballast Configurationcluster implementation (Cluster ID 0x0301).
 
  Code is auto-generated. Modifications may be overwritten!
 </summary>
namespace ZigBeeNet.ZCL.Clusters
{
   public class ZclBallastConfigurationCluster : ZclCluster
   {
       <summary>
        The ZigBee Cluster Library Cluster ID
       </summary>
       public const ushort CLUSTER_ID = 0x0301;

       <summary>
        The ZigBee Cluster Library Cluster Name
       </summary>
       public const string CLUSTER_NAME = "Ballast Configuration";

       // Attribute initialisation
       protected override Dictionary<ushort, ZclAttribute> InitializeAttributes()
       {
           Dictionary<ushort, ZclAttribute> attributeMap = new Dictionary<ushort, ZclAttribute>(0);

           return attributeMap;
       }

        Default constructor to create a Ballast Configuration cluster.
       
       <param name= zigbeeEndpoint the {@link ZigBeeEndpoint}
       </param>
       public ZclBallastConfigurationCluster(ZigBeeEndpoint zigbeeEndpoint)
           : base(zigbeeEndpoint, CLUSTER_ID, CLUSTER_NAME)
       {
       }

   }
}
