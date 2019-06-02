using System;
using System.Text;
using ZigBeeNet.Transaction;
using ZigBeeNet.ZCL;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.ZDO.Command
{
    /// <summary>
    /// Management Network Discovery value object class.
    /// 
    /// The Mgmt_NWK_Disc_req is generated from a Local Device requesting that the
    /// Remote Device execute a Scan to report back networks in the vicinity of the Local
    /// Device. The destination addressing on this command shall be unicast.
    /// 
    /// </summary>
    public class ManagementNetworkDiscovery : ZdoResponse
    {
        /// <summary>
        /// ScanChannels command message field.
        /// </summary>
        public int ScanChannels { get; set; }

        /// <summary>
        /// ScanDuration command message field.
        /// </summary>
        public byte ScanDuration { get; set; }

        /// <summary>
        /// StartIndex command message field.
        /// </summary>
        public byte StartIndex { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ManagementNetworkDiscovery()
        {
            ClusterId = 0x0030;
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

            ScanChannels = (int)deserializer.Deserialize(ZclDataType.Get(DataType.BITMAP_32_BIT));
            ScanDuration = (byte)deserializer.Deserialize(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            StartIndex = (byte)deserializer.Deserialize(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append("ManagementNetworkDiscovery [")
                   .Append(base.ToString())
                   .Append(", scanChannels=")
                   .Append(ScanChannels)
                   .Append(", scanDuration=")
                   .Append(ScanDuration)
                   .Append(", startIndex=")
                   .Append(StartIndex)
                   .Append(']');

            return builder.ToString();
        }

    }
}
