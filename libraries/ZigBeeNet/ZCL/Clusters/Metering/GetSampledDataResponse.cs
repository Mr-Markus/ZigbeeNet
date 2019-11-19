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
    /// Get Sampled Data Response value object class.
    ///
    /// Cluster: Metering. Command ID 0x07 is sent FROM the server.
    /// This command is a specific command used for the Metering cluster.
    ///
    /// FIXME: This command is used to send the requested sample data to the client. It is
    /// generated in response to a GetSampledData command.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class GetSampledDataResponse : ZclCommand
    {
        /// <summary>
        /// The cluster ID to which this command belongs.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0702;

        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x07;

        /// <summary>
        /// Sample ID command message field.
        /// 
        /// Unique identifier allocated to this Sampling session. This field allows devices
        /// to match response data with the appropriate request.
        /// </summary>
        public ushort SampleId { get; set; }

        /// <summary>
        /// Sample Start Time command message field.
        /// 
        /// A UTC Time field to denote the time of the first sample returned in this response.
        /// </summary>
        public DateTime SampleStartTime { get; set; }

        /// <summary>
        /// Sample Type command message field.
        /// 
        /// An 8 bit enumeration that identifies the type of data being sampled.
        /// </summary>
        public byte SampleType { get; set; }

        /// <summary>
        /// Sample Request Interval command message field.
        /// 
        /// An unsigned 16-bit field representing the interval or time in seconds between
        /// samples.
        /// </summary>
        public ushort SampleRequestInterval { get; set; }

        /// <summary>
        /// Number Of Samples command message field.
        /// 
        /// Represents the number of samples being requested, This value cannot exceed the
        /// size stipulated in the MaxNumberofSamples field in the StartSampling command. If
        /// more samples are requested than can be delivered, the GetSampleDataResponse
        /// command will return the number of samples equal to MaxNumberofSamples field. If
        /// fewer samples are available for the time period, only those available shall be
        /// returned.
        /// </summary>
        public ushort NumberOfSamples { get; set; }

        /// <summary>
        /// Samples command message field.
        /// 
        /// Series of data samples captured using the interval specified by the
        /// SampleRequestInterval field in the StartSampling command. Each sample contains
        /// the change in the relevant data since the previous sample. Data is organised in a
        /// chronological order, the oldest sample is transmitted first and the most recent
        /// sample is transmitted last. Invalid samples should be marked as 0xFFFFFF.
        /// </summary>
        public uint Samples { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public GetSampledDataResponse()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
            CommandDirection = ZclCommandDirection.SERVER_TO_CLIENT;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(SampleId, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            serializer.Serialize(SampleStartTime, ZclDataType.Get(DataType.UTCTIME));
            serializer.Serialize(SampleType, ZclDataType.Get(DataType.ENUMERATION_8_BIT));
            serializer.Serialize(SampleRequestInterval, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            serializer.Serialize(NumberOfSamples, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            serializer.Serialize(Samples, ZclDataType.Get(DataType.UNSIGNED_24_BIT_INTEGER));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            SampleId = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            SampleStartTime = deserializer.Deserialize<DateTime>(ZclDataType.Get(DataType.UTCTIME));
            SampleType = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.ENUMERATION_8_BIT));
            SampleRequestInterval = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            NumberOfSamples = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            Samples = deserializer.Deserialize<uint>(ZclDataType.Get(DataType.UNSIGNED_24_BIT_INTEGER));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("GetSampledDataResponse [");
            builder.Append(base.ToString());
            builder.Append(", SampleId=");
            builder.Append(SampleId);
            builder.Append(", SampleStartTime=");
            builder.Append(SampleStartTime);
            builder.Append(", SampleType=");
            builder.Append(SampleType);
            builder.Append(", SampleRequestInterval=");
            builder.Append(SampleRequestInterval);
            builder.Append(", NumberOfSamples=");
            builder.Append(NumberOfSamples);
            builder.Append(", Samples=");
            builder.Append(Samples);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
