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
    /// Get Scene Membership Response value object class.
    ///
    /// Cluster: Scenes. Command ID 0x06 is sent FROM the server.
    /// This command is a specific command used for the Scenes cluster.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class GetSceneMembershipResponse : ZclCommand
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
        /// Status command message field.
        /// </summary>
        public byte Status { get; set; }

        /// <summary>
        /// Capacity command message field.
        /// </summary>
        public byte Capacity { get; set; }

        /// <summary>
        /// Group ID command message field.
        /// </summary>
        public ushort GroupId { get; set; }

        /// <summary>
        /// Scene Count command message field.
        /// </summary>
        public byte SceneCount { get; set; }

        /// <summary>
        /// Scene List command message field.
        /// </summary>
        public List<byte> SceneList { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public GetSceneMembershipResponse()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
            CommandDirection = ZclCommandDirection.SERVER_TO_CLIENT;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(Status, ZclDataType.Get(DataType.ENUMERATION_8_BIT));
            serializer.Serialize(Capacity, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            serializer.Serialize(GroupId, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            serializer.Serialize(SceneCount, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            serializer.Serialize(SceneList, ZclDataType.Get(DataType.N_X_UNSIGNED_8_BIT_INTEGER));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            Status = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.ENUMERATION_8_BIT));
            Capacity = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            GroupId = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            SceneCount = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            SceneList = deserializer.Deserialize<List<byte>>(ZclDataType.Get(DataType.N_X_UNSIGNED_8_BIT_INTEGER));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("GetSceneMembershipResponse [");
            builder.Append(base.ToString());
            builder.Append(", Status=");
            builder.Append(Status);
            builder.Append(", Capacity=");
            builder.Append(Capacity);
            builder.Append(", GroupId=");
            builder.Append(GroupId);
            builder.Append(", SceneCount=");
            builder.Append(SceneCount);
            builder.Append(", SceneList=");
            builder.Append(SceneList == null? "" : string.Join(", ", SceneList));
            builder.Append(']');

            return builder.ToString();
        }
    }
}
