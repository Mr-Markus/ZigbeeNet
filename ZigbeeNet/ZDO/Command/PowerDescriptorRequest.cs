using System;
using System.Collections.Generic;
using System.Text;
using ZigbeeNet.ZCL;
using ZigbeeNet.ZCL.Protocol;

namespace ZigbeeNet.ZDO.Command
{
    /**
     * Power Descriptor Request value object class.
     * The Power_Desc_req command is generated from a local device wishing to
     * inquire as to the power descriptor of a remote device. This command shall be
     * unicast either to the remote device itself or to an alternative device that contains
     * the discovery information of the remote device.
     * <p>
     * Code is auto-generated. Modifications may be overwritten!
     */
    public class PowerDescriptorRequest : ZdoCommand, IZigBeeTransactionMatcher
    {
        /**
     * NWKAddrOfInterest command message field.
     */
        private ushort NwkAddrOfInterest { get; set; }

        /**
         * Default constructor.
         */
        public PowerDescriptorRequest()
        {
            ClusterId = 0x0003;
        }

        public override void Serialize(ZclFieldSerializer serializer)
        {
            base.Serialize(serializer);

            serializer.Serialize(NwkAddrOfInterest, ZclDataType.Get(DataType.NWK_ADDRESS));
        }


        public override void Deserialize(ZclFieldDeserializer deserializer)
        {
            base.Deserialize(deserializer);

            NwkAddrOfInterest = (ushort)deserializer.Deserialize(ZclDataType.Get(DataType.NWK_ADDRESS));
        }


        public bool isTransactionMatch(ZigBeeCommand request, ZigBeeCommand response)
        {
            if (response is PowerDescriptorResponse)) {
                return (((PowerDescriptorRequest)request).NwkAddrOfInterest.Equals(((PowerDescriptorResponse)response).NwkAddrOfInterest));
            }
            return false;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder(62);
            builder.Append("PowerDescriptorRequest [");
            builder.Append(base.ToString());
            builder.Append(", nwkAddrOfInterest=");
            builder.Append(NwkAddrOfInterest);
            builder.Append(']');
            return builder.ToString();
        }
    }
}
