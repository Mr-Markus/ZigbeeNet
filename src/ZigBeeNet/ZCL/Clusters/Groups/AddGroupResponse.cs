﻿// License text here
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.ZCL.Protocol;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Clusters.Groups;

<summary>
 Add Group Response value object class.
 
 Cluster: Groups. Command is sentFROM the server.
  This command is a specific command used for the Groups cluster.
 
  Code is auto-generated. Modifications may be overwritten!
 </summary>

namespace ZigBeeNet.ZCL.Clusters.Groups
{
       public class AddGroupResponse : ZclCommand
       {
           <summary>
            Status command message field.
           </summary>
           public byte Status { get; set; }

           <summary>
            Group ID command message field.
           </summary>
           public ushort GroupID { get; set; }


           <summary>
            Default constructor.
           </summary>
           public AddGroupResponse()
           {
               GenericCommand = false;
               ClusterId = 4;
               CommandId = 0;
               CommandDirection = ZclCommandDirection.SERVER_TO_CLIENT;
           }

           public override void Serialize(ZclFieldSerializer serializer)
           {
            serializer.Serialize(Status, ZclDataType.Get(DataType.ENUMERATION_8_BIT));
            serializer.Serialize(GroupID, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
           }

           public override void Deserialize(ZclFieldDeserializer deserializer)
           {
               Status = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.ENUMERATION_8_BIT));
               GroupID = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
           }

           public override string ToString()
           {
               var builder = new StringBuilder();

               builder.Append("AddGroupResponse [");
               builder.Append(base.ToString());
               builder.Append(", Status=");
               builder.Append(Status);
               builder.Append(", GroupID=");
               builder.Append(GroupID);
               builder.Append(']');

               return builder.ToString();
           }

       }
}
