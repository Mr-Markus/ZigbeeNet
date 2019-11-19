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
    /// Gp Response value object class.
    ///
    /// Cluster: Green Power. Command ID 0x06 is sent FROM the server.
    /// This command is a specific command used for the Green Power cluster.
    ///
    /// This command is generated when sink requests to send any information to a specific GPD
    /// with Rx capability.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class GpResponse : ZclCommand
    {
        /// <summary>
        /// The cluster ID to which this command belongs.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0021;

        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x06;

        /// <summary>
        /// Options command message field.
        /// </summary>
        public byte Options { get; set; }

        /// <summary>
        /// Temp Master Short Address command message field.
        /// </summary>
        public ushort TempMasterShortAddress { get; set; }

        /// <summary>
        /// Temp Master Tx Channel command message field.
        /// </summary>
        public byte TempMasterTxChannel { get; set; }

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
        /// Gpd Command ID command message field.
        /// </summary>
        public byte GpdCommandId { get; set; }

        /// <summary>
        /// Gpd Command Payload command message field.
        /// </summary>
        public ByteArray GpdCommandPayload { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public GpResponse()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
            CommandDirection = ZclCommandDirection.SERVER_TO_CLIENT;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(Options, ZclDataType.Get(DataType.BITMAP_8_BIT));
            serializer.Serialize(TempMasterShortAddress, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            serializer.Serialize(TempMasterTxChannel, ZclDataType.Get(DataType.BITMAP_8_BIT));
            serializer.Serialize(GpdSrcId, ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER));
            serializer.Serialize(GpdIeee, ZclDataType.Get(DataType.IEEE_ADDRESS));
            serializer.Serialize(Endpoint, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            serializer.Serialize(GpdCommandId, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            serializer.Serialize(GpdCommandPayload, ZclDataType.Get(DataType.OCTET_STRING));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            Options = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.BITMAP_8_BIT));
            TempMasterShortAddress = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            TempMasterTxChannel = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.BITMAP_8_BIT));
            GpdSrcId = deserializer.Deserialize<uint>(ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER));
            GpdIeee = deserializer.Deserialize<IeeeAddress>(ZclDataType.Get(DataType.IEEE_ADDRESS));
            Endpoint = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            GpdCommandId = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            GpdCommandPayload = deserializer.Deserialize<ByteArray>(ZclDataType.Get(DataType.OCTET_STRING));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("GpResponse [");
            builder.Append(base.ToString());
            builder.Append(", Options=");
            builder.Append(Options);
            builder.Append(", TempMasterShortAddress=");
            builder.Append(TempMasterShortAddress);
            builder.Append(", TempMasterTxChannel=");
            builder.Append(TempMasterTxChannel);
            builder.Append(", GpdSrcId=");
            builder.Append(GpdSrcId);
            builder.Append(", GpdIeee=");
            builder.Append(GpdIeee);
            builder.Append(", Endpoint=");
            builder.Append(Endpoint);
            builder.Append(", GpdCommandId=");
            builder.Append(GpdCommandId);
            builder.Append(", GpdCommandPayload=");
            builder.Append(GpdCommandPayload);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
