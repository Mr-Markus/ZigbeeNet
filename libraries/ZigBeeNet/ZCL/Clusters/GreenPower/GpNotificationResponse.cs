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
    /// Gp Notification Response value object class.
    ///
    /// Cluster: Green Power. Command ID 0x00 is sent FROM the server.
    /// This command is a specific command used for the Green Power cluster.
    ///
    /// This command is generated when the sink acknowledges the reception of full unicast GP
    /// Notification command. The GP Notification Response command is sent in unicast to the
    /// originating proxy.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class GpNotificationResponse : ZclCommand
    {
        /// <summary>
        /// The cluster ID to which this command belongs.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0021;

        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x00;

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
        /// Gpd Security Frame Counter command message field.
        /// </summary>
        public uint GpdSecurityFrameCounter { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public GpNotificationResponse()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
            CommandDirection = ZclCommandDirection.SERVER_TO_CLIENT;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(Options, ZclDataType.Get(DataType.BITMAP_8_BIT));
            serializer.Serialize(GpdSrcId, ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER));
            serializer.Serialize(GpdIeee, ZclDataType.Get(DataType.IEEE_ADDRESS));
            serializer.Serialize(GpdSecurityFrameCounter, ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            Options = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.BITMAP_8_BIT));
            GpdSrcId = deserializer.Deserialize<uint>(ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER));
            GpdIeee = deserializer.Deserialize<IeeeAddress>(ZclDataType.Get(DataType.IEEE_ADDRESS));
            GpdSecurityFrameCounter = deserializer.Deserialize<uint>(ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("GpNotificationResponse [");
            builder.Append(base.ToString());
            builder.Append(", Options=");
            builder.Append(Options);
            builder.Append(", GpdSrcId=");
            builder.Append(GpdSrcId);
            builder.Append(", GpdIeee=");
            builder.Append(GpdIeee);
            builder.Append(", GpdSecurityFrameCounter=");
            builder.Append(GpdSecurityFrameCounter);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
