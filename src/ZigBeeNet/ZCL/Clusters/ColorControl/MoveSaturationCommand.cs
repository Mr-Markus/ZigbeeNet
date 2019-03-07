﻿// License text here
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.ZCL.Protocol;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Clusters.ColorControl;

<summary>
 Move Saturation Command value object class.
 
 Cluster: Color Control. Command is sentTO the server.
  This command is a specific command used for the Color Control cluster.
 
  Code is auto-generated. Modifications may be overwritten!
 </summary>

namespace ZigBeeNet.ZCL.Clusters.ColorControl
{
       public class MoveSaturationCommand : ZclCommand
       {
           <summary>
            Move mode command message field.
           </summary>
           public byte MoveMode { get; set; }

           <summary>
            Rate command message field.
           </summary>
           public byte Rate { get; set; }


           <summary>
            Default constructor.
           </summary>
           public MoveSaturationCommand()
           {
               GenericCommand = false;
               ClusterId = 768;
               CommandId = 4;
               CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
           }

           public override void Serialize(ZclFieldSerializer serializer)
           {
            serializer.Serialize(MoveMode, ZclDataType.Get(DataType.ENUMERATION_8_BIT));
            serializer.Serialize(Rate, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
           }

           public override void Deserialize(ZclFieldDeserializer deserializer)
           {
               MoveMode = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.ENUMERATION_8_BIT));
               Rate = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
           }

           public override string ToString()
           {
               var builder = new StringBuilder();

               builder.Append("MoveSaturationCommand [");
               builder.Append(base.ToString());
               builder.Append(", MoveMode=");
               builder.Append(MoveMode);
               builder.Append(", Rate=");
               builder.Append(Rate);
               builder.Append(']');

               return builder.ToString();
           }

       }
}
