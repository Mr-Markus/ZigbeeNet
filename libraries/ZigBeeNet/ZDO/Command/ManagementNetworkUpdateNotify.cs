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
    /// Management Network Update Notify value object class.
    ///
    ///
    /// The Mgmt_NWK_Update_notify is provided to enable ZigBee devices to report the
    /// condition on local channels to a network manager. The scanned channel list is the report
    /// of channels scanned and it is followed by a list of records, one for each channel scanned,
    /// each record including one byte of the energy level measured during the scan, or 0xff if
    /// there is too much interference on this channel. <br> When sent in response to a
    /// Mgmt_NWK_Update_req command the status field shall represent the status of the
    /// request. When sent unsolicited the status field shall be set to SUCCESS. A Status of
    /// NOT_SUPPORTED indicates that the request was directed to a device which was not the
    /// ZigBee Coordinator or that the ZigBee Coordinator does not support End Device Binding.
    /// Otherwise, End_Device_Bind_req processing is performed as described below,
    /// including transmission of the End_Device_Bind_rsp.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class ManagementNetworkUpdateNotify : ZdoRequest
    {
        /// <summary>
        /// The ZDO cluster ID.
        /// </summary>
        public const ushort CLUSTER_ID = 0x8038;

        /// <summary>
        /// Status command message field.
        /// </summary>
        public ZdoStatus Status { get; set; }

        /// <summary>
        /// Scanned Channels command message field.
        /// </summary>
        public uint ScannedChannels { get; set; }

        /// <summary>
        /// Total Transmissions command message field.
        /// </summary>
        public ushort TotalTransmissions { get; set; }

        /// <summary>
        /// Transmission Failures command message field.
        /// </summary>
        public ushort TransmissionFailures { get; set; }

        /// <summary>
        /// Energy Values command message field.
        /// </summary>
        public List<byte> EnergyValues { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ManagementNetworkUpdateNotify()
        {
            ClusterId = CLUSTER_ID;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            base.Serialize(serializer);

            serializer.Serialize(Status, ZclDataType.Get(DataType.ZDO_STATUS));
            serializer.Serialize(ScannedChannels, ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER));
            serializer.Serialize(TotalTransmissions, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            serializer.Serialize(TransmissionFailures, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            serializer.Serialize(EnergyValues.Count, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            for (int cnt = 0; cnt < EnergyValues.Count; cnt++)
            {
                serializer.Serialize(EnergyValues[cnt], ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            }
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            base.Deserialize(deserializer);

            // Create lists
            EnergyValues = new List<byte>();

            Status = deserializer.Deserialize<ZdoStatus>(ZclDataType.Get(DataType.ZDO_STATUS));
            if (Status != ZdoStatus.SUCCESS)
            {
                // Don't read the full response if we have an error
                return;
            }
            ScannedChannels = deserializer.Deserialize<uint>(ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER));
            TotalTransmissions = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            TransmissionFailures = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            byte? scannedChannelsListCount = (byte?) deserializer.Deserialize(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            if (scannedChannelsListCount != null)
            {
                for (int cnt = 0; cnt < scannedChannelsListCount; cnt++)
                {
                    EnergyValues.Add((byte) deserializer.Deserialize(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER)));
                }
            }
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("ManagementNetworkUpdateNotify [");
            builder.Append(base.ToString());
            builder.Append(", Status=");
            builder.Append(Status);
            builder.Append(", ScannedChannels=");
            builder.Append(ScannedChannels);
            builder.Append(", TotalTransmissions=");
            builder.Append(TotalTransmissions);
            builder.Append(", TransmissionFailures=");
            builder.Append(TransmissionFailures);
            builder.Append(", EnergyValues=");
            builder.Append(string.Join(", ", EnergyValues));
            builder.Append(']');

            return builder.ToString();
        }
    }
}
