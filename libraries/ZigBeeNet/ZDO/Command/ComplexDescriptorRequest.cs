using System;
using System.Text;
using ZigBeeNet.Transaction;
using ZigBeeNet.ZCL;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.ZDO.Command
{
    /// <summary>
    /// Complex Descriptor Request value object class.
    /// 
    /// The Complex_Desc_req command is generated from a local device wishing to
    /// inquire as to the complex descriptor of a remote device. This command shall be
    /// unicast either to the remote device itself or to an alternative device that contains
    /// the discovery information of the remote device.
    /// 
    /// </summary>
    public class ComplexDescriptorRequest : ZdoRequest, IZigBeeTransactionMatcher
    {
        /// <summary>
        /// NWKAddrOfInterest command message field.
/// </summary>
        public ushort NwkAddrOfInterest { get; set; }

        /// <summary>
        /// Default constructor.
/// </summary>
        public ComplexDescriptorRequest()
        {
            ClusterId = 0x0010;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            base.Serialize(serializer);

            serializer.Serialize(NwkAddrOfInterest, ZclDataType.Get(DataType.NWK_ADDRESS));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            base.Deserialize(deserializer);

            NwkAddrOfInterest = (ushort)deserializer.Deserialize(ZclDataType.Get(DataType.NWK_ADDRESS));
        }

        public bool IsTransactionMatch(ZigBeeCommand request, ZigBeeCommand response)
        {
            if (!(response is ComplexDescriptorResponse))
            {
                return false;
            }

            return (((ComplexDescriptorRequest)request).NwkAddrOfInterest.Equals(((ComplexDescriptorResponse)response).NwkAddrOfInterest));
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append("ComplexDescriptorRequest [")
                   .Append(base.ToString())
                   .Append(", nwkAddrOfInterest=")
                   .Append(NwkAddrOfInterest)
                   .Append(']');

            return builder.ToString();
        }

    }
}
