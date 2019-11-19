using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.Security;
using ZigBeeNet.ZCL.Clusters.Thermostat;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Protocol;


namespace ZigBeeNet.ZCL.Clusters.Thermostat
{
    /// <summary>
    /// Setpoint Raise/Lower Command value object class.
    ///
    /// Cluster: Thermostat. Command ID 0x00 is sent TO the server.
    /// This command is a specific command used for the Thermostat cluster.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class SetpointRaiseLowerCommand : ZclCommand
    {
        /// <summary>
        /// The cluster ID to which this command belongs.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0201;

        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x00;

        /// <summary>
        /// Mode command message field.
        /// </summary>
        public byte Mode { get; set; }

        /// <summary>
        /// Amount command message field.
        /// </summary>
        public sbyte Amount { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public SetpointRaiseLowerCommand()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(Mode, ZclDataType.Get(DataType.ENUMERATION_8_BIT));
            serializer.Serialize(Amount, ZclDataType.Get(DataType.SIGNED_8_BIT_INTEGER));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            Mode = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.ENUMERATION_8_BIT));
            Amount = deserializer.Deserialize<sbyte>(ZclDataType.Get(DataType.SIGNED_8_BIT_INTEGER));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("SetpointRaiseLowerCommand [");
            builder.Append(base.ToString());
            builder.Append(", Mode=");
            builder.Append(Mode);
            builder.Append(", Amount=");
            builder.Append(Amount);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
