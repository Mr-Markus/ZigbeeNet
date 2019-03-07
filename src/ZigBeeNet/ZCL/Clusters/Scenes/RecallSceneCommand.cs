﻿// License text here
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.ZCL.Protocol;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Clusters.Scenes;

<summary>
 Recall Scene Command value object class.
 
 Cluster: Scenes. Command is sentTO the server.
  This command is a specific command used for the Scenes cluster.
 
 * The Recall Scene command may be addressed to a single device or to a group. 
  Code is auto-generated. Modifications may be overwritten!
 </summary>

namespace ZigBeeNet.ZCL.Clusters.Scenes
{
       public class RecallSceneCommand : ZclCommand
       {
           <summary>
            Group ID command message field.
           </summary>
           public ushort GroupID { get; set; }

           <summary>
            Scene ID command message field.
           </summary>
           public byte SceneID { get; set; }


           <summary>
            Default constructor.
           </summary>
           public RecallSceneCommand()
           {
               GenericCommand = false;
               ClusterId = 5;
               CommandId = 5;
               CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
           }

           public override void Serialize(ZclFieldSerializer serializer)
           {
            serializer.Serialize(GroupID, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            serializer.Serialize(SceneID, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
           }

           public override void Deserialize(ZclFieldDeserializer deserializer)
           {
               GroupID = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
               SceneID = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
           }

           public override string ToString()
           {
               var builder = new StringBuilder();

               builder.Append("RecallSceneCommand [");
               builder.Append(base.ToString());
               builder.Append(", GroupID=");
               builder.Append(GroupID);
               builder.Append(", SceneID=");
               builder.Append(SceneID);
               builder.Append(']');

               return builder.ToString();
           }

       }
}
