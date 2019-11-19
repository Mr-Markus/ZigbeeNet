using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.Security;
using ZigBeeNet.ZCL.Clusters.Identify;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Protocol;


namespace ZigBeeNet.ZCL.Clusters.Identify
{
    /// <summary>
    /// Identify Query Response value object class.
    ///
    /// Cluster: Identify. Command ID 0x00 is sent FROM the server.
    /// This command is a specific command used for the Identify cluster.
    ///
    /// The identify query response command is generated in response to receiving an Identify
    /// Query command in the case that the device is currently identifying itself.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class IdentifyQueryResponse : ZclCommand
    {
        /// <summary>
        /// The cluster ID to which this command belongs.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0003;

        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x00;

        /// <summary>
        /// Identify Time command message field.
        /// </summary>
        public ushort IdentifyTime { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public IdentifyQueryResponse()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
            CommandDirection = ZclCommandDirection.SERVER_TO_CLIENT;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(IdentifyTime, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            IdentifyTime = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("IdentifyQueryResponse [");
            builder.Append(base.ToString());
            builder.Append(", IdentifyTime=");
            builder.Append(IdentifyTime);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
