// License text here
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.ZCL.Protocol;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Clusters.Groups;


namespace ZigBeeNet.ZCL.Clusters.Groups
{
    /// <summary>
    /// Get Group Membership Command value object class.
    /// <para>
    /// Cluster: Groups. Command is sent TO the server.
    /// This command is a specific command used for the Groups cluster.
    /// </para>
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class GetGroupMembershipCommand : ZclCommand
    {
        /// <summary>
        /// Group count command message field.
        /// </summary>
        public byte GroupCount { get; set; }

        /// <summary>
        /// Group list command message field.
        /// </summary>
        public List<ushort> GroupList { get; set; }


        /// <summary>
        /// Default constructor.
        /// </summary>
        public GetGroupMembershipCommand()
        {
            GenericCommand = false;
            ClusterId = 4;
            CommandId = 2;
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
