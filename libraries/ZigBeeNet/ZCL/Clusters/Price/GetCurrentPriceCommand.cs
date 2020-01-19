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
    /// Get Current Price Command value object class.
    ///
    /// Cluster: Price. Command ID 0x00 is sent TO the server.
    /// This command is a specific command used for the Price cluster.
    ///
    /// This command initiates a PublishPrice command for the current time. On receipt of this
    /// command, the device shall send a PublishPrice command for the currently scheduled
    /// time.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class GetCurrentPriceCommand : ZclCommand
    {
        /// <summary>
        /// The cluster ID to which this command belongs.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0700;

        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x00;

        /// <summary>
        /// Command Options command message field.
        /// </summary>
        public byte CommandOptions { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public GetCurrentPriceCommand()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(CommandOptions, ZclDataType.Get(DataType.BITMAP_8_BIT));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            CommandOptions = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.BITMAP_8_BIT));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("GetCurrentPriceCommand [");
            builder.Append(base.ToString());
            builder.Append(", CommandOptions=");
            builder.Append(CommandOptions);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
