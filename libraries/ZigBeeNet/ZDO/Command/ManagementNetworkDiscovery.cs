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
    /// Management Network Discovery value object class.
    ///
    ///
    /// The Mgmt_NWK_Disc_req is generated from a Local Device requesting that the Remote
    /// Device execute a Scan to report back networks in the vicinity of the Local Device. The
    /// destination addressing on this command shall be unicast.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class ManagementNetworkDiscovery : ZdoRequest
    {
        /// <summary>
        /// The ZDO cluster ID.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0030;

        /// <summary>
        /// Scan Channels command message field.
        /// </summary>
        public int ScanChannels { get; set; }

        /// <summary>
        /// Scan Duration command message field.
        /// </summary>
        public byte ScanDuration { get; set; }

        /// <summary>
        /// Start Index command message field.
        /// </summary>
        public byte StartIndex { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ManagementNetworkDiscovery()
        {
            ClusterId = CLUSTER_ID;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            base.Serialize(serializer);

            serializer.Serialize(ScanChannels, ZclDataType.Get(DataType.BITMAP_32_BIT));
            serializer.Serialize(ScanDuration, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            serializer.Serialize(StartIndex, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            base.Deserialize(deserializer);

            ScanChannels = deserializer.Deserialize<int>(ZclDataType.Get(DataType.BITMAP_32_BIT));
            ScanDuration = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            StartIndex = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("ManagementNetworkDiscovery [");
            builder.Append(base.ToString());
            builder.Append(", ScanChannels=");
            builder.Append(ScanChannels);
            builder.Append(", ScanDuration=");
            builder.Append(ScanDuration);
            builder.Append(", StartIndex=");
            builder.Append(StartIndex);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
