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
    /// Confirm Key Data Request Command value object class.
    ///
    /// Cluster: Key Establishment. Command ID 0x02 is sent TO the server.
    /// This command is a specific command used for the Key Establishment cluster.
    ///
    /// The Confirm Key Request command allows the initiator sending device to confirm the key
    /// established with the responder receiving device based on performing a cryptographic
    /// hash using part of the generated keying material and the identities and ephemeral data
    /// of both parties.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class ConfirmKeyDataRequestCommand : ZclCommand
    {
        /// <summary>
        /// The cluster ID to which this command belongs.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0800;

        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x02;

        /// <summary>
        /// Secure Message Authentication Code command message field.
        /// </summary>
        public ByteArray SecureMessageAuthenticationCode { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ConfirmKeyDataRequestCommand()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(SecureMessageAuthenticationCode, ZclDataType.Get(DataType.RAW_OCTET));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            SecureMessageAuthenticationCode = deserializer.Deserialize<ByteArray>(ZclDataType.Get(DataType.RAW_OCTET));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("ConfirmKeyDataRequestCommand [");
            builder.Append(base.ToString());
            builder.Append(", SecureMessageAuthenticationCode=");
            builder.Append(SecureMessageAuthenticationCode);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
