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
    /// View Scene Response value object class.
    ///
    /// Cluster: Scenes. Command ID 0x01 is sent FROM the server.
    /// This command is a specific command used for the Scenes cluster.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class ViewSceneResponse : ZclCommand
    {
        /// <summary>
        /// The cluster ID to which this command belongs.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0005;

        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x01;

        /// <summary>
        /// Status command message field.
        /// </summary>
        public ZclStatus Status { get; set; }

        /// <summary>
        /// Group ID command message field.
        /// </summary>
        public ushort GroupId { get; set; }

        /// <summary>
        /// Scene ID command message field.
        /// </summary>
        public byte SceneId { get; set; }

        /// <summary>
        /// Transition Time command message field.
        /// </summary>
        public ushort TransitionTime { get; set; }

        /// <summary>
        /// Scene Name command message field.
        /// </summary>
        public string SceneName { get; set; }

        /// <summary>
        /// Extension Field Sets command message field.
        /// </summary>
        public List<ExtensionFieldSet> ExtensionFieldSets { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ViewSceneResponse()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
            CommandDirection = ZclCommandDirection.SERVER_TO_CLIENT;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(Status, ZclDataType.Get(DataType.ZCL_STATUS));
            serializer.Serialize(GroupId, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            serializer.Serialize(SceneId, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            serializer.Serialize(TransitionTime, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            serializer.Serialize(SceneName, ZclDataType.Get(DataType.CHARACTER_STRING));
            serializer.Serialize(ExtensionFieldSets, ZclDataType.Get(DataType.N_X_EXTENSION_FIELD_SET));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            Status = deserializer.Deserialize<ZclStatus>(ZclDataType.Get(DataType.ZCL_STATUS));
            GroupId = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            SceneId = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            TransitionTime = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            SceneName = deserializer.Deserialize<string>(ZclDataType.Get(DataType.CHARACTER_STRING));
            ExtensionFieldSets = deserializer.Deserialize<List<ExtensionFieldSet>>(ZclDataType.Get(DataType.N_X_EXTENSION_FIELD_SET));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("ViewSceneResponse [");
            builder.Append(base.ToString());
            builder.Append(", Status=");
            builder.Append(Status);
            builder.Append(", GroupId=");
            builder.Append(GroupId);
            builder.Append(", SceneId=");
            builder.Append(SceneId);
            builder.Append(", TransitionTime=");
            builder.Append(TransitionTime);
            builder.Append(", SceneName=");
            builder.Append(SceneName);
            builder.Append(", ExtensionFieldSets=");
            builder.Append(ExtensionFieldSets == null? "" : string.Join(", ", ExtensionFieldSets));
            builder.Append(']');

            return builder.ToString();
        }
    }
}
