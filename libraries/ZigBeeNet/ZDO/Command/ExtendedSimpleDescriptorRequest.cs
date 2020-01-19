using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.ZCL;
using ZigBeeNet.ZCL.Protocol;
using ZigBeeNet.ZDO.Field;


namespace ZigBeeNet.ZDO.Command
{
    /// <summary>
    /// Extended Simple Descriptor Request value object class.
    ///
    ///
    /// The Extended_Simple_Desc_req command is generated from a local device wishing to
    /// inquire as to the simple descriptor of a remote device on a specified endpoint. This
    /// command shall be unicast either to the remote device itself or to an alternative device
    /// that contains the discovery information of the remote device. The
    /// Extended_Simple_Desc_req is intended for use with devices which employ a larger
    /// number of application input or output clusters than can be described by the
    /// Simple_Desc_req.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class ExtendedSimpleDescriptorRequest : ZdoRequest
    {
        /// <summary>
        /// The ZDO cluster ID.
        /// </summary>
        public const ushort CLUSTER_ID = 0x001D;

        /// <summary>
        /// NWK Addr Of Interest command message field.
        /// </summary>
        public ushort NwkAddrOfInterest { get; set; }

        /// <summary>
        /// Endpoint command message field.
        /// </summary>
        public byte Endpoint { get; set; }

        /// <summary>
        /// Start Index command message field.
        /// </summary>
        public byte StartIndex { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ExtendedSimpleDescriptorRequest()
        {
            ClusterId = CLUSTER_ID;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            base.Serialize(serializer);

            serializer.Serialize(NwkAddrOfInterest, ZclDataType.Get(DataType.NWK_ADDRESS));
            serializer.Serialize(Endpoint, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            serializer.Serialize(StartIndex, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            base.Deserialize(deserializer);

            NwkAddrOfInterest = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.NWK_ADDRESS));
            Endpoint = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            StartIndex = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("ExtendedSimpleDescriptorRequest [");
            builder.Append(base.ToString());
            builder.Append(", NwkAddrOfInterest=");
            builder.Append(NwkAddrOfInterest);
            builder.Append(", Endpoint=");
            builder.Append(Endpoint);
            builder.Append(", StartIndex=");
            builder.Append(StartIndex);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
