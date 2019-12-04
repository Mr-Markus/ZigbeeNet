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
    /// Complex Descriptor Request value object class.
    ///
    ///
    /// The Complex_Desc_req command is generated from a local device wishing to inquire as to
    /// the complex descriptor of a remote device. This command shall be unicast either to the
    /// remote device itself or to an alternative device that contains the discovery
    /// information of the remote device.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class ComplexDescriptorRequest : ZdoRequest, IZigBeeTransactionMatcher
    {
        /// <summary>
        /// The ZDO cluster ID.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0010;

        /// <summary>
        /// NWK Addr Of Interest command message field.
        /// </summary>
        public ushort NwkAddrOfInterest { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ComplexDescriptorRequest()
        {
            ClusterId = CLUSTER_ID;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            base.Serialize(serializer);

            serializer.Serialize(NwkAddrOfInterest, ZclDataType.Get(DataType.NWK_ADDRESS));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            base.Deserialize(deserializer);

            NwkAddrOfInterest = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.NWK_ADDRESS));
        }

        public bool IsTransactionMatch(ZigBeeCommand request, ZigBeeCommand response)
        {
            if (!(response is ComplexDescriptorResponse))
            {
                return false;
            }

            return (((ComplexDescriptorRequest) request).NwkAddrOfInterest.Equals(((ComplexDescriptorResponse) response).NwkAddrOfInterest));
         }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("ComplexDescriptorRequest [");
            builder.Append(base.ToString());
            builder.Append(", NwkAddrOfInterest=");
            builder.Append(NwkAddrOfInterest);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
