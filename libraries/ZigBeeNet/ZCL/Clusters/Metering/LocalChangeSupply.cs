using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.Security;
using ZigBeeNet.ZCL.Clusters.Metering;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Protocol;


namespace ZigBeeNet.ZCL.Clusters.Metering
{
    /// <summary>
    /// Local Change Supply value object class.
    ///
    /// Cluster: Metering. Command ID 0x0C is sent TO the server.
    /// This command is a specific command used for the Metering cluster.
    ///
    /// This command is a simplified version of the ChangeSupply command, intended to be sent
    /// from an IHD to a meter as the consequence of a user action on the IHD. Its purpose is to
    /// provide a local disconnection/reconnection button on the IHD in addition to the one on
    /// the meter.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class LocalChangeSupply : ZclCommand
    {
        /// <summary>
        /// The cluster ID to which this command belongs.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0702;

        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x0C;

        /// <summary>
        /// Proposed Supply Status command message field.
        /// 
        /// An 8-bit enumeration field indicating the status of the energy supply controlled
        /// by the Metering Device following implementation of this command.
        /// </summary>
        public byte ProposedSupplyStatus { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public LocalChangeSupply()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(ProposedSupplyStatus, ZclDataType.Get(DataType.ENUMERATION_8_BIT));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            ProposedSupplyStatus = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.ENUMERATION_8_BIT));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("LocalChangeSupply [");
            builder.Append(base.ToString());
            builder.Append(", ProposedSupplyStatus=");
            builder.Append(ProposedSupplyStatus);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
