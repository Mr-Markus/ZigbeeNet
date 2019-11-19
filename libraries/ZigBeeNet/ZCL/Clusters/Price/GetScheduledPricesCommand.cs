using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.Security;
using ZigBeeNet.ZCL.Clusters.Price;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Protocol;


namespace ZigBeeNet.ZCL.Clusters.Price
{
    /// <summary>
    /// Get Scheduled Prices Command value object class.
    ///
    /// Cluster: Price. Command ID 0x01 is sent TO the server.
    /// This command is a specific command used for the Price cluster.
    ///
    /// This command initiates a PublishPrice command for available price events. A server
    /// device shall be capable of storing five price events at a minimum On receipt of this
    /// command, the device shall send a PublishPrice command for the currently scheduled
    /// time.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class GetScheduledPricesCommand : ZclCommand
    {
        /// <summary>
        /// The cluster ID to which this command belongs.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0700;

        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x01;

        /// <summary>
        /// Start Time command message field.
        /// 
        /// UTCTime stamp representing the minimum ending time for any scheduled or currently
        /// active pricing events to be resent. If a command has a StartTime of 0x00000000,
        /// replace that StartTime with the current time stamp.
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// Number Of Events command message field.
        /// 
        /// Represents the maximum number of events to be sent. A value of 0 would indicate all
        /// available events are to be returned. Example: Number of Events = 1 would return the
        /// first event with an EndTime greater than or equal to the value of StartTime field in
        /// the GetScheduledPrices command. (EndTime would be StartTime plus Duration of the
        /// event listed in the device’s event table).
        /// </summary>
        public byte NumberOfEvents { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public GetScheduledPricesCommand()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(StartTime, ZclDataType.Get(DataType.UTCTIME));
            serializer.Serialize(NumberOfEvents, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            StartTime = deserializer.Deserialize<DateTime>(ZclDataType.Get(DataType.UTCTIME));
            NumberOfEvents = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("GetScheduledPricesCommand [");
            builder.Append(base.ToString());
            builder.Append(", StartTime=");
            builder.Append(StartTime);
            builder.Append(", NumberOfEvents=");
            builder.Append(NumberOfEvents);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
