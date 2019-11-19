using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.Security;
using ZigBeeNet.ZCL.Clusters.RSSILocation;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Protocol;


namespace ZigBeeNet.ZCL.Clusters.RSSILocation
{
    /// <summary>
    /// Report RSSI Measurements Command value object class.
    ///
    /// Cluster: RSSI Location. Command ID 0x06 is sent FROM the server.
    /// This command is a specific command used for the RSSI Location cluster.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class ReportRssiMeasurementsCommand : ZclCommand
    {
        /// <summary>
        /// The cluster ID to which this command belongs.
        /// </summary>
        public const ushort CLUSTER_ID = 0x000B;

        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x06;

        /// <summary>
        /// Reporting Address command message field.
        /// </summary>
        public IeeeAddress ReportingAddress { get; set; }

        /// <summary>
        /// Number Of Neighbors command message field.
        /// </summary>
        public byte NumberOfNeighbors { get; set; }

        /// <summary>
        /// Neighbors Information command message field.
        /// </summary>
        public List<NeighborInformation> NeighborsInformation { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ReportRssiMeasurementsCommand()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
            CommandDirection = ZclCommandDirection.SERVER_TO_CLIENT;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(ReportingAddress, ZclDataType.Get(DataType.IEEE_ADDRESS));
            serializer.Serialize(NumberOfNeighbors, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            serializer.Serialize(NeighborsInformation, ZclDataType.Get(DataType.N_X_NEIGHBORS_INFORMATION));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            ReportingAddress = deserializer.Deserialize<IeeeAddress>(ZclDataType.Get(DataType.IEEE_ADDRESS));
            NumberOfNeighbors = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            NeighborsInformation = deserializer.Deserialize<List<NeighborInformation>>(ZclDataType.Get(DataType.N_X_NEIGHBORS_INFORMATION));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("ReportRssiMeasurementsCommand [");
            builder.Append(base.ToString());
            builder.Append(", ReportingAddress=");
            builder.Append(ReportingAddress);
            builder.Append(", NumberOfNeighbors=");
            builder.Append(NumberOfNeighbors);
            builder.Append(", NeighborsInformation=");
            builder.Append(NeighborsInformation);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
