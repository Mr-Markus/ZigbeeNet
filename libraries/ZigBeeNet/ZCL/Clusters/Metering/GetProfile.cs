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
    /// Get Profile value object class.
    ///
    /// Cluster: Metering. Command ID 0x00 is sent TO the server.
    /// This command is a specific command used for the Metering cluster.
    ///
    /// The GetProfile command is generated when a client device wishes to retrieve a list of
    /// captured Energy, Gas or water consumption for profiling purposes.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class GetProfile : ZclCommand
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
        /// Interval Channel command message field.
        /// 
        /// Enumerated value used to select the quantity of interest returned by the
        /// GetProfileReponse command.
        /// </summary>
        public byte IntervalChannel { get; set; }

        /// <summary>
        /// End Time command message field.
        /// 
        /// 32-bit value (in UTCTime) used to select an Intervals block from all the Intervals
        /// blocks available. The Intervals block returned is the most recent block with its
        /// EndTime equal or older to the one provided. The most recent Intervals block is
        /// requested using an End Time set to 0x00000000, subsequent Intervals block are
        /// requested using an End time set to the EndTime of the previous block - (number of
        /// intervals of the previous block * ProfileIntervalPeriod).
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// Number Of Periods command message field.
        /// 
        /// Represents the number of intervals being requested. This value cannot exceed the
        /// size stipulated in the MaxNumberOfPeriodsDelivered attribute. If more
        /// intervals are requested than can be delivered, the GetProfileResponse will
        /// return the number of intervals equal to MaxNumberOfPeriodsDelivered. If fewer
        /// intervals are available for the time period, only those available are returned.
        /// </summary>
        public byte NumberOfPeriods { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public GetProfile()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(IntervalChannel, ZclDataType.Get(DataType.ENUMERATION_8_BIT));
            serializer.Serialize(EndTime, ZclDataType.Get(DataType.UTCTIME));
            serializer.Serialize(NumberOfPeriods, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            IntervalChannel = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.ENUMERATION_8_BIT));
            EndTime = deserializer.Deserialize<DateTime>(ZclDataType.Get(DataType.UTCTIME));
            NumberOfPeriods = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("GetProfile [");
            builder.Append(base.ToString());
            builder.Append(", IntervalChannel=");
            builder.Append(IntervalChannel);
            builder.Append(", EndTime=");
            builder.Append(EndTime);
            builder.Append(", NumberOfPeriods=");
            builder.Append(NumberOfPeriods);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
