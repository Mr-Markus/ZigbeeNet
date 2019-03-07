﻿// License text here
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.ZCL.Protocol;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Clusters.IASACE;

<summary>
 Get Zone ID Map Command value object class.
 
 Cluster: IAS ACE. Command is sentTO the server.
  This command is a specific command used for the IAS ACE cluster.
 
  Code is auto-generated. Modifications may be overwritten!
 </summary>

namespace ZigBeeNet.ZCL.Clusters.IASACE
{
       public class GetZoneIDMapCommand : ZclCommand
       {

           <summary>
            Default constructor.
           </summary>
           public GetZoneIDMapCommand()
           {
               GenericCommand = false;
               ClusterId = 1281;
               CommandId = 5;
               CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
           }

           public override string ToString()
           {
               var builder = new StringBuilder();

               builder.Append("GetZoneIDMapCommand [");
               builder.Append(base.ToString());
               builder.Append(']');

               return builder.ToString();
           }

       }
}
