using System;
using System.Text;
using ZigBeeNet.Transaction;
using ZigBeeNet.ZCL;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.ZDO.Command
{
    /**
   * Bind Request value object class.
   * 
   * The Bind_req is generated from a Local Device wishing to create a Binding Table
   * entry for the source and destination addresses contained as parameters. The
   * destination addressing on this command shall be unicast only, and the destination
   * address shall be that of a Primary binding table cache or to the SrcAddress itself.
   * The Binding Manager is optionally supported on the source device (unless that
   * device is also the ZigBee Coordinator) so that device shall issue a
   * NOT_SUPPORTED status to the Bind_req if not supported.
   */
    public class BindRequest : ZdoRequest, IZigBeeTransactionMatcher
    {
        /**
         * SrcAddress command message field.
         * 
         * The IEEE address for the source.
         */
        public IeeeAddress SrcAddress { get; set; }

        /**
         * SrcEndpoint command message field.
         * 
         * The source endpoint for the binding entry.
         */
        public int SrcEndpoint { get; set; }

        /**
         * BindCluster command message field.
         * 
         * The identifier of the cluster on the source device that is bound to the destination.
         */
        public int BindCluster { get; set; }

        /**
         * DstAddrMode command message field.
         * 
         * The addressing mode for the destination address used in this command. This field
         * can take one of the non-reserved values from the following list:
         * 0x00 = reserved
         * 0x01 = 16-bit group address for DstAddress and DstEndp not present
         * 0x02 = reserved
         * 0x03 = 64-bit extended address for DstAddress and DstEndp present
         * 0x04 � 0xff = reserved
         */
        public int DstAddrMode { get; set; }

        /**
         * DstAddress command message field.
         * 
         * The destination address for the binding entry.
         */
        public IeeeAddress DstAddress { get; set; }

        /**
         * DstEndpoint command message field.
         * 
         * This field shall be present only if the DstAddrMode field has a value of 0x03 and,
         * if present, shall be the destination endpoint for the binding entry.
         */
        public int DstEndpoint { get; set; }

        /**
         * Default constructor.
         */
        public BindRequest()
        {
            ClusterId = 0x0021;
        }

        public override void Serialize(ZclFieldSerializer serializer)
        {
            base.Serialize(serializer);

            serializer.Serialize(SrcAddress, ZclDataType.Get(DataType.IEEE_ADDRESS));
            serializer.Serialize(SrcEndpoint, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            serializer.Serialize(BindCluster, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            serializer.Serialize(DstAddrMode, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            serializer.Serialize(DstAddress, ZclDataType.Get(DataType.IEEE_ADDRESS));
            serializer.Serialize(DstEndpoint, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
        }

        public override void Deserialize(ZclFieldDeserializer deserializer)
        {
            base.Deserialize(deserializer);

            SrcAddress = (IeeeAddress)deserializer.Deserialize(ZclDataType.Get(DataType.IEEE_ADDRESS));
            SrcEndpoint = (int)deserializer.Deserialize(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            BindCluster = (int)deserializer.Deserialize(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            DstAddrMode = (int)deserializer.Deserialize(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            DstAddress = (IeeeAddress)deserializer.Deserialize(ZclDataType.Get(DataType.IEEE_ADDRESS));
            DstEndpoint = (int)deserializer.Deserialize(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
        }

        public bool IsTransactionMatch(ZigBeeCommand request, ZigBeeCommand response)
        {
            if (!(response is BindResponse)) {
                return false;
            }

            return ((ZdoRequest)request).DestinationAddress.Equals(((BindResponse)response).SourceAddress);
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append("BindRequest [")
                   .Append(base.ToString())
                   .Append(", srcAddress=")
                   .Append(SrcAddress)
                   .Append(", srcEndpoint=")
                   .Append(SrcEndpoint)
                   .Append(", bindCluster=")
                   .Append(BindCluster)
                   .Append(", dstAddrMode=")
                   .Append(DstAddrMode)
                   .Append(", dstAddress=")
                   .Append(DstAddress)
                   .Append(", dstEndpoint=")
                   .Append(DstEndpoint)
                   .Append(']');

            return builder.ToString();
        }

    }
}