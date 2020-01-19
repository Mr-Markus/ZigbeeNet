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
    /// Network Address Request value object class.
    ///
    ///
    /// The NWK_addr_req is generated from a Local Device wishing to inquire as to the 16-bit
    /// address of the Remote Device based on its known IEEE address. The destination
    /// addressing on this command shall be unicast or broadcast to all devices for which
    /// macRxOnWhenIdle = TRUE.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class NetworkAddressRequest : ZdoRequest, IZigBeeTransactionMatcher
    {
        /// <summary>
        /// The ZDO cluster ID.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0000;

        /// <summary>
        /// IEEE Addr command message field.
        /// </summary>
        public IeeeAddress IeeeAddr { get; set; }

        /// <summary>
        /// Request Type command message field.
        /// 
        /// Request type for this command: 0x00 – Single device response 0x01 – Extended
        /// response 0x02-0xFF – reserved
        /// </summary>
        public byte RequestType { get; set; }

        /// <summary>
        /// Start Index command message field.
        /// </summary>
        public byte StartIndex { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public NetworkAddressRequest()
        {
            ClusterId = CLUSTER_ID;
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

            IeeeAddr = deserializer.Deserialize<IeeeAddress>(ZclDataType.Get(DataType.IEEE_ADDRESS));
            RequestType = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            StartIndex = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
        }

        public bool IsTransactionMatch(ZigBeeCommand request, ZigBeeCommand response)
        {
            if (!(response is NetworkAddressResponse))
            {
                return false;
            }

            return (((NetworkAddressRequest) request).IeeeAddr.Equals(((NetworkAddressResponse) response).IeeeAddrRemoteDev));
         }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("NetworkAddressRequest [");
            builder.Append(base.ToString());
            builder.Append(", IeeeAddr=");
            builder.Append(IeeeAddr);
            builder.Append(", RequestType=");
            builder.Append(RequestType);
            builder.Append(", StartIndex=");
            builder.Append(StartIndex);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
