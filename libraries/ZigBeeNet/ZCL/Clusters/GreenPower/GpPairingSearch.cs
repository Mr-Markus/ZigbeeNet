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
    /// Gp Pairing Search value object class.
    ///
    /// Cluster: Green Power. Command ID 0x01 is sent TO the server.
    /// This command is a specific command used for the Green Power cluster.
    ///
    /// The GP Pairing Search command is generated when the proxy needs to discover pairing
    /// information for a particular GPD.
    /// On receipt of this command, the device is informed about a proxy requesting pairing
    /// information on particular GPD.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class GpPairingSearch : ZclCommand
    {
        /// <summary>
        /// The cluster ID to which this command belongs.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0021;

        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x01;

        /// <summary>
        /// Options command message field.
        /// </summary>
        public ushort Options { get; set; }

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
        /// Default constructor.
        /// </summary>
        public GpPairingSearch()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(Options, ZclDataType.Get(DataType.BITMAP_16_BIT));
            serializer.Serialize(GpdSrcId, ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER));
            serializer.Serialize(GpdIeee, ZclDataType.Get(DataType.IEEE_ADDRESS));
            serializer.Serialize(Endpoint, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            Options = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.BITMAP_16_BIT));
            GpdSrcId = deserializer.Deserialize<uint>(ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER));
            GpdIeee = deserializer.Deserialize<IeeeAddress>(ZclDataType.Get(DataType.IEEE_ADDRESS));
            Endpoint = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("GpPairingSearch [");
            builder.Append(base.ToString());
            builder.Append(", Options=");
            builder.Append(Options);
            builder.Append(", GpdSrcId=");
            builder.Append(GpdSrcId);
            builder.Append(", GpdIeee=");
            builder.Append(GpdIeee);
            builder.Append(", Endpoint=");
            builder.Append(Endpoint);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
