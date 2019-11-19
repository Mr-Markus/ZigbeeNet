using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.Security;
using ZigBeeNet.ZCL.Clusters.Groups;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Protocol;


namespace ZigBeeNet.ZCL.Clusters.Groups
{
    /// <summary>
    /// Get Group Membership Command value object class.
    ///
    /// Cluster: Groups. Command ID 0x02 is sent TO the server.
    /// This command is a specific command used for the Groups cluster.
    ///
    /// The get group membership command allows the sending device to inquire about the group
    /// membership of the receiving device and endpoint in a number of ways.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class GetGroupMembershipCommand : ZclCommand
    {
        /// <summary>
        /// The cluster ID to which this command belongs.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0004;

        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x02;

        /// <summary>
        /// Group Count command message field.
        /// </summary>
        public byte GroupCount { get; set; }

        /// <summary>
        /// Group List command message field.
        /// </summary>
        public List<ushort> GroupList { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public GetGroupMembershipCommand()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(GroupCount, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            serializer.Serialize(GroupList, ZclDataType.Get(DataType.N_X_UNSIGNED_16_BIT_INTEGER));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            GroupCount = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            GroupList = deserializer.Deserialize<List<ushort>>(ZclDataType.Get(DataType.N_X_UNSIGNED_16_BIT_INTEGER));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("GetGroupMembershipCommand [");
            builder.Append(base.ToString());
            builder.Append(", GroupCount=");
            builder.Append(GroupCount);
            builder.Append(", GroupList=");
            builder.Append(GroupList);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
