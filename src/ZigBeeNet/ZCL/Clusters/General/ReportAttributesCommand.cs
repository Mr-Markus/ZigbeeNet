﻿// License text here
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.ZCL.Protocol;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Clusters.General;

<summary>
 Report Attributes Command value object class.
 
 Cluster: General. Command is sentTO the server.
  This command is a generic command used across the profile.
 
 * The report attributes command is used by a device to report the values of one or * more of its attributes to another device, bound a priori. Individual clusters, defined * elsewhere in the ZCL, define which attributes are to be reported and at what * interval. 
  Code is auto-generated. Modifications may be overwritten!
 </summary>

namespace ZigBeeNet.ZCL.Clusters.General
{
       public class ReportAttributesCommand : ZclCommand
       {
           <summary>
            Reports command message field.
           </summary>
           public List<AttributeReport> Reports { get; set; }


           <summary>
            Default constructor.
           </summary>
           public ReportAttributesCommand()
           {
               GenericCommand = true;
               CommandId = 10;
               CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
           }

           public override void Serialize(ZclFieldSerializer serializer)
           {
            serializer.Serialize(Reports, ZclDataType.Get(DataType.N_X_ATTRIBUTE_REPORT));
           }

           public override void Deserialize(ZclFieldDeserializer deserializer)
           {
               Reports = deserializer.Deserialize<List<AttributeReport>>(ZclDataType.Get(DataType.N_X_ATTRIBUTE_REPORT));
           }

           public override string ToString()
           {
               var builder = new StringBuilder();

               builder.Append("ReportAttributesCommand [");
               builder.Append(base.ToString());
               builder.Append(", Reports=");
               builder.Append(Reports);
               builder.Append(']');

               return builder.ToString();
           }

       }
}
