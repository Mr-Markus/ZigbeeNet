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
    /// Get Group Membership Response value object class.
    ///
    /// Cluster: Groups. Command ID 0x02 is sent FROM the server.
    /// This command is a specific command used for the Groups cluster.
    ///
    /// The get group membership response command is sent by the groups cluster server in
    /// response to a get group membership command.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class GetGroupMembershipResponse : ZclCommand
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
        /// Capacity command message field.
        /// </summary>
        public byte Capacity { get; set; }

        /// <summary>
        /// Group List command message field.
        /// </summary>
        public List<ushort> GroupList { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public GetGroupMembershipResponse()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
            CommandDirection = ZclCommandDirection.SERVER_TO_CLIENT;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(Capacity, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            serializer.Serialize(GroupList.Count, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            for (int cnt = 0; cnt < GroupList.Count; cnt++)
            {
                serializer.Serialize(GroupList[cnt], ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            }
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            // Create lists
            GroupList = new List<ushort>();

            Capacity = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            byte? groupCount = (byte?) deserializer.Deserialize(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            if (groupCount != null)
            {
                for (int cnt = 0; cnt < groupCount; cnt++)
                {
                    GroupList.Add((ushort) deserializer.Deserialize(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER)));
                }
            }
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("GetGroupMembershipResponse [");
            builder.Append(base.ToString());
            builder.Append(", Capacity=");
            builder.Append(Capacity);
            builder.Append(", GroupList=");
            builder.Append(GroupList == null? "" : string.Join(", ", GroupList));
            builder.Append(']');

            return builder.ToString();
        }
    }
}
