using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.ZCL.Clusters.General
{
    public class ReadAttributesResponse : ZclCommand
    {
        /**
         * Records command message field.
         */
        public List<ReadAttributeStatusRecord> Records { get; set; }

        /**
         * Default constructor.
         */
        public ReadAttributesResponse()
        {
            IsGenericCommand = true;
            CommandId = 1;
            Direction = ZclCommandDirection.ClientToServer;
        }


        public override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(Records, ZclDataType.Get(DataType.N_X_READ_ATTRIBUTE_STATUS_RECORD));
        }

        public override void Deserialize(ZclFieldDeserializer deserializer)
        {
            Records = (List<ReadAttributeStatusRecord>)deserializer.Deserialize(ZclDataType.Get(DataType.N_X_READ_ATTRIBUTE_STATUS_RECORD));
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("ReadAttributesResponse [")
                   .Append(base.ToString())
                   .Append(", records=")
                   .Append(Records)
                   .Append(']');

            return builder.ToString();
        }

    }
}
