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
    /// Initiate Key Establishment Request Command value object class.
    ///
    /// Cluster: Key Establishment. Command ID 0x00 is sent TO the server.
    /// This command is a specific command used for the Key Establishment cluster.
    ///
    /// The Initiate Key Establishment Request command allows a device to initiate key
    /// establishment with another device. The sender will transmit its identity information
    /// and key establishment protocol information to the receiving device.
    /// If the device does not currently have the resources to respond to a key establishment
    /// request it shall send a Terminate Key Establishment command with the result value set to
    /// NO_RESOURCES and the Wait Time field shall be set to an approximation of the time that
    /// must pass before the device will have the resources to process a new Key Establishment
    /// Request.
    /// If the device can process this request, it shall check the Issuer field of the device's
    /// implicit certificate. If the Issuer field does not contain a value that corresponds to a
    /// known Certificate Authority, the device shall send a Terminate Key Establishment
    /// command with the result set to UNKNOWN_ISSUER.
    /// If the device accepts the request it shall send an Initiate Key Establishment Response
    /// command containing its own identity information. The device should verify the
    /// certificate belongs to the address that the device is communicating with. The binding
    /// between the identity of the communicating device and its address is verifiable using
    /// out-of-band method.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class InitiateKeyEstablishmentRequestCommand : ZclCommand
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
        /// Key Establishment Suite command message field.
        /// 
        /// This will be the type of Key Establishment that the initiator is requesting for the
        /// Key Establishment Cluster. For CBKE-ECMQV this will be 0x0001.
        /// </summary>
        public ushort KeyEstablishmentSuite { get; set; }

        /// <summary>
        /// Ephemeral Data Generate Time command message field.
        /// 
        /// This value indicates approximately how long the initiator device will take in
        /// seconds to generate the Ephemeral Data Request command. The valid range is 0x00 to
        /// 0xFE.
        /// </summary>
        public byte EphemeralDataGenerateTime { get; set; }

        /// <summary>
        /// Confirm Key Generate Time command message field.
        /// 
        /// This value indicates approximately how long the initiator device will take in
        /// seconds to generate the Confirm Key Request command. The valid range is 0x00 to
        /// 0xFE.
        /// </summary>
        public byte ConfirmKeyGenerateTime { get; set; }

        /// <summary>
        /// Identity command message field.
        /// 
        /// For KeyEstablishmentSuite = 0x0001 (CBKE), the identity field shall be the block
        /// of octets containing the implicit certificate CERTU.
        /// </summary>
        public ByteArray Identity { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public InitiateKeyEstablishmentRequestCommand()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(KeyEstablishmentSuite, ZclDataType.Get(DataType.BITMAP_16_BIT));
            serializer.Serialize(EphemeralDataGenerateTime, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            serializer.Serialize(ConfirmKeyGenerateTime, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            serializer.Serialize(Identity, ZclDataType.Get(DataType.RAW_OCTET));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            KeyEstablishmentSuite = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.BITMAP_16_BIT));
            EphemeralDataGenerateTime = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            ConfirmKeyGenerateTime = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            Identity = deserializer.Deserialize<ByteArray>(ZclDataType.Get(DataType.RAW_OCTET));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("InitiateKeyEstablishmentRequestCommand [");
            builder.Append(base.ToString());
            builder.Append(", KeyEstablishmentSuite=");
            builder.Append(KeyEstablishmentSuite);
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
