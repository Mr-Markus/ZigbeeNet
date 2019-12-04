using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.ZCL;
using ZigBeeNet.ZCL.Protocol;
using ZigBeeNet.ZDO.Field;


namespace ZigBeeNet.ZDO.Command
{
    /// <summary>
    /// Network Update Request value object class.
    ///
    ///
    /// This command is provided to allow updating of network configuration parameters or to
    /// request information from devices on network conditions in the local operating
    /// environment. The destination addressing on this primitive shall be unicast or
    /// broadcast to all devices for which macRxOnWhenIdle = TRUE.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class NetworkUpdateRequest : ZdoRequest
    {
        /// <summary>
        /// The ZDO cluster ID.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0038;

        /// <summary>
        /// Scan Channels command message field.
        /// </summary>
        public int ScanChannels { get; set; }

        /// <summary>
        /// Scan Duration command message field.
        /// </summary>
        public byte ScanDuration { get; set; }

        /// <summary>
        /// Scan Count command message field.
        /// </summary>
        public byte ScanCount { get; set; }

        /// <summary>
        /// NWK Update ID command message field.
        /// </summary>
        public byte NwkUpdateId { get; set; }

        /// <summary>
        /// NWK Manager Addr command message field.
        /// </summary>
        public ushort NwkManagerAddr { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public NetworkUpdateRequest()
        {
            ClusterId = CLUSTER_ID;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            base.Serialize(serializer);

            serializer.Serialize(ScanChannels, ZclDataType.Get(DataType.BITMAP_32_BIT));
            serializer.Serialize(ScanDuration, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            serializer.Serialize(ScanCount, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            serializer.Serialize(NwkUpdateId, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            serializer.Serialize(NwkManagerAddr, ZclDataType.Get(DataType.NWK_ADDRESS));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            base.Deserialize(deserializer);

            ScanChannels = deserializer.Deserialize<int>(ZclDataType.Get(DataType.BITMAP_32_BIT));
            ScanDuration = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            ScanCount = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            NwkUpdateId = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            NwkManagerAddr = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.NWK_ADDRESS));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("NetworkUpdateRequest [");
            builder.Append(base.ToString());
            builder.Append(", ScanChannels=");
            builder.Append(ScanChannels);
            builder.Append(", ScanDuration=");
            builder.Append(ScanDuration);
            builder.Append(", ScanCount=");
            builder.Append(ScanCount);
            builder.Append(", NwkUpdateId=");
            builder.Append(NwkUpdateId);
            builder.Append(", NwkManagerAddr=");
            builder.Append(NwkManagerAddr);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
