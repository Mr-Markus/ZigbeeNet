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
    /// Gp Proxy Table Request value object class.
    ///
    /// Cluster: Green Power. Command ID 0x0B is sent FROM the server.
    /// This command is a specific command used for the Green Power cluster.
    ///
    /// To request selected Proxy Table entries, by index or by GPD ID.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class GpProxyTableRequest : ZclCommand
    {
        /// <summary>
        /// The cluster ID to which this command belongs.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0021;

        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x0B;

        /// <summary>
        /// Options command message field.
        /// </summary>
        public byte Options { get; set; }

        /// <summary>
        /// Gpd Src ID command message field.
        /// </summary>
        public uint GpdSrcId { get; set; }

        /// <summary>
        /// Gpd IEEE command message field.
        /// </summary>
        public IeeeAddress GpdIeee { get; set; }

        /// <summary>
        /// Endpoint command message field.
        /// </summary>
        public byte Endpoint { get; set; }

        /// <summary>
        /// Index command message field.
        /// </summary>
        public byte Index { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public GpProxyTableRequest()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
            CommandDirection = ZclCommandDirection.SERVER_TO_CLIENT;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(Options, DataType.BITMAP_8_BIT);
            serializer.Serialize(GpdSrcId, DataType.UNSIGNED_32_BIT_INTEGER);
            serializer.Serialize(GpdIeee, DataType.IEEE_ADDRESS);
            serializer.Serialize(Endpoint, DataType.UNSIGNED_8_BIT_INTEGER);
            serializer.Serialize(Index, DataType.UNSIGNED_8_BIT_INTEGER);
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            Options = deserializer.Deserialize<byte>(DataType.BITMAP_8_BIT);
            GpdSrcId = deserializer.Deserialize<uint>(DataType.UNSIGNED_32_BIT_INTEGER);
            GpdIeee = deserializer.Deserialize<IeeeAddress>(DataType.IEEE_ADDRESS);
            Endpoint = deserializer.Deserialize<byte>(DataType.UNSIGNED_8_BIT_INTEGER);
            Index = deserializer.Deserialize<byte>(DataType.UNSIGNED_8_BIT_INTEGER);
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("GpProxyTableRequest [");
            builder.Append(base.ToString());
            builder.Append(", Options=");
            builder.Append(Options);
            builder.Append(", GpdSrcId=");
            builder.Append(GpdSrcId);
            builder.Append(", GpdIeee=");
            builder.Append(GpdIeee);
            builder.Append(", Endpoint=");
            builder.Append(Endpoint);
            builder.Append(", Index=");
            builder.Append(Index);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
