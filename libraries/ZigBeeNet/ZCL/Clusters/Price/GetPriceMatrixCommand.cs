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
    /// Get Price Matrix Command value object class.
    ///
    /// Cluster: Price. Command ID 0x07 is sent TO the server.
    /// This command is a specific command used for the Price cluster.
    ///
    /// This command initiates a PublishPriceMatrix command for the scheduled Price Matrix
    /// updates. A server device shall be capable of storing at least two instances, current and
    /// next instance to be activated in the future. <br> A ZCL Default response with status
    /// NOT_FOUND shall be returned if there are no Price Matrix updates available.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class GetPriceMatrixCommand : ZclCommand
    {
        /// <summary>
        /// The cluster ID to which this command belongs.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0700;

        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x07;

        /// <summary>
        /// Issuer Tariff ID command message field.
        /// 
        /// IssuerTariffID indicates the tariff to which the requested Price Matrix belongs.
        /// </summary>
        public uint IssuerTariffId { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public GetPriceMatrixCommand()
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

            builder.Append("GetPriceMatrixCommand [");
            builder.Append(base.ToString());
            builder.Append(", IssuerTariffId=");
            builder.Append(IssuerTariffId);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
