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
    /// View Group Response value object class.
    ///
    /// Cluster: Groups. Command ID 0x01 is sent FROM the server.
    /// This command is a specific command used for the Groups cluster.
    ///
    /// The view group response command is sent by the groups cluster server in response to a view
    /// group command.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class ViewGroupResponse : ZclCommand
    {
        /// <summary>
        /// The cluster ID to which this command belongs.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0004;

        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x01;

        /// <summary>
        /// Status command message field.
        /// </summary>
        public byte Status { get; set; }

        /// <summary>
        /// Group ID command message field.
        /// </summary>
        public ushort GroupId { get; set; }

        /// <summary>
        /// Group Name command message field.
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ViewGroupResponse()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
            CommandDirection = ZclCommandDirection.SERVER_TO_CLIENT;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(Status, ZclDataType.Get(DataType.ENUMERATION_8_BIT));
            serializer.Serialize(GroupId, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            serializer.Serialize(GroupName, ZclDataType.Get(DataType.CHARACTER_STRING));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            Status = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.ENUMERATION_8_BIT));
            GroupId = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            GroupName = deserializer.Deserialize<string>(ZclDataType.Get(DataType.CHARACTER_STRING));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("ViewGroupResponse [");
            builder.Append(base.ToString());
            builder.Append(", Status=");
            builder.Append(Status);
            builder.Append(", GroupId=");
            builder.Append(GroupId);
            builder.Append(", GroupName=");
            builder.Append(GroupName);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
