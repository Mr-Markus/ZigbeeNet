﻿// License text here
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.ZCL.Protocol;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Clusters.ColorControl;

<summary>
 Move to Hue Command value object class.
 
 Cluster: Color Control. Command is sentTO the server.
  This command is a specific command used for the Color Control cluster.
 
  Code is auto-generated. Modifications may be overwritten!
 </summary>

namespace ZigBeeNet.ZCL.Clusters.ColorControl
{
       public class MoveToHueCommand : ZclCommand
       {
           <summary>
            Hue command message field.
           </summary>
           public byte Hue { get; set; }

           <summary>
            Direction command message field.
           </summary>
           public byte Direction { get; set; }

           <summary>
            Transition time command message field.
           </summary>
           public ushort TransitionTime { get; set; }


           <summary>
            Default constructor.
           </summary>
           public MoveToHueCommand()
           {
               GenericCommand = false;
               ClusterId = 768;
               CommandId = 0;
               CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
           }

           public override void Serialize(ZclFieldSerializer serializer)
           {
            serializer.Serialize(Hue, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            serializer.Serialize(Direction, ZclDataType.Get(DataType.ENUMERATION_8_BIT));
            serializer.Serialize(TransitionTime, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
           }

           public override void Deserialize(ZclFieldDeserializer deserializer)
           {
               Hue = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
               Direction = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.ENUMERATION_8_BIT));
               TransitionTime = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
           }

           public override string ToString()
           {
               var builder = new StringBuilder();

               builder.Append("MoveToHueCommand [");
               builder.Append(base.ToString());
               builder.Append(", Hue=");
               builder.Append(Hue);
               builder.Append(", Direction=");
               builder.Append(Direction);
               builder.Append(", TransitionTime=");
               builder.Append(TransitionTime);
               builder.Append(']');

               return builder.ToString();
           }

       }
}
