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
    /// Confirm Key Response value object class.
    ///
    /// Cluster: Key Establishment. Command ID 0x02 is sent FROM the server.
    /// This command is a specific command used for the Key Establishment cluster.
    ///
    /// The Confirm Key Response command allows the responder to verify the initiator has
    /// derived the same secret key. This is done by sending the initiator a cryptographic hash
    /// generated using the keying material and the identities and ephemeral data of both
    /// parties.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class ConfirmKeyResponse : ZclCommand
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
        public ConfirmKeyResponse()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
            CommandDirection = ZclCommandDirection.SERVER_TO_CLIENT;
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

            builder.Append("ConfirmKeyResponse [");
            builder.Append(base.ToString());
            builder.Append(", SecureMessageAuthenticationCode=");
            builder.Append(SecureMessageAuthenticationCode);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
