using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.Security;
using ZigBeeNet.ZCL.Clusters.Metering;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Protocol;


namespace ZigBeeNet.ZCL.Clusters.Metering
{
    /// <summary>
    /// Get Sampled Data value object class.
    ///
    /// Cluster: Metering. Command ID 0x08 is sent TO the server.
    /// This command is a specific command used for the Metering cluster.
    ///
    /// This command is used to request sampled data from the server. Note that it is the
    /// responsibility of the client to ensure that it does not request more samples than can be
    /// held in a single command payload.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class GetSampledData : ZclCommand
    {
        /// <summary>
        /// The cluster ID to which this command belongs.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0702;

        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x08;

        /// <summary>
        /// Sample ID command message field.
        /// 
        /// Unique identifier allocated to this Sampling session. This field allows devices
        /// to match response data with the appropriate request.
        /// </summary>
        public ushort SampleId { get; set; }

        /// <summary>
        /// Earliest Sample Time command message field.
        /// 
        /// A UTC Timestamp indicating the earliest time of a sample to be returned. Samples
        /// with a timestamp equal to or greater than the specified EarliestSampleTime shall
        /// be returned.
        /// </summary>
        public DateTime EarliestSampleTime { get; set; }

        /// <summary>
        /// Sample Type command message field.
        /// 
        /// An 8 bit enumeration that identifies the required type of sampled data.
        /// </summary>
        public byte SampleType { get; set; }

        /// <summary>
        /// Number Of Samples command message field.
        /// 
        /// Represents the number of samples being requested, This value cannot exceed the
        /// size stipulated in the MaxNumberofSamples field in the StartSampling command. If
        /// more samples are requested than can be delivered, the GetSampledDataResponse
        /// command will return the number of samples equal to the MaxNumberofSamples field.
        /// If fewer samples are available for the time period, only those available are
        /// returned.
        /// </summary>
        public ushort NumberOfSamples { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public GetSampledData()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(SampleId, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            serializer.Serialize(EarliestSampleTime, ZclDataType.Get(DataType.UTCTIME));
            serializer.Serialize(SampleType, ZclDataType.Get(DataType.ENUMERATION_8_BIT));
            serializer.Serialize(NumberOfSamples, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            SampleId = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            EarliestSampleTime = deserializer.Deserialize<DateTime>(ZclDataType.Get(DataType.UTCTIME));
            SampleType = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.ENUMERATION_8_BIT));
            NumberOfSamples = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("GetSampledData [");
            builder.Append(base.ToString());
            builder.Append(", SampleId=");
            builder.Append(SampleId);
            builder.Append(", EarliestSampleTime=");
            builder.Append(EarliestSampleTime);
            builder.Append(", SampleType=");
            builder.Append(SampleType);
            builder.Append(", NumberOfSamples=");
            builder.Append(NumberOfSamples);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
