﻿// License text here
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.ZCL.Protocol;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Clusters.ColorControl;

<summary>
 Move Color Command value object class.
 
 Cluster: Color Control. Command is sentTO the server.
  This command is a specific command used for the Color Control cluster.
 
  Code is auto-generated. Modifications may be overwritten!
 </summary>

namespace ZigBeeNet.ZCL.Clusters.ColorControl
{
       public class MoveColorCommand : ZclCommand
       {
           <summary>
            RateX command message field.
           </summary>
           public short RateX { get; set; }

           <summary>
            RateY command message field.
           </summary>
           public short RateY { get; set; }


           <summary>
            Default constructor.
           </summary>
           public MoveColorCommand()
           {
               GenericCommand = false;
               ClusterId = 768;
               CommandId = 8;
               CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
           }

           public override void Serialize(ZclFieldSerializer serializer)
           {
            serializer.Serialize(RateX, ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER));
            serializer.Serialize(RateY, ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER));
           }

           public override void Deserialize(ZclFieldDeserializer deserializer)
           {
               RateX = deserializer.Deserialize<short>(ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER));
               RateY = deserializer.Deserialize<short>(ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER));
           }

           public override string ToString()
           {
               var builder = new StringBuilder();

               builder.Append("MoveColorCommand [");
               builder.Append(base.ToString());
               builder.Append(", RateX=");
               builder.Append(RateX);
               builder.Append(", RateY=");
               builder.Append(RateY);
               builder.Append(']');

               return builder.ToString();
           }

       }
}
