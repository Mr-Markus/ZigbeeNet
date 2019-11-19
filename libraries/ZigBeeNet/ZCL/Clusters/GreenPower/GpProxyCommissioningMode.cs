using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.Security;
using ZigBeeNet.ZCL.Clusters.GreenPower;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Protocol;


namespace ZigBeeNet.ZCL.Clusters.GreenPower
{
    /// <summary>
    /// Gp Proxy Commissioning Mode value object class.
    ///
    /// Cluster: Green Power. Command ID 0x02 is sent FROM the server.
    /// This command is a specific command used for the Green Power cluster.
    ///
    /// This command is generated when the sink wishes to instruct the proxies to enter/exit
    /// commissioning mode. The GP Proxy Commissioning Mode command is typically sent using
    /// network-wide broadcast.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class GpProxyCommissioningMode : ZclCommand
    {
        /// <summary>
        /// The cluster ID to which this command belongs.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0021;

        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x02;

        /// <summary>
        /// Options command message field.
        /// </summary>
        public byte Options { get; set; }

        /// <summary>
        /// Commissioning Window command message field.
        /// </summary>
        public ushort CommissioningWindow { get; set; }

        /// <summary>
        /// Channel command message field.
        /// </summary>
        public byte Channel { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public GpProxyCommissioningMode()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
            CommandDirection = ZclCommandDirection.SERVER_TO_CLIENT;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(Options, ZclDataType.Get(DataType.BITMAP_8_BIT));
            serializer.Serialize(CommissioningWindow, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            serializer.Serialize(Channel, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            Options = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.BITMAP_8_BIT));
            CommissioningWindow = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            Channel = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("GpProxyCommissioningMode [");
            builder.Append(base.ToString());
            builder.Append(", Options=");
            builder.Append(Options);
            builder.Append(", CommissioningWindow=");
            builder.Append(CommissioningWindow);
            builder.Append(", Channel=");
            builder.Append(Channel);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
