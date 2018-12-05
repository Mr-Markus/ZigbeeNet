using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.ZCL;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.ZDO.Command
{
    public class IeeeAddressRequest : ZdoRequest, IZigBeeTransactionMatcher
    {
        /**
         * NWKAddrOfInterest command message field.
         */
        public int NwkAddrOfInterest { get; set; }

        /**
         * RequestType command message field.
         */
        public int RequestType { get; set; }

        /**
         * StartIndex command message field.
         */
        public int StartIndex { get; set; }

        /**
         * Default constructor.
         */
        public IeeeAddressRequest()
        {
            ClusterId = 0x0001;
        }

        public override void Serialize(ZclFieldSerializer serializer)
        {
            base.Serialize(serializer);

            serializer.Serialize(NwkAddrOfInterest, ZclDataType.Get(DataType.NWK_ADDRESS));
            serializer.Serialize(RequestType, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            serializer.Serialize(StartIndex, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
        }

        public override void Deserialize(ZclFieldDeserializer deserializer)
        {
            base.Deserialize(deserializer);

            NwkAddrOfInterest = (int)deserializer.Deserialize(ZclDataType.Get(DataType.NWK_ADDRESS));
            RequestType = (int)deserializer.Deserialize(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            StartIndex = (int)deserializer.Deserialize(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
        }

        public bool IsTransactionMatch(ZigBeeCommand request, ZigBeeCommand response)
        {
            if (!(response is IeeeAddressResponse)) {
                return false;
            }

            return ((IeeeAddressRequest)request).NwkAddrOfInterest.Equals(((IeeeAddressResponse)response).NwkAddrRemoteDev);
        }

    public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append("IeeeAddressRequest [")
                   .Append(base.ToString())
                   .Append(", nwkAddrOfInterest=")
                   .Append(NwkAddrOfInterest)
                   .Append(", requestType=")
                   .Append(RequestType)
                   .Append(", startIndex=")
                   .Append(StartIndex)
                   .Append(']');

            return builder.ToString();
        }

    }
}
