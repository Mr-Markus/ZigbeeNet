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
    /// Mirror Removed value object class.
    ///
    /// Cluster: Metering. Command ID 0x02 is sent TO the server.
    /// This command is a specific command used for the Metering cluster.
    ///
    /// The Mirror Removed Command allows the ESI to inform a sleepy Metering Device mirroring
    /// support has been removed or halted.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class MirrorRemoved : ZclCommand
    {
        /// <summary>
        /// The cluster ID to which this command belongs.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0702;

        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x02;

        /// <summary>
        /// Removed Endpoint ID command message field.
        /// 
        /// 16 Bit Unsigned Integer indicating the End Point ID previously containing the
        /// Metering Devices meter data.
        /// </summary>
        public ushort RemovedEndpointId { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public MirrorRemoved()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(RemovedEndpointId, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            RemovedEndpointId = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("MirrorRemoved [");
            builder.Append(base.ToString());
            builder.Append(", RemovedEndpointId=");
            builder.Append(RemovedEndpointId);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
