﻿// License text here
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.ZCL.Protocol;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Clusters.General;

<summary>
 Read Reporting Configuration Command value object class.
 
 Cluster: General. Command is sentTO the server.
  This command is a generic command used across the profile.
 
 * The Read Reporting Configuration command is used to read the configuration * details of the reporting mechanism for one or more of the attributes of a cluster. 
  Code is auto-generated. Modifications may be overwritten!
 </summary>

namespace ZigBeeNet.ZCL.Clusters.General
{
       public class ReadReportingConfigurationCommand : ZclCommand
       {
           <summary>
            Records command message field.
           </summary>
           public List<AttributeRecord> Records { get; set; }


           <summary>
            Default constructor.
           </summary>
           public ReadReportingConfigurationCommand()
           {
               GenericCommand = true;
               CommandId = 8;
               CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
           }

           public override void Serialize(ZclFieldSerializer serializer)
           {
            serializer.Serialize(Records, ZclDataType.Get(DataType.N_X_ATTRIBUTE_RECORD));
           }

           public override void Deserialize(ZclFieldDeserializer deserializer)
           {
               Records = deserializer.Deserialize<List<AttributeRecord>>(ZclDataType.Get(DataType.N_X_ATTRIBUTE_RECORD));
           }

           public override string ToString()
           {
               var builder = new StringBuilder();

               builder.Append("ReadReportingConfigurationCommand [");
               builder.Append(base.ToString());
               builder.Append(", Records=");
               builder.Append(Records);
               builder.Append(']');

               return builder.ToString();
           }

       }
}
