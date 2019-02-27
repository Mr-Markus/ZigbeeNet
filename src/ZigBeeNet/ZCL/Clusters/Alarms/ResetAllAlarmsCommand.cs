// License text here
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.ZCL.Protocol;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Clusters.Alarms;

/// <summary>
 /// Reset All Alarms Command value object class.
 ///
 /// Cluster: Alarms. Command is sentTO the server.
 /// This command is a specific command used for the Alarms cluster.
 ///
 /// Code is auto-generated. Modifications may be overwritten!
 /// </summary>

namespace ZigBeeNet.ZCL.Clusters.Alarms
{
       public class ResetAllAlarmsCommand : ZclCommand
       {

           /// <summary>
           /// Default constructor.
           /// </summary>
           public ResetAllAlarmsCommand()
           {
               GenericCommand = false;
               ClusterId = 9;
               CommandId = 1;
               CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
           }

           public override string ToString()
           {
               var builder = new StringBuilder();

               builder.Append("ResetAllAlarmsCommand [");
               builder.Append(base.ToString());
               builder.Append(']');

               return builder.ToString();
           }

       }
}
