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
    /// Get Block Period Command value object class.
    ///
    /// Cluster: Price. Command ID 0x03 is sent TO the server.
    /// This command is a specific command used for the Price cluster.
    ///
    /// This command initiates a PublishBlockPeriod command for the currently scheduled
    /// block periods. A server device shall be capable of storing at least two commands, the
    /// current period and a period to be activated in the near future. <br> A ZCL Default
    /// response with status NOT_FOUND shall be returned if there are no events available.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class GetBlockPeriodCommand : ZclCommand
    {
        /// <summary>
        /// The cluster ID to which this command belongs.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0700;

        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x03;

        /// <summary>
        /// Start Time command message field.
        /// 
        /// UTCTime stamp representing the minimum ending time for any scheduled or currently
        /// block period events to be resent. If a command has a Start Time of 0x00000000,
        /// replace that Start Time with the current time stamp.
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// Number Of Events command message field.
        /// 
        /// An 8 bit Integer which indicates the maximum number of Publish Block Period
        /// commands that can be sent. Example: Number of Events = 1 would return the first event
        /// with an EndTime greater than or equal to the value of Start Time field in the
        /// GetBlockPeriod(s) command. (EndTime would be StartTime plus Duration of the
        /// event listed in the device’s event table). Number of Events = 0 would return all
        /// available Publish Block Periods, starting with the current block in progress.
        /// 8460 command. The least significant nibble represents an enumeration of the
        /// tariff (Generation Meters shall use the ‘Received’ Tariff.). If the TariffType is
        /// not specified, the server shall assume that the request is for the ‘Delivered’
        /// Tariff. The most significant nibble is reserved.
        /// </summary>
        public byte NumberOfEvents { get; set; }

        /// <summary>
        /// Tariff Type command message field.
        /// </summary>
        public byte TariffType { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public GetBlockPeriodCommand()
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
            serializer.Serialize(TariffType, ZclDataType.Get(DataType.BITMAP_8_BIT));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            StartTime = deserializer.Deserialize<DateTime>(ZclDataType.Get(DataType.UTCTIME));
            NumberOfEvents = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            TariffType = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.BITMAP_8_BIT));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("GetBlockPeriodCommand [");
            builder.Append(base.ToString());
            builder.Append(", StartTime=");
            builder.Append(StartTime);
            builder.Append(", NumberOfEvents=");
            builder.Append(NumberOfEvents);
            builder.Append(", TariffType=");
            builder.Append(TariffType);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
