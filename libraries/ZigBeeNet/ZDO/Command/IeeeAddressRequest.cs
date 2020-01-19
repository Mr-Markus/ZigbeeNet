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
    /// IEEE Address Request value object class.
    ///
    ///
    /// The IEEE_addr_req is generated from a Local Device wishing to inquire as to the 64-bit
    /// IEEE address of the Remote Device based on their known 16-bit address. The destination
    /// addressing on this command shall be unicast.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class IeeeAddressRequest : ZdoRequest, IZigBeeTransactionMatcher
    {
        /// <summary>
        /// The ZDO cluster ID.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0001;

        /// <summary>
        /// NWK Addr Of Interest command message field.
        /// </summary>
        public ushort NwkAddrOfInterest { get; set; }

        /// <summary>
        /// Request Type command message field.
        /// </summary>
        public byte RequestType { get; set; }

        /// <summary>
        /// Start Index command message field.
        /// </summary>
        public byte StartIndex { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public IeeeAddressRequest()
        {
            ClusterId = CLUSTER_ID;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            base.Serialize(serializer);

            serializer.Serialize(NwkAddrOfInterest, ZclDataType.Get(DataType.NWK_ADDRESS));
            serializer.Serialize(RequestType, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            serializer.Serialize(StartIndex, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            base.Deserialize(deserializer);

            NwkAddrOfInterest = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.NWK_ADDRESS));
            RequestType = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            StartIndex = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
        }

        public bool IsTransactionMatch(ZigBeeCommand request, ZigBeeCommand response)
        {
            if (!(response is IeeeAddressResponse))
            {
                return false;
            }

            return (((IeeeAddressRequest) request).NwkAddrOfInterest.Equals(((IeeeAddressResponse) response).NwkAddrRemoteDev));
         }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("IeeeAddressRequest [");
            builder.Append(base.ToString());
            builder.Append(", NwkAddrOfInterest=");
            builder.Append(NwkAddrOfInterest);
            builder.Append(", RequestType=");
            builder.Append(RequestType);
            builder.Append(", StartIndex=");
            builder.Append(StartIndex);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
