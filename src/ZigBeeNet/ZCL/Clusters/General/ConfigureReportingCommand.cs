﻿// License text here
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.ZCL.Protocol;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Clusters.General;

<summary>
 Configure Reporting Command value object class.
 
 Cluster: General. Command is sentTO the server.
  This command is a generic command used across the profile.
 
 * The Configure Reporting command is used to configure the reporting mechanism * for one or more of the attributes of a cluster. * <br> * The individual cluster definitions specify which attributes shall be available to this * reporting mechanism, however specific implementations of a cluster may make * additional attributes available. 
  Code is auto-generated. Modifications may be overwritten!
 </summary>

namespace ZigBeeNet.ZCL.Clusters.General
{
       public class ConfigureReportingCommand : ZclCommand
       {
           <summary>
            Records command message field.
           </summary>
           public List<AttributeReportingConfigurationRecord> Records { get; set; }


           <summary>
            Default constructor.
           </summary>
           public ConfigureReportingCommand()
           {
               GenericCommand = true;
               CommandId = 6;
               CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
           }

           public override void Serialize(ZclFieldSerializer serializer)
           {
            serializer.Serialize(Records, ZclDataType.Get(DataType.N_X_ATTRIBUTE_REPORTING_CONFIGURATION_RECORD));
           }

           public override void Deserialize(ZclFieldDeserializer deserializer)
           {
               Records = deserializer.Deserialize<List<AttributeReportingConfigurationRecord>>(ZclDataType.Get(DataType.N_X_ATTRIBUTE_REPORTING_CONFIGURATION_RECORD));
           }

           public override string ToString()
           {
               var builder = new StringBuilder();

               builder.Append("ConfigureReportingCommand [");
               builder.Append(base.ToString());
               builder.Append(", Records=");
               builder.Append(Records);
               builder.Append(']');

               return builder.ToString();
           }

       }
}
