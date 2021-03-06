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
    /// Complex Descriptor Response value object class.
    ///
    ///
    /// The Complex_Desc_rsp is generated by a remote device in response to a Complex_Desc_req
    /// directed to the remote device. This command shall be unicast to the originator of the
    /// Complex_Desc_req command.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class ComplexDescriptorResponse : ZdoResponse
    {
        /// <summary>
        /// The ZDO cluster ID.
        /// </summary>
        public const ushort CLUSTER_ID = 0x8010;

        /// <summary>
        /// NWK Addr Of Interest command message field.
        /// </summary>
        public ushort NwkAddrOfInterest { get; set; }

        /// <summary>
        /// Length command message field.
        /// </summary>
        public byte Length { get; set; }

        /// <summary>
        /// Complex Descriptor command message field.
        /// </summary>
        public ComplexDescriptor ComplexDescriptor { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ComplexDescriptorResponse()
        {
            ClusterId = CLUSTER_ID;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            base.Serialize(serializer);

            serializer.Serialize(Status, DataType.ZDO_STATUS);
            serializer.Serialize(NwkAddrOfInterest, DataType.NWK_ADDRESS);
            serializer.Serialize(Length, DataType.UNSIGNED_8_BIT_INTEGER);
            serializer.Serialize(ComplexDescriptor, DataType.COMPLEX_DESCRIPTOR);
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            base.Deserialize(deserializer);

            Status = deserializer.Deserialize<ZdoStatus>(DataType.ZDO_STATUS);
            if (Status != ZdoStatus.SUCCESS)
            {
                // Don't read the full response if we have an error
                return;
            }
            NwkAddrOfInterest = deserializer.Deserialize<ushort>(DataType.NWK_ADDRESS);
            Length = deserializer.Deserialize<byte>(DataType.UNSIGNED_8_BIT_INTEGER);
            ComplexDescriptor = deserializer.Deserialize<ComplexDescriptor>(DataType.COMPLEX_DESCRIPTOR);
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("ComplexDescriptorResponse [");
            builder.Append(base.ToString());
            builder.Append(", Status=");
            builder.Append(Status);
            builder.Append(", NwkAddrOfInterest=");
            builder.Append(NwkAddrOfInterest);
            builder.Append(", Length=");
            builder.Append(Length);
            builder.Append(", ComplexDescriptor=");
            builder.Append(ComplexDescriptor);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
