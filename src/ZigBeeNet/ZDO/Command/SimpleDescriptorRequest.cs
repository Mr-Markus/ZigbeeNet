using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.Transaction;
using ZigBeeNet.ZCL;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.ZDO.Command
{
    /// <summary>
     /// Simple Descriptor Request value object class.
     /// 
     /// The Simple_Desc_req command is generated from a local device wishing to
     /// inquire as to the simple descriptor of a remote device on a specified endpoint. This
     /// command shall be unicast either to the remote device itself or to an alternative
     /// device that contains the discovery information of the remote device.
     /// </summary>
    public class SimpleDescriptorRequest : ZdoRequest, IZigBeeTransactionMatcher
    {
        /// <summary>
         /// NWKAddrOfInterest command message field.
         /// </summary>
        public ushort NwkAddrOfInterest { get; set; }

        /// <summary>
         /// Endpoint command message field.
         /// </summary>
        public byte Endpoint { get; set; }

        /// <summary>
         /// Default constructor.
         /// </summary>
        public SimpleDescriptorRequest()
        {
            ClusterId = 0x0004;
        }


        internal override void Serialize(ZclFieldSerializer serializer)
        {
            base.Serialize(serializer);

            serializer.Serialize(NwkAddrOfInterest, ZclDataType.Get(DataType.NWK_ADDRESS));
            serializer.Serialize(Endpoint, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
        }

        public void deserialize(ZclFieldDeserializer deserializer)
        {
            base.Deserialize(deserializer);

            NwkAddrOfInterest = (ushort)deserializer.Deserialize(ZclDataType.Get(DataType.NWK_ADDRESS));
            Endpoint = (byte)deserializer.Deserialize(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
        }

        public bool IsTransactionMatch(ZigBeeCommand request, ZigBeeCommand response)
        {
            if (!(response is SimpleDescriptorResponse)) {
                return false;
            }

            return ((SimpleDescriptorRequest)request).Endpoint.Equals(((SimpleDescriptorResponse)response).SimpleDescriptor.Endpoint) &&
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
