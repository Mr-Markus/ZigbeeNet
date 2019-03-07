﻿// License text here
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.ZCL.Protocol;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Clusters.OnOff;

<summary>
 Toggle Command value object class.
 
 Cluster: On/Off. Command is sentTO the server.
  This command is a specific command used for the On/Off cluster.
 
  Code is auto-generated. Modifications may be overwritten!
 </summary>

namespace ZigBeeNet.ZCL.Clusters.OnOff
{
       public class ToggleCommand : ZclCommand
       {

           <summary>
            Default constructor.
           </summary>
           public ToggleCommand()
           {
               GenericCommand = false;
               ClusterId = 6;
               CommandId = 2;
               CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
           }

           public override string ToString()
           {
               var builder = new StringBuilder();

               builder.Append("ToggleCommand [");
               builder.Append(base.ToString());
               builder.Append(']');

               return builder.ToString();
           }

       }
}
