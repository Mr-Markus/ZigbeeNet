// License text here
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.ZCL.Protocol;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Clusters.LevelControl;

<summary>
 Stop Command value object class.
 
 Cluster: Level Control. Command is sentTO the server.
  This command is a specific command used for the Level Control cluster.
 
  Code is auto-generated. Modifications may be overwritten!
 </summary>

namespace ZigBeeNet.ZCL.Clusters.LevelControl
{
       public class StopCommand : ZclCommand
       {

           <summary>
            Default constructor.
           </summary>
           public StopCommand()
           {
               GenericCommand = false;
               ClusterId = 8;
               CommandId = 3;
               CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
           }

           public override string ToString()
           {
               var builder = new StringBuilder();

               builder.Append("StopCommand [");
               builder.Append(base.ToString());
               builder.Append(']');

               return builder.ToString();
           }

       }
}
