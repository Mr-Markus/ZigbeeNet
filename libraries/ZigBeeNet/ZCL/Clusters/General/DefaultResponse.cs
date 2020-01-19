using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.Security;
using ZigBeeNet.ZCL.Clusters.General;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Protocol;


namespace ZigBeeNet.ZCL.Clusters.General
{
    /// <summary>
    /// Default Response value object class.
    ///
    /// Cluster: General. Command ID 0x0B is sent TO the server.
    /// This command is a generic command used across the profile.
    ///
    /// The default response command is generated when a device receives a unicast command,
    /// there is no other relevant response specified for the command, and either an error
    /// results or the Disable default response bit of its Frame control field is set to 0.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class DefaultResponse : ZclCommand
    {
        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x0B;

        /// <summary>
        /// Command Identifier command message field.
        /// </summary>
        public byte CommandIdentifier { get; set; }

        /// <summary>
        /// Status Code command message field.
        /// </summary>
        public ZclStatus StatusCode { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public DefaultResponse()
        {
            CommandId = COMMAND_ID;
            GenericCommand = true;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(CommandIdentifier, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            serializer.Serialize(StatusCode, ZclDataType.Get(DataType.ZCL_STATUS));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            CommandIdentifier = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            StatusCode = deserializer.Deserialize<ZclStatus>(ZclDataType.Get(DataType.ZCL_STATUS));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("DefaultResponse [");
            builder.Append(base.ToString());
            builder.Append(", CommandIdentifier=");
            builder.Append(CommandIdentifier);
            builder.Append(", StatusCode=");
            builder.Append(StatusCode);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
