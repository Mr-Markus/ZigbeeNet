﻿// License text here
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.ZCL.Protocol;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Clusters.LevelControl;

<summary>
 Step (with On/Off) Command value object class.
 
 Cluster: Level Control. Command is sentTO the server.
  This command is a specific command used for the Level Control cluster.
 
  Code is auto-generated. Modifications may be overwritten!
 </summary>

namespace ZigBeeNet.ZCL.Clusters.LevelControl
{
       public class StepWithOnOffCommand : ZclCommand
       {
           <summary>
            Step mode command message field.
           </summary>
           public byte StepMode { get; set; }

           <summary>
            Step size command message field.
           </summary>
           public byte StepSize { get; set; }

           <summary>
            Transition time command message field.
           </summary>
           public ushort TransitionTime { get; set; }


           <summary>
            Default constructor.
           </summary>
           public StepWithOnOffCommand()
           {
               GenericCommand = false;
               ClusterId = 8;
               CommandId = 6;
               CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
           }

           public override void Serialize(ZclFieldSerializer serializer)
           {
            serializer.Serialize(StepMode, ZclDataType.Get(DataType.ENUMERATION_8_BIT));
            serializer.Serialize(StepSize, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            serializer.Serialize(TransitionTime, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
           }

           public override void Deserialize(ZclFieldDeserializer deserializer)
           {
               StepMode = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.ENUMERATION_8_BIT));
               StepSize = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
               TransitionTime = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
           }

           public override string ToString()
           {
               var builder = new StringBuilder();

               builder.Append("StepWithOnOffCommand [");
               builder.Append(base.ToString());
               builder.Append(", StepMode=");
               builder.Append(StepMode);
               builder.Append(", StepSize=");
               builder.Append(StepSize);
               builder.Append(", TransitionTime=");
               builder.Append(TransitionTime);
               builder.Append(']');

               return builder.ToString();
           }

       }
}
