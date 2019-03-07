﻿// License text here
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.ZCL.Protocol;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Clusters.Scenes;

<summary>
 Remove All Scenes Command value object class.
 
 Cluster: Scenes. Command is sentTO the server.
  This command is a specific command used for the Scenes cluster.
 
 * The Remove All Scenes may be addressed to a single device or to a group. 
  Code is auto-generated. Modifications may be overwritten!
 </summary>

namespace ZigBeeNet.ZCL.Clusters.Scenes
{
       public class RemoveAllScenesCommand : ZclCommand
       {
           <summary>
            Group ID command message field.
           </summary>
           public ushort GroupID { get; set; }


           <summary>
            Default constructor.
           </summary>
           public RemoveAllScenesCommand()
           {
               GenericCommand = false;
               ClusterId = 5;
               CommandId = 3;
               CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
           }

           public override void Serialize(ZclFieldSerializer serializer)
           {
            serializer.Serialize(GroupID, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
           }

           public override void Deserialize(ZclFieldDeserializer deserializer)
           {
               GroupID = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
           }

           public override string ToString()
           {
               var builder = new StringBuilder();

               builder.Append("RemoveAllScenesCommand [");
               builder.Append(base.ToString());
               builder.Append(", GroupID=");
               builder.Append(GroupID);
               builder.Append(']');

               return builder.ToString();
           }

       }
}
