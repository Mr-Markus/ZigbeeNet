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
    /// Get Scene Membership Command value object class.
    ///
    /// Cluster: Scenes. Command ID 0x06 is sent TO the server.
    /// This command is a specific command used for the Scenes cluster.
    ///
    /// The Get Scene Membership command can be used to find an unused scene number within the
    /// group when no commissioning tool is in the network, or for a commissioning tool to get
    /// used scenes for a group on a single device or on all devices in the group.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class GetSceneMembershipCommand : ZclCommand
    {
        /// <summary>
        /// The cluster ID to which this command belongs.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0005;

        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x06;

        /// <summary>
        /// Group ID command message field.
        /// </summary>
        public ushort GroupId { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public GetSceneMembershipCommand()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(GroupId, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            GroupId = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("GetSceneMembershipCommand [");
            builder.Append(base.ToString());
            builder.Append(", GroupId=");
            builder.Append(GroupId);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
