using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.Security;
using ZigBeeNet.ZCL.Clusters.ElectricalMeasurement;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Protocol;


namespace ZigBeeNet.ZCL.Clusters.ElectricalMeasurement
{
    /// <summary>
    /// Get Profile Info Response Command value object class.
    ///
    /// Cluster: Electrical Measurement. Command ID 0x00 is sent FROM the server.
    /// This command is a specific command used for the Electrical Measurement cluster.
    ///
    /// Returns the power profiling information requested in the GetProfileInfo command. The
    /// power profiling information consists of a list of attributes which are profiled along
    /// with the period used to profile them.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class GetProfileInfoResponseCommand : ZclCommand
    {
        /// <summary>
        /// The cluster ID to which this command belongs.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0B04;

        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x00;

        /// <summary>
        /// Profile Count command message field.
        /// </summary>
        public byte ProfileCount { get; set; }

        /// <summary>
        /// Profile Interval Period command message field.
        /// </summary>
        public byte ProfileIntervalPeriod { get; set; }

        /// <summary>
        /// Max Number Of Intervals command message field.
        /// </summary>
        public byte MaxNumberOfIntervals { get; set; }

        /// <summary>
        /// List Of Attributes command message field.
        /// </summary>
        public ushort ListOfAttributes { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public GetProfileInfoResponseCommand()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
            CommandDirection = ZclCommandDirection.SERVER_TO_CLIENT;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(ProfileCount, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            serializer.Serialize(ProfileIntervalPeriod, ZclDataType.Get(DataType.ENUMERATION_8_BIT));
            serializer.Serialize(MaxNumberOfIntervals, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            serializer.Serialize(ListOfAttributes, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            ProfileCount = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            ProfileIntervalPeriod = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.ENUMERATION_8_BIT));
            MaxNumberOfIntervals = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            ListOfAttributes = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("GetProfileInfoResponseCommand [");
            builder.Append(base.ToString());
            builder.Append(", ProfileCount=");
            builder.Append(ProfileCount);
            builder.Append(", ProfileIntervalPeriod=");
            builder.Append(ProfileIntervalPeriod);
            builder.Append(", MaxNumberOfIntervals=");
            builder.Append(MaxNumberOfIntervals);
            builder.Append(", ListOfAttributes=");
            builder.Append(ListOfAttributes);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
