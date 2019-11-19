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
    /// Set Low Credit Warning Level value object class.
    ///
    /// Cluster: Prepayment. Command ID 0x09 is sent TO the server.
    /// This command is a specific command used for the Prepayment cluster.
    ///
    /// FIXME: This command is sent from client to a Prepayment server to set the warning level
    /// for low credit.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class SetLowCreditWarningLevel : ZclCommand
    {
        /// <summary>
        /// The cluster ID to which this command belongs.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0705;

        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x09;

        /// <summary>
        /// Low Credit Warning Level command message field.
        /// </summary>
        public uint LowCreditWarningLevel { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public SetLowCreditWarningLevel()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(LowCreditWarningLevel, ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            LowCreditWarningLevel = deserializer.Deserialize<uint>(ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("SetLowCreditWarningLevel [");
            builder.Append(base.ToString());
            builder.Append(", LowCreditWarningLevel=");
            builder.Append(LowCreditWarningLevel);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
