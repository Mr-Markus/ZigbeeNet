using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.Security;
using ZigBeeNet.ZCL.Clusters.KeyEstablishment;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Protocol;


namespace ZigBeeNet.ZCL.Clusters.KeyEstablishment
{
    /// <summary>
    /// Initiate Key Establishment Response value object class.
    ///
    /// Cluster: Key Establishment. Command ID 0x00 is sent FROM the server.
    /// This command is a specific command used for the Key Establishment cluster.
    ///
    /// The Initiate Key Establishment Response command allows a device to respond to a device
    /// requesting the initiation of key establishment with it. The sender will transmit its
    /// identity information and key establishment protocol information to the receiving
    /// device.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class InitiateKeyEstablishmentResponse : ZclCommand
    {
        /// <summary>
        /// The cluster ID to which this command belongs.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0800;

        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x00;

        /// <summary>
        /// Requested Key Establishment Suite command message field.
        /// 
        /// This will be the type of KeyEstablishmentSuite that the initiator has requested be
        /// used for the key establishment exchange. The device shall set a single bit in the
        /// bitmask indicating the requested suite, all other bits shall be set to zero.
        /// </summary>
        public ushort RequestedKeyEstablishmentSuite { get; set; }

        /// <summary>
        /// Ephemeral Data Generate Time command message field.
        /// 
        /// This value indicates approximately how long in seconds the responder device takes
        /// to generate the Ephemeral Data Response message. The valid range is 0x00 to 0xFE.
        /// </summary>
        public byte EphemeralDataGenerateTime { get; set; }

        /// <summary>
        /// Confirm Key Generate Time command message field.
        /// 
        /// This value indicates approximately how long the responder device will take in
        /// seconds to generate the Confirm Key Response message. The valid range is 0x00 to
        /// 0xFE.
        /// </summary>
        public byte ConfirmKeyGenerateTime { get; set; }

        /// <summary>
        /// Identity command message field.
        /// 
        /// For KeyEstablishmentSuite = 0x0001 (CBKE), the identity field shall be the block
        /// of Octets containing the implicit certificate CERTU .
        /// </summary>
        public ByteArray Identity { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public InitiateKeyEstablishmentResponse()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
            CommandDirection = ZclCommandDirection.SERVER_TO_CLIENT;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(RequestedKeyEstablishmentSuite, ZclDataType.Get(DataType.BITMAP_16_BIT));
            serializer.Serialize(EphemeralDataGenerateTime, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            serializer.Serialize(ConfirmKeyGenerateTime, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            serializer.Serialize(Identity, ZclDataType.Get(DataType.RAW_OCTET));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            RequestedKeyEstablishmentSuite = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.BITMAP_16_BIT));
            EphemeralDataGenerateTime = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            ConfirmKeyGenerateTime = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            Identity = deserializer.Deserialize<ByteArray>(ZclDataType.Get(DataType.RAW_OCTET));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("InitiateKeyEstablishmentResponse [");
            builder.Append(base.ToString());
            builder.Append(", RequestedKeyEstablishmentSuite=");
            builder.Append(RequestedKeyEstablishmentSuite);
            builder.Append(", EphemeralDataGenerateTime=");
            builder.Append(EphemeralDataGenerateTime);
            builder.Append(", ConfirmKeyGenerateTime=");
            builder.Append(ConfirmKeyGenerateTime);
            builder.Append(", Identity=");
            builder.Append(Identity);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
