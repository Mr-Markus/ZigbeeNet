using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.Transaction;
using ZigBeeNet.ZCL;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.ZDO.Command
{
    /**
     * Active Endpoints Request value object class.
     * 
     * The Active_EP_req command is generated from a local device wishing to acquire
     * the list of endpoints on a remote device with simple descriptors. This command
     * shall be unicast either to the remote device itself or to an alternative device that
     * contains the discovery information of the remote device.
     */
    public class ActiveEndpointsRequest : ZdoRequest, IZigBeeTransactionMatcher
    {
        /**
         * NWKAddrOfInterest command message field.
         */
        public ushort NwkAddrOfInterest { get; set; }

        /**
         * Default constructor.
         */
        public ActiveEndpointsRequest()
        {
            ClusterId = 0x0005;
        }


        public override void Serialize(ZclFieldSerializer serializer)
        {
            base.Serialize(serializer);

            serializer.Serialize(NwkAddrOfInterest, ZclDataType.Get(DataType.NWK_ADDRESS));
        }

        public override void Deserialize(ZclFieldDeserializer deserializer)
        {
            base.Deserialize(deserializer);

            NwkAddrOfInterest = (ushort)deserializer.Deserialize(ZclDataType.Get(DataType.NWK_ADDRESS));
        }

        public bool IsTransactionMatch(ZigBeeCommand request, ZigBeeCommand response)
        {
            if (!(response is ActiveEndpointsResponse))
            {
                return false;
            }

            return ((ActiveEndpointsRequest)request).NwkAddrOfInterest.Equals(((ActiveEndpointsResponse)response).NwkAddrOfInterest);
        }


        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append("ActiveEndpointsRequest [")
                   .Append(base.ToString())
                   .Append(", nwkAddrOfInterest=")
                   .Append(NwkAddrOfInterest)
                   .Append(']');

            return builder.ToString();
        }

    }
}
