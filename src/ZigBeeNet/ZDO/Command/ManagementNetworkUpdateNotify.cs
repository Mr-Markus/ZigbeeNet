using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.Transaction;
using ZigBeeNet.ZCL;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.ZDO.Command
{
    /// <summary>
    /// Management Network Update Notify value object class.
    /// 
    /// The Mgmt_NWK_Update_notify is provided to enable ZigBee devices to report
    /// the condition on local channels to a network manager. The scanned channel list is
    /// the report of channels scanned and it is followed by a list of records, one for each
    /// channel scanned, each record including one byte of the energy level measured
    /// during the scan, or 0xff if there is too much interference on this channel.
    /// <br>
    /// When sent in response to a Mgmt_NWK_Update_req command the Status field
    /// shall represent the Status of the request. When sent unsolicited the Status field
    /// shall be set to SUCCESS.
    /// A Status of NOT_SUPPORTED indicates that the request was directed to a device
    /// which was not the ZigBee Coordinator or that the ZigBee Coordinator does not
    /// support End Device Binding. Otherwise, End_Device_Bind_req processing is
    /// performed as described below, including transmission of the
    /// End_Device_Bind_rsp.
    /// 
    /// </summary>
    public class ManagementNetworkUpdateNotify : ZdoResponse
    {
        /// <summary>
        /// ScannedChannels command message field.
        /// </summary>
        public int ScannedChannels { get; set; }

        /// <summary>
        /// TotalTransmissions command message field.
        /// </summary>
        public ushort TotalTransmissions { get; set; }

        /// <summary>
        /// TransmissionFailures command message field.
        /// </summary>
        public ushort TransmissionFailures { get; set; }

        /// <summary>
        /// EnergyValues command message field.
        /// </summary>
        public List<int> EnergyValues { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ManagementNetworkUpdateNotify()
        {
            ClusterId = 0x8038;
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
            EnergyValues = new List<int>();

            Status = (ZdoStatus)deserializer.Deserialize(ZclDataType.Get(DataType.ZDO_STATUS));

            if (Status != ZdoStatus.SUCCESS)
            {
                // Don't read the full response if we have an error
                return;
            }

            ScannedChannels = (int)deserializer.Deserialize(ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER));
            TotalTransmissions = (ushort)deserializer.Deserialize(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            TransmissionFailures = (ushort)deserializer.Deserialize(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            byte? scannedChannelsListCount = (byte?)deserializer.Deserialize(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));

            if (scannedChannelsListCount != null)
            {
                for (int cnt = 0; cnt < scannedChannelsListCount; cnt++)
                {
                    EnergyValues.Add((int)deserializer.Deserialize(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER)));
                }
            }
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append("ManagementNetworkUpdateNotify [")
                   .Append(base.ToString())
                   .Append(", Status=")
                   .Append(Status)
                   .Append(", scannedChannels=")
                   .Append(ScannedChannels)
                   .Append(", totalTransmissions=")
                   .Append(TotalTransmissions)
                   .Append(", transmissionFailures=")
                   .Append(TransmissionFailures)
                   .Append(", energyValues=")
                   .Append(EnergyValues)
                   .Append(']');

            return builder.ToString();
        }

    }
}
