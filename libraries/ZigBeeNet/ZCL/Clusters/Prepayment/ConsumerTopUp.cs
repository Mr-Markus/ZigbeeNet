using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.Security;
using ZigBeeNet.ZCL.Clusters.Prepayment;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Protocol;


namespace ZigBeeNet.ZCL.Clusters.Prepayment
{
    /// <summary>
    /// Consumer Top Up value object class.
    ///
    /// Cluster: Prepayment. Command ID 0x04 is sent TO the server.
    /// This command is a specific command used for the Prepayment cluster.
    ///
    /// FIXME: The ConsumerTopUp command is used by the IPD and the ESI as a method of applying
    /// credit top up values to the prepayment meter.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class ConsumerTopUp : ZclCommand
    {
        /// <summary>
        /// The cluster ID to which this command belongs.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0705;

        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x04;

        /// <summary>
        /// Originating Device command message field.
        /// </summary>
        public byte OriginatingDevice { get; set; }

        /// <summary>
        /// Top Up Code command message field.
        /// </summary>
        public ByteArray TopUpCode { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ConsumerTopUp()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(OriginatingDevice, ZclDataType.Get(DataType.ENUMERATION_8_BIT));
            serializer.Serialize(TopUpCode, ZclDataType.Get(DataType.OCTET_STRING));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            OriginatingDevice = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.ENUMERATION_8_BIT));
            TopUpCode = deserializer.Deserialize<ByteArray>(ZclDataType.Get(DataType.OCTET_STRING));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("ConsumerTopUp [");
            builder.Append(base.ToString());
            builder.Append(", OriginatingDevice=");
            builder.Append(OriginatingDevice);
            builder.Append(", TopUpCode=");
            builder.Append(TopUpCode);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
