﻿using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.Transaction;
using ZigBeeNet.ZCL;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.ZDO.Command
{
    /**
     * Management Routing Request value object class.
     * 
     * The Mgmt_Rtg_req is generated from a Local Device wishing to retrieve the
     * contents of the Routing Table from the Remote Device. The destination
     * addressing on this command shall be unicast only and the destination address
     * must be that of the ZigBee Router or ZigBee Coordinator.
     */
    public class ManagementRoutingRequest : ZdoRequest, IZigBeeTransactionMatcher
    {
        /**
         * StartIndex command message field.
         */
        public int StartIndex { get; set; }

        /**
         * Default constructor.
         */
        public ManagementRoutingRequest()
        {
            ClusterId = 0x0032;
        }

        public override void Serialize(ZclFieldSerializer serializer)
        {
            base.Serialize(serializer);

            serializer.Serialize(StartIndex, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
        }

        public override void Deserialize(ZclFieldDeserializer deserializer)
        {
            base.Deserialize(deserializer);

            StartIndex = (int)deserializer.Deserialize(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
        }

        public bool IsTransactionMatch(ZigBeeCommand request, ZigBeeCommand response)
        {
            if (!(response is ManagementRoutingResponse))
            {
                return false;
            }

            return ((ManagementRoutingRequest)request).DestinationAddress.Equals(((ManagementRoutingResponse)response).SourceAddress);
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append("ManagementRoutingRequest [")
                   .Append(base.ToString())
                   .Append(", startIndex=")
                   .Append(StartIndex)
                   .Append(']');

            return builder.ToString();
        }
    }
}
