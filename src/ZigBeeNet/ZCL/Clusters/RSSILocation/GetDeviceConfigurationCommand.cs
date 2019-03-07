// License text here
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.ZCL.Protocol;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Clusters.RSSILocation;

<summary>
 Get Device Configuration Command value object class.
 
 Cluster: RSSI Location. Command is sentTO the server.
  This command is a specific command used for the RSSI Location cluster.
 
  Code is auto-generated. Modifications may be overwritten!
 </summary>

namespace ZigBeeNet.ZCL.Clusters.RSSILocation
{
       public class GetDeviceConfigurationCommand : ZclCommand
       {
           <summary>
            Target Address command message field.
           </summary>
           public IeeeAddress TargetAddress { get; set; }


           <summary>
            Default constructor.
           </summary>
           public GetDeviceConfigurationCommand()
           {
               GenericCommand = false;
               ClusterId = 11;
               CommandId = 2;
               CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
           }

           public override void Serialize(ZclFieldSerializer serializer)
           {
            serializer.Serialize(TargetAddress, ZclDataType.Get(DataType.IEEE_ADDRESS));
           }

           public override void Deserialize(ZclFieldDeserializer deserializer)
           {
               TargetAddress = deserializer.Deserialize<IeeeAddress>(ZclDataType.Get(DataType.IEEE_ADDRESS));
           }

           public override string ToString()
           {
               var builder = new StringBuilder();

               builder.Append("GetDeviceConfigurationCommand [");
               builder.Append(base.ToString());
               builder.Append(", TargetAddress=");
               builder.Append(TargetAddress);
               builder.Append(']');

               return builder.ToString();
           }

       }
}
