using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.Transaction;
using ZigBeeNet.ZCL;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.ZDO.Command
{
    /// <summary>
    /// Node Descriptor Request value object class.
    /// 
    /// The Node_Desc_req command is generated from a local device wishing to inquire
    /// as to the node descriptor of a remote device. This command shall be unicast either
    /// to the remote device itself or to an alternative device that contains the discovery
    /// information of the remote device.
    /// 
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class NodeDescriptorRequest : ZdoRequest, IZigBeeTransactionMatcher
    {
        /// <summary>
        /// DestinationAddress command message field.
        /// </summary>
        public IZigBeeAddress Destination
        {
            get => base.DestinationAddress;
            set => base.DestinationAddress = value;
        }

        /// <summary>
        /// NWKAddrOfInterest command message field.
        /// </summary>
        public ushort NwkAddrOfInterest { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public NodeDescriptorRequest()
        {
            ClusterId = 0x0002;
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
            if (!(response is NodeDescriptorResponse))
            {
                return false;
            }

            return ((NodeDescriptorRequest)request).NwkAddrOfInterest.Equals(((NodeDescriptorResponse)response).NwkAddrOfInterest);
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append("NodeDescriptorRequest [")
                   .Append(base.ToString())
                   .Append(", nwkAddrOfInterest=")
                   .Append(NwkAddrOfInterest)
                   .Append(']');

            return builder.ToString();
        }

    }
}
