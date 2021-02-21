using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.Security;
using ZigBeeNet.ZCL.Clusters.Scenes;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Protocol;


namespace ZigBeeNet.ZCL.Clusters.Scenes
{
    /// <summary>
    /// Remove All Scenes Response value object class.
    ///
    /// Cluster: Scenes. Command ID 0x03 is sent FROM the server.
    /// This command is a specific command used for the Scenes cluster.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class RemoveAllScenesResponse : ZclCommand
    {
        /// <summary>
        /// The cluster ID to which this command belongs.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0005;

        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x03;

        /// <summary>
        /// Status command message field.
        /// </summary>
        public ZclStatus Status { get; set; }

        /// <summary>
        /// Group ID command message field.
        /// </summary>
        public ushort GroupId { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public RemoveAllScenesResponse()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
            CommandDirection = ZclCommandDirection.SERVER_TO_CLIENT;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(Status, ZclDataType.Get(DataType.ZCL_STATUS));
            serializer.Serialize(GroupId, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            Status = deserializer.Deserialize<ZclStatus>(ZclDataType.Get(DataType.ZCL_STATUS));
            GroupId = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("RemoveAllScenesResponse [");
            builder.Append(base.ToString());
            builder.Append(", Status=");
            builder.Append(Status);
            builder.Append(", GroupId=");
            builder.Append(GroupId);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
