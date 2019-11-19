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
    /// Get Device Configuration Command value object class.
    ///
    /// Cluster: RSSI Location. Command ID 0x02 is sent TO the server.
    /// This command is a specific command used for the RSSI Location cluster.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class GetDeviceConfigurationCommand : ZclCommand
    {
        /// <summary>
        /// The cluster ID to which this command belongs.
        /// </summary>
        public const ushort CLUSTER_ID = 0x000B;

        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x02;

        /// <summary>
        /// Target Address command message field.
        /// </summary>
        public IeeeAddress TargetAddress { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public GetDeviceConfigurationCommand()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(TargetAddress, ZclDataType.Get(DataType.IEEE_ADDRESS));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            TargetAddress = deserializer.Deserialize<IeeeAddress>(ZclDataType.Get(DataType.IEEE_ADDRESS));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("GetDeviceConfigurationCommand [");
            builder.Append(base.ToString());
            builder.Append(", TargetAddress=");
            builder.Append(TargetAddress);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
