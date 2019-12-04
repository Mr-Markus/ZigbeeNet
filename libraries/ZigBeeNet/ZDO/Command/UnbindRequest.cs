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
    /// Unbind Request value object class.
    ///
    ///
    /// The Unbind_req is generated from a Local Device wishing to remove a Binding Table entry
    /// for the source and destination addresses contained as parameters. The destination
    /// addressing on this command shall be unicast only and the destination address must be
    /// that of the a Primary binding table cache or the SrcAddress.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class UnbindRequest : ZdoRequest, IZigBeeTransactionMatcher
    {
        /// <summary>
        /// The ZDO cluster ID.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0022;

        /// <summary>
        /// Src Address command message field.
        /// 
        /// The IEEE address for the source.
        /// </summary>
        public IeeeAddress SrcAddress { get; set; }

        /// <summary>
        /// Src Endpoint command message field.
        /// 
        /// The source endpoint for the binding entry.
        /// </summary>
        public byte SrcEndpoint { get; set; }

        /// <summary>
        /// Bind Cluster command message field.
        /// 
        /// The identifier of the cluster on the source device that is bound to the destination.
        /// </summary>
        public ushort BindCluster { get; set; }

        /// <summary>
        /// DST Addr Mode command message field.
        /// 
        /// The addressing mode for the destination address used in this command. This field
        /// can take one of the non-reserved values from the following list: 0x00 = reserved
        /// 0x01 = 16-bit group address for DstAddress and DstEndp not present 0x02 = reserved
        /// 0x03 = 64-bit extended address for DstAddress and DstEndp present 0x04 – 0xff =
        /// reserved
        /// </summary>
        public byte DstAddrMode { get; set; }

        /// <summary>
        /// DST Address command message field.
        /// 
        /// The destination address for the binding entry.
        /// </summary>
        public IeeeAddress DstAddress { get; set; }

        /// <summary>
        /// DST Endpoint command message field.
        /// 
        /// This field shall be present only if the DstAddrMode field has a value of 0x03 and, if
        /// present, shall be the destination endpoint for the binding entry.
        /// </summary>
        public byte DstEndpoint { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public UnbindRequest()
        {
            ClusterId = CLUSTER_ID;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            base.Serialize(serializer);

            serializer.Serialize(SrcAddress, ZclDataType.Get(DataType.IEEE_ADDRESS));
            serializer.Serialize(SrcEndpoint, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            serializer.Serialize(BindCluster, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            serializer.Serialize(DstAddrMode, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            serializer.Serialize(DstAddress, ZclDataType.Get(DataType.IEEE_ADDRESS));
            serializer.Serialize(DstEndpoint, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            base.Deserialize(deserializer);

            SrcAddress = deserializer.Deserialize<IeeeAddress>(ZclDataType.Get(DataType.IEEE_ADDRESS));
            SrcEndpoint = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            BindCluster = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            DstAddrMode = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            DstAddress = deserializer.Deserialize<IeeeAddress>(ZclDataType.Get(DataType.IEEE_ADDRESS));
            DstEndpoint = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
        }

        public bool IsTransactionMatch(ZigBeeCommand request, ZigBeeCommand response)
        {
            return (response is UnbindResponse) && ((ZdoRequest) request).DestinationAddress.Equals(((UnbindResponse) response).SourceAddress);
         }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("UnbindRequest [");
            builder.Append(base.ToString());
            builder.Append(", SrcAddress=");
            builder.Append(SrcAddress);
            builder.Append(", SrcEndpoint=");
            builder.Append(SrcEndpoint);
            builder.Append(", BindCluster=");
            builder.Append(BindCluster);
            builder.Append(", DstAddrMode=");
            builder.Append(DstAddrMode);
            builder.Append(", DstAddress=");
            builder.Append(DstAddress);
            builder.Append(", DstEndpoint=");
            builder.Append(DstEndpoint);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
