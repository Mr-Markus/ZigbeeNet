using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.Transaction;
using ZigBeeNet.ZCL;
using ZigBeeNet.ZCL.Protocol;
using ZigBeeNet.ZDO.Field;


namespace ZigBeeNet.ZDO.Command
{
    /// <summary>
    /// Simple Descriptor Request value object class.
    ///
    ///
    /// The Simple_Desc_req command is generated from a local device wishing to inquire as to
    /// the simple descriptor of a remote device on a specified endpoint. This command shall be
    /// unicast either to the remote device itself or to an alternative device that contains the
    /// discovery information of the remote device.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class SimpleDescriptorRequest : ZdoRequest, IZigBeeTransactionMatcher
    {
        /// <summary>
        /// The ZDO cluster ID.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0004;

        /// <summary>
        /// NWK Addr Of Interest command message field.
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
            ClusterId = CLUSTER_ID;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            base.Serialize(serializer);

            serializer.Serialize(NwkAddrOfInterest, ZclDataType.Get(DataType.NWK_ADDRESS));
            serializer.Serialize(Endpoint, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            base.Deserialize(deserializer);

            NwkAddrOfInterest = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.NWK_ADDRESS));
            Endpoint = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
        }

        public bool IsTransactionMatch(ZigBeeCommand request, ZigBeeCommand response)
        {
            if (!(response is SimpleDescriptorResponse))
            {
                return false;
            }

            return (((SimpleDescriptorRequest) request).Endpoint.Equals(((SimpleDescriptorResponse) response).SimpleDescriptor.Endpoint))
                    && (((SimpleDescriptorRequest) request).NwkAddrOfInterest.Equals(((SimpleDescriptorResponse) response).NwkAddrOfInterest));
         }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("SimpleDescriptorRequest [");
            builder.Append(base.ToString());
            builder.Append(", NwkAddrOfInterest=");
            builder.Append(NwkAddrOfInterest);
            builder.Append(", Endpoint=");
            builder.Append(Endpoint);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
