// License text here
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.ZCL.Protocol;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Clusters.Scenes;


namespace ZigBeeNet.ZCL.Clusters.Scenes
{
    /// <summary>
    /// Get Scene Membership Command value object class.
    /// <para>
    /// Cluster: Scenes. Command is sent TO the server.
    /// This command is a specific command used for the Scenes cluster.
    ///
    /// The Get Scene Membership command can be used to find an unused scene
    /// number within the group when no commissioning tool is in the network, or for a
    /// commissioning tool to get used scenes for a group on a single device or on all
    /// devices in the group.
    /// </para>
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class GetSceneMembershipCommand : ZclCommand
    {
        /// <summary>
        /// Group ID command message field.
        /// </summary>
        public ushort GroupID { get; set; }


        /// <summary>
        /// Default constructor.
        /// </summary>
        public GetSceneMembershipCommand()
        {
            GenericCommand = false;
            ClusterId = 5;
            CommandId = 6;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(GroupID, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            GroupID = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("GetSceneMembershipCommand [");
            builder.Append(base.ToString());
            builder.Append(", GroupID=");
            builder.Append(GroupID);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
