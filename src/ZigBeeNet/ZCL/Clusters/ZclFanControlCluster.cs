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
Fan Controlcluster implementation (Cluster ID 0x0202).
 
  Code is auto-generated. Modifications may be overwritten!
 </summary>
namespace ZigBeeNet.ZCL.Clusters
{
   public class ZclFanControlCluster : ZclCluster
   {
       <summary>
        The ZigBee Cluster Library Cluster ID
       </summary>
       public const ushort CLUSTER_ID = 0x0202;

       <summary>
        The ZigBee Cluster Library Cluster Name
       </summary>
       public const string CLUSTER_NAME = "Fan Control";

       // Attribute initialisation
       protected override Dictionary<ushort, ZclAttribute> InitializeAttributes()
       {
           Dictionary<ushort, ZclAttribute> attributeMap = new Dictionary<ushort, ZclAttribute>(0);

           return attributeMap;
       }

        Default constructor to create a Fan Control cluster.
       
       <param name= zigbeeEndpoint the {@link ZigBeeEndpoint}
       </param>
       public ZclFanControlCluster(ZigBeeEndpoint zigbeeEndpoint)
           : base(zigbeeEndpoint, CLUSTER_ID, CLUSTER_NAME)
       {
       }

   }
}
