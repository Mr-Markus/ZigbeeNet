﻿using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.ZCL;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.ZDO.Command
{
    /**
     * Simple Descriptor Request value object class.
     * 
     * The Simple_Desc_req command is generated from a local device wishing to
     * inquire as to the simple descriptor of a remote device on a specified endpoint. This
     * command shall be unicast either to the remote device itself or to an alternative
     * device that contains the discovery information of the remote device.
     */
    public class SimpleDescriptorRequest : ZdoRequest, IZigBeeTransactionMatcher
    {
        /**
         * NWKAddrOfInterest command message field.
         */
        public int NwkAddrOfInterest { get; set; }

        /**
         * Endpoint command message field.
         */
        public int Endpoint { get; set; }

        /**
         * Default constructor.
         */
        public SimpleDescriptorRequest()
        {
            ClusterId = 0x0004;
        }


        public override void Serialize(ZclFieldSerializer serializer)
        {
            base.Serialize(serializer);

            serializer.Serialize(NwkAddrOfInterest, ZclDataType.Get(DataType.NWK_ADDRESS));
            serializer.Serialize(Endpoint, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
        }

        public void deserialize(ZclFieldDeserializer deserializer)
        {
            base.Deserialize(deserializer);

            NwkAddrOfInterest = (int)deserializer.Deserialize(ZclDataType.Get(DataType.NWK_ADDRESS));
            Endpoint = (int)deserializer.Deserialize(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
        }

        public bool IsTransactionMatch(ZigBeeCommand request, ZigBeeCommand response)
        {
            if (!(response is SimpleDescriptorResponse)) {
                return false;
            }

            return ((SimpleDescriptorRequest)request).Endpoint.Equals(((SimpleDescriptorResponse)response).getSimpleDescriptor().getEndpoint()) &&
                   ((SimpleDescriptorRequest)request).NwkAddrOfInterest.Equals(((SimpleDescriptorResponse)response).NwkAddrOfInterest);
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append("SimpleDescriptorRequest [")
                   .Append(base.ToString())
                   .Append(", nwkAddrOfInterest=")
                   .Append(NwkAddrOfInterest)
                   .Append(", endpoint=")
                   .Append(Endpoint)
                   .Append(']');

            return builder.ToString();
        }

    }
}
