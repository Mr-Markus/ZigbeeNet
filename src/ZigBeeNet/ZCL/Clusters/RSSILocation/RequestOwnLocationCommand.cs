﻿// License text here
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.ZCL.Protocol;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Clusters.RSSILocation;

<summary>
 Request Own Location Command value object class.
 
 Cluster: RSSI Location. Command is sentFROM the server.
  This command is a specific command used for the RSSI Location cluster.
 
  Code is auto-generated. Modifications may be overwritten!
 </summary>

namespace ZigBeeNet.ZCL.Clusters.RSSILocation
{
       public class RequestOwnLocationCommand : ZclCommand
       {
           <summary>
            Requesting Address command message field.
           </summary>
           public IeeeAddress RequestingAddress { get; set; }


           <summary>
            Default constructor.
           </summary>
           public RequestOwnLocationCommand()
           {
               GenericCommand = false;
               ClusterId = 11;
               CommandId = 7;
               CommandDirection = ZclCommandDirection.SERVER_TO_CLIENT;
           }

           public override void Serialize(ZclFieldSerializer serializer)
           {
            serializer.Serialize(RequestingAddress, ZclDataType.Get(DataType.IEEE_ADDRESS));
           }

           public override void Deserialize(ZclFieldDeserializer deserializer)
           {
               RequestingAddress = deserializer.Deserialize<IeeeAddress>(ZclDataType.Get(DataType.IEEE_ADDRESS));
           }

           public override string ToString()
           {
               var builder = new StringBuilder();

               builder.Append("RequestOwnLocationCommand [");
               builder.Append(base.ToString());
               builder.Append(", RequestingAddress=");
               builder.Append(RequestingAddress);
               builder.Append(']');

               return builder.ToString();
           }

       }
}
