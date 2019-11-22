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
    /// RSSI Ping Command value object class.
    ///
    /// Cluster: RSSI Location. Command ID 0x04 is sent FROM the server.
    /// This command is a specific command used for the RSSI Location cluster.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class RssiPingCommand : ZclCommand
    {
        /// <summary>
        /// The cluster ID to which this command belongs.
        /// </summary>
        public const ushort CLUSTER_ID = 0x000B;

        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x04;

        /// <summary>
        /// Location Type command message field.
        /// </summary>
        public byte LocationType { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public RssiPingCommand()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
            CommandDirection = ZclCommandDirection.SERVER_TO_CLIENT;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(LocationType, ZclDataType.Get(DataType.DATA_8_BIT));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            LocationType = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.DATA_8_BIT));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("RssiPingCommand [");
            builder.Append(base.ToString());
            builder.Append(", LocationType=");
            builder.Append(LocationType);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
