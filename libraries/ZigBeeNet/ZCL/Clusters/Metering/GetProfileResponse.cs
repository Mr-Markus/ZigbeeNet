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
    /// Get Profile Response value object class.
    ///
    /// Cluster: Metering. Command ID 0x00 is sent FROM the server.
    /// This command is a specific command used for the Metering cluster.
    ///
    /// This command is sent when the Client command GetProfile is received.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class GetProfileResponse : ZclCommand
    {
        /// <summary>
        /// The cluster ID to which this command belongs.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0702;

        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x00;

        /// <summary>
        /// End Time command message field.
        /// 
        /// 32-bit value (in UTC) representing the end time of the most chronologically recent
        /// interval being requested. Example: Data collected from 2:00 PM to 3:00 PM would be
        /// specified as a 3:00 PM interval (end time). It is important to note that the current
        /// interval accumulating is not included in most recent block but can be retrieved
        /// using the CurrentPartialProfileIntervalValue attribute.
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// Status command message field.
        /// </summary>
        public byte Status { get; set; }

        /// <summary>
        /// Profile Interval Period command message field.
        /// 
        /// Represents the interval or time frame used to capture metered Energy, Gas, and
        /// Water consumption for profiling purposes.
        /// </summary>
        public byte ProfileIntervalPeriod { get; set; }

        /// <summary>
        /// Number Of Periods Delivered command message field.
        /// 
        /// Represents the number of intervals the device is returning. Please note the number
        /// of periods returned in the Get Profile Response command can be calculated when the
        /// packets are received and can replace the usage of this field. The intent is to
        /// provide this information as a convenience.
        /// </summary>
        public byte NumberOfPeriodsDelivered { get; set; }

        /// <summary>
        /// Intervals command message field.
        /// 
        /// Series of interval data captured using the period specified by the
        /// ProfileIntervalPeriod field. The content of the interval data depends of the type
        /// of information requested using the Channel field in the Get Profile Command, and
        /// will represent the change in that information since the previous interval. Data is
        /// organized in a reverse chronological order, the most recent interval is
        /// transmitted first and the oldest interval is transmitted last. Invalid intervals
        /// should be marked as 0xFFFFFF.
        /// </summary>
        public uint Intervals { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public GetProfileResponse()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
            CommandDirection = ZclCommandDirection.SERVER_TO_CLIENT;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(EndTime, ZclDataType.Get(DataType.UTCTIME));
            serializer.Serialize(Status, ZclDataType.Get(DataType.ENUMERATION_8_BIT));
            serializer.Serialize(ProfileIntervalPeriod, ZclDataType.Get(DataType.ENUMERATION_8_BIT));
            serializer.Serialize(NumberOfPeriodsDelivered, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            serializer.Serialize(Intervals, ZclDataType.Get(DataType.UNSIGNED_24_BIT_INTEGER));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            EndTime = deserializer.Deserialize<DateTime>(ZclDataType.Get(DataType.UTCTIME));
            Status = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.ENUMERATION_8_BIT));
            ProfileIntervalPeriod = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.ENUMERATION_8_BIT));
            NumberOfPeriodsDelivered = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            Intervals = deserializer.Deserialize<uint>(ZclDataType.Get(DataType.UNSIGNED_24_BIT_INTEGER));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("GetProfileResponse [");
            builder.Append(base.ToString());
            builder.Append(", EndTime=");
            builder.Append(EndTime);
            builder.Append(", Status=");
            builder.Append(Status);
            builder.Append(", ProfileIntervalPeriod=");
            builder.Append(ProfileIntervalPeriod);
            builder.Append(", NumberOfPeriodsDelivered=");
            builder.Append(NumberOfPeriodsDelivered);
            builder.Append(", Intervals=");
            builder.Append(Intervals);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
