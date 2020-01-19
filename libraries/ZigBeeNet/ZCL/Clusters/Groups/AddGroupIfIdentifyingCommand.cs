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
    /// Add Group If Identifying Command value object class.
    ///
    /// Cluster: Groups. Command ID 0x05 is sent TO the server.
    /// This command is a specific command used for the Groups cluster.
    ///
    /// The add group if identifying command allows the sending device to add group membership
    /// in a particular group for one or more endpoints on the receiving device, on condition
    /// that it is identifying itself. Identifying functionality is controlled using the
    /// identify cluster.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class AddGroupIfIdentifyingCommand : ZclCommand
    {
        /// <summary>
        /// The cluster ID to which this command belongs.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0004;

        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x05;

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
        public AddGroupIfIdentifyingCommand()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(GroupId, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            serializer.Serialize(GroupName, ZclDataType.Get(DataType.CHARACTER_STRING));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            GroupId = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            GroupName = deserializer.Deserialize<string>(ZclDataType.Get(DataType.CHARACTER_STRING));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("AddGroupIfIdentifyingCommand [");
            builder.Append(base.ToString());
            builder.Append(", GroupId=");
            builder.Append(GroupId);
            builder.Append(", GroupName=");
            builder.Append(GroupName);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
