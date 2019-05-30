using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.Transaction;
using ZigBeeNet.ZCL;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.ZDO.Command
{
    /// <summary>
     /// Power Descriptor Request value object class.
     /// The Power_Desc_req command is generated from a local device wishing to
     /// inquire as to the power descriptor of a remote device. This command shall be
     /// unicast either to the remote device itself or to an alternative device that contains
     /// the discovery information of the remote device.
     /// 
     /// Code is auto-generated. Modifications may be overwritten!
     /// </summary>
    public class PowerDescriptorRequest : ZdoCommand, IZigBeeTransactionMatcher
    {
        /// <summary>
         /// NWKAddrOfInterest command message field.
        /// </summary>
        public ushort NwkAddrOfInterest { get; set; }

        /// <summary>
         /// Default constructor.
         /// </summary>
        public PowerDescriptorRequest()
        {
            ClusterId = 0x0003;
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
            if (response is PowerDescriptorResponse) {
                return (((PowerDescriptorRequest)request).NwkAddrOfInterest.Equals(((PowerDescriptorResponse)response).NwkAddrOfInterest));
            }

            return false;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append("PowerDescriptorRequest [")
                   .Append(base.ToString())
                   .Append(", nwkAddrOfInterest=")
                   .Append(NwkAddrOfInterest)
                   .Append(']');

            return builder.ToString();
        }
    }
}
