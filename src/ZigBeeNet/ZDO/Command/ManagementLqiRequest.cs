using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.Transaction;
using ZigBeeNet.ZCL;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.ZDO.Command
{
    /**
     * Management LQI Request value object class.
     * 
     * The Mgmt_Lqi_req is generated from a Local Device wishing to obtain a
     * neighbor list for the Remote Device along with associated LQI values to each
     * neighbor. The destination addressing on this command shall be unicast only and
     * the destination address must be that of a ZigBee Coordinator or ZigBee Router.
     */
    public class ManagementLqiRequest : ZdoRequest, IZigBeeTransactionMatcher
    {
        /**
         * StartIndex command message field.
         */
        public byte StartIndex;

        /**
         * Default constructor.
         */
        public ManagementLqiRequest()
        {
            ClusterId = 0x0031;
        }

        public override void Serialize(ZclFieldSerializer serializer)
        {
            base.Serialize(serializer);

            serializer.Serialize(StartIndex, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
        }

        public override void Deserialize(ZclFieldDeserializer deserializer)
        {
            base.Deserialize(deserializer);

            StartIndex = (byte)deserializer.Deserialize(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
        }

        public bool IsTransactionMatch(ZigBeeCommand request, ZigBeeCommand response)
        {
            if (!(response is ManagementLqiResponse)) {
                return false;
            }

            return ((ManagementLqiRequest)request).DestinationAddress.Equals(((ManagementLqiResponse)response).SourceAddress);
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append("ManagementLqiRequest [")
                   .Append(base.ToString())
                   .Append(", startIndex=")
                   .Append(StartIndex)
                   .Append(']');

            return builder.ToString();
        }

    }
}
