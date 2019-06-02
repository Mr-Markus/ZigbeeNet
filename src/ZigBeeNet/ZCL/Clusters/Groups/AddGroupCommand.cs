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
    /// Add Group Command value object class.
    /// <para>
    /// Cluster: Groups. Command is sent TO the server.
    /// This command is a specific command used for the Groups cluster.
    /// </para>
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class AddGroupCommand : ZclCommand
    {
        /// <summary>
        /// Group ID command message field.
        /// </summary>
        public ushort GroupID { get; set; }

        /// <summary>
        /// Group Name command message field.
        /// </summary>
        public string GroupName { get; set; }


        /// <summary>
        /// Default constructor.
        /// </summary>
        public AddGroupCommand()
        {
            GenericCommand = false;
            ClusterId = 4;
            CommandId = 0;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(GroupID, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            serializer.Serialize(GroupName, ZclDataType.Get(DataType.CHARACTER_STRING));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            GroupID = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            GroupName = deserializer.Deserialize<string>(ZclDataType.Get(DataType.CHARACTER_STRING));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("AddGroupCommand [");
            builder.Append(base.ToString());
            builder.Append(", GroupID=");
            builder.Append(GroupID);
            builder.Append(", GroupName=");
            builder.Append(GroupName);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
