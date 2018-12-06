﻿using System;
using System.Collections.Generic;
using System.Text;
using ZigbeeNet.Transaction;
using ZigBeeNet;
using ZigBeeNet.ZCL;
using ZigBeeNet.ZCL.Protocol;
using ZigBeeNet.ZDO;

namespace ZigbeeNet.ZDO.Command
{
    /**
     * Management Bind Request value object class.
     * 
     * The Mgmt_Bind_req is generated from a Local Device wishing to retrieve the
     * contents of the Binding Table from the Remote Device. The destination
     * addressing on this command shall be unicast only and the destination address
     * must be that of a Primary binding table cache or source device holding its own
     * binding table.
     * 
     * Code is auto-generated. Modifications may be overwritten!
     */
    public class ManagementBindRequest : ZdoRequest, IZigBeeTransactionMatcher
    {
        /**
         * StartIndex command message field.
         */
        public int StartIndex { get; set; }

        /**
         * Default constructor.
         */
        public ManagementBindRequest()
        {
            ClusterId = 0x0033;
        }

        public override void Serialize(ZclFieldSerializer serializer)
        {
            base.Serialize(serializer);

            serializer.Serialize(StartIndex, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
        }

        public override void Deserialize(ZclFieldDeserializer deSerializer)
        {
            base.Deserialize(deSerializer);

            StartIndex = (int)deSerializer.Deserialize(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
        }

        public bool IsTransactionMatch(ZigBeeCommand request, ZigBeeCommand response)
        {
            if (!(response is ManagementBindResponse)) {
                return false;
            }

            return ((ZdoRequest)request).DestinationAddress.Equals(((ManagementBindResponse)response).SourceAddress);
        }

        public override string ToString()
        {
             StringBuilder builder = new StringBuilder();

            builder.Append("ManagementBindRequest [")
                   .Append(base.ToString())
                   .Append(", startIndex=")
                   .Append(StartIndex)
                   .Append(']');

            return builder.ToString();
        }

    }
}
