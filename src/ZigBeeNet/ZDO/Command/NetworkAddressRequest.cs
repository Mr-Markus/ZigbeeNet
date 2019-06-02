using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.Transaction;
using ZigBeeNet.ZCL;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.ZDO.Command
{
    /// <summary>
     /// Network Address Request value object class.
     /// <p>
     /// The NWK_addr_req is generated from a Local Device wishing to inquire as to the
     /// 16-bit address of the Remote Device based on its known IEEE address. The
     /// destination addressing on this command shall be unicast or broadcast to all
     /// devices for which macRxOnWhenIdle = TRUE.
     /// </summary>
    public class NetworkAddressRequest : ZdoRequest, IZigBeeTransactionMatcher
    {
        /// <summary>
         /// IEEEAddr command message field.
         /// </summary>
        public IeeeAddress IeeeAddr { get; set; }

        /// <summary>
         /// RequestType command message field.
         /// 
         /// Request type for this command:
         /// 0x00 – Single device response
         /// 0x01 – Extended response
         /// 0x02-0xFF – reserved
         /// </summary>
        public byte RequestType { get; set; }

        /// <summary>
         /// StartIndex command message field.
         /// </summary>
        public byte StartIndex { get; set; }

        /// <summary>
         /// Default constructor.
         /// </summary>
        public NetworkAddressRequest()
        {
            ClusterId = 0x0000;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            base.Serialize(serializer);

            serializer.Serialize(IeeeAddr, ZclDataType.Get(DataType.IEEE_ADDRESS));
            serializer.Serialize(RequestType, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            serializer.Serialize(StartIndex, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            base.Deserialize(deserializer);

            IeeeAddr = (IeeeAddress)deserializer.Deserialize(ZclDataType.Get(DataType.IEEE_ADDRESS));
            RequestType = (byte)deserializer.Deserialize(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            StartIndex = (byte)deserializer.Deserialize(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
        }

        public bool IsTransactionMatch(ZigBeeCommand request, ZigBeeCommand response)
        {
            if (!(response is NetworkAddressResponse)) {
                return false;
            }

            return ((NetworkAddressRequest)request).IeeeAddr.Equals(((NetworkAddressResponse)response).IeeeAddrRemoteDev);
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append("NetworkAddressRequest [")
                   .Append(base.ToString())
                   .Append(", ieeeAddr=")
                   .Append(IeeeAddr)
                   .Append(", requestType=")
                   .Append(RequestType)
                   .Append(", startIndex=")
                   .Append(StartIndex)
                   .Append(']');

            return builder.ToString();
        }

    }
}
