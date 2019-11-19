using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.Security;
using ZigBeeNet.ZCL.Clusters.Price;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Protocol;


namespace ZigBeeNet.ZCL.Clusters.Price
{
    /// <summary>
    /// Get Block Thresholds Command value object class.
    ///
    /// Cluster: Price. Command ID 0x08 is sent TO the server.
    /// This command is a specific command used for the Price cluster.
    ///
    /// This command initiates a PublishBlockThreshold command for the scheduled Block
    /// Threshold updates. A server device shall be capable of storing at least two instances,
    /// current and next instance to be activated in the future. <br> A ZCL Default response with
    /// status NOT_FOUND shall be returned if there are no Price Matrix updates available.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class GetBlockThresholdsCommand : ZclCommand
    {
        /// <summary>
        /// The cluster ID to which this command belongs.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0700;

        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x08;

        /// <summary>
        /// Issuer Tariff ID command message field.
        /// 
        /// IssuerTariffID indicates the tariff to which the requested Price Matrix belongs.
        /// </summary>
        public uint IssuerTariffId { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public GetBlockThresholdsCommand()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(IssuerTariffId, ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            IssuerTariffId = deserializer.Deserialize<uint>(ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("GetBlockThresholdsCommand [");
            builder.Append(base.ToString());
            builder.Append(", IssuerTariffId=");
            builder.Append(IssuerTariffId);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
