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
    /// Remove Group Command value object class.
    ///
    /// Cluster: Groups. Command ID 0x03 is sent TO the server.
    /// This command is a specific command used for the Groups cluster.
    ///
    /// The remove group command allows the sender to request that the receiving entity or
    /// entities remove their membership, if any, in a particular group.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class RemoveGroupCommand : ZclCommand
    {
        /// <summary>
        /// The cluster ID to which this command belongs.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0004;

        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x03;

        /// <summary>
        /// Group ID command message field.
        /// </summary>
        public ushort GroupId { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public RemoveGroupCommand()
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

            builder.Append("RemoveGroupCommand [");
            builder.Append(base.ToString());
            builder.Append(", GroupId=");
            builder.Append(GroupId);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
