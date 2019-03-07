// License text here
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.ZCL.Protocol;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Clusters.ColorControl;

<summary>
 Move to Color Temperature Command value object class.
 
 Cluster: Color Control. Command is sentTO the server.
  This command is a specific command used for the Color Control cluster.
 
  Code is auto-generated. Modifications may be overwritten!
 </summary>

namespace ZigBeeNet.ZCL.Clusters.ColorControl
{
       public class MoveToColorTemperatureCommand : ZclCommand
       {
           <summary>
            Color Temperature command message field.
           </summary>
           public ushort ColorTemperature { get; set; }

           <summary>
            Transition time command message field.
           </summary>
           public ushort TransitionTime { get; set; }


           <summary>
            Default constructor.
           </summary>
           public MoveToColorTemperatureCommand()
           {
               GenericCommand = false;
               ClusterId = 768;
               CommandId = 10;
               CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
           }

           public override void Serialize(ZclFieldSerializer serializer)
           {
            serializer.Serialize(ColorTemperature, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            serializer.Serialize(TransitionTime, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
           }

           public override void Deserialize(ZclFieldDeserializer deserializer)
           {
               ColorTemperature = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
               TransitionTime = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
           }

           public override string ToString()
           {
               var builder = new StringBuilder();

               builder.Append("MoveToColorTemperatureCommand [");
               builder.Append(base.ToString());
               builder.Append(", ColorTemperature=");
               builder.Append(ColorTemperature);
               builder.Append(", TransitionTime=");
               builder.Append(TransitionTime);
               builder.Append(']');

               return builder.ToString();
           }

       }
}
