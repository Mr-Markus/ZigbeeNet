﻿// License text here
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.ZCL.Protocol;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Clusters.Commissioning;

<summary>
 Restart Device Response Response value object class.
 
 Cluster: Commissioning. Command is sentFROM the server.
  This command is a specific command used for the Commissioning cluster.
 
  Code is auto-generated. Modifications may be overwritten!
 </summary>

namespace ZigBeeNet.ZCL.Clusters.Commissioning
{
       public class RestartDeviceResponseResponse : ZclCommand
       {
           <summary>
            Status command message field.
           </summary>
           public byte Status { get; set; }


           <summary>
            Default constructor.
           </summary>
           public RestartDeviceResponseResponse()
           {
               GenericCommand = false;
               ClusterId = 21;
               CommandId = 0;
               CommandDirection = ZclCommandDirection.SERVER_TO_CLIENT;
           }

           public override void Serialize(ZclFieldSerializer serializer)
           {
            serializer.Serialize(Status, ZclDataType.Get(DataType.ENUMERATION_8_BIT));
           }

           public override void Deserialize(ZclFieldDeserializer deserializer)
           {
               Status = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.ENUMERATION_8_BIT));
           }

           public override string ToString()
           {
               var builder = new StringBuilder();

               builder.Append("RestartDeviceResponseResponse [");
               builder.Append(base.ToString());
               builder.Append(", Status=");
               builder.Append(Status);
               builder.Append(']');

               return builder.ToString();
           }

       }
}
