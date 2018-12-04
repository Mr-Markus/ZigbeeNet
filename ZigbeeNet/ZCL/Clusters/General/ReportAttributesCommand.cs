using System;
using System.Collections.Generic;
using System.Text;
using ZigbeeNet.ZCL.Fileld;
using ZigbeeNet.ZCL.Protocol;

namespace ZigbeeNet.ZCL.Clusters.General
{
    public class ReportAttributesCommand : ZclCommand
    {
        /**
         * Reports command message field.
         */
        public List<AttributeReport> Reports { get; set; }

        /**
         * Default constructor.
         */
        public ReportAttributesCommand()
        {
            IsGenericCommand = true;
            CommandId = 10;
            Direction = ZclCommandDirection.ClientToServer;
        }


        public override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(Reports, ZclDataType.Get(DataType.N_X_ATTRIBUTE_REPORT));
        }

        public override void Deserialize(ZclFieldDeserializer deserializer)
        {
            Reports = (List<AttributeReport>)deserializer.Deserialize(ZclDataType.Get(DataType.N_X_ATTRIBUTE_REPORT));
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("ReportAttributesCommand [")
                   .Append(base.ToString())
                   .Append(", reports=")
                   .Append(Reports)
                   .Append(']');

            return builder.ToString();
        }
    }
}
