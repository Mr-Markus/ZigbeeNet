using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.Security;
using ZigBeeNet.ZCL.Clusters.RssiLocation;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Protocol;


namespace ZigBeeNet.ZCL.Clusters.RssiLocation
{
    /// <summary>
    /// Get Location Data Command value object class.
    ///
    /// Cluster: RSSI Location. Command ID 0x03 is sent TO the server.
    /// This command is a specific command used for the RSSI Location cluster.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class GetLocationDataCommand : ZclCommand
    {
        /// <summary>
        /// The cluster ID to which this command belongs.
        /// </summary>
        public const ushort CLUSTER_ID = 0x000B;

        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x03;

        /// <summary>
        /// Header command message field.
        /// </summary>
        public byte Header { get; set; }

        /// <summary>
        /// Number Responses command message field.
        /// </summary>
        public byte NumberResponses { get; set; }

        /// <summary>
        /// Target Address command message field.
        /// </summary>
        public IeeeAddress TargetAddress { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public GetLocationDataCommand()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(Header, ZclDataType.Get(DataType.BITMAP_8_BIT));
            serializer.Serialize(NumberResponses, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            serializer.Serialize(TargetAddress, ZclDataType.Get(DataType.IEEE_ADDRESS));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            Header = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.BITMAP_8_BIT));
            NumberResponses = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            TargetAddress = deserializer.Deserialize<IeeeAddress>(ZclDataType.Get(DataType.IEEE_ADDRESS));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("GetLocationDataCommand [");
            builder.Append(base.ToString());
            builder.Append(", Header=");
            builder.Append(Header);
            builder.Append(", NumberResponses=");
            builder.Append(NumberResponses);
            builder.Append(", TargetAddress=");
            builder.Append(TargetAddress);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
